﻿using CMS_DTO;
using CMS_DTO.CMSEmployee;
using CMS_Entity;
using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSEmployees
{
    public class CMSDepositPackageFactory
    {
        public CMS_DepositPackageModel CreateNew()
        {
            using (var cxt = new CMS_Context())
            {
                CMS_DepositPackageModel model = new CMS_DepositPackageModel();
                model.RateSMS = GetSMSRate(cxt);
                model.RateUSD = GetUSDRate(cxt);
                return model;
            }
        }
        public bool CreateOrUpdate(CMS_DepositPackageModel model , ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if(string.IsNullOrEmpty(model.Id))
                        {
                            var _Id = Guid.NewGuid().ToString();
                            var e = new CMS_DepositPackage
                            {
                                Id = _Id,
                                PackageName = model.PackageName,
                                PackageSMS = model.PackageSMS,
                                //PackagePrice = model.PackagePrice,
                                Discount = model.Discount,
                                SMSPrice = model.SMSPrice,
                                IsActive = model.IsActive,
                                UpdatedBy = model.UpdatedBy,
                                UpdatedDate = DateTime.Now,
                                CreatedBy = model.CreatedBy,
                                CreatedDate = DateTime.Now,
                            };
                            cxt.CMS_DepositPackage.Add(e);
                        }
                        else
                        {
                            var e = cxt.CMS_DepositPackage.Find(model.Id);
                            if(e != null)
                            {
                                e.PackageName = model.PackageName;
                                e.PackageSMS = model.PackageSMS;
                                //e.PackagePrice = model.PackagePrice;
                                e.Discount = model.Discount;
                                e.SMSPrice = model.SMSPrice;
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

        public bool Delete(string Id, ref string msg)
        {
            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_DepositPackage.Find(Id);
                    cxt.CMS_DepositPackage.Remove(e);
                    cxt.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = "Không thể xóa nhân viên này";
                result = false;
            }
            return result;
        }

        public CMS_DepositPackageModel GetDetail(string Id)
        {
            try
            {
                
                using (var cxt = new CMS_Context())
                {
                    decimal smsRate = GetSMSRate(cxt);
                    decimal usdRate = GetUSDRate(cxt);
                    var data = cxt.CMS_DepositPackage.Where(x => x.Id.Equals(Id))
                                                .Select(x => new CMS_DepositPackageModel
                                                {
                                                    Id = x.Id,
                                                    PackageName = x.PackageName,
                                                    PackageSMS = x.PackageSMS,
                                                    PackagePrice = x.PackageSMS * smsRate,
                                                    RateSMS = smsRate,
                                                    RateUSD = usdRate,
                                                    Discount = x.Discount,
                                                    SMSPrice = x.SMSPrice,
                                                    IsActive = x.IsActive,
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

        public List<CMS_DepositPackageModel> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    decimal smsRate = GetSMSRate(cxt);
                    decimal usdRate = GetUSDRate(cxt);
                    decimal pmRate = GetPMRate(cxt);
                    var data = cxt.CMS_DepositPackage.Select(x => new CMS_DepositPackageModel
                    {
                        Id = x.Id,
                        PackageName = x.PackageName,
                        PackageSMS = x.PackageSMS,
                        PackagePrice = x.PackageSMS * smsRate,
                        RateSMS = smsRate,
                        Discount = x.Discount,
                        SMSPrice = x.SMSPrice,
                        IsActive = x.IsActive,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        PriceUSD =  usdRate != 0 ? (x.Discount != 0 ? ((x.PackageSMS * smsRate ) - (x.PackageSMS * smsRate * x.Discount / 100)) / usdRate : (x.PackageSMS * smsRate) / usdRate) : 0,
                        //PriceDefault = pmRate == 0 ? 0 : usdRate/ pmRate,
                        PriceDefault = usdRate != 0 ? (x.Discount != 0 ? ((x.PackageSMS * smsRate) - (x.PackageSMS * smsRate * x.Discount / 100)) / usdRate : (x.PackageSMS * smsRate) / usdRate) : 0,
                    }).OrderBy(o=>o.PackageSMS).ToList();
                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }
        private decimal GetSMSRate(CMS_Context cxt)
        {
            decimal configSMS = cxt.CMS_SysConfigs.Where(x => x.ValueType.Equals((int)Commons.ConfigType.SMS)).Select(x => x.Value ).FirstOrDefault();
            return configSMS;
        }

        private decimal GetUSDRate(CMS_Context cxt)
        {
            var configUSD = cxt.CMS_SysConfigs.FirstOrDefault(x => x.ValueType == (int)Commons.ConfigType.USD);
            return configUSD != null ? configUSD.Value : 0;
        }

        private decimal GetPMRate(CMS_Context cxt)
        {
            var configPM = cxt.CMS_SysConfigs.FirstOrDefault(x => x.ValueType == (int)Commons.ConfigType.PMUSD);
            return configPM != null ? configPM.Value : 0;
        }
    }
}
