using CMS_DTO.CMSAPI;
using CMS_Entity;
using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSAPI
{
    public class CMSAPIFactory
    {
        public bool CreateOrUpdate(CMS_APIModels model, ref string Id, ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var beginTran = cxt.Database.BeginTransaction())
                {
                    try
                    {                        
                        if (string.IsNullOrEmpty(model.Id))
                        {
                            var _Id = Guid.NewGuid().ToString();
                            var e = new CMS_API()
                            {
                                APIName = model.APIName,
                                LinkAPI = model.LinkAPI,
                                APIType = model.APIType,
                                Description = model.Description,
                                CreatedBy = model.CreatedBy,
                                CreatedDate = DateTime.Now,
                                IsActive = model.IsActive,
                                UpdatedBy = model.UpdatedBy,
                                UpdatedDate = DateTime.Now,
                                Id = _Id
                            };
                            Id = _Id;
                            cxt.CMS_API.Add(e);
                        }
                        else
                        {
                            var e = cxt.CMS_API.Find(model.Id);
                            if (e != null)
                            {
                                e.APIName = model.APIName;
                                e.LinkAPI = model.LinkAPI;
                                e.APIType = model.APIType;
                                e.Description = model.Description;
                                e.IsActive = model.IsActive;
                                e.UpdatedBy = model.UpdatedBy;
                                e.UpdatedDate = DateTime.Now;
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

        public bool Delete(string Id, ref string msg)
        {
            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_API.Find(Id);
                    cxt.CMS_API.Remove(e);
                    cxt.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = "Không thể xóa API này";
                result = false;
            }
            return result;
        }

        public CMS_APIModels GetDetail(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_API.Select(x => new CMS_APIModels
                    {
                        APIName = x.APIName,
                        LinkAPI = x.LinkAPI,
                        APIType = x.APIType,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        Description = x.Description,
                        Id = x.Id,
                        IsActive = x.IsActive,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                    }).Where(x => x.Id.Equals(Id)).FirstOrDefault();

                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public List<CMS_APIModels> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_API.Select(x => new CMS_APIModels
                    {
                        APIName = x.APIName,
                        LinkAPI = x.LinkAPI,
                        APIType = x.APIType,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        Description = x.Description,
                        Id = x.Id,
                        IsActive = x.IsActive,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                    }).ToList();
                    /* response data */
                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }
    }
}
