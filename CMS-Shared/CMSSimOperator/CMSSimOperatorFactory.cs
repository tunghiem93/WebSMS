using CMS_DTO.CMSGSM;
using CMS_DTO.CMSSimOperator;
using CMS_Entity;
using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS_Shared.CMSSimOperator
{
    public class CMSSimOperatorFactory
    {
        public bool CreateOrUpdate(CMS_SimOperatorModels model, ref string msg)
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
                            var e = new CMS_SimOperator
                            {
                                Id = _Id,
                                HeaderPhone = model.HeaderPhone,
                                OperaterName = model.OperaterName,
                                IsActive = model.IsActive,
                                UpdatedBy = model.UpdatedBy,
                                UpdatedDate = DateTime.Now,
                                CreatedBy = model.CreatedBy,
                                CreatedDate = DateTime.Now,
                            };
                            cxt.CMS_SimOperator.Add(e);
                        }
                        else
                        {
                            var e = cxt.CMS_SimOperator.Find(model.Id);
                            if (e != null)
                            {
                                e.HeaderPhone = model.HeaderPhone;
                                e.OperaterName = model.OperaterName;
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
                    var e = cxt.CMS_SimOperator.Find(Id);
                    cxt.CMS_SimOperator.Remove(e);
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

        public CMS_SimOperatorModels GetDetail(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_SimOperator.Where(x => x.Id.Equals(Id))
                                                .Select(x => new CMS_SimOperatorModels
                                                {
                                                    Id = x.Id,
                                                    HeaderPhone = x.HeaderPhone,
                                                    OperaterName = x.OperaterName,
                                                    IsActive = x.IsActive,
                                                    UpdatedBy = x.UpdatedBy,
                                                    UpdatedDate = x.UpdatedDate,
                                                    CreatedBy = x.CreatedBy,
                                                    CreatedDate = x.CreatedDate
                                                }).FirstOrDefault();
                    return data;
                }
            }
            catch (Exception) { }
            return null;
        }

        public List<CMS_SimOperatorModels> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_SimOperator.Select(x => new CMS_SimOperatorModels
                    {
                        Id = x.Id,
                        HeaderPhone = x.HeaderPhone,
                        OperaterName = x.OperaterName,
                        IsActive = x.IsActive,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate
                    }).ToList();
                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }
    }
}
