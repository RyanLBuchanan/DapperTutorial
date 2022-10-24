// Dapper Plus
// Doc: https://dapper-tutorial.net/step-by-step-tutorial

// @nuget: Dapper -Version 1.60.6
// @nuget: Z.Dapper.Plus

using Dapper;
using System.Data;
using System.Data.SqlClient;
using DapperTutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperTutorial
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CreateDatabase();
            List<Customer> customers = new List<Customer>();
            using (IDbConnection db = new SqlConnection(FiddleHelper.GetConnectionStringSqlServer()))
            {
                customers = db.Query<Customer>("Select * From Customers").ToList();
            }
            return View(customers);
        }

        public static void CreateDatabase()
        {
            using (var connection = new SqlConnection(FiddleHelper.GetConnectionStringSqlServer()))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"   
CREATE TABLE [dbo].[Customers] (
    [CustomerID] INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (MAX) NULL,
    [LastName]   NVARCHAR (MAX) NULL,
    [Email]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Customers] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);

INSERT INTO Customers (FirstName, LastName, Email) Values ('Carson', 'Alexander', 'carson.alexander@example.com');
INSERT INTO Customers (FirstName, LastName, Email) Values ('Meredith', 'Alonso', 'Meredith.Alonso@example.com');
INSERT INTO Customers (FirstName, LastName, Email) Values ('Arturo', 'Anand', 'Arturo.Anand@example.com');
INSERT INTO Customers (FirstName, LastName, Email) Values ('Gytis', 'Barzdukas', 'Gytis.Barzdukas@example.com');
INSERT INTO Customers (FirstName, LastName, Email) Values ('Yan', 'Li', 'Yan.Li@example.com');
";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}