using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DT = System.Data;
using QC = System.Data.SqlClient;  // System.Data.dll  

namespace Link.Controllers
{
    public class Result
    {
        public int Score { get; set; }

        public string Url { get; set; }
    }

    public class HomeController : Controller
    {
        public IActionResult Index()
        {


            return View();
        }

        public IActionResult Popid()
        {
            ViewBag.contents = "";
            using (var connection = new QC.SqlConnection(  
                "Server=tcp:ganondorf2.database.windows.net,1433;Initial Catalog=ganondorf2;Persist Security Info=False;User ID=tanel3203;Password=b1gBadpassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"  
                ))  
            {  
        
                connection.Open();  
                Console.WriteLine("Connected successfully.");  

                ViewBag.contents = HomeController.SelectRows(connection);
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

                HomeController.InsertRows(connection, inputFirstName,
                                                        inputLastName,
                                                        inputBirthDate,
                                                        inputTitle,
                                                        inputUrl,
                                                        inputDescription,
                                                        inputOwnerName,
                                                        inputCategory,
                                                        inputPoints);

            }

            Console.WriteLine("Submitted!..."); 

            return View();
        }





        static public List<Result> SelectRows(QC.SqlConnection connection)  
        {  
            List<Result> names = new List<Result>();

            using (var command = new QC.SqlCommand())  
            {  
                command.Connection = connection;  
                command.CommandType = DT.CommandType.Text;  
                command.CommandText = @"  
                                        SELECT 
                                            TOP 10
                                                SUM(CAST(g1.Points AS int)) as Points,
                                                g1.Url
                                            FROM
                                                ganondorf AS g1
                                            GROUP BY
                                                g1.Url
                                            ORDER BY
                                                Points DESC

                                        ;";  

                QC.SqlDataReader reader = command.ExecuteReader();  
                
                while (reader.Read())  
                {  
                    Console.WriteLine("{0}",  
                        reader.GetString(1));                       


                    names.Add(new Result() {
                        Score = reader.GetInt32(0),
                        Url = reader.GetString(1)
                        });
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
                command.ExecuteScalar();  
                Console.WriteLine("New table created");  
            }  
        }  


    }
}
