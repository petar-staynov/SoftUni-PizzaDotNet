namespace PizzaDotNet.Data.Common.CustomValidationAttributes
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize, string unit = "b")
        {
            switch (unit.ToLower())
            {
                case "kb":
                    this.maxFileSize = maxFileSize * 1000;
                    break;
                case "mb":
                    this.maxFileSize = maxFileSize * 1000 * 1000;
                    break;
                case "gb":
                    this.maxFileSize = maxFileSize * 1000 * 1000 * 1000;
                    break;
                default:
                    this.maxFileSize = maxFileSize;
                    break;
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > this.maxFileSize)
                {
                    return new ValidationResult(this.GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return $"Maximum allowed file size is {this.maxFileSize} bytes.";
        }
    }
}
