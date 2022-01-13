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
    public partial class Details : ContentPage
    {
        PatientTelecomInfo _model;
        public Details(PatientTelecomInfo model)
        {
            _model = model;
            InitializeComponent();
            BindingContext = new ViewModels.TelecomInfo.Details(_model);
        }
        protected override void OnAppearing()
        {
            BindingContext = new ViewModels.TelecomInfo.Details(_model);
        }
    }
}