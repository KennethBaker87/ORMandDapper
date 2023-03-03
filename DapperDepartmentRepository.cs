using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMandDapper
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection _conn;

        public DapperDepartmentRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _conn.Query<Department>("SELECT * FROM departments");
        }
        public Product GetProduct(int id)
        {
            return _conn.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @id;",
                new { id = id });
        }
        public void UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE products " +
                "SET Name = @name, " +
                "Price = @price, " +
                "CategoryID = @catID, " +
                "OnSale - @onSale, " +
                "StockLevel = @stock;" +
                "WHERE ProductID = @id;",
                new 
                { 
                    id = product.ProductID,
                    name = product.Name, 
                    price = product.Price, 
                    catID = product.CategoryID, 
                    onSale = product.OnSale, 
                    stock = product.StockLevel
                });

        }
        public void InsertDepartments(string name)
        {
            _conn.Execute("INSERT INTO departments (Name) VALUES (@name)", new {name = name });
        }
    }
}
