using BLL.Exceptions;
using DAL;
using DLA;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class Customers
    {
        public async Task<Customer> CreateAsync(Customer customer)
        {
            Customer customerResult = null;

            // Validar que los campos obligatorios estén completos
            if (string.IsNullOrWhiteSpace(customer.FirstName))
            {
                throw new CustomerExceptions("El nombre es un campo obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(customer.LastName))
            {
                throw new CustomerExceptions("El apellido es un campo obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(customer.City))
            {
                throw new CustomerExceptions("La ciudad es un campo obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(customer.Country))
            {
                throw new CustomerExceptions("El país es un campo obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(customer.Phone))
            {
                throw new CustomerExceptions("El teléfono es un campo obligatorio.");
            }

            // Validar formato de número de teléfono
            if (!IsValidPhone(customer.Phone))
            {
                throw new CustomerExceptions("El número de teléfono no tiene un formato válido.");
            }

            using (var repository = RepositoryFactory.CreateRepositiry())
            {
                // Buscar si el cliente existe basado en el nombre y apellido
                Customer customerSearch = await repository.RetreiveAsync<Customer>(c => c.FirstName == customer.FirstName && c.LastName == customer.LastName);
                if (customerSearch == null)
                {
                    // Si no hay duplicados, crear el cliente
                    customerResult = await repository.CreateAsync(customer);
                }
                else
                {
                    // Lanzar excepción si el cliente ya existe basado en el nombre y apellido
                    CustomerExceptions.ThrowCustomerAlreadyExistsException(customerSearch.FirstName, customerSearch.LastName);
                }

                return customerResult!;
            }
        }

        // Método auxiliar para validar el formato de un número de teléfono
        private bool IsValidPhone(string phone)
        {
            // Puedes ajustar esta expresión regular según las reglas de formato de teléfono que necesites
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\d{10}$");
        }
        public async Task<Customer> RetrieveByAsync(int id)
        {
            Customer result = null;

            using (var repository = RepositoryFactory.CreateRepositiry())
            {
                Customer customer = await repository.RetreiveAsync<Customer>(c => c.Id == id);

                // Check if customer was found
                if (customer == null)
                {
                    // Throw a CustomerNotFoundException (assuming you have this class)
                    CustomerExceptions.ThrowInvalidCustomerIdException(id);
                }

                return customer;
            }


        }

        public async Task<List<Customer>> RetrieveAllAsync()
        {
            List<Customer> result = null;

            using (var r = RepositoryFactory.CreateRepositiry())
            {
                Expression<Func<Customer, bool>> allCustomersCriteria = x => true;
                result = await r.FilterAsync<Customer>(allCustomersCriteria);
            }
            return result;
        }
        public async Task<bool> UpdateAsync(Customer customer)
        {
            bool Result = false;
            using (var repository = RepositoryFactory.CreateRepositiry())
            {
                // Validar que el nombre del cliente no exista
                Customer customerSearch =
                await repository.RetreiveAsync<Customer>
                (c => c.FirstName == customer.FirstName
                && c.Id != customer.Id);
                if (customerSearch == null)
                {
                    // No existe
                    Result = await repository.UpdateAsync(customer);
                }
                else
                {
                    // Podemos implementar alguna lógica para
                    // indicar que no se pudo modificar
                    CustomerExceptions.ThrowCustomerAlreadyExistsException(customerSearch.FirstName, customerSearch.LastName);
                }
            }
            return Result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool Result = false;
            // Buscar un cliente para ver si tiene Orders (Ordenes de Compra)
            var customer = await RetrieveByAsync(id);
            if (customer != null)
            {
                // Eliminar el cliente
                using (var repository = RepositoryFactory.CreateRepositiry())
                {
                    Result = await repository.DeleteAsync(customer);
                }
            }
            else
            {
                // Podemos implementar alguna lógica
                // para indicar que el producto no existe
                CustomerExceptions.ThrowInvalidCustomerIdException(id);
            }
            return Result;
        }

    }

}






