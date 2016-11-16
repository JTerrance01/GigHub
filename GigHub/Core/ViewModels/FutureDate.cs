using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.Core.ViewModels
{
    //  NOTE: Custom Server-side DataAnnotations Validation Attribute
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;

            var dateIsValid = DateTime.TryParseExact(Convert.ToString(value)
                , "d MMM yyyy"
                , CultureInfo.CurrentCulture
                , DateTimeStyles.None
                , out dateTime);

            return (dateIsValid && dateTime > DateTime.Now);
        }
    }
}