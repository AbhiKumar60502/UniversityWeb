using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shm.Logging.ApplicationInsights;
using Shm.Services.Types;

namespace Shm.Services.Functions
{
    public class FunctionHandler : IFunctionHandler
    {
        private readonly IMediator _mediator;
        private readonly IApplicationLogger<ILogger<FunctionHandler>> _logger;

        public FunctionHandler(IMediator mediator, IApplicationLogger<ILogger<FunctionHandler>> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ObjectResult> Handle<T>(Func<IRequest<T>> commandBuilder)
        {
            return await Handle(() => Task.FromResult(commandBuilder()));
        }
        
        public async Task<ObjectResult> Handle<T>(Func<Task<IRequest<T>>> commandBuilder)
        {
            try
            {
                var command = await commandBuilder();
                var result = await _mediator.Send(command);

                return new OkObjectResult(result);
            }
            catch (DomainException e)
            {
                _logger.Log(
                    nameof(FunctionHandler),
                    "DomainException handling function command",
                    new Dictionary<string, string>(),
                    LogLevel.Warning,
                    e);
                
                return new BadRequestObjectResult(new ErrorResponseModel(e));
            }
            catch (Exception e)
            {
                _logger.Log(
                    nameof(FunctionHandler),
                    "Exception handling function command",
                    new Dictionary<string, string>(),
                    LogLevel.Error,
                    e);

                return new ObjectResult(new ErrorResponseModel(e)) { StatusCode = 500 };
            }
        }
    }
}
