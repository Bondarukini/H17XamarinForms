using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace H17XamarinForms
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if(Source==null)
                return null;

            var itemSourcew = ImageSource.FromResource(Source);
            return itemSourcew;
        }
    }
}
