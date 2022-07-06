using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Shm.Services.Functions
{
    public interface IFunctionHandler
    {
        Task<ObjectResult> Handle<T>(Func<IRequest<T>> commandBuilder);

        Task<ObjectResult> Handle<T>(Func<Task<IRequest<T>>> commandBuilder);
    }
}