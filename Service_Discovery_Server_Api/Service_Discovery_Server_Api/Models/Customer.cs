using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Service_Discovery_Server_Api.Models;

namespace Service_Discovery_Server_Api.Models
{
    public class Customer
    {
        [NotNull]
        [MaxLength(10)]
        [Key]
        public int CustomerId { get; set; }
       
        [StringLength(45)]
        public required string FirstName { get; set; }
       
        [StringLength(45)]
        public required string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(45)]
        public required string Email { get; set; }

        [StringLength(100)]
        public required string StreesAddress { get; set; }

        [StringLength(45)]
        public required string City { get; set; }

        [StringLength(45)]
        public required string State { get; set; }

        [NotNull]
        [MaxLength(12)]
        public int ZipCode { get; set; }

        [StringLength(45)]
        public required string Country { get; set; }

        [Required]
        public string Sex { get; set; }
    }


public static class CustomerEndpoints
{
	public static void MapCustomerEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Customer").WithTags(nameof(Customer));

        group.MapGet("/", async (HungDbContext db) =>
        {
            return await db.Customers.ToListAsync();
        })
        .WithName("GetAllCustomers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Customer>, NotFound>> (int customerid, HungDbContext db) =>
        {
            return await db.Customers.AsNoTracking()
                .FirstOrDefaultAsync(model => model.CustomerId == customerid)
                is Customer model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCustomerById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int customerid, Customer customer, HungDbContext db) =>
        {
            var affected = await db.Customers
                .Where(model => model.CustomerId == customerid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.CustomerId, customer.CustomerId)
                  .SetProperty(m => m.FirstName, customer.FirstName)
                  .SetProperty(m => m.LastName, customer.LastName)
                  .SetProperty(m => m.DateOfBirth, customer.DateOfBirth)
                  .SetProperty(m => m.Email, customer.Email)
                  .SetProperty(m => m.StreesAddress, customer.StreesAddress)
                  .SetProperty(m => m.City, customer.City)
                  .SetProperty(m => m.State, customer.State)
                  .SetProperty(m => m.ZipCode, customer.ZipCode)
                  .SetProperty(m => m.Country, customer.Country)
                  .SetProperty(m => m.Sex, customer.Sex)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCustomer")
        .WithOpenApi();

        group.MapPost("/", async (Customer customer, HungDbContext db) =>
        {
            db.Customers.Add(customer);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Customer/{customer.CustomerId}",customer);
        })
        .WithName("CreateCustomer")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int customerid, HungDbContext db) =>
        {
            var affected = await db.Customers
                .Where(model => model.CustomerId == customerid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCustomer")
        .WithOpenApi();
    }
}}
