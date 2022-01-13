using H17XamarinForms.Models.Cards.PatientDetails;
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
    public partial class Details : ContentPage
    {
        PatientTypeInfo _model;
        public Details(PatientTypeInfo model)
        {
            _model = model;
            InitializeComponent();
            BindingContext = new ViewModels.TypeInfo.Details(_model);
        }
        protected override void OnAppearing()
        {
            BindingContext = new ViewModels.TypeInfo.Details(_model);
        }
    }
}