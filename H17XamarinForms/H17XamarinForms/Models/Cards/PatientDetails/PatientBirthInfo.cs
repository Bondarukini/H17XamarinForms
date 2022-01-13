using H17XamarinForms.Services;
using System;
namespace H17XamarinForms.Models.Cards.PatientDetails
{
    public class PatientBirthInfo : DeepCopy
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthCity { get; set; }
        public string BirthState { get; set; } 
        public string BirthCountry { get; set; } 
        public string Birthsex { get; set; }

        public override string ToString()
        {
            return $"{BirthCountry}, {BirthState}, {BirthCity}, {Birthsex}, {BirthDate.ToString("dd MMMM yyyy")}";
        }
    }
}
