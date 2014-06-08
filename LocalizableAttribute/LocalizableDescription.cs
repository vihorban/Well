using System;
using System.ComponentModel;
using System.Windows;

namespace LocalizableAttribute
{
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
                return Application.Current.TryFindResource(DescriptionValue).ToString();
            }
        }
    }
}