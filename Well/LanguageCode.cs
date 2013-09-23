using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Well.Objects;

namespace Well
{
    public class LanguageCode
    {
        public static string getCode(Languages lang)
        {
            if (lang == Languages.Ukrainian)
                return "uk-UA";
            if (lang == Languages.Russian)
                return "ru-RU";
            if (lang == Languages.English)
                return "en-GB";
            return "en-GB";
        }
    }
}
