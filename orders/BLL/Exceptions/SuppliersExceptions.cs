using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    internal class SuppliersExceptions : Exception
    {
        public SuppliersExceptions() : base("No Supplier found in the database.")
        {
        }

        private SuppliersExceptions(string message) : base(message)
        {
        }

        public static void ThrowSupplierAlreadyExistsException(string companyName, string contactName)
        {
            throw new SuppliersExceptions($"A Supplier with the name '{companyName}' and contact '{contactName}' already exists.");
        }

        public static void ThrowInvalidSupplierIdException(int id)
        {
            throw new SuppliersExceptions($"Invalid Supplier ID: {id}");
        }
    }
}



   