using System;

namespace YT.Challenge.Domain.Models
{
    public class TypedResult<T> : Result
    {
        public T Data { get; protected set; }
        public TypedResult(T data) : base(true, string.Empty)
        {
            Status = true;
            Message = string.Empty;
            Data = data;
        }
        public TypedResult(Exception ex) : base(false, string.Empty)
        {
            if (ex.InnerException != null)
                Message = ex.InnerException.Message;
            else
                Message = ex.Message;
        }
        public TypedResult(bool status, string message, T data) : base(status, message)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}