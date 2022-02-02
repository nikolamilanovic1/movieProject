﻿using System.ComponentModel.DataAnnotations;

namespace MoveisAPI.Validation
{
    public class FristLetterUppercaseAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var firstLetter = value.ToString()[0].ToString();

            if (firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult("First letter should be uppercase");
            }

            return ValidationResult.Success;
        }
    }
}
