using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Shm.Services.Functions;
using Shm.Servicing.DataContracts;
using Shm.Servicing.DataContracts.Response;
using Shm.Servicing.Handlers.WorkOrderHandlers;

namespace Shm.Servicing.Functions
{
    public class WorkOrderFunction
    {
        private readonly IFunctionHandler _functionHandler;

        public WorkOrderFunction(IFunctionHandler functionHandler)
        {
            _functionHandler = functionHandler;
        }

        [FunctionName(nameof(GetWorkOrderHeader))]
        public async Task<IActionResult> GetWorkOrderHeader(
            [HttpTrigger(AuthorizationLevel.Anonymous, HttpConstants.GetMethod, Route = "marina/{marinaId}/workorder/{workOrderId}")] HttpRequest req,
            int marinaId,
            int workOrderId)
        {
            IRequest<WorkOrderResponse> BuildCommand() => new GetWorkOrderHeaderCommand(marinaId, workOrderId);
            return await _functionHandler.Handle(BuildCommand);
        }

        [FunctionName(nameof(GetWorkOrderDetail))]
        public async Task<IActionResult> GetWorkOrderDetail(
            [HttpTrigger(AuthorizationLevel.Anonymous, HttpConstants.GetMethod, Route = "marina/{marinaId}/workorder/{workOrderId}/detail")] HttpRequest req,
            int marinaId,
            int workOrderId)
        {
            IRequest<WorkOrderResponse> BuildCommand() => new GetWorkOrderDetailCommand(marinaId, workOrderId);
            return await _functionHandler.Handle(BuildCommand);
        }

        [FunctionName(nameof(PostWorkOrder))]
        public async Task<IActionResult> PostWorkOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, HttpConstants.PostMethod, Route = "marina/{marinaId}/workorder")]
            WorkOrderRequest workOrderRequest,
            int marinaId)
        {
            IRequest<WorkOrderResponse> BuildCommand() => new PostWorkOrderCommand(marinaId, workOrderRequest);
            return await _functionHandler.Handle(BuildCommand);
        }

        [FunctionName(nameof(PatchWorkOrder))]
        public async Task<IActionResult> PatchWorkOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, HttpConstants.PatchMethod, Route = "marina/{marinaId}/workorder/{workOrderId}")]
            JsonPatchDocument<WorkOrderRequest> jsonPatchDocument,
            int marinaId,
            int workOrderId)
        {
            IRequest<WorkOrderResponse> BuildCommand() => new PatchWorkOrderCommand(marinaId, workOrderId, jsonPatchDocument);
            return await _functionHandler.Handle(BuildCommand);
        }
    }
}
