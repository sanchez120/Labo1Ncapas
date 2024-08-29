using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class productExceptions : Exception 
    {
        public productExceptions() : base("No product found in the database.")
        {
            throw new productExceptions();
        }

        private productExceptions(string message) : base(message)
        {
            throw new productExceptions();
        }

        public static void ThrowProductAlreadyExistsException(string productName, int? supplierId)
        {
            throw new productExceptions($"A product with the name '{productName} and supplier id {supplierId} already exists.");
        }

        public static void ThrowInvalidProductIdException(int id)
        {
            throw new productExceptions($"Invalid Product ID: {id}");
        }
    }
}
