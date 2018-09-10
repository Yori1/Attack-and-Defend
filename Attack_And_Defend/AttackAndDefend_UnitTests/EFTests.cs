using System;
using Attack_And_Defend.Data;
using Attack_And_Defend.Logic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Xunit;
using System.Data.SQLite;

namespace AttackAndDefend_UnitTests
{
    public class EFTests
    {
        SQLiteConnection createDatabaseAndConnection()
        {
            var connection = new SQLiteConnection("DataSource=:memory:");
            connection.Open();

            var options = getDbContextOptions(connection);

            // Create the schema in the database
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
            }

            return connection;
        }

        DbContextOptions<ApplicationDbContext> getDbContextOptions(SQLiteConnection conn)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(conn)
                    .Options;
            return options;
        }

        [Fact]
        public void CreateUser()
        {
            SQLiteConnection conn = createDatabaseAndConnection();
            var options = getDbContextOptions(conn);

            using (ApplicationDbContext context = new ApplicationDbContext(options))
            {
                 
            }


        }
    }
}
