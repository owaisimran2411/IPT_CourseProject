using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using System.Data.SqlClient;

using CsvHelper;

namespace DatabasePopulator
{
    public partial class Service1 : ServiceBase
    {
        public static int minute_to_ms = 60000;
        public Service1()
        {
            InitializeComponent();
        }
        Timer timer = new Timer();
        protected override void OnStart(string[] args)
        {
            PopulateDB();
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval += 120 * minute_to_ms;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            // call the function
            PopulateDB();
        }

        private void PopulateDB()
        {
            // open the csv file
            string filePath = System.Configuration.ConfigurationManager.AppSettings["csvFilePath"];
            
            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                var fileRows = fileContent.Split('\n');

                // opening the connection to database
                var dataSource = System.Configuration.ConfigurationManager.AppSettings["datasource"];
                var database = System.Configuration.ConfigurationManager.AppSettings["database"];
                var username = System.Configuration.ConfigurationManager.AppSettings["username"];
                var password = System.Configuration.ConfigurationManager.AppSettings["password"];

                string connString = @"Data Source=" + dataSource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

                SqlConnection conn = new SqlConnection(connString);


                try
                {
                    WriteErrorLog("Openning Connection ...");

                    //open connection
                    conn.Open();

                    WriteErrorLog("Connection successful!");



                    
                    for (int i = 1; i < fileRows.Length - 2; i++)
                    {
                        var data = fileRows[i].Split(',');
                        StringBuilder strBuilder = new StringBuilder();
                        strBuilder.Append($"IF NOT EXISTS (SELECT * FROM AdsData WHERE AdsData.ad_id = {data[12]})" +
                            "INSERT INTO AdsData (brand_name, description, item_condition, model_year, manufacturer, fuel_type," +
                            "image_url, transmission, engine_capacity, engine_mileage, price, details_url, ad_id) VALUES ");

                        string insertValues = string.Empty;

                        
                        int id = Convert.ToInt32(data[12]);
                        insertValues = $"('{data[0]}', '{data[1]}', '{data[2]}', '{data[3]}', '{data[4]}', '{data[5]}', '{data[6].Substring(8)}', '{data[7]}', '{data[8]}', '{data[9]}', '{data[10]}', '{data[11].Substring(8)}', '{id}')";
                        
                        strBuilder.Append(insertValues);
                        string sqlQuery = strBuilder.ToString();
                        
                        using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                        {
                            command.ExecuteNonQuery();
                            WriteErrorLog("Query Inserted");
                        }
                    }

                }
                catch (Exception e)
                {
                    WriteErrorLog("Error: " + e.Message);
                }


            }
            else
            {
                WriteErrorLog($"File: {filePath} does not exist");
                /*OnStop();*/
            }
        }


        // function for logging errors
        public static void WriteErrorLog(string Message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
    }
}
