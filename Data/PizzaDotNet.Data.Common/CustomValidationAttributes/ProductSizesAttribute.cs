namespace PizzaDotNet.Data.Common.CustomValidationAttributes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public class ProductSizesAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var itemsList = (IList)value;

            bool atLeastOneValid = false;
            foreach (object obj in itemsList)
            {
                Type objType = obj.GetType();
                PropertyInfo sizeProp = objType.GetProperty("Name");
                PropertyInfo priceProp = objType.GetProperty("Price");

                if (sizeProp == null || priceProp == null)
                {
                    continue;
                }

                var sizeValue = sizeProp.GetValue(obj);
                string sizeString = (string)sizeValue;

                var priceValue = priceProp.GetValue(obj);
                decimal priceDecimal = (decimal)priceValue;

                if (String.IsNullOrEmpty(sizeString))
                {
                    continue;
                }

                // Check for valid name and invalid price
                if (sizeString.Length > 0 && priceDecimal < 0M)
                {
                    return new ValidationResult(this.InvalidSizePriceMessage());
                }

                // Check for valid price and invalid name
                if (sizeString.Length <= 0 && priceDecimal >= 0M)
                {
                    return new ValidationResult(this.InvalidSizeNameMessage());
                }

                 atLeastOneValid = true;
            }

            return atLeastOneValid
                ? ValidationResult.Success
                : new ValidationResult(this.InvalidSizeMessage());
        }

        private string InvalidSizeMessage()
        {
            return $"Please enter a valid size with a name and a price of 0 or more.";
        }

        private string InvalidSizeNameMessage()
        {
            return $"Invalid Size name.";
        }

        private string InvalidSizePriceMessage()
        {
            return $"Invalid Size price.";
        }
    }
}
