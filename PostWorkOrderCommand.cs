using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shm.Services.Types;
using Shm.Servicing.Data;
using Shm.Servicing.DataContracts;
using Shm.Servicing.DataContracts.Response;
using Shm.Servicing.Models;

namespace Shm.Servicing.Handlers.WorkOrderHandlers
{
    public class PostWorkOrderCommand : IRequest<WorkOrderResponse>
    {
        public PostWorkOrderCommand(int marinaId, WorkOrderRequest workOrderRequest)
        {
            if (marinaId <= 0)
            {
                throw new DomainException(string.Format(CommonExceptionStrings.CannotBeLessThanZero, nameof(marinaId)));
            }

            MarinaId = marinaId;
            WorkOrderRequest = workOrderRequest;
        }

        public int MarinaId { get; set; }

        public WorkOrderRequest WorkOrderRequest { get; set; }
    }

    public class PostWorkOrderCommandHandler : IRequestHandler<PostWorkOrderCommand, WorkOrderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkOrderDataManager _workOrderDataManager;

        public PostWorkOrderCommandHandler(IWorkOrderDataManager workOrderDataManager, IMapper mapper)
        {
            _workOrderDataManager = workOrderDataManager;
            _mapper = mapper;
        }

        public async Task<WorkOrderResponse> Handle(PostWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var workOrder = _mapper.Map<WorkOrder>(request.WorkOrderRequest);
            workOrder.MarinaId = request.MarinaId;

            var revision = _mapper.Map<Revision>(request.WorkOrderRequest.Revision);
            revision.RevisionNumber = 1;
            workOrder.Revisions.Add(revision);

            var insertedWorkOrderId = await _workOrderDataManager.InsertWorkOrderAsync(workOrder)
                                      ?? throw new Exception("Inserting Work Order did not return the inserted primary key");

            workOrder.Id = insertedWorkOrderId;

            return _mapper.Map<WorkOrderResponse>(workOrder);
        }
    }
}
