using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSBaseFactory
{
    public class BaseFactory
    {
        public static void FormatExcelExport(IXLWorksheet ws, int lastRow, int lastCol)
        {
            // Format Excel
            ws.Range(1, 1, 1, lastCol).Style.Font.SetBold(true);
            ws.Range(1, 1, lastRow, lastCol).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
            ws.Range(1, 1, lastRow, lastCol).Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;

            ws.Range(1, 1, lastRow - 1, lastCol).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range(1, 1, lastRow - 1, lastCol).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            ws.Range(1, 1, lastRow - 1, lastCol).Style.Fill.SetBackgroundColor(XLColor.FromArgb(217, 217, 217));
            ws.Columns(1, lastCol).AdjustToContents();
        }

        public DataTable ReadExcelFile(string path, string sheetName)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection conn = new OleDbConnection())
            {
                //conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0'";
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "SELECT * FROM [" + sheetName + "$]";
                cmd.Connection = conn;
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = cmd;
                try
                {
                    conn.Open();
                    da.Fill(dt);
                }
                finally
                {
                    da.Dispose();
                    cmd.Dispose();
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Dispose();
                }
                return dt;
            }
        }
    }    
}
