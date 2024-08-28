using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class CustomerExceptions : Exception
    {
        // You can add more static methods here to throw other customer-related exceptions

        public CustomerExceptions()
        {
            throw new CustomerExceptions($"no se encontró ningún cliente en la base de datos ");
        }

        public CustomerExceptions(string message) : base(message)
        {
            // Optional: Add constructor logic for logging or custom error handling
            throw new Exception(message);
        }

        public static void ThrowCustomerAlreadyExistsException(string firstName, string lastName)
        {
            throw new CustomerExceptions($"Una cliente con el nombre ya existe {firstName} {lastName}.");
        }

        public static void ThrowInvalidCustomerDataException(int id)
        {
            throw new CustomerExceptions($"ID de cliente no válido: {id}");
        }
    }
}
