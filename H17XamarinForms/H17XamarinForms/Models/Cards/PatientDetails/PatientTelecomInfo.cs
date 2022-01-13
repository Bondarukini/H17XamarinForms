using H17XamarinForms.Services;
using System;

namespace H17XamarinForms.Models.Cards.PatientDetails
{
    public class PatientTelecomInfo : DeepCopy
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string System { get; set; }
        public string Value { get; set; }
        public string Use { get; set; } //homew /work

        public override string ToString()
        {
            return $"{System} - {Value} ({Use})";
        }
    }
}
