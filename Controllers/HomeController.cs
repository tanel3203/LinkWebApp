using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DT = System.Data;
using QC = System.Data.SqlClient;  // System.Data.dll  
using Link.Models;

namespace Link.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            using (var connection = new QC.SqlConnection(  
                "Server=tcp:ganondorf2.database.windows.net,1433;Initial Catalog=ganondorf2;Persist Security Info=False;User ID=tanel3203;Password=b1gBadpassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"  
                ))  
            {  
                
                connection.Open();  
                Console.WriteLine("Connected successfully.");  


                Console.WriteLine("Create below."); 
                HomeController.CreateTable(connection);
                Console.WriteLine("Create above");  
            }
            


            return View();
        }

        public IActionResult Popid()
        {
            ViewData["Message"] = "Your app description page.";
            ViewData["andmed"] = "initialized";
            ViewBag.contents = "inited";
            using (var connection = new QC.SqlConnection(  
                "Server=tcp:ganondorf2.database.windows.net,1433;Initial Catalog=ganondorf2;Persist Security Info=False;User ID=tanel3203;Password=b1gBadpassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"  
                ))  
            {  
        
                connection.Open();  
                Console.WriteLine("Connected successfully.");  


                //Program.DeleteRows(connection);
                Console.WriteLine("Select below."); 
                ViewBag.contents = HomeController.SelectRows(connection);
                //ViewData["andmed"] = HomeController.SelectRows(connection).ToArray().ToString();
                Console.WriteLine("Select above");  
            }

            return View();
        }

  
        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submitted(string inputFirstName,
                                        string inputLastName,
                                        string inputBirthDate,
                                        string inputTitle,
                                        string inputUrl,
                                        string inputDescription,
                                        string inputOwnerName,
                                        string inputCategory,
                                        string inputPoints)
        {

            using (var connection = new QC.SqlConnection(  
                "Server=tcp:ganondorf2.database.windows.net,1433;Initial Catalog=ganondorf2;Persist Security Info=False;User ID=tanel3203;Password=b1gBadpassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"  
                ))  
            {  
                connection.Open();  
                Console.WriteLine("Connected successfully.");  

                Console.WriteLine("Insert below");  
                HomeController.InsertRows(connection, inputFirstName,
                                                        inputLastName,
                                                        inputBirthDate,
                                                        inputTitle,
                                                        inputUrl,
                                                        inputDescription,
                                                        inputOwnerName,
                                                        inputCategory,
                                                        inputPoints);
                Console.WriteLine("Insert closed"); 

            }

            Console.WriteLine("Submitted!..."); 
            Console.WriteLine(inputPoints);
            Console.WriteLine("Press any key to finish...");  
            Console.ReadKey(true);   

            /* Finish up 

            inputFirstName = null;
            inputLastName = null;
            inputBirthDate = null;
            inputTitle = null;
            inputUrl = null;
            inputDescription = null;
            inputOwnerName = null;
            inputCategory = null;
            inputPoints = null;*/

            return View();
        }





        static public List<string> SelectRows(QC.SqlConnection connection)  
        {  
            List<string> names = new List<string>();

            using (var command = new QC.SqlCommand())  
            {  
                command.Connection = connection;  
                command.CommandType = DT.CommandType.Text;  
                command.CommandText = @"  
SELECT  
        FirstName
    FROM  
        ganondorf  
     ";  

                QC.SqlDataReader reader = command.ExecuteReader();  
                names.Add("aa");
                while (reader.Read())  
                {  
                    Console.WriteLine("{0}",  
                        reader.GetString(0));                      


                    names.Add(reader.GetString(0));
                }  
            }  
        return names;
        } 



        static public void InsertRows(QC.SqlConnection connection, 
                                        string inputFirstName,
                                        string inputLastName,
                                        string inputBirthDate,
                                        string inputTitle,
                                        string inputUrl,
                                        string inputDescription,
                                        string inputOwnerName,
                                        string inputCategory,
                                        string inputPoints)  
        {  
            QC.SqlParameter parameter;  

            using (var command = new QC.SqlCommand())  
            {  
                command.Connection = connection;  
                command.CommandType = DT.CommandType.Text;  
                command.CommandText = @"  
INSERT INTO ganondorf 
        (FirstName,
        LastName,
        BirthDate,
        Title,
        Url,
        Description,
        OwnerName,
        Category,
        Points
        )  
    OUTPUT  
        INSERTED.Title  
    VALUES  
        (@FirstName,
        @LastName,
        @BirthDate,
        @Title,
        @Url,
        @Description,
        @OwnerName,
        @Category,
        @Points
        ); ";  


                parameter = new QC.SqlParameter("@FirstName", DT.SqlDbType.NVarChar, 255);  
                parameter.Value = inputFirstName;  
                command.Parameters.Add(parameter);  

                parameter = new QC.SqlParameter("@LastName", DT.SqlDbType.NVarChar, 255);  
                parameter.Value = inputLastName;  
                command.Parameters.Add(parameter);  

                parameter = new QC.SqlParameter("@BirthDate", DT.SqlDbType.NVarChar, 255);  
                parameter.Value = inputBirthDate;  
                command.Parameters.Add(parameter);  

                parameter = new QC.SqlParameter("@Title", DT.SqlDbType.NVarChar, 255);  
                parameter.Value = inputTitle;  
                command.Parameters.Add(parameter);  

                parameter = new QC.SqlParameter("@Url", DT.SqlDbType.NVarChar, 255);  
                parameter.Value = inputUrl;  
                command.Parameters.Add(parameter);  

                parameter = new QC.SqlParameter("@Description", DT.SqlDbType.NVarChar, 255);  
                parameter.Value = inputDescription;  
                command.Parameters.Add(parameter);  

                parameter = new QC.SqlParameter("@OwnerName", DT.SqlDbType.NVarChar, 255);  
                parameter.Value = inputOwnerName;  
                command.Parameters.Add(parameter);  

                parameter = new QC.SqlParameter("@Category", DT.SqlDbType.NVarChar, 255);  
                parameter.Value = inputCategory;  
                command.Parameters.Add(parameter);  

                parameter = new QC.SqlParameter("@Points", DT.SqlDbType.NVarChar, 255);  
                parameter.Value = inputPoints;  
                command.Parameters.Add(parameter);  
                
                string title = (string)command.ExecuteScalar();  
                Console.WriteLine("Record added - {0}", title);  
            }  
        }  

        static public void CreateTable(QC.SqlConnection connection)  
        {  

            using (var command = new QC.SqlCommand())  
            {  
                command.Connection = connection;  
                command.CommandType = DT.CommandType.Text;  
                command.CommandText = @"  
CREATE TABLE ganondorf 
        (
        FirstName varchar(255),
        LastName varchar(255),
        BirthDate varchar(255),
        Title varchar(255),
        Url varchar(255),
        Description varchar(255),
        OwnerName varchar(255),
        Category varchar(255),
        Points varchar(255)
        ); ";  
                /*
                parameter = new QC.SqlParameter("@TableName", DT.SqlDbType.NVarChar, 25);  
                parameter.Value = "LinkDb";  
                command.Parameters.Add(parameter);  
                */
                //int productId = (int)command.ExecuteScalar();  
                command.ExecuteScalar();  
                Console.WriteLine("New table created");  
            }  
        }  


    }
}
