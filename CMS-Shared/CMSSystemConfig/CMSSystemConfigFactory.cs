using CMS_DTO.CMSCategories;
using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSRate;
using CMS_DTO.CMSSiteMaitain;
using CMS_DTO.CMSTime;
using CMS_Entity;
using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSSystemConfig
{
    public class CMSSystemConfigFactory
    {
        public CMS_SiteMaintainModels GetDetailSiteMaintain(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_ConfigRates.Select(x => new CMS_SiteMaintainModels
                    {
                        Id = x.Id,
                        Content = x.Description,
                    }).Where(x => x.Id.Equals(Id)).FirstOrDefault();

                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public bool UpdateSiteMaintain(CMS_SiteMaintainModels model, ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var beginTran = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(model.Id))
                        {
                            var e = cxt.CMS_ConfigRates.Find(model.Id);
                            if (e != null)
                            {
                                e.Description = model.Content;
                            }
                        }
                        cxt.SaveChanges();
                        beginTran.Commit();
                    }
                    catch (Exception ex)
                    {
                        msg = "Lỗi đường truyền mạng";
                        beginTran.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

        public CMS_RateModels GetDetailRatePMUSD(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_ConfigRates.Select(x => new CMS_RateModels
                    {
                        Id = x.Id,
                        Rate = x.Rate,
                        RateType = x.RateType,
                    }).Where(x => x.Id.Equals(Id)).FirstOrDefault();

                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public bool UpdateRatePMUSD(CMS_RateModels model, ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var beginTran = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(model.Id))
                        {
                            var e = cxt.CMS_ConfigRates.Find(model.Id);
                            if (e != null)
                            {
                                e.Rate = model.Rate;
                                e.RateType = (int)Commons.RateType.PMUSD;
                            }
                        }
                        cxt.SaveChanges();
                        beginTran.Commit();
                    }
                    catch (Exception ex)
                    {
                        msg = "Lỗi đường truyền mạng";
                        beginTran.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

        public CMS_RateModels GetDetailRateSMSMarketing(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_ConfigRates.Select(x => new CMS_RateModels
                    {
                        Id = x.Id,
                        Rate = x.Rate,
                        RateType = x.RateType,
                    }).Where(x => x.Id.Equals(Id) && x.RateType.Equals((int)Commons.RateType.SMSMarketing)).FirstOrDefault();

                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public bool UpdateRateSMSMarketing(CMS_RateModels model, ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var beginTran = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(model.Id))
                        {
                            var e = cxt.CMS_ConfigRates.Find(model.Id);
                            if (e != null)
                            {
                                e.Rate = model.Rate;
                                e.RateType = (int)Commons.RateType.SMSMarketing;
                            }
                        }
                        cxt.SaveChanges();
                        beginTran.Commit();
                    }
                    catch (Exception ex)
                    {
                        msg = "Lỗi đường truyền mạng";
                        beginTran.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

        public CMS_RateModels GetDetailRateSMSOTP(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_ConfigRates.Select(x => new CMS_RateModels
                    {
                        Id = x.Id,
                        Rate = x.Rate,
                        RateType = x.RateType,
                    }).Where(x => x.Id.Equals(Id) && x.RateType.Equals((int)Commons.RateType.SMSOTP)).FirstOrDefault();

                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public bool UpdateRateSMSOTP(CMS_RateModels model, ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var beginTran = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(model.Id))
                        {
                            var e = cxt.CMS_ConfigRates.Find(model.Id);
                            if (e != null)
                            {
                                e.Rate = model.Rate;
                                e.RateType = (int)Commons.RateType.SMSOTP;
                            }
                        }
                        cxt.SaveChanges();
                        beginTran.Commit();
                    }
                    catch (Exception ex)
                    {
                        msg = "Lỗi đường truyền mạng";
                        beginTran.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

        public CustomerModels GetDetailCreditNewMember(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Customers.Select(x => new CustomerModels
                    {
                        TotalCredit = x.TotalCredit,
                    }).Where(x => x.ID.Equals(Id)).FirstOrDefault();

                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }
                

        public CMS_TimeModels GetDetailWaitingTime(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Times.Select(x => new CMS_TimeModels
                    {
                        WaitingTime = x.WaitingTime,
                    }).Where(x => x.Id.Equals(Id)).FirstOrDefault();

                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public bool UpdateWaitingTime(CMS_TimeModels model, ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var beginTran = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(model.Id))
                        {                            
                            var e = cxt.CMS_Times.Find(model.Id);
                            if (e != null)
                            {
                                e.WaitingTime = model.WaitingTime;
                            }
                        }
                        cxt.SaveChanges();
                        beginTran.Commit();
                    }
                    catch (Exception ex)
                    {
                        msg = "Lỗi đường truyền mạng";
                        beginTran.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }        
    }
}
