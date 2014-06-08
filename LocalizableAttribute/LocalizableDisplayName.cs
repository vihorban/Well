using System;
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
            get { return Application.Current.TryFindResource(DisplayNameValue).ToString(); }
        }
    }
}