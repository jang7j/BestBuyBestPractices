
using BestBuyBestPractices;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

string connString = config.GetConnectionString("DefaultConnection");

IDbConnection conn = new MySqlConnection(connString);

#region Department

var departmentRepo = new DapperDepartmentRepository(conn);

Console.WriteLine("Enter a new department: ");
string newDepartment = Console.ReadLine();

departmentRepo.InsertDepartment(newDepartment);

var departments = departmentRepo.GetAllDepartments();

foreach (var department in departments)
{
    Console.WriteLine(department.DepartmentID);
    Console.WriteLine(department.Name);
    Console.WriteLine();
}
#endregion


#region Read

var productRepo = new DapperProductRepository(conn);

Console.WriteLine("Enter name of the product: ");
var name = Console.ReadLine();
Console.WriteLine("Enter price of the product: ");
var price = double.Parse(Console.ReadLine());
Console.WriteLine("Enter product's category ID: ");
var categoryID = int.Parse(Console.ReadLine());
productRepo.CreateProduct(name, price, categoryID);

var productCollection = productRepo.GetAllProducts();
foreach (var product in productCollection)
{
    Console.WriteLine(product.ProductID);
    Console.WriteLine(product.Name);
    Console.WriteLine(product.Price);
    Console.WriteLine(product.OnSale);
    Console.WriteLine(product.StockLevel);
}
#endregion

#region Update
var productUpdate = productRepo.GetProduct(940);
productUpdate.Name = "Migas Taco";
productUpdate.Price = 3.00;
productUpdate.StockLevel = "50";

productRepo.UpdateProduct(productUpdate);

#endregion

#region Delete

productRepo.DeleteProduct(940);


#endregion

