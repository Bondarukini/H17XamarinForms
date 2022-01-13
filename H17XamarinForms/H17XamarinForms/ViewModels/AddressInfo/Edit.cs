using H17XamarinForms.Models.Cards;
using H17XamarinForms.Models.Cards.PatientDetails;
using H17XamarinForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace H17XamarinForms.ViewModels.AddressInfo
{
    internal class Edit:BaseViewModel
    {
        public delegate void ModelChanged(PatientAddressInfo value);
        static public event ModelChanged CurrentItemChangedEventArgs;
        static public event ModelChanged CurrentItemDeletedEventArgs;

        PatientAddressInfo _currentModel;


        public string Line { get { return _currentModel.Line; } set { _currentModel.Line = value; OnPropertyChanged(nameof(Line)); } }
        public string City { get { return _currentModel.City; } set { _currentModel.City = value; OnPropertyChanged(nameof(City)); } }
        public string State { get { return _currentModel.State; } set { _currentModel.State = value; OnPropertyChanged(nameof(State)); } }
        public string Country { get { return _currentModel.Country; } set { _currentModel.Country = value; OnPropertyChanged(nameof(Country)); } }


        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command DeleteCommand { get; }




        public Edit(PatientAddressInfo model) 
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
                    api.Post("api", "PatientAddressInfos", "Update", _currentModel);
                    if (api.IsError)
                        throw new Exception(api.ErrorMessage);

                    CurrentItemChangedEventArgs?.Invoke(_currentModel);

                    await App.Current.MainPage.DisplayAlert("Success", "Saved", "Close");
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
                    api.Post("api", "PatientAddressInfos", "Delete", _currentModel.Id);
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
