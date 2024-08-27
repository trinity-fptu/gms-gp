using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ValidateAttributes
{
    public enum ComparisonType
    {
        GreaterThan,
        LessThan
    }

    /// <summary>
    /// Validates that the date property is either greater than or less than the specified comparison date.
    /// The input comparisonDate in string format (yyyy-MM-dd), or "currenttime" for current date and time
    /// </summary>
    public class DateComparisonAttribute : ValidationAttribute
    {
        private readonly DateTime _comparisonDate;
        private readonly ComparisonType _comparisonType;

        /// <param name="comparisonDate">The comparison date in string format (yyyy-MM-dd), or "currenttime" for current date and time.</param>
        /// <param name="comparisonType">The type of comparison to perform.</param>
        public DateComparisonAttribute(string comparisonDate, ComparisonType comparisonType)
        {
            if (comparisonDate.ToLower() == "currenttime")
            {
                _comparisonDate = DateTime.Today;
            }
            else if (!DateTime.TryParse(comparisonDate, out _comparisonDate))
            {
                throw new ArgumentException("Invalid comparison date", nameof(comparisonDate));
            }
            _comparisonType = comparisonType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value is DateTime date)
            {
                int result = DateTime.Compare(date, _comparisonDate);
                switch (_comparisonType)
                {
                    case ComparisonType.GreaterThan:
                        if (result < 0)
                        {
                            return new ValidationResult($"Date must be after {_comparisonDate.ToShortDateString()}.");
                        }
                        break;
                    case ComparisonType.LessThan:
                        if (result > 0)
                        {
                            return new ValidationResult($"Date must be before  {_comparisonDate.ToShortDateString()}.");
                        }
                        break;
                    default:
                        throw new InvalidOperationException("Invalid comparison type.");
                }
            }
            return ValidationResult.Success;
        }
    }

}
