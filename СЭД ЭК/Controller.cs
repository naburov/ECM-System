using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;


namespace СЭД_ЭК
{
    class DBUtils
    {
        public static string username { get; set; }
        public static string password { get; set; }
        public static MySqlConnection GetDBConnection()
        { 
             string host = "127.0.0.1";
             int port = 3306;
             string database = "";
            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }

    class DBMySQLUtils
    {
        public static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            // Connection String.
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }
    }

    class ExportToExcel
    {
        static public void Export(string address, System.Data.DataTable dt)
        {
            var excel_app = new Application();

            excel_app.Visible = false;
            var workbook = excel_app.Workbooks.Add(Type.Missing);
            var worksheet = (Worksheet)excel_app.ActiveSheet;

            char colIndex = 'A';
            int rowIndex = 1;

            foreach (DataColumn col in dt.Columns)
            {
                colIndex++;
                worksheet.Cells[rowIndex, colIndex.ToString()] = col.ColumnName.ToString();
                ((Range) worksheet.Columns[colIndex]).AutoFit();
            }
            
            foreach (DataRow row in dt.Rows)
            {
                colIndex = 'A';
                rowIndex++;
                foreach (object o in row.ItemArray)
                {
                    worksheet.Cells[rowIndex, colIndex.ToString()] = o;
                    colIndex++;
                }
            }

            for (colIndex = 'A'; colIndex < dt.Rows[0].ItemArray.Length; colIndex++)
            {
                ((Range) worksheet.Columns[colIndex]).AutoFit();
            }

            workbook.SaveAs(address);
            workbook.Close();
            excel_app.Quit();
            GC.Collect();
        }
    }
}



