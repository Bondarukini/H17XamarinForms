using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace H17XamarinForms.Pages.PatientInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage
    {
        Models.Cards.Patient _patient;
        public Details(Models.Cards.Patient patient)
        {
            _patient = patient;
            InitializeComponent();
            BindingContext = new ViewModels.PatientInfo.Details(_patient);
        }
        protected override void OnAppearing()
        {
            BindingContext = new ViewModels.PatientInfo.Details(_patient);
        }
        
    }
}