using System;

namespace Shm.Services.Functions
{
    public class ErrorResponseModel
    {
        public ErrorResponseModel(Exception exception)
        {
            ErrorMessage = exception.Message;
        }

        public string ErrorMessage { get; }
    }
}
