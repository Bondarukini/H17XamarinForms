using H17XamarinForms.Models.Cards.PatientDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace H17XamarinForms.Pages.AddressInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage
    {
        PatientAddressInfo _model;
        public Details(PatientAddressInfo model)
        {
            _model = model;
            InitializeComponent();
            BindingContext = new ViewModels.AddressInfo.Details(_model);
        }
        protected override void OnAppearing()
        {
            BindingContext = new ViewModels.AddressInfo.Details(_model);
        }
    }
}