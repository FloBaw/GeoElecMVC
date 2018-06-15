using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;

using GeoElecMVC.Models;

namespace GeoElecMVC.SuperAdminUserRoles
{
    public class ManageSuperAdminUserRoles : ISuperAdminUserRoles<UserRoles>
    {
        private string connectionString;
        public ManageSuperAdminUserRoles(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void Add(UserRoles item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO \"AspNetUserRoles\" (\"UserId\", \"RoleId\") VALUES ((SELECT \"AspNetUsers\".\"Id\" FROM \"AspNetUsers\" WHERE \"AspNetUsers\".\"UserName\"=@UserName),(SELECT \"AspNetRoles\".\"Id\" FROM \"AspNetRoles\" WHERE \"AspNetRoles\".\"Name\"=@Name))", item);
                //dbConnection.Execute("INSERT INTO \"AspNetUserRoles\" (\"UserId\",\"RoleId\") VALUES(@UserId,@RoleId)", item);
            }
        }

        public IEnumerable<UserRoles> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<UserRoles>("SELECT \"AspNetUsers\".\"UserName\", \"AspNetRoles\".\"Name\" FROM \"AspNetUsers\", \"AspNetRoles\", \"AspNetUserRoles\" WHERE \"AspNetUsers\".\"Id\" = \"AspNetUserRoles\".\"UserId\" AND \"AspNetRoles\".\"Id\" = \"AspNetUserRoles\".\"RoleId\" order by \"AspNetUsers\".\"UserName\"");
                //return dbConnection.Query<UserRoles>("SELECT \"AspNetUsers\".\"UserName\", \"AspNetRoles\".\"Name\", \"AspNetUserRoles\".\"UserId\" FROM \"AspNetUsers\", \"AspNetRoles\", \"AspNetUserRoles\" WHERE \"AspNetUsers\".\"Id\" = \"AspNetUserRoles\".\"UserId\" AND \"AspNetRoles\".\"Id\" = \"AspNetUserRoles\".\"RoleId\"");
                //return dbConnection.Query<UserRoles>("SELECT * FROM \"AspNetUserRoles\"");
            }
        }

        public UserRoles FindByID(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<UserRoles>("SELECT * FROM \"AspNetUserRoles\" WHERE \"UserId\" = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public UserRoles FindByUserName(string username)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<UserRoles>("SELECT \"AspNetUsers\".\"UserName\", \"AspNetRoles\".\"Name\" FROM \"AspNetUsers\", \"AspNetRoles\", \"AspNetUserRoles\" WHERE \"AspNetUsers\".\"Id\" = \"AspNetUserRoles\".\"UserId\" AND \"AspNetRoles\".\"Id\" = \"AspNetUserRoles\".\"RoleId\" AND \"AspNetUsers\".\"UserName\" = @Username", new { Username = username }).FirstOrDefault();
                //return dbConnection.Query<UserRoles>("SELECT * FROM \"AspNetUserRoles\" WHERE \"AspNetUserRoles\".\"UserId\" IN (SELECT \"AspNetUserRoles\".\"UserId\" FROM \"AspNetUserRoles\", \"AspNetUsers\" WHERE \"AspNetUserRoles\".\"UserId\" = \"AspNetUsers\".\"Id\" AND \"AspNetUsers\".\"UserName\" = @Username)", new { Username = username }).FirstOrDefault();
            }
        }

        public void Remove(string username)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM \"AspNetUserRoles\" WHERE \"AspNetUserRoles\".\"UserId\" IN (SELECT \"AspNetUserRoles\".\"UserId\" FROM \"AspNetUserRoles\", \"AspNetUsers\" WHERE \"AspNetUserRoles\".\"UserId\" = \"AspNetUsers\".\"Id\" AND \"AspNetUsers\".\"UserName\" = @Username)", new { Username = username });
                //dbConnection.Execute("DELETE FROM \"AspNetUserRoles\" WHERE \"UserId\"=@Id", new { Id = id });
            }
        }

        public void Update(UserRoles item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE \"AspNetUserRoles\" SET \"UserId\" = (SELECT \"AspNetUsers\".\"Id\" FROM \"AspNetUsers\" WHERE \"AspNetUsers\".\"UserName\"=@UserName), \"RoleId\" = (SELECT \"AspNetRoles\".\"Id\" FROM \"AspNetRoles\" WHERE \"AspNetRoles\".\"Name\"=@Name) WHERE \"UserId\" = (SELECT \"AspNetUsers\".\"Id\" FROM \"AspNetUsers\" WHERE \"AspNetUsers\".\"UserName\"=@UserName)", item);
                //dbConnection.Query("UPDATE \"AspNetUserRoles\" SET \"UserId\" = @UserId,  \"RoleId\"  = @RoleId WHERE \"UserId\" = @UserId", item);
            }
        }


        public IEnumerable<UserRoles> FindAwaitingUsers()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                return dbConnection.Query<UserRoles>("SELECT \"AspNetUsers\".\"UserName\" FROM \"AspNetUsers\" " +
                    "WHERE \"AspNetUsers\".\"Id\" not in (select \"AspNetUserRoles\".\"UserId\" from \"AspNetUserRoles\")");
                //return dbConnection.Query<UserRoles>("SELECT \"AspNetUsers\".\"UserName\" FROM \"AspNetUsers\" WHERE \"AspNetUsers\".\"Id\" not in (select \"AspNetUserRoles\".\"UserId\" from \"AspNetUserRoles\", \"AspNetRoles\" where \"AspNetRoles\".\"Id\" = \"AspNetUserRoles\".\"RoleId\" and \"AspNetUsers\".\"Id\" = \"AspNetUserRoles\".\"UserId\") order by \"AspNetUsers\".\"UserName\"");
            }
        }

        public UserRoles FindAwaitingUserByUserName(string username)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<UserRoles>("SELECT \"AspNetUsers\".\"UserName\" FROM \"AspNetUsers\" WHERE \"AspNetUsers\".\"UserName\" = @Username", new { Username = username }).FirstOrDefault();
            }
        }

        public void RemoveAwaiting(string username)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM \"AspNetUsers\" WHERE \"AspNetUsers\".\"UserName\"= @Username", new { Username = username });
            }
        }
    }
}
