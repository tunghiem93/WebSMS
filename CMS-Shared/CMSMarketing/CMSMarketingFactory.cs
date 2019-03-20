using ClosedXML.Excel;
using CMS_DTO.CMSBase;
using CMS_Entity;
using CMS_Shared.CMSBaseFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSMarketing
{
    public class CMSMarketingFactory
    {
        BaseFactory _baseFactory = new BaseFactory();

        public CMS_ReportModels Export(ref IXLWorksheet wsMarketing, ref IXLWorksheet wsTime)
        {
            var result = new CMS_ReportModels();
            try
            {
                string[] lstHeaders = new string[] {"Phone", "Message" };
                    
                int row = 1;
                //add header to excel file
                for (int i = 1; i <= lstHeaders.Length; i++)
                    wsMarketing.Cell(row, i).Value = lstHeaders[i - 1];

                wsMarketing.Cell(2, 1).Value = "0987654321";
                wsMarketing.Cell(2, 2).Value = "Content your mesage at here!";
                //format
                wsMarketing.Range(1, 1, 2, 2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
                wsMarketing.Range(1, 1, 2, 2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
                wsMarketing.Range(1, 1, 2, 2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
                wsMarketing.Range(1, 1, 2, 2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);
                int cols = lstHeaders.Length;
                row = 2;
                BaseFactory.FormatExcelExport(wsMarketing, row, cols);
                //Sheet 2
                wsTime.Cell(1, 1).Value = "Runing time";
                wsTime.Cell(2, 1).Value = "60s";
                //format
                wsTime.Range(1, 1, 2, 1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
                wsTime.Range(1, 1, 2, 1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
                wsTime.Range(1, 1, 2, 1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
                wsTime.Range(1, 1, 2, 1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);
                BaseFactory.FormatExcelExport(wsTime, row, 1);
                //============
                result.IsOk = true;
            }
          
            catch (Exception ex)
            {
                result.IsOk = false;
                result.Message = ex.Message;
                NSLog.Logger.Error(ex);
            }

            return result;
        }

        public bool Import(string filePath, ref string msg)
        {
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        DataTable dtMarketing = _baseFactory.ReadExcelFile(@filePath, "Marketing");
                        DataTable dtTime = _baseFactory.ReadExcelFile(@filePath, "Time");
                        string tmpExcelPath = System.Web.HttpContext.Current.Server.MapPath("~/ImportExportTemplate") + "/MarketingImportTemplate.xlsx";
                        DataTable dtTmpMarketing = _baseFactory.ReadExcelFile(@tmpExcelPath, "Marketing");
                        DataTable dtTmpTime = _baseFactory.ReadExcelFile(@tmpExcelPath, "Time");
                        NSLog.Logger.Info("Import marketing susscess");
                        cxt.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error("Import marketing error: ", ex);
                        trans.Rollback();
                    }
                    finally
                    {
                        cxt.Dispose();
                    }
                }
            }
            return true;
        }        
    }
}
