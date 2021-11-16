using MVC_SIS_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_SIS_UI.Models
{
    public class AddStateVM : IValidatableObject
    {
        public State currentState{ get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (currentState == null)
            {
                errors.Add(new ValidationResult("Please enter state name and abbreviation"));
            }

            if (currentState.StateName == null || currentState.StateName == "")
            {
                errors.Add(new ValidationResult("Please enter a State Name"));
            }

            //state abbrev validation
            if (currentState.StateAbbreviation == null || currentState.StateAbbreviation == "")
            {
                errors.Add(new ValidationResult("Please enter a State Abbreviation"));
            }

            return errors;
        }
    }
}