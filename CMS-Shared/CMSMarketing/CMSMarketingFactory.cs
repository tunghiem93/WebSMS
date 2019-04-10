using ClosedXML.Excel;
using CMS_DTO.CMSBase;
using CMS_DTO.CMSMarketing;
using CMS_DTO.CMSSimOperator;
using CMS_Entity;
using CMS_Entity.Entity;
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

        public CMS_ReportModels Export(ref IXLWorksheet wsMarketing/*, ref IXLWorksheet wsTime*/)
        {
            var result = new CMS_ReportModels();
            try
            {
                string[] lstHeaders = new string[] { "Phone", "Message" };

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
                //wsTime.Cell(1, 1).Value = "Runing time";
                //wsTime.Cell(2, 1).Value = "60s";
                ////format
                //wsTime.Range(1, 1, 2, 1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
                //wsTime.Range(1, 1, 2, 1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
                //wsTime.Range(1, 1, 2, 1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
                //wsTime.Range(1, 1, 2, 1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);
                //BaseFactory.FormatExcelExport(wsTime, row, 1);
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

        public List<CMS_MarketingModels> Import(string filePath,string empId, string empName, string empphone, ref string msg)
        {
            using (var cxt = new CMS_Context())
            {
                List<CMS_MarketingModels> listMessage = new List<CMS_MarketingModels>();
                try
                {
                    DataTable dtMarketing = _baseFactory.GetDataFromExcel(@filePath, 1);
                    //DataTable dtTime = _baseFactory.GetDataFromExcel(@filePath, 2);
                    decimal rate = GetSMSRate(cxt, (int)Commons.ConfigType.SMSMarketing);
                    for (int i = 0; i < dtMarketing.Rows.Count; i++)
                    {
                        string phone = Convert.ToString(dtMarketing.Rows[i][0]);
                        string smsContent = Convert.ToString(dtMarketing.Rows[i][1]);
                        string subPhone = phone.Substring(0, 2);
                        string subPhone1 = phone.Substring(0, 1);
                        if (!subPhone.Equals("84") && !subPhone1.Equals("0"))
                        {
                            phone = "84" + phone;
                        }
                        
                        if (!string.IsNullOrEmpty(phone))
                        {
                            string smsConvert = Commons.ConvertUnicodeToWithoutAccent(smsContent);
                            int count2 = smsConvert.Length;
                            int smsFee = count2 / 80; // < 80 = 1sms
                            CMS_MarketingModels model = new CMS_MarketingModels() {
                                CustomerId = empId,
                                CustomerName = string.Format("{0} ({1})", empName, empphone),
                                CountMessage = count2,
                                SendTo = phone,
                                SMSContent = smsContent,
                                SMSType = (int)Commons.SMSType.Marketing,
                                Status = (int)Commons.SMSStatus.Sent,
                                SMSPrice = (smsFee + 1)*rate,
                                SMSRate = rate
                            };
                            listMessage.Add(model);
                        }
                    }
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("Import marketing error: ", ex);
                }
                finally
                {
                    cxt.Dispose();
                }
                return listMessage;
            }

        }

        public List<CMS_MarketingModels> GetList(int smsType)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Marketing.Where(x => x.SMSType.Equals(smsType)).Select(x => new CMS_MarketingModels
                    {
                        Id = x.Id,
                        CustomerId = x.CustomerId,
                        CustomerName = x.CustomerName,
                        OperatorName = x.OperatorName,
                        RunTime = x.RunTime,
                        SendFrom = x.SendFrom,
                        SendTo = x.SendTo,
                        SMSContent = x.SMSContent,
                        SMSPrice = x.SMSPrice,
                        SMSType = x.SMSType,
                        IsActive = x.IsActive,
                        Status = x.Status,
                        TimeInput = x.TimeInput,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                    }).ToList();
                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }
        public CMS_MarketingModels GetDetail(string Id)
        {
            try
            {

                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Marketing.Where(x => x.Id.Equals(Id))
                                                .Select(x => new CMS_MarketingModels
                                                {
                                                    Id = x.Id,
                                                    CustomerId = x.CustomerId,
                                                    CustomerName = x.CustomerName,
                                                    OperatorName = x.OperatorName,
                                                    RunTime = x.RunTime,
                                                    SendFrom = x.SendFrom,
                                                    SendTo = x.SendTo,
                                                    SMSContent = x.SMSContent,
                                                    SMSPrice = x.SMSPrice,
                                                    SMSType = x.SMSType,
                                                    IsActive = x.IsActive,
                                                    Status = x.Status,
                                                    TimeInput = x.TimeInput,
                                                    UpdatedBy = x.UpdatedBy,
                                                    UpdatedDate = x.UpdatedDate,
                                                    CreatedBy = x.CreatedBy,
                                                    CreatedDate = x.CreatedDate,
                                                }).FirstOrDefault();
                    return data;
                }
            }
            catch (Exception) { }
            return null;
        }
        public bool CreateOrUpdate(CMS_MarketingModels model, ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(model.Id))
                        {
                            var _Id = Guid.NewGuid().ToString();
                            var e = new CMS_Marketing
                            {
                                Id = _Id,
                                CustomerId = model.CustomerId,
                                CustomerName = model.CustomerName,
                                OperatorName = model.OperatorName,
                                RunTime = model.RunTime.Value,
                                SendFrom = model.SendFrom,
                                SendTo = model.SendTo,
                                SMSContent = model.SMSContent,
                                SMSPrice = model.SMSPrice,
                                SMSType = model.SMSType,
                                Status = model.Status,
                                SMSRate = model.SMSRate,
                                TimeInput = model.TimeInput,
                                IsActive = model.IsActive,
                                UpdatedBy = model.UpdatedBy,
                                UpdatedDate = DateTime.Now,
                                CreatedBy = model.CreatedBy,
                                CreatedDate = DateTime.Now,
                            };
                            cxt.CMS_Marketing.Add(e);
                        }
                        else
                        {
                            var e = cxt.CMS_Marketing.Find(model.Id);
                            if (e != null)
                            {
                                e.CustomerId = model.CustomerId;
                                e.CustomerName = model.CustomerName;
                                e.OperatorName = model.OperatorName;
                                e.RunTime = model.RunTime.Value;
                                e.SendFrom = model.SendFrom;
                                e.SendTo = model.SendTo;
                                e.SMSContent = model.SMSContent;
                                e.SMSPrice = model.SMSPrice;
                                e.SMSType = model.SMSType;
                                e.Status = model.Status;
                                e.SMSRate = model.SMSRate;
                                e.TimeInput = model.TimeInput;
                                e.IsActive = model.IsActive;
                                e.UpdatedDate = DateTime.Now;
                                e.UpdatedBy = model.UpdatedBy;
                            }
                        }
                        cxt.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        msg = "Vui lòng kiểm tra đường truyền";
                        result = false;
                        trans.Rollback();
                    }
                    finally
                    {
                        cxt.Dispose();
                    }
                }
            }
            return result;
        }

        public bool InsertFromExcel(CMS_MarketingModels model, ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(model.Id))
                        {
                            var _Id = Guid.NewGuid().ToString();
                            model.Id = _Id;
                            var e = new CMS_Marketing
                            {
                                Id = _Id,
                                CustomerId = model.CustomerId,
                                CustomerName = model.CustomerName,
                                OperatorName = model.OperatorName,
                                RunTime = model.RunTime.Value,
                                SendFrom = model.SendFrom,
                                SendTo = model.SendTo,
                                SMSContent = model.SMSContent,
                                SMSPrice = model.SMSPrice,
                                SMSType = model.SMSType,
                                Status = model.Status,
                                TimeInput = model.TimeInput,
                                IsActive = model.IsActive,
                                SMSRate = model.SMSRate,
                                UpdatedBy = model.UpdatedBy,
                                UpdatedDate = DateTime.Now,
                                CreatedBy = model.CreatedBy,
                                CreatedDate = DateTime.Now,
                            };
                            cxt.CMS_Marketing.Add(e);
                            var customer = cxt.CMS_Customers.Where(x => x.Id.Equals(model.CustomerId)).FirstOrDefault();
                            customer.TotalCredit = customer.TotalCredit - model.SMSPrice;
                        }
                        cxt.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        msg = "Vui lòng kiểm tra đường truyền";
                        result = false;
                        trans.Rollback();
                    }
                    finally
                    {
                        cxt.Dispose();
                    }
                }
            }
            return result;
        }
        public bool UpdateSMSStatus(string id, int status, ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(id))
                        {

                            var e = cxt.CMS_Marketing.Find(id);
                            if (e != null)
                            {
                                e.Status = status;
                                e.UpdatedDate = DateTime.Now;
                                e.UpdatedBy = "GSM";
                                if (status.Equals((int)Commons.SMSStatus.Fail))
                                {
                                    var customer = cxt.CMS_Customers.Where(x => x.Id.Equals(e.CustomerId)).FirstOrDefault();
                                    customer.TotalCredit = customer.TotalCredit + e.SMSPrice;
                                }
                            }
                            
                        }
                        cxt.SaveChanges();
                        trans.Commit();
                        NSLog.Logger.Info("Update Status SMS success: " + id + "==" + status);
                    }
                    catch (Exception ex)
                    {
                        msg = "Vui lòng kiểm tra đường truyền";
                        result = false;
                        NSLog.Logger.Error("Update Status SMS  "+ id + " :", ex);
                        trans.Rollback();
                    }
                    finally
                    {
                        cxt.Dispose();
                    }
                }
            }
            return result;
        }
        public bool CheckTotalCredit(string cusId, decimal credit)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Customers.Where(x => x.Id.Equals(cusId))
                                                .Select(x => x.TotalCredit).FirstOrDefault();
                    return data >= credit;
                }
            }
            catch (Exception) { }
            return false;
        }
        private decimal GetSMSRate(CMS_Context cxt, int smsType)
        {
            decimal configSMS = cxt.CMS_SysConfigs.Where(x => x.ValueType.Equals(smsType)).Select(x => x.Value).FirstOrDefault();
            return configSMS;
        }
        public decimal GetSMSRate(int smsType)
        {
            using (var cxt = new CMS_Context())
            {
                return GetSMSRate(cxt, smsType);
            }
        }
        public string GetOperatorName(string phone, List<CMS_SimOperatorModels> listOp)
        {
            //097, 8497, +8497
            string name = "";
            string headerPhone1 = phone;
            if (phone.Contains("+"))
            {
                headerPhone1 = phone.Trim().Substring(0, 5);
            }
            else
            {
                string headerPhone = phone.Trim().Substring(0, 1);
                if (headerPhone.Equals("0"))
                {
                    headerPhone1 = "+84" + phone.Trim().Substring(1, 2);
                }else
                {
                    if(phone.Trim().Substring(0, 2).Equals("84"))
                    {
                        headerPhone1 = "+" + phone.Trim().Substring(0, 4);
                    }
                    else
                    {
                        headerPhone1 = "+84" + phone.Trim().Substring(0, 2);
                    }
                }
            }          
            name = listOp.Where(x => x.IsActive && x.HeaderPhone.Equals(headerPhone1)).Select(x => x.OperaterName).FirstOrDefault();
            return name;
        }
    }
}
