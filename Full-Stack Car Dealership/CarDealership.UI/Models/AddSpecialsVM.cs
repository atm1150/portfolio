using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDealership.UI.Models
{
    public class AddSpecialsVM : IValidatableObject
    {
        public Special Special { get; set; }
        public List<Special> SpecialList { get; set; }
        public AddSpecialsVM()
        {
            Special = new Special();
            SpecialList = new List<Special>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
         
            if (string.IsNullOrEmpty(Special.Title))
            {
                errors.Add(new ValidationResult("Title is required",
                    new[] { "special.Title" }));
            }
            if (string.IsNullOrEmpty(Special.SpecialDescription))
            {
                errors.Add(new ValidationResult("Description is required",
                    new[] { "special.SpecialDescription" }));
            }
            return errors;
        }
    }
}