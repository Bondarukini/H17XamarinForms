using H17XamarinForms.Models.Cards.PatientDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace H17XamarinForms.Pages.DocumentsInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage
    {
        PatientDocumentsInfo _model;
        public Details(PatientDocumentsInfo model)
        {
            _model = model;
            InitializeComponent();
            BindingContext = new ViewModels.DocumentsInfo.Details(_model);
        }
        protected override void OnAppearing()
        {
            BindingContext = new ViewModels.DocumentsInfo.Details(_model);
        }
    }
}