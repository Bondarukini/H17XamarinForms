using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace H17XamarinForms.Pages.TypeInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Create : ContentPage
    {
        public Create(Guid patientId)
        {
            InitializeComponent();
            BindingContext = new ViewModels.TypeInfo.Create(patientId);
        }
        
    }
}