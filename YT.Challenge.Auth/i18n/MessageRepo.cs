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
                case MessageKey.USER_EXISTS:
                    return "User already exists!";
                case MessageKey.USER_CREATION_FAILED:
                    return "User creation failed! Please check user details and try again.";
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