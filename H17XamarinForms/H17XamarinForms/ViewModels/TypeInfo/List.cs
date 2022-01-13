using H17XamarinForms.Models.Cards;
using H17XamarinForms.Models.Cards.PatientDetails;
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

namespace H17XamarinForms.ViewModels.TypeInfo
{
    internal class List : BaseViewModel
    {
        PatientTypeInfo _currentModel;
        static public ObservableCollection<PatientTypeInfo> _list;

        public ObservableCollection<PatientTypeInfo> ValueList
        {
            get {
                return _list;
            }
        }
        public PatientTypeInfo CurrentModel
        {
            get { return _currentModel; }
            set {
                if (value != null)
                {
                    _currentModel = value;
                    App.Current.MainPage.Navigation.PushAsync(new Pages.TypeInfo.Details(_currentModel));
                }
            }
        }


        public List(Guid patientId) {
            _list = new ObservableCollection<PatientTypeInfo>();
            _currentModel = new PatientTypeInfo();
            _currentModel.PatientId = patientId;

            CreateCommand = new Command(CreatePatientExecute, CreatePatientCanExecuted);
            this.PropertyChanged += (_, __) => CreateCommand.ChangeCanExecute();


            Create.ItemCreatedEventArgs += (x => { _list.Add(x); });
            Edit.CurrentItemChangedEventArgs += (x => { _list.Remove(_list.Where(y => y.Id == x.Id).SingleOrDefault()); _list.Add(Patient.CopyFrom<PatientTypeInfo>(x)); });
            Edit.CurrentItemDeletedEventArgs += (x => { _list.Remove(_list.Where(y => y.Id == x.Id).SingleOrDefault()); });

            //Get Data from server
            Task.Run(() => GetPatientsFromServer(patientId));
            Task.Run(() => ConnectToEvents());
        }

        void ConnectToEvents()
        {
            Thread.Sleep(100);
            Page page = App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1];
            var patientsListView = page.FindByName<ListView>("ValueList");
            patientsListView.ItemTapped += PatientsListView_ItemTapped;
        }

        private void PatientsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _currentModel = e.Item as PatientTypeInfo;
            App.Current.MainPage.Navigation.PushAsync(new Pages.TypeInfo.Details(_currentModel));
            
        }

        void GetPatientsFromServer(Guid patientId)
        {
            try
            {
                using (var api = new API<List<PatientTypeInfo>>())
                {
                    var queryProperties = new Dictionary<string, string>();
                    queryProperties.Add(nameof(patientId), patientId.ToString());

                    api.Get("api", "PatientTypeInfos", "List", queryProperties);
                    if (api.IsError)
                        throw new Exception(api.ErrorMessage);

                    App.Current.Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        foreach (var item in api.Data)
                        {
                            _list.Add(item);
                        }
                    });

                    
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK =(");
            }
            finally
            {
                Page page = App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1];
                var valueList = page.FindByName<ListView>("ValueList");
                var preloader = page.FindByName<ActivityIndicator>("Preloader");

                App.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
                {
                    preloader.IsVisible = false;
                    preloader.IsRunning = false;

                    valueList.IsVisible = true;
                });

            }
        }
        public Command CreateCommand { get; }
        async void CreatePatientExecute() { 
            await App.Current.MainPage.Navigation.PushAsync(new Pages.TypeInfo.Create(_currentModel.PatientId));
        }
        bool CreatePatientCanExecuted()
        {
            return true;
        }


    }
}
