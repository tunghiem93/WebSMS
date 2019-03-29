using CMS_DTO.CMSGSM;
using CMS_Entity;
using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS_Shared.CMSGSM
{
    public class CMSGSMFactory
    {
        public bool CreateOrUpdate(CMS_GMSModels model, ref string msg)
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
                            var e = new CMS_GSM
                            {
                                Id = _Id,
                                GSMName = model.GSMName,
                                IsActive = model.IsActive,
                                UpdatedBy = model.UpdatedBy,
                                UpdatedDate = DateTime.Now,
                                CreatedBy = model.CreatedBy,
                                CreatedDate = DateTime.Now,
                            };
                            cxt.CMS_GSM.Add(e);
                        }
                        else
                        {
                            var e = cxt.CMS_GSM.Find(model.Id);
                            if (e != null)
                            {
                                e.GSMName = model.GSMName;
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
                    var e = cxt.CMS_GSM.Find(Id);
                    cxt.CMS_GSM.Remove(e);
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

        public CMS_GMSModels GetDetail(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_GSM.Where(x => x.Id.Equals(Id))
                                                .Select(x => new CMS_GMSModels
                                                {
                                                    Id = x.Id,
                                                    GSMName = x.GSMName,
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

        public List<CMS_GMSModels> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_GSM.Select(x => new CMS_GMSModels
                    {
                        Id = x.Id,
                        GSMName = x.GSMName,
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
