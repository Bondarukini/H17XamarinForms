using H17XamarinForms.Models.Cards;
using H17XamarinForms.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace H17XamarinForms.ViewModels.PatientInfo
{
    internal class Create:BaseViewModel
    {

        public delegate void ModelChanged(Patient value);
        static public event ModelChanged ItemCreatedEventArgs;


        string _family { get; set; }
        string _given { get; set; }
        string _prefix { get; set; }
        string _mothersMaidName { get; set; }
        string _gender { get; set; }
        string _maritalStatus { get; set; }


        double _disabilityAdjustedLifeYears { get; set; }
        double _qualityAdjustedLifeYears { get; set; }
        DateTime _decreasedDateTime { get; set; }


        public string Family { get { return _family; } set { _family = value; OnPropertyChanged(nameof(Family)); } }
        public string Given { get { return _given; } set { _given = value; OnPropertyChanged(nameof(Given)); } }
        public string Prefix { get { return _prefix; } set { _prefix = value; OnPropertyChanged(nameof(Prefix)); } }
        public string MothersMaidenName { get { return _mothersMaidName; } set { _mothersMaidName = value; OnPropertyChanged(nameof(MothersMaidenName)); } }
        public string Gender { get { return _gender; } set { _gender = value; OnPropertyChanged(nameof(Gender)); } }
        public string MaritalStatus { get { return _maritalStatus; } set { _maritalStatus = value; OnPropertyChanged(nameof(MaritalStatus)); } }
        public double DisabilityAdjustedLifeYears { get { return _disabilityAdjustedLifeYears; } set { _disabilityAdjustedLifeYears = value; OnPropertyChanged(nameof(DisabilityAdjustedLifeYears)); } }
        public double QualityAdjustedLifeYears { get { return _qualityAdjustedLifeYears; } set { _qualityAdjustedLifeYears = value; OnPropertyChanged(nameof(QualityAdjustedLifeYears)); } }
        public DateTime DeceasedDateTime { get { return _decreasedDateTime; } set { _decreasedDateTime = value; OnPropertyChanged(nameof(DeceasedDateTime)); } }


        public Create()
        {
            SaveNewItemCommand = new Command(OnSaveNewItemExcecute, OnSaveNewItemCanExecute);
            this.PropertyChanged += (_, __) => SaveNewItemCommand.ChangeCanExecute();

            CancelNewItemCommand = new Command(OnCancelNewItemExcecute, OnCancelNewItemCanExecute);
            this.PropertyChanged += (_, __) => CancelNewItemCommand.ChangeCanExecute();
        }
        public Command SaveNewItemCommand { get; }
        public Command CancelNewItemCommand { get; }

        async void OnSaveNewItemExcecute()
        {
            Patient patient = new Patient();

            patient.Family = this.Family;
            patient.Given = this.Given;
            patient.Prefix = this.Prefix;
            patient.MothersMaidenName = this.MothersMaidenName;
            patient.Gender = this.Gender;
            patient.MaritalStatus = this.MaritalStatus;
            patient.DisabilityAdjustedLifeYears = this.DisabilityAdjustedLifeYears;
            patient.QualityAdjustedLifeYears = this.QualityAdjustedLifeYears;
            patient.DeceasedDateTime = this.DeceasedDateTime;

           
            using (var api = new API<Patient>())
            {
                try
                {
                    api.Post("api", "patients", "create", patient);

                    if (api.IsError)
                        throw new Exception(api.ErrorMessage);


                    ItemCreatedEventArgs.Invoke(patient);


                    await App.Current.MainPage.DisplayAlert("Success", "Patient added", "Close");
                    await App.Current.MainPage.Navigation.PopAsync();

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
    }
}
