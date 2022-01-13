using H17XamarinForms.Models.Cards;
using H17XamarinForms.Models.Cards.PatientDetails;
using H17XamarinForms.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace H17XamarinForms.ViewModels.BirthInfo
{
    internal class Details : BaseViewModel
    {
        static public PatientBirthInfo _currentModel;

        public DateTime BirthDate { get { return _currentModel.BirthDate; } set { _currentModel.BirthDate = value; OnPropertyChanged(nameof(BirthDate)); } }
        public string BirthCity { get { return _currentModel.BirthCity; } set { _currentModel.BirthCity = value; OnPropertyChanged(nameof(BirthCity)); } }
        public string BirthState { get { return _currentModel.BirthState; } set { _currentModel.BirthState = value; OnPropertyChanged(nameof(BirthState)); } }
        public string BirthCountry { get { return _currentModel.BirthCountry; } set { _currentModel.BirthCountry = value; OnPropertyChanged(nameof(BirthCountry)); } }
        public string Birthsex { get { return _currentModel.Birthsex; } set { _currentModel.Birthsex = value; OnPropertyChanged(nameof(Birthsex)); } }


        public Details(PatientBirthInfo selectedPatient)
        {
            _currentModel = selectedPatient;


            EditCommand = new Command(OnEditItemExcecute, OnEditItemCanExecute);
            this.PropertyChanged += (_, __) => EditCommand.ChangeCanExecute();

            CancelCommand = new Command(OnCancelNewItemExcecute, OnCancelNewItemCanExecute);
            this.PropertyChanged += (_, __) => CancelCommand.ChangeCanExecute();


            Edit.CurrentItemChangedEventArgs += (x => { _currentModel = PatientBirthInfo.CopyFrom(x); });
            Edit.CurrentItemDeletedEventArgs += (x => { _currentModel = new PatientBirthInfo(); });

        }




        public Command EditCommand { get; }
        public Command CancelCommand { get; }




        async void OnEditItemExcecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Pages.BirthInfo.Edit(_currentModel));
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
