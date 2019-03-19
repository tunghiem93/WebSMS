using CMS_DTO;
using CMS_DTO.CMSEmployee;
using CMS_DTO.CMSPaymentMethod;
using CMS_Entity;
using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSEmployees
{
    public class CMSPaymentMethodFactory
    {
        public bool CreateOrUpdate(CMS_PaymentMethodModels model, ref string msg)
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
                            var e = new CMS_PaymentMethod
                            {
                                Id = _Id,
                                PaymentName = model.PaymentName,
                                PaymentType = model.PaymentType,
                                WalletMoney = model.WalletMoney,
                                ReferenceExchange = model.ReferenceExchange,
                                ScaleNumber = model.ScaleNumber,
                                TagContent = model.TagContent,
                                IsActive = model.IsActive,
                                UpdatedBy = model.UpdatedBy,
                                UpdatedDate = DateTime.Now,
                                CreatedBy = model.CreatedBy,
                                CreatedDate = DateTime.Now,
                            };
                            cxt.CMS_PaymentMethod.Add(e);
                        }
                        else
                        {
                            var e = cxt.CMS_PaymentMethod.Find(model.Id);
                            if (e != null)
                            {
                                e.PaymentName = model.PaymentName;
                                e.PaymentType = model.PaymentType;
                                e.WalletMoney = model.WalletMoney;
                                e.ReferenceExchange = model.ReferenceExchange;
                                e.ScaleNumber = model.ScaleNumber;
                                e.TagContent = model.TagContent;
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
                    var e = cxt.CMS_PaymentMethod.Find(Id);
                    cxt.CMS_PaymentMethod.Remove(e);
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

        public CMS_PaymentMethodModels GetDetail(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_PaymentMethod.Where(x => x.Id.Equals(Id))
                                                .Select(x => new CMS_PaymentMethodModels
                                                {
                                                    Id = x.Id,
                                                    PaymentName = x.PaymentName,
                                                    PaymentType = x.PaymentType,
                                                    WalletMoney = x.WalletMoney,
                                                    ReferenceExchange = x.ReferenceExchange,
                                                    ScaleNumber = x.ScaleNumber,
                                                    TagContent = x.TagContent,
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

        public List<CMS_PaymentMethodModels> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_PaymentMethod.Select(x => new CMS_PaymentMethodModels
                    {
                        Id = x.Id,
                        PaymentName = x.PaymentName,
                        PaymentType = x.PaymentType,
                        WalletMoney = x.WalletMoney,
                        ReferenceExchange = x.ReferenceExchange,
                        ScaleNumber = x.ScaleNumber,
                        TagContent = x.TagContent,
                        IsActive = x.IsActive,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                    }).ToList();
                    return data;
                }
            }
            catch (Exception) { }
            return null;
        }
    }
}
