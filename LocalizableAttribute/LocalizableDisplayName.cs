using System;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.ComponentModel;
using System.Windows;

namespace LocalizableAttribute
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class LocalizableDisplayName : DisplayNameAttribute
    {
        public LocalizableDisplayName
        (string displayName)
            : base(displayName)
        {
        }

        public override string DisplayName
        {
            get
            {
                return Application.Current.FindResource(DisplayNameValue).ToString();
            }
        }
    }
}
