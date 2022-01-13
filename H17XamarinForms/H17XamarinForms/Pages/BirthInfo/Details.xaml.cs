using H17XamarinForms.Models.Cards.PatientDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace H17XamarinForms.Pages.BirthInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage
    {
        PatientBirthInfo _model;
        public Details(PatientBirthInfo model)
        {
            InitializeComponent();
            BindingContext = new ViewModels.BirthInfo.Details(_model);
        }

        protected override void OnAppearing()
        {
            BindingContext = new ViewModels.BirthInfo.Details(_model);
        }
    }
}