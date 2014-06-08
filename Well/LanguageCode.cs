using System;
using Well.Objects;

namespace Well
{
    public class LanguageCode
    {
        public static string GetCode(Languages lang)
        {
            switch (lang)
            {
                case Languages.Ukrainian:
                    return "uk-UA";
                case Languages.Russian:
                    return "ru-RU";
                case Languages.English:
                    return "en-GB";
                default:
                    throw new NotImplementedException("Language not supported");
            }
        }
    }
}