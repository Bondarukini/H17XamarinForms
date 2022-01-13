using H17XamarinForms.Services;
using System;

namespace H17XamarinForms.Models.Cards.PatientDetails
{
    public class PatientTypeInfo : DeepCopy
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Key} - {Value}";
        }
    }
}
