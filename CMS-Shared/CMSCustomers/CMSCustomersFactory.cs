using CMS_DTO.CMSCustomer;
using CMS_Entity;
using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS_Shared.Utilities;
namespace CMS_Shared.CMSCustomers
{
    public class CMSCustomersFactory
    {
        public bool InsertOrUpdate(CustomerModels model, ref string ActiveCode, ref string msg, ref string key)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        var CheckEmail = cxt.CMS_Customers.Any(x => x.Email == model.Email);
                        if (CheckEmail)
                        {
                            msg = "E-mail is exits system";
                            key = "Email";
                            return false;
                        }
                        var CheckPhone = cxt.CMS_Customers.Any(x => x.Phone == model.Phone);
                        if (CheckPhone)
                        {
                            msg = "Phone is exits system";
                            key = "Phone";
                            return false;
                        }

                        if (string.IsNullOrEmpty(model.ID))
                        {
                            ActiveCode = CommonHelper.RandomVerifiCode();
                            var e = new CMS_Customers
                            {
                                Id = Guid.NewGuid().ToString(),
                                CreatedBy = model.CreatedBy,
                                CreatedDate = DateTime.Now,
                                Email = model.Email,
                                FirstName = model.FirstName,
                                IsActive = model.IsActive,
                                LastName = model.LastName,
                                Password = model.Password,
                                Password2 = model.Password2,
                                Phone = model.Phone,
                                UpdatedBy = model.UpdatedBy,
                                UpdatedDate = DateTime.Now,
                                Status = model.Status
                            };
                            e.CustomerActiveCode.Add(new CMS_CustomerActiveCode
                            {
                                Id = Guid.NewGuid().ToString(),
                                CustomerId = e.Id,
                                Code = ActiveCode,
                                IsActive = true,
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now
                            });
                            cxt.CMS_Customers.Add(e);
                        }
                        else
                        {
                            var e = cxt.CMS_Customers.Find(model.ID);
                            if (e != null)
                            {
                                e.UpdatedBy = model.UpdatedBy;
                                e.FirstName = model.FirstName;
                                e.Email = model.Email;
                                e.IsActive = model.IsActive;
                                e.LastName = model.LastName;
                                e.Password = model.Password;
                                e.Phone = model.Phone;
                                e.Status = model.Status;
                            }
                        }
                        cxt.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        ActiveCode = "";
                        trans.Rollback();
                        msg = "Lỗi đường truyền mạng";
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
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        var e = cxt.CMS_Customers.Find(Id);
                        cxt.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        msg = "Không thể xóa thể loại này";
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

        public CustomerModels GetDetail(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Customers.Where(x => x.Id.Equals(Id))
                                                .Select(x => new CustomerModels
                                                {
                                                    CreatedBy = x.CreatedBy,
                                                    CreatedDate = x.CreatedDate,
                                                    Email = x.Email,
                                                    FirstName = x.FirstName,
                                                    ID = x.Id,
                                                    IsActive = x.IsActive,
                                                    LastName = x.LastName,
                                                    Password = x.Password,
                                                    Phone = x.Phone,
                                                    UpdatedBy = x.UpdatedBy,
                                                    UpdatedDate = x.UpdatedDate
                                                }).FirstOrDefault();
                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public List<CustomerModels> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Customers.Select(x => new CustomerModels
                    {
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        ID = x.Id,
                        IsActive = x.IsActive,
                        LastName = x.LastName,
                        Password = x.Password,
                        Phone = x.Phone,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate
                    }).ToList();
                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public bool VerifyCode(string code, string email)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        var Cus = cxt.CMS_Customers.FirstOrDefault(x => x.Email == email);
                        if (Cus != null)
                        {
                            var CusId = Cus.Id;
                            var result = cxt.CMS_CustomerActiveCode.Any(x => x.CustomerId == CusId && x.Code == code && x.IsActive);
                            if (result)
                            {
                                //get Credit default
                                var Credit = cxt.CMS_SysConfigs.FirstOrDefault(x => x.RateType == (int)Commons.RateType.CreditDefault);
                                if (Credit != null)
                                {
                                    Cus.TotalCredit = Credit.Rate;
                                }
                                Cus.Status = (int)Commons.CustomerStatus.Open;
                                cxt.SaveChanges();
                                return true;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("VerifyCode", ex);
            }
            return false;
        }

        public bool ResendCode(string email, ref string activeCode)
        {
            try
            {
                using(var cxt = new CMS_Context())
                {
                    var Cus = cxt.CMS_Customers.FirstOrDefault(x => x.Email == email);
                    if (Cus != null)
                    {
                        var CusActiveCode = cxt.CMS_CustomerActiveCode.Where(x => x.CustomerId == Cus.Id).ToList();
                        if (CusActiveCode != null)
                        {
                            CusActiveCode.ForEach(x =>
                            {
                                x.IsActive = false;
                            });
                            activeCode = CommonHelper.RandomVerifiCode();
                            cxt.CMS_CustomerActiveCode.Add(new CMS_CustomerActiveCode
                            {
                                Id = Guid.NewGuid().ToString(),
                                CustomerId = Cus.Id,
                                Code = activeCode,
                                IsActive = true,
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now
                            });
                            cxt.SaveChanges();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex) {
                activeCode = "";
            }
            return false;
        }
    }
}
