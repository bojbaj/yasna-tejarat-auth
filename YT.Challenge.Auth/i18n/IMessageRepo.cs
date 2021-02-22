namespace YT.Challenge.Auth.i18n
{
    public interface IMessageRepo
    {
        string Get(MessageKey key);
        string Get(MessageKey key, LanguageCode langCode);
    }
}