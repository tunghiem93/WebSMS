﻿using CMS_DTO.CMSCategories;
using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSSysConfig;
using CMS_Entity;
using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSSystemConfig
{
    public class CMSSysConfigFactory
    {
        public List<CMS_SysConfigModels> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_SysConfigs.Select(x => new CMS_SysConfigModels
                    {                        
                        Id = x.Id,
                        Rate = x.Rate,
                        RateType = x.RateType,
                        WaitingTime = x.WaitingTime,
                        SiteContent = x.SiteContent,
                        Description = x.Description
                    }).ToList();
                    if (data == null)
                    {
                        data = new List<CMS_SysConfigModels>();
                    }
                    /* response data */
                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public CMS_SysConfigModels GetDetailSiteMaintain(string id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_SysConfigs.Select(x => new CMS_SysConfigModels
                    {
                        Id = x.Id,
                        SiteContent = x.SiteContent,
                    }).Where(o=>o.Id.Equals(id)).FirstOrDefault();
                    if (data == null)
                    {
                        data = new CMS_SysConfigModels();
                    }
                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public bool UpdateSiteMaintain(CMS_SysConfigModels model, ref string msg)
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
                            var e = cxt.CMS_SysConfigs.Find(model.Id);
                            if (e != null)
                            {
                                e.SiteContent = model.SiteContent;
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

        public CMS_SysConfigModels GetDetailRatePMUSD(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_SysConfigs.Select(x => new CMS_SysConfigModels
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

        public bool UpdateRatePMUSD(CMS_SysConfigModels model, ref string msg)
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
                            var e = cxt.CMS_SysConfigs.Find(model.Id);
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

        public CMS_SysConfigModels GetDetailRateSMSMarketing(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_SysConfigs.Select(x => new CMS_SysConfigModels
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

        public bool UpdateRateSMSMarketing(CMS_SysConfigModels model, ref string msg)
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
                            var e = cxt.CMS_SysConfigs.Find(model.Id);
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

        public CMS_SysConfigModels GetDetailRateSMSOTP(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_SysConfigs.Select(x => new CMS_SysConfigModels
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

        public bool UpdateRateSMSOTP(CMS_SysConfigModels model, ref string msg)
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
                            var e = cxt.CMS_SysConfigs.Find(model.Id);
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
                

        public CMS_SysConfigModels GetDetailWaitingTime(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_SysConfigs.Select(x => new CMS_SysConfigModels
                    {
                        WaitingTime = x.WaitingTime,
                    }).Where(x => x.Id.Equals(Id)).FirstOrDefault();

                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public bool UpdateWaitingTime(CMS_SysConfigModels model, ref string msg)
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
                            var e = cxt.CMS_SysConfigs.Find(model.Id);
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