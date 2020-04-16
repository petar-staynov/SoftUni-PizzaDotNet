// namespace PizzaDotNet.Data.Common.CustomValidationAttributes
// {
//     using System.Collections;
//     using System.Collections.Generic;
//     using System.ComponentModel.DataAnnotations;
//
//     using PizzaDotNet.Data.Models;
//
//     public class ProductSizesAttribute : ValidationAttribute
//     {
//         protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//         {
//             var list = value as IList<object>;
//             if (list == null)
//             {
//                 return new ValidationResult(this.InvalidSizeListMessage());
//             }
//
//             if (list.Count <= 0)
//             {
//                 return new ValidationResult(this.InvalidSizeListMessage());
//             }
//
//             foreach (object valueItem in (IEnumerable) value)
//             {
//                 // TODO: Fix this circular reference
//                 var sizeItem = valueItem;
//
//                 if (sizeItem == null || sizeItem.Size == null || sizeItem.Price == null)
//                 {
//                     return new ValidationResult(this.InvalidSizeMessage());
//                 }
//
//                 if (sizeItem.Size.Length <= 0)
//                 {
//                     return new ValidationResult(this.InvalidSizeLengthMessage());
//                 }
//
//                 if (sizeItem.Price <= 0)
//                 {
//                     return new ValidationResult(this.InvalidSizePriceMessage());
//                 }
//             }
//
//             return ValidationResult.Success;
//         }
//
//         private string InvalidSizeListMessage()
//         {
//             return $"Product must have at least one size.";
//         }
//
//         private string InvalidSizeMessage()
//         {
//             return $"Invalid size entered.";
//         }
//
//         private string InvalidSizeLengthMessage()
//         {
//             return $"Size must have a name.";
//         }
//
//         private string InvalidSizePriceMessage()
//         {
//             return $"Invalid price entered.";
//         }
//     }
// }
