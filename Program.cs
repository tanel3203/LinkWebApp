using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using DT = System.Data;
using QC = System.Data.SqlClient;  // System.Data.dll  


namespace Link
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var connection = new QC.SqlConnection(  
                "Server=tcp:ganondorf2.database.windows.net,1433;Initial Catalog=ganondorf2;Persist Security Info=False;User ID=tanel3203;Password=b1gBadpassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"  
                ))  
            {  
                connection.Open();  
                Console.WriteLine("Connected successfully.");  

                //Program.CreateTable(connection);

                //Program.DeleteRows(connection);
                //Program.SelectRows(connection);
                //Program.InsertRows(connection);


                Console.WriteLine("Press any key to finish...");  
                Console.ReadKey(true);  
            }  

           var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        static public void SelectRows(QC.SqlConnection connection)  
        {  
            using (var command = new QC.SqlCommand())  
            {  
                command.Connection = connection;  
                command.CommandType = DT.CommandType.Text;  
                command.CommandText = @"  
SELECT  
        FirstName
    FROM  
        TereDb  
     ";  

                QC.SqlDataReader reader = command.ExecuteReader();  

                while (reader.Read())  
                {  
                    Console.WriteLine("{0}",  
                        reader.GetString(0));  
                }  
            }  
        } 

        static public void InsertRows(QC.SqlConnection connection)  
        {  
            QC.SqlParameter parameter;  

            using (var command = new QC.SqlCommand())  
            {  
                command.Connection = connection;  
                command.CommandType = DT.CommandType.Text;  
                command.CommandText = @"  
INSERT INTO LinkDb 
        (PersonID,  
        FirstName,  
        LastName
        )  
    OUTPUT  
        INSERTED.PersonID  
    VALUES  
        (@PersonID,  
        @FirstName,  
        @LastName 
        ); ";  

                parameter = new QC.SqlParameter("@PersonID", DT.SqlDbType.Int, 50);  
                parameter.Value = 55;  
                command.Parameters.Add(parameter);  

                parameter = new QC.SqlParameter("@FirstName", DT.SqlDbType.NVarChar, 50);  
                parameter.Value = "aaaaass";  
                command.Parameters.Add(parameter);  

                parameter = new QC.SqlParameter("@LastName", DT.SqlDbType.NVarChar, 50);  
                parameter.Value = "sdfsd";  
                command.Parameters.Add(parameter);  

                int personId = (int)command.ExecuteScalar();  
                Console.WriteLine("The generated PersonID = {0}.", personId);  
            }  
        }  
        static public void DeleteRows(QC.SqlConnection connection)  
        {  
            QC.SqlParameter parameter;  

            using (var command = new QC.SqlCommand())  
            {  
                command.Connection = connection;  
                command.CommandType = DT.CommandType.Text;  
                command.CommandText = @"  
DELETE FROM SalesLT.Product  
    WHERE NOT
        (ProductNumber=@ProductNumber
        ); ";  

                parameter = new QC.SqlParameter("@ProductNumber", DT.SqlDbType.NVarChar, 25);  
                parameter.Value = "ALLO2017";  
                command.Parameters.Add(parameter);  

                int productId = (int)command.ExecuteScalar();  
                Console.WriteLine("Deleted");  
            }  
        }  
        static public void CreateTable(QC.SqlConnection connection)  
        {  
            QC.SqlParameter parameter;  

            using (var command = new QC.SqlCommand())  
            {  
                command.Connection = connection;  
                command.CommandType = DT.CommandType.Text;  
                command.CommandText = @"  
CREATE TABLE TereDb 
        (
        FirstName varchar(255),
        LastName varchar(255)
        ); ";   
                /*
                parameter = new QC.SqlParameter("@TableName", DT.SqlDbType.NVarChar, 25);  
                parameter.Value = "LinkDb";  
                command.Parameters.Add(parameter);  
                */
                //int productId = (int)command.ExecuteScalar();  
                Console.WriteLine("New table created");  
            }  
        }  
    }
}



