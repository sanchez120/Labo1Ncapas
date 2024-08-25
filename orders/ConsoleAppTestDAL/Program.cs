// See https://aka.ms/new-console-template for more information
using DLA;
using Entities.Models;
using System;
using System.Linq.Expressions;

//CreateAsync().GetAwaiter().GetResult();
//RetreiveAsync().GetAwaiter().GetResult();
UpdateAsync().GetAwaiter().GetResult();


Console.ReadKey();
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


static async Task UpdateAsync()
{
    // supuesto: existe el objeto a modificar
    
    using(var repository = RepositoryFactory.CreateRepositiry())
    {
        var customerToUpdate = await repository.RetreiveAsync<Customer>(c => c.Id == 78);

        if (customerToUpdate != null)
        {
            customerToUpdate.FirstName = "Liu";
            customerToUpdate.LastName = "Wong";
            customerToUpdate.City = "Toronto";
            customerToUpdate.Country = "Canada";
            customerToUpdate.Phone = "+14337 6353039";
        }

        try
        {
            bool update = await repository.UpdateAsync(customerToUpdate);
            if (update)
            {
                Console.WriteLine("customer updated suceessfully");
            }
            else
            {
                Console.WriteLine("customer update faild.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
}


