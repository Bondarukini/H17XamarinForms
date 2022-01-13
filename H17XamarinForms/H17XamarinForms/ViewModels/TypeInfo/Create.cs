using H17XamarinForms.Models.Cards;
using H17XamarinForms.Models.Cards.PatientDetails;
using H17XamarinForms.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace H17XamarinForms.ViewModels.TypeInfo
{
    internal class Create : BaseViewModel
    {
        public delegate void ModelChanged(PatientTypeInfo value);
        static public event ModelChanged ItemCreatedEventArgs;

        PatientTypeInfo _currentModel;

        public string Key { get { return _currentModel.Key; } set { _currentModel.Key = value; OnPropertyChanged(nameof(Key)); } }
        public string Value { get { return _currentModel.Value; } set { _currentModel.Value = value; OnPropertyChanged(nameof(Value)); } }


        public Command SaveCahgesCommand { get; set; }
        public Command CancelCahgesCommand { get; set; }




        public Create(Guid patientId) {

            _currentModel = new PatientTypeInfo();
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
                using (var api = new API<PatientTypeInfo>())
                {
                    api.Post("api", "PatientTypeInfos", "Create",_currentModel);
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
