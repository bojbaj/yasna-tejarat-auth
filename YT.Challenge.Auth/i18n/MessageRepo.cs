namespace YT.Challenge.Auth.i18n
{
    public class MessageRepo : IMessageRepo
    {
        private LanguageCode lastLangCode = LanguageCode.ENGLISH;
        public string Get(MessageKey key, LanguageCode langCode)
        {
            lastLangCode = langCode;
            switch (key)
            {
                case MessageKey.INVALID_USER_PASS:
                    return "Username or password is invalid!";
                default:
                    return key.ToString();
            }
        }

        public string Get(MessageKey key)
        {
            return Get(key, lastLangCode);
        }
    }
}