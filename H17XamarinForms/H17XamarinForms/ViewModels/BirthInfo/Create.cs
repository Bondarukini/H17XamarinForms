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
    internal class Create : BaseViewModel
    {
        public delegate void ModelChanged(PatientBirthInfo value);
        static public event ModelChanged ItemCreatedEventArgs;

        PatientBirthInfo _currentModel;


        public DateTime BirthDate { get { return _currentModel.BirthDate; } set { _currentModel.BirthDate = value;  OnPropertyChanged(nameof(BirthDate)); } }
        public string BirthCity { get { return _currentModel.BirthCity; } set { _currentModel.BirthCity = value; OnPropertyChanged(nameof(BirthCity)); } }
        public string BirthState { get { return _currentModel.BirthState; } set { _currentModel.BirthState = value; OnPropertyChanged(nameof(BirthState)); } }
        public string BirthCountry { get { return _currentModel.BirthCountry; } set { _currentModel.BirthCountry = value; OnPropertyChanged(nameof(BirthCountry)); } }
        public string Birthsex { get { return _currentModel.Birthsex; } set { _currentModel.Birthsex = value; OnPropertyChanged(nameof(Birthsex)); } }


        public Command SaveCahgesCommand { get; set; }
        public Command CancelCahgesCommand { get; set; }




        public Create(Guid patientId) {

            _currentModel = new PatientBirthInfo();
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
                using (var api = new API<PatientBirthInfo>())
                {
                    api.Post("api", "PatientBirthInfos", "Create",_currentModel);
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
