// See https://aka.ms/new-console-template for more information
using DLA;
using Entities.Models;
using System;
using System.Linq.Expressions;

//CreateAsync().GetAwaiter().GetResult();
RetreiveAsync().GetAwaiter().GetResult();

// crear un objeto 
static async Task CreateAsync()
{

    // Add customer
    Customer customer = new Customer()
    {
        FirstName = "jonatan",
        LastName = "sanchez",
        City = "Bogota",
        Country = "colombia",
        Phone = "3005629782"

    };
    using(var repository = RepositoryFactory.CreateRepositiry())
    {
        try
        {
            var createdCustomer = await repository.CreateAsync(customer);
            Console.WriteLine($"Added customer: {createdCustomer.LastName} {createdCustomer.FirstName}");
        }
        catch (Exception ex) {
            Console.WriteLine($"Error: { ex.Message}" );
    }
}
 }

static async Task RetreiveAsync()
{
    using (var repository = RepositoryFactory.CreateRepositiry())
    {
        try
        {
            Expression<Func<Customer, bool>> criteria = c => c.FirstName == "jonatan" && c.LastName == "sanchez";
            var Customer = await repository.RetreiveAsync(criteria);
            if (Customer != null)
            {
                Console.WriteLine($"Retrived customer: {Customer.FirstName} \t{Customer.LastName} \t City: {Customer.City}\t country: {Customer.Country}");
            }
            else
            {
                Console.WriteLine("customer not exist");
            }
        }
         catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}





