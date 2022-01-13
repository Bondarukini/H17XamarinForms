using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace H17XamarinForms.Services
{
    internal class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        { 
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler!=null)
                handler(this, new PropertyChangedEventArgs(property));
        }
    }
}
