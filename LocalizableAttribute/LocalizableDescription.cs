using System;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.ComponentModel;
using System.Windows;

namespace LocalizableAttribute
{
    /// <summary>
    /// Attribute for localization.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class LocalizableDescriptionAttribute : DescriptionAttribute
    {
        public LocalizableDescriptionAttribute
        (string description)
            : base(description)
        {
        }

        public override string Description
        {
            get
            {
                return Application.Current.FindResource(DescriptionValue).ToString();;
            }
        }
    }
}