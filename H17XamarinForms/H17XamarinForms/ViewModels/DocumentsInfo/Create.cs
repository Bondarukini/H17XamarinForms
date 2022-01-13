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
    internal class Create : BaseViewModel
    {
        public delegate void ModelChanged(PatientDocumentsInfo value);
        static public event ModelChanged ItemCreatedEventArgs;

        PatientDocumentsInfo _currentModel;

        public string Key { get { return _currentModel.Key; } set { _currentModel.Key = value; OnPropertyChanged(nameof(Key)); } }
        public string Value { get { return _currentModel.Value; } set { _currentModel.Value = value; OnPropertyChanged(nameof(Value)); } }


        public Command SaveCahgesCommand { get; set; }
        public Command CancelCahgesCommand { get; set; }




        public Create(Guid patientId) {

            _currentModel = new PatientDocumentsInfo();
            _currentModel.PatientId = patientId;

            SaveCahgesCommand = new Command(OnSaveChangeExchenge, OnSaveChangeCanExcenge);
            this.PropertyChanged += (_, __) => SaveCahgesCommand.ChangeCanExecute();

            CancelCahgesCommand = new Command(OnCancelChangeExchenge, OnCancelChangeCanExcenge);
            this.PropertyChanged += (_, __) => CancelCahgesCommand.ChangeCanExecute();
        }




        async void OnSaveChangeExchenge()
        {

            //TODO: Если нажать на кноаку создать неськолько раз, то создаётся неконтролируемое уол-во обектов в последующие разы доперезапуска программы
            //TODO: что-то не так с подписью на коллекци- возможно нужно проверять на дбликаты при сохдании - при том что в базе всё отлично
            try
            {
                using (var api = new API<PatientDocumentsInfo>())
                {
                    api.Post("api", "PatientDocumentsInfos", "Create",_currentModel);
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
