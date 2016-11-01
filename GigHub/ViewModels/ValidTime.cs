using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.ViewModels
{
    //  NOTE: Custom Server-side Time DataAnnotations Validation Attribute
    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;

            var timeIsValid = DateTime.TryParseExact(Convert.ToString(value)
                , "HH:mm"
                , CultureInfo.CurrentCulture
                , DateTimeStyles.None
                , out dateTime);

            return (timeIsValid);
        }
    }
}