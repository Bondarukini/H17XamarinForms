using H17XamarinForms.Models.Cards;
using H17XamarinForms.Models.Cards.PatientDetails;
using H17XamarinForms.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace H17XamarinForms.ViewModels.AddressInfo
{
    internal class Details : BaseViewModel
    {
        PatientAddressInfo _currentModel;



        public string Line { get { return _currentModel.Line; } set { _currentModel.Line = value; OnPropertyChanged(nameof(Line)); } }
        public string City { get { return _currentModel.City; } set { _currentModel.City = value; OnPropertyChanged(nameof(City)); } }
        public string State { get { return _currentModel.State; } set { _currentModel.State = value; OnPropertyChanged(nameof(State)); } }
        public string Country { get { return _currentModel.Country; } set { _currentModel.Country = value; OnPropertyChanged(nameof(Country)); } }
      

        public Details(PatientAddressInfo selectedPatient)
        {
            _currentModel = selectedPatient;


            EditCommand = new Command(OnEditItemExcecute, OnEditItemCanExecute);
            this.PropertyChanged += (_, __) => EditCommand.ChangeCanExecute();

            CancelCommand = new Command(OnCancelNewItemExcecute, OnCancelNewItemCanExecute);
            this.PropertyChanged += (_, __) => CancelCommand.ChangeCanExecute();


            Edit.CurrentItemChangedEventArgs += (x => { _currentModel = PatientAddressInfo.CopyFrom(x); });
            Edit.CurrentItemDeletedEventArgs += (x => { _currentModel = new PatientAddressInfo(); });
        }




        public Command EditCommand { get; }
        public Command CancelCommand { get; }




        async void OnEditItemExcecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Pages.AddressInfo.Edit(_currentModel));
        }
        bool OnEditItemCanExecute()
        {
            return true;
        }



        async void OnCancelNewItemExcecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        bool OnCancelNewItemCanExecute()
        {
            return true;
        }
    }
}
