using H17XamarinForms.Models.Cards;
using H17XamarinForms.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace H17XamarinForms.ViewModels.PatientInfo
{
    internal class Details : BaseViewModel
    {
        Patient _currentModel;
        public Patient CurrentModel { get { return _currentModel; }  set { _currentModel = value; OnPropertyChanged(nameof(CurrentModel)); } }


        public string Family { get { return _currentModel.Family; } set { _currentModel.Family = value; OnPropertyChanged(nameof(Family)); } }
        public string Given { get { return _currentModel.Given; } set { _currentModel.Given = value; OnPropertyChanged(nameof(Given)); } }
        public string Prefix { get { return _currentModel.Prefix; } set { _currentModel.Prefix = value; OnPropertyChanged(nameof(Prefix)); } }
        public string MothersMaidenName { get { return _currentModel.MothersMaidenName; } set { _currentModel.MothersMaidenName = value; OnPropertyChanged(nameof(MothersMaidenName)); } }
        public string Gender { get { return _currentModel.Gender; } set { _currentModel.Gender = value; OnPropertyChanged(nameof(Gender)); } }
        public string MaritalStatus { get { return _currentModel.MaritalStatus; } set { _currentModel.MaritalStatus = value; OnPropertyChanged(nameof(MaritalStatus)); } }
        public double DisabilityAdjustedLifeYears { get { return _currentModel.DisabilityAdjustedLifeYears; } set { _currentModel.DisabilityAdjustedLifeYears = value; OnPropertyChanged(nameof(DisabilityAdjustedLifeYears)); } }
        public double QualityAdjustedLifeYears { get { return _currentModel.QualityAdjustedLifeYears; } set { _currentModel.QualityAdjustedLifeYears = value; OnPropertyChanged(nameof(QualityAdjustedLifeYears)); } }
        public DateTime DeceasedDateTime { get { return _currentModel.DeceasedDateTime; } set { _currentModel.DeceasedDateTime = value; OnPropertyChanged(nameof(DeceasedDateTime)); } }


        public Details(Patient selectedPatient)
        {
            _currentModel = selectedPatient;


            EditCommand = new Command(OnEditItemExcecute, OnEditItemCanExecute);
            this.PropertyChanged += (_, __) => EditCommand.ChangeCanExecute();

            CancelCommand = new Command(OnCancelNewItemExcecute, OnCancelNewItemCanExecute);
            this.PropertyChanged += (_, __) => CancelCommand.ChangeCanExecute();

            AddressInfoCommand = new Command(OnAddressInfoExcecute, OnAddressInfoCanExcecute);
            this.PropertyChanged += (_, __) => AddressInfoCommand.ChangeCanExecute();

            BirthInfoCommand = new Command(OnBirthInfoExcecute, OnBirthInfoCanExcecute);
            this.PropertyChanged += (_, __) => BirthInfoCommand.ChangeCanExecute();

            DocumentsInfoCommand = new Command(OnDocumentsInfoExcecute, OnDocumentsInfoCanExcecute);
            this.PropertyChanged += (_, __) => DocumentsInfoCommand.ChangeCanExecute();

            TelecomInfoCommand = new Command(OnTelecomInfoExcecute, OnTelecomInfoCanExcecute);
            this.PropertyChanged += (_, __) => TelecomInfoCommand.ChangeCanExecute();

            TypeInfoCommand = new Command(OnTypeInfoExcecute, OnTypeInfoCanExcecute);
            this.PropertyChanged += (_, __) => TypeInfoCommand.ChangeCanExecute();


            Edit.CurrentItemChangedEventArgs += (x => { CurrentModel =  Patient.CopyFrom<Patient>(x); });
            Edit.CurrentItemDeletedEventArgs += (x => { _currentModel = new Patient(); });

            Task.Run(() => { SetUserImage(); }); 

        }
        Random rnd = new Random();
        void SetUserImage() {
            Thread.Sleep(50);

            Page page = App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count-1];

            App.Current.MainPage.Dispatcher.BeginInvokeOnMainThread(() =>
            {
                Xamarin.Forms.Image image = page.FindByName<Xamarin.Forms.Image>("patientImage");
                image.Source = ImageSource.FromResource($"H17XamarinForms.Images.{rnd.Next(1, 6)}.jpg");
                //image.Source = new UriImageSource { Uri = new System.Uri("https://mobileprogrammerblog.files.wordpress.com/2017/01/mvvm1.png") };
            });
        }


        public Command EditCommand { get; }
        public Command CancelCommand { get; }
        public Command AddressInfoCommand { get; }
        public Command BirthInfoCommand { get; }
        public Command DocumentsInfoCommand { get; }

        public Command TelecomInfoCommand { get; }
        public Command TypeInfoCommand { get; }



        async void OnEditItemExcecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Pages.PatientInfo.Edit(_currentModel));
        }
        bool OnEditItemCanExecute()
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


        async void OnAddressInfoExcecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Pages.AddressInfo.List(_currentModel.Id));
        }
        bool OnAddressInfoCanExcecute()
        {
            return true;
        }


        async void OnBirthInfoExcecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Pages.BirthInfo.List(_currentModel.Id));
        }
        bool OnBirthInfoCanExcecute()
        {
            return true;
        }



        async void OnDocumentsInfoExcecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Pages.DocumentsInfo.List(_currentModel.Id));
        }
        bool OnDocumentsInfoCanExcecute()
        {
            return true;
        }

        async void OnTelecomInfoExcecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Pages.TelecomInfo.List(_currentModel.Id));
        }
        bool OnTelecomInfoCanExcecute()
        {
            return true;
        }

        async void OnTypeInfoExcecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Pages.TypeInfo.List(_currentModel.Id));
        }
        bool OnTypeInfoCanExcecute()
        {
            return true;
        }
    }
}
