using H17XamarinForms.Models.Cards;
using H17XamarinForms.Models.Cards.PatientDetails;
using H17XamarinForms.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace H17XamarinForms.ViewModels.TelecomInfo
{
    internal class Details : BaseViewModel
    {
        PatientTelecomInfo _currentModel;


        public string System { get { return _currentModel.System; } set { _currentModel.System = value; OnPropertyChanged(nameof(System)); } }
        public string Value { get { return _currentModel.Value; } set { _currentModel.Value = value; OnPropertyChanged(nameof(Value)); } }
        public string Use { get { return _currentModel.Use; } set { _currentModel.Use = value; OnPropertyChanged(nameof(Use)); } }


        public Details(PatientTelecomInfo selectedPatient)
        {
            _currentModel = selectedPatient;


            EditCommand = new Command(OnEditItemExcecute, OnEditItemCanExecute);
            this.PropertyChanged += (_, __) => EditCommand.ChangeCanExecute();

            CancelCommand = new Command(OnCancelNewItemExcecute, OnCancelNewItemCanExecute);
            this.PropertyChanged += (_, __) => CancelCommand.ChangeCanExecute();


            Edit.CurrentItemChangedEventArgs += (x => { _currentModel = Patient.CopyFrom<PatientTelecomInfo>(x); });
            Edit.CurrentItemDeletedEventArgs += (x => { _currentModel = new PatientTelecomInfo(); });
        }




        public Command EditCommand { get; }
        public Command CancelCommand { get; }




        async void OnEditItemExcecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Pages.TelecomInfo.Edit(_currentModel));
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
