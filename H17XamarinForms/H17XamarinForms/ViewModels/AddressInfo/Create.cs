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
    internal class Create : BaseViewModel
    {
        public delegate void ModelChanged(PatientAddressInfo value);
        static public event ModelChanged ItemCreatedEventArgs;


        PatientAddressInfo _currentModel;


        public string Line { get { return _currentModel.Line; } set { _currentModel.Line = value;  OnPropertyChanged(nameof(Line)); } }
        public string City { get { return _currentModel.City; } set { _currentModel.City = value; OnPropertyChanged(nameof(City)); } }
        public string State { get { return _currentModel.State; } set { _currentModel.State = value; OnPropertyChanged(nameof(State)); } }
        public string Country { get { return _currentModel.Country; } set { _currentModel.Country = value; OnPropertyChanged(nameof(Country)); } }


        public Command SaveCahgesCommand { get; set; }
        public Command CancelCahgesCommand { get; set; }




        public Create(Guid patientId) {

            _currentModel = new PatientAddressInfo();
            _currentModel.PatientId = patientId;

            SaveCahgesCommand = new Command(OnSaveChangeExchenge, OnSaveChangeCanExcenge);
            this.PropertyChanged += (_, __) => SaveCahgesCommand.ChangeCanExecute();

            CancelCahgesCommand = new Command(OnCancelChangeExchenge, OnCancelChangeCanExcenge);
            this.PropertyChanged += (_, __) => CancelCahgesCommand.ChangeCanExecute();
        }




        async void OnSaveChangeExchenge()
        {
            try
            {
                using (var api = new API<PatientAddressInfo>())
                {
                    api.Post("api", "PatientAddressInfos", "Create",_currentModel);
                    if (api.IsError)
                        throw new Exception(api.ErrorMessage);


                    ItemCreatedEventArgs.Invoke(_currentModel);

                    await App.Current.MainPage.DisplayAlert("Success", "Saved", "Close");
                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception ex) {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK =(");
            }
        }
        bool OnSaveChangeCanExcenge() {
            return true;
        }


        async void OnCancelChangeExchenge()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        bool OnCancelChangeCanExcenge()
        {
            return true;
        }
    }
}
