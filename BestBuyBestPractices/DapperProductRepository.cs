using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
                
        private readonly IDbConnection _connection;          //setting the connection lines 15-20

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("insert into products (name, price, categoryID) values (@name, @price, @categoryID);",
                new { productName = name, price = price, categoryID = categoryID });
        }
        public IEnumerable<Product> GetAllProducts()
        {
           
            return _connection.Query<Product>("select * from products;");    //this statement will get sent to SQL. 
                                                                            //dapper extends => IDbConnection (_connection)
        }



        public Product GetProduct(int id)
        {
            return _connection.QuerySingle<Product>("Select * from products where products = @id", new { id });  //<- anonymous type
        }

        public void UpdateProduct(Product product)
        {
            _connection.Execute("Update products SET Name = @Name, Price = @Price, CategoryID = @CategoryID, Onsale = @Onsale, Stocklevel = @Stocklevel WHERE productID = @productID",
                new
                {
                    name = product.Name,
                    price = product.Price,
                    categoryID = product.CategoryID,
                    productID = product.ProductID,
                    onsale = product.OnSale,
                    stocklevel = product.StockLevel

                });
        }
        public void DeleteProduct(int id)
        {
            _connection.Execute("DELETE FROM sales WHERE productID = @id", new { id = id });
            _connection.Execute("DELETE FROM reviews WHERE productID = @id", new { id = id });
            _connection.Execute("DELETE FROM products WHERE productID = @id", new { id = id });
        }
    }
}
