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
    internal class Create : BaseViewModel
    {
        public delegate void ModelChanged(PatientTelecomInfo value);
        static public event ModelChanged ItemCreatedEventArgs;

        PatientTelecomInfo _currentModel;


        public string System { get { return _currentModel.System; } set { _currentModel.System = value; OnPropertyChanged(nameof(System)); } }
        public string Value { get { return _currentModel.Value; } set { _currentModel.Value = value; OnPropertyChanged(nameof(Value)); } }
        public string Use { get { return _currentModel.Use; } set { _currentModel.Use = value; OnPropertyChanged(nameof(Use)); } }


        public Command SaveCahgesCommand { get; set; }
        public Command CancelCahgesCommand { get; set; }




        public Create(Guid patientId) {

            _currentModel = new PatientTelecomInfo();
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
                using (var api = new API<PatientTelecomInfo>())
                {
                    api.Post("api", "PatientTelecomInfos", "Create",_currentModel);
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
