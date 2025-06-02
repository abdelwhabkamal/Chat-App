using System;

namespace API.Common
{
    public class Response<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public string ErrorCode { get; set; }

        public Response(T data, string message, bool success, string errorCode)
        {
            Data = data;
            Message = message;
            Success = success;
            ErrorCode = errorCode;
        }
        public static Response<T> SuccessResponse(T data, string message = "Operation successful",string errorCode="")
        {
            return new Response<T>(data, message, true, errorCode);
        }
        public static Response<T> ErrorResponse(string message, string errorCode )
        {
            return new Response<T>(default!, message, false, errorCode);
        }
        
    }
}