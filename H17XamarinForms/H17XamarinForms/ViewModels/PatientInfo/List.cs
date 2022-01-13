using H17XamarinForms.Models.Cards;
using H17XamarinForms.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace H17XamarinForms.ViewModels.PatientInfo
{
    internal class List : BaseViewModel
    {
        Patient _currentModel;
        static public ObservableCollection<Patient> _patients;

        public ObservableCollection<Patient> Patients {
            get {
                return _patients;
            }
        }
        //public Patient CurrentPatient
        //{
        //    get { return _currentModel; }
        //    set {
        //        if (value != null)
        //        {
        //            _currentModel = value;
        //            Page currentPage = App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1];
        //            var listView = currentPage.FindByName<ListView>("Patients");
        //            listView.SelectedItem = null;

        //            App.Current.MainPage.Navigation.PushAsync(new Pages.PatientInfo.Details(_currentModel));

        //        }
        //    }
        //}


        public List() {
            _patients = new ObservableCollection<Patient>();

            AddNewPatientCommand = new Command(OnAddNewPatientExecute, OnAddNewPatientCanExecuted);
            this.PropertyChanged += (_, __) => AddNewPatientCommand.ChangeCanExecute();


            Create.ItemCreatedEventArgs += (x => { _patients.Add(x); });
            Edit.CurrentItemChangedEventArgs += (x => { _patients.Remove(_patients.Where(y => y.Id == x.Id).SingleOrDefault()); _patients.Add(Patient.CopyFrom<Patient>(x)); });
            Edit.CurrentItemDeletedEventArgs += (x => { _patients.Remove(_patients.Where(y => y.Id == x.Id).SingleOrDefault()); });

            //Get Data from server
            Task.Run(() => GetPatientsFromServer());
            Task.Run(() => ConnectToEvents());
        }
        void ConnectToEvents() {
            Thread.Sleep(1000);
            Page page = App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1];
            var patientsListView = page.FindByName<ListView>("ValueList");
            patientsListView.ItemTapped += PatientsListView_ItemTapped;
        }

        private void PatientsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _currentModel = e.Item as Patient;
            App.Current.MainPage.Navigation.PushAsync(new Pages.PatientInfo.Details(_currentModel));
        }

        void GetPatientsFromServer()
        {
            try
            {
                using (var api = new API<List<Patient>>())
                {
                    api.Get("api", "Patients", "List");
                    if (api.IsError)
                        throw new Exception(api.ErrorMessage);

                    App.Current.Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        foreach (var item in api.Data)
                        {
                            _patients.Add(item);
                        }
                    });


                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK =(");
            }
            finally {
                Page page = App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1];
                var patientsListView = page.FindByName<ListView>("ValueList");
                var preloader = page.FindByName<ActivityIndicator>("Preloader");

                App.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
                {
                    preloader.IsVisible = false;
                    preloader.IsRunning = false;

                    patientsListView.IsVisible = true;
                });

            }
        }

        public Command AddNewPatientCommand { get; }
        async void OnAddNewPatientExecute() { 
            await App.Current.MainPage.Navigation.PushAsync(new Pages.PatientInfo.Create());
        }
         bool OnAddNewPatientCanExecuted()
        {
            return true;
        }


    }
}
