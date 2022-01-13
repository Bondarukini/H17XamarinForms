using H17XamarinForms.Models.Cards;
using H17XamarinForms.Models.Cards.PatientDetails;
using H17XamarinForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace H17XamarinForms.ViewModels.BirthInfo
{
    internal class Edit:BaseViewModel
    {
        public delegate void ModelChanged(PatientBirthInfo value);
        static public event ModelChanged CurrentItemChangedEventArgs;
        static public event ModelChanged CurrentItemDeletedEventArgs;

        PatientBirthInfo _currentModel;

        public DateTime BirthDate { get { return _currentModel.BirthDate; } set { _currentModel.BirthDate = value; OnPropertyChanged(nameof(BirthDate)); } }
        public string BirthCity { get { return _currentModel.BirthCity; } set { _currentModel.BirthCity = value; OnPropertyChanged(nameof(BirthCity)); } }
        public string BirthState { get { return _currentModel.BirthState; } set { _currentModel.BirthState = value; OnPropertyChanged(nameof(BirthState)); } }
        public string BirthCountry { get { return _currentModel.BirthCountry; } set { _currentModel.BirthCountry = value; OnPropertyChanged(nameof(BirthCountry)); } }
        public string Birthsex { get { return _currentModel.Birthsex; } set { _currentModel.Birthsex = value; OnPropertyChanged(nameof(Birthsex)); } }


        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command DeleteCommand { get; }




        public Edit(PatientBirthInfo model) 
        {
            _currentModel = model;


            SaveCommand = new Command(OnSaveChangeExchenge, OnSaveChangeCanExcenge);
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();

            CancelCommand = new Command(OnCancelChangeExchenge, OnCancelChangeCanExcenge);
            this.PropertyChanged += (_, __) => CancelCommand.ChangeCanExecute();

            DeleteCommand = new Command(OnDeleteExchenge, OnDeletelCanExcenge);
            this.PropertyChanged += (_, __) => DeleteCommand.ChangeCanExecute();
        }



        async void OnSaveChangeExchenge()
        {
            try
            {
                using (var api = new API<bool>())
                {
                    api.Post("api", "PatientBirthInfos", "Update", _currentModel);
                    if (api.IsError)
                        throw new Exception(api.ErrorMessage);


                    CurrentItemChangedEventArgs?.Invoke(_currentModel);


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

        async void OnDeleteExchenge()
        {
            try
            {
                using (var api = new API<bool>())
                {
                    api.Post("api", "PatientBirthInfos", "Delete", _currentModel.Id);
                    if (api.IsError)
                        throw new Exception(api.ErrorMessage);


                    CurrentItemDeletedEventArgs.Invoke(_currentModel);


                    await App.Current.MainPage.DisplayAlert("Success", "Deleted", "Close");

                    var navigationStack = App.Current.MainPage.Navigation.NavigationStack;
                    App.Current.MainPage.Navigation.RemovePage(navigationStack[navigationStack.Count - 2]);
                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK =(");
            }
        }
        bool OnDeletelCanExcenge()
        {
            return true;
        }


      
    }
}
