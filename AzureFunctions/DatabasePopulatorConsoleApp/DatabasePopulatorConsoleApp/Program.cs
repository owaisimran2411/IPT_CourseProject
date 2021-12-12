using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace DatabasePopulatorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "F:\\IPT_CourseProject\\AzureFunctions\\PakwheelsDataScrapperConsoleApp\\PakwheelsDataScrapperConsoleApp\\bin\\Debug\\adsData.csv";
            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                var fileRows = fileContent.Split('\n');

                // opening the connection to database
                var dataSource = "DESKTOP-D0VQCM8\\MSSQLSERVERDEV";
                var database = "IPT_CourseProject";
                var username = "sa";
                var password = "owais123";

                string connString = @"Data Source=" + dataSource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

                SqlConnection conn = new SqlConnection(connString);


                try
                {
                    Console.WriteLine("Openning Connection ...");

                    //open connection
                    conn.Open();

                    Console.WriteLine("Connection successful!");

                   

                    Console.WriteLine($"{fileRows[0]}");
                    for (int i = 1; i < fileRows.Length - 2; i++)
                    {
                        var data = fileRows[i].Split(',');
                        StringBuilder strBuilder = new StringBuilder();
                        strBuilder.Append($"IF NOT EXISTS (SELECT * FROM AdsData WHERE AdsData.ad_id = {data[12]})" +
                            "INSERT INTO AdsData (brand_name, description, item_condition, model_year, manufacturer, fuel_type," +
                            "image_url, transmission, engine_capacity, engine_mileage, price, details_url, ad_id) VALUES ");
                        
                        string insertValues = string.Empty;
                        
                       /* if (i == fileRows.Length - 3)
                        {*/
                            /*
                             * 0-brandName, 1
                             * 1-descriptionText, 8
                             * 2-itemCondition, 2
                             * 3-modelYear, 3
                             * 4-manufacturer, 4
                             * 5-fuelType, 5
                             * 6-imageUrl, 11
                             * 7-transmission, 6
                             * 8-engineDisplacement, 7
                             * 9-mileage, 9
                             * 10-price, 12
                             * 11-detailsUrl, 10
                             * 12-adId, 0
                             */
                            /*int id = Convert.ToInt32(data[12]);
                            insertValues = $"({data[0]}, {data[1]}, {data[2]}, {data[3]}, {data[4]}, {data[5]}, {data[6].ToString()}, {data[7]}, {data[8]}, {data[9]}, {data[10]}, {data[11].ToString()}, {id})";
                            Console.WriteLine($"{insertValues}");*/
                        /*}
                        else
                        {*/
                        int id = Convert.ToInt32(data[12]);
                        insertValues = $"('{data[0]}', '{data[1]}', '{data[2]}', '{data[3]}', '{data[4]}', '{data[5]}', '{data[6].Substring(8)}', '{data[7]}', '{data[8]}', '{data[9]}', '{data[10]}', '{data[11].Substring(8)}', '{id}')";
                        /*Console.WriteLine($"{insertValues}");*/
                        /*}*/
                        strBuilder.Append(insertValues);
                        string sqlQuery = strBuilder.ToString();
                        Console.WriteLine(id);
                        using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                        {
                            command.ExecuteNonQuery();
                            Console.WriteLine("Query Inserted");
                        }
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

                
            }
            else
            {
                Console.WriteLine($"File: {filePath} does not exist");
                /*OnStop();*/
            }
        }
    }
}
