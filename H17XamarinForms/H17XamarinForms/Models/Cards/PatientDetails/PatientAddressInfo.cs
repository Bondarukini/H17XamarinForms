using H17XamarinForms.Services;
using System;

namespace H17XamarinForms.Models.Cards.PatientDetails
{
    public class PatientAddressInfo:DeepCopy
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string Line { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return $"{Country}, {State}, {City}, {Line}";
        }
    }
}
