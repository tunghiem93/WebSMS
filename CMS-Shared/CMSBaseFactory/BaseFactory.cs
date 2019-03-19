using ClosedXML.Excel;
using System;
using System.Collections.Generic;
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
    }    
}
