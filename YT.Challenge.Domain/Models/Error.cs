namespace YT.Challenge.Domain.Models
{
    public class Error
    {
        public string TechnicalMessage { get; protected set; }
        public Error(string technicalMessage)
        {
            TechnicalMessage = technicalMessage;
        }
    }
}