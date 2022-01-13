using H17XamarinForms.Models.Cards;
using H17XamarinForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace H17XamarinForms.ViewModels.PatientInfo
{
    internal class Edit : BaseViewModel
    {
        public delegate void ModelChanged(Patient value);
        static public event ModelChanged CurrentItemChangedEventArgs;
        static public event ModelChanged CurrentItemDeletedEventArgs;

        Patient _currentPatientCache;

        string _family { get; set; }
        string _given { get; set; }
        string _prefix { get; set; }
        string _mothersMaidName { get; set; }
        string _gender { get; set; }
        string _maritalStatus { get; set; }


        double _disabilityAdjustedLifeYears { get; set; }
        double _qualityAdjustedLifeYears { get; set; }
        DateTime _decreasedDateTime { get; set; }


        public string Family { get { return _currentPatientCache.Family; } set { _currentPatientCache.Family = value; OnPropertyChanged(nameof(Family)); } }
        public string Given { get { return _currentPatientCache.Given; } set { _currentPatientCache.Given = value; OnPropertyChanged(nameof(Given)); } }
        public string Prefix { get { return _currentPatientCache.Prefix; } set { _currentPatientCache.Prefix = value; OnPropertyChanged(nameof(Prefix)); } }
        public string MothersMaidenName { get { return _currentPatientCache.MothersMaidenName; } set { _currentPatientCache.MothersMaidenName = value; OnPropertyChanged(nameof(MothersMaidenName)); } }
        public string Gender { get { return _currentPatientCache.Gender; } set { _currentPatientCache.Gender = value; OnPropertyChanged(nameof(Gender)); } }
        public string MaritalStatus { get { return _currentPatientCache.MaritalStatus; } set { _currentPatientCache.MaritalStatus = value; OnPropertyChanged(nameof(MaritalStatus)); } }
        public double DisabilityAdjustedLifeYears { get { return _currentPatientCache.DisabilityAdjustedLifeYears; } set { _currentPatientCache.DisabilityAdjustedLifeYears = value; OnPropertyChanged(nameof(DisabilityAdjustedLifeYears)); } }
        public double QualityAdjustedLifeYears { get { return _currentPatientCache.QualityAdjustedLifeYears; } set { _currentPatientCache.QualityAdjustedLifeYears = value; OnPropertyChanged(nameof(QualityAdjustedLifeYears)); } }
        public DateTime DeceasedDateTime { get { return _currentPatientCache.DeceasedDateTime; } set { _currentPatientCache.DeceasedDateTime = value; OnPropertyChanged(nameof(DeceasedDateTime)); } }


        public Edit(Patient selectedPatient)
        {
            _currentPatientCache = selectedPatient;



            SaveNewItemCommand = new Command(OnSaveNewItemExcecute, OnSaveNewItemCanExecute);
            this.PropertyChanged += (_, __) => SaveNewItemCommand.ChangeCanExecute();

            CancelNewItemCommand = new Command(OnCancelNewItemExcecute, OnCancelNewItemCanExecute);
            this.PropertyChanged += (_, __) => CancelNewItemCommand.ChangeCanExecute();

            DeleteItemCommand = new Command(OnDeleteItemExcecute, OnDeleteItemCanExecute);
            this.PropertyChanged += (_,__) => DeleteItemCommand.ChangeCanExecute();


            EditPatientAddressInfoCommand = new Command(OnEditAddressExcecute, OnEditAddressCanExcecute);
            this.PropertyChanged += (_, __) => EditPatientAddressInfoCommand.ChangeCanExecute();
        }




        public Command SaveNewItemCommand { get; }
        public Command CancelNewItemCommand { get; }

        public Command DeleteItemCommand { get; }

        public Command EditPatientAddressInfoCommand { get; set; }

        async void OnSaveNewItemExcecute()
        {
            Patient patient = new Patient();

            patient.Id = this._currentPatientCache.Id;
            patient.Family = this.Family;
            patient.Given = this.Given;
            patient.Prefix = this.Prefix;
            patient.MothersMaidenName = this.MothersMaidenName;
            patient.Gender = this.Gender;
            patient.MaritalStatus = this.MaritalStatus;
            patient.DisabilityAdjustedLifeYears = this.DisabilityAdjustedLifeYears;
            patient.QualityAdjustedLifeYears = this.QualityAdjustedLifeYears;
            patient.DeceasedDateTime = this.DeceasedDateTime;

           
            using (var api = new API<bool>())
            {
                try
                {
                    api.Post("api", "patients", "update", patient);

                    if (api.IsError)
                        throw new Exception(api.ErrorMessage);

                    CurrentItemChangedEventArgs?.Invoke(_currentPatientCache);


                    await App.Current.MainPage.DisplayAlert("Success", "Saved", "Close");
                    

                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
        bool OnSaveNewItemCanExecute()
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


        async void OnEditAddressExcecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Pages.PatientInfo.Edit(_currentPatientCache));
        }
        bool OnEditAddressCanExcecute()
        {
            return true;
        }

        async void OnDeleteItemExcecute()
        {
            using (var api = new API<bool>())
            {
                try
                {
                    api.Post("api", "patients", "delete", _currentPatientCache.Id.ToString());

                    if (api.IsError || api.Data==false)
                        throw new Exception(api.ErrorMessage);


                    CurrentItemDeletedEventArgs.Invoke(_currentPatientCache);
                    await App.Current.MainPage.DisplayAlert("Deleted success", "Saved", "Close");

                    //back to list
                    var navigationStack = App.Current.MainPage.Navigation.NavigationStack;
                    App.Current.MainPage.Navigation.RemovePage(navigationStack[navigationStack.Count - 2]);
                    await App.Current.MainPage.Navigation.PopAsync();

                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
        bool OnDeleteItemCanExecute()
        {
            return true;
        }

    }
}
