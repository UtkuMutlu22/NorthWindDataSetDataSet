using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Utilities
{
   public static class ValidationTool
    {
        public static void Validate(IValidator validator,object entity)
        {

            var result = validator.Validate((IValidationContext)entity);
            foreach (var item in result.Errors)
            {
                Console.WriteLine(item.ErrorMessage);
            }
           
        }
    }
}
