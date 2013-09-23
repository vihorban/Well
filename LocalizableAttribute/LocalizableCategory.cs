using System;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.ComponentModel;
using System.Windows;

namespace LocalizableAttribute
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class LocalizableCategory : CategoryAttribute
    {
        public LocalizableCategory
        (string category)
            : base(category)
        {
        }

        public string LocalizedCategory
        {
            get
            {
                return Application.Current.FindResource(Category).ToString();
            }
        }
    }
}
