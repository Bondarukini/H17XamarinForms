using H17XamarinForms.Models.Cards;
using H17XamarinForms.Models.Cards.PatientDetails;
using H17XamarinForms.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace H17XamarinForms.ViewModels.DocumentsInfo
{
    internal class Details : BaseViewModel
    {
        PatientDocumentsInfo _currentModel;

        public string Key { get { return _currentModel.Key; } set { _currentModel.Key = value; OnPropertyChanged(nameof(Key)); } }
        public string Value { get { return _currentModel.Value; } set { _currentModel.Value = value; OnPropertyChanged(nameof(Value)); } }


        public Details(PatientDocumentsInfo selectedPatient)
        {
            _currentModel = selectedPatient;


            EditCommand = new Command(OnEditItemExcecute, OnEditItemCanExecute);
            this.PropertyChanged += (_, __) => EditCommand.ChangeCanExecute();

            CancelCommand = new Command(OnCancelNewItemExcecute, OnCancelNewItemCanExecute);
            this.PropertyChanged += (_, __) => CancelCommand.ChangeCanExecute();


            Edit.CurrentItemChangedEventArgs += (x => { _currentModel = Patient.CopyFrom<PatientDocumentsInfo>(x); });
            Edit.CurrentItemDeletedEventArgs += (x => { _currentModel = new PatientDocumentsInfo(); });
        }




        public Command EditCommand { get; }
        public Command CancelCommand { get; }




        async void OnEditItemExcecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Pages.DocumentsInfo.Edit(_currentModel));
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
