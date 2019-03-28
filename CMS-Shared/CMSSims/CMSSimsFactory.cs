using CMS_DTO.CMSSims;
using CMS_Entity;
using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS_Shared.CMSSims
{
    public class CMSSimsFactory
    {
        public bool CreateOrUpdate(CMS_SimsModels model, ref string msg)
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
                            var e = new CMS_Sims
                            {
                                Id = _Id,
                                OperatorName = model.OperatorName,
                                SimName = model.SimName,
                                SimNumber = model.SimNumber,
                                Status = model.Status,
                                IsActive = model.IsActive,
                                UpdatedBy = model.UpdatedBy,
                                UpdatedDate = DateTime.Now,
                                CreatedBy = model.CreatedBy,
                                CreatedDate = DateTime.Now
                            };
                            cxt.CMS_Sims.Add(e);
                        }
                        else
                        {
                            var e = cxt.CMS_Sims.Find(model.Id);
                            if (e != null)
                            {
                                e.OperatorName = model.OperatorName;
                                e.SimName = model.SimName;
                                e.SimNumber = model.SimNumber;
                                e.Status = model.Status;
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
                    var e = cxt.CMS_Sims.Find(Id);
                    cxt.CMS_Sims.Remove(e);
                    cxt.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = "Không thể xóa Sim này";
                result = false;
            }
            return result;
        }

        public CMS_SimsModels GetDetail(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Sims.Where(x => x.Id.Equals(Id))
                                                .Select(x => new CMS_SimsModels
                                                {
                                                    Id = x.Id,
                                                    OperatorName = x.OperatorName,
                                                    SimName = x.SimName,
                                                    SimNumber = x.SimNumber,
                                                    Status = x.Status,
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

        public List<CMS_SimsModels> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Sims.Select(x => new CMS_SimsModels
                    {
                        Id = x.Id,
                        OperatorName = x.OperatorName,
                        SimName = x.SimName,
                        SimNumber = x.SimNumber,
                        Status = x.Status,
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

        public bool UpdateStatusSim(string simName, int status, string operatorName)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        bool isCreateNew = true;
                        var e = cxt.CMS_Sims.Where(x => x.SimName.Equals(simName)).FirstOrDefault();
                        if (e != null)
                        {
                            if (!string.IsNullOrEmpty(e.Id))
                            {
                                isCreateNew = false;
                            }
                        }
                        if (isCreateNew)
                        {
                            var _Id = Guid.NewGuid().ToString();
                            var modelCreate = new CMS_Sims
                            {
                                Id = _Id,
                                OperatorName = operatorName,
                                SimName = simName,
                                SimNumber = "",
                                Status = status,
                                IsActive = true,
                                UpdatedBy = "centri",
                                UpdatedDate = DateTime.Now,
                                CreatedBy = "centri",
                                CreatedDate = DateTime.Now
                            };
                            cxt.CMS_Sims.Add(modelCreate);
                        }
                        else
                        {
                            if (e != null)
                            {
                                e.OperatorName = operatorName;
                                e.SimName = simName;
                                e.Status = status;
                                e.UpdatedDate = DateTime.Now;
                                e.UpdatedBy = "centri";
                            }
                        }
                        cxt.SaveChanges();
                        trans.Commit();
                        NSLog.Logger.Info(string.Format("Update Status Sim ({0}-{1}): Success", simName, status));
                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error("Update Status Sim: ", ex);
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
    }
}
