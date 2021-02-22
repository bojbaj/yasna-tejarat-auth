namespace YT.Challenge.Domain.Models
{
    public class Result
    {
        public bool Status { get; protected set; }
        public string Message { get; protected set; }
        public Result(bool status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}