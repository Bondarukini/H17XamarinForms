using H17XamarinForms.Models.Cards.PatientDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace H17XamarinForms.Pages.TelecomInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Edit : ContentPage
    {
        public Edit(PatientTelecomInfo model)
        {
            InitializeComponent();
            BindingContext = new ViewModels.TelecomInfo.Edit(model);
        }
        
    }
}