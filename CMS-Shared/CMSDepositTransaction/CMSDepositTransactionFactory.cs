using CMS_DTO;
using CMS_Entity;
using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSDepositTransaction
{
    public class CMSDepositTransactionFactory
    {
        public bool CreateDepositTransaction(List<CMS_DepositTransactionsModel> model, ref string msg)
        {
            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var es = new List<CMS_DepositTransactions>();
                    foreach (var item in model)
                    {
                        var e = new CMS_DepositTransactions
                        {
                            Id = Guid.NewGuid().ToString(),
                            CreatedDate = DateTime.Now,
                            CustomerId = item.CustomerId,
                            CustomerName = item.CustomerName,
                            ExchangeRate = item.ExchangeRate,
                            CreatedBy = item.CustomerId,
                            IsActive = true,
                            PackageId = item.PackageId,
                            PackageName = item.PackageName,
                            PackagePrice = item.PackagePrice,
                            PackageSMS = item.PackageSMS,
                            PayCoin = item.PayCoin,
                            PaymentMethodId = item.PaymentMethodId,
                            PaymentMethodName = item.PaymentMethodName,
                            SMSPrice = item.SMSPrice,
                            UpdatedDate = DateTime.Now,
                            WalletMoney = item.WalletMoney,
                            Status = (int)Commons.DepositStatus.WaitingPay,
                            DepositNo = item.DepositNo
                        };
                        es.Add(e);
                        var c = cxt.CMS_Customers.Where(x => x.Id.Equals(item.CustomerId)).FirstOrDefault();
                        if (c != null)
                        {
                            c.TotalCredit += item.PackageSMS;// change after confirm
                            c.UpdatedBy = item.CreatedBy;
                            c.UpdatedDate = DateTime.Now;
                        }
                    }
                    cxt.CMS_DepositTransactions.AddRange(es);
                    cxt.SaveChanges();
                    msg = "Create deposite order successful";
                }
            }
            catch (Exception ex)
            {
                result = false;
                msg = "Create deposite order not successful";
                NSLog.Logger.Error("CreateDepositTransaction", ex);
            }
            return result;
        }

        public List<CMS_DepositTransactionsModel> GetListDepositTransaction(string customerId, IEnumerable<int> Status = null)
        {
            var data = new List<CMS_DepositTransactionsModel>();
            try
            {
                using (var cxt = new CMS_Context())
                {
                    if (Status != null)
                    {
                        data = cxt.CMS_DepositTransactions
                            .Where(x => x.CustomerId == customerId && Status.Contains(x.Status))
                            .Select(x => new CMS_DepositTransactionsModel()
                            {
                                CreatedBy = x.CreatedBy,
                                CreatedDate = x.CreatedDate,
                                CustomerId = x.CustomerId,
                                CustomerName = x.CustomerName,
                                ExchangeRate = x.ExchangeRate,
                                Id = x.Id,
                                IsActive = x.IsActive,
                                PackageId = x.PackageId,
                                PackageName = x.PackageName,
                                PackagePrice = x.PackagePrice,
                                PackageSMS = x.PackageSMS,
                                PayCoin = x.PayCoin,
                                PaymentMethodId = x.PaymentMethodId,
                                PaymentMethodName = x.PaymentMethodName,
                                SMSPrice = x.SMSPrice,
                                Status = x.Status,
                                WalletMoney = x.WalletMoney,
                                DepositNo = x.DepositNo,
                                sStatus = x.Status == (int)Commons.DepositStatus.WaitingPay ? "Waiting Pay"
                            : (x.Status == (int)Commons.DepositStatus.ConfirmedPay ? "Confirmed Pay" : (x.Status == (int)Commons.DepositStatus.WaitingCustomer ? "Waiting Customer" : (x.Status == (int)Commons.DepositStatus.Completed ? "Completed" : "Cancel")))
                            }).ToList();
                    }
                    else
                    {
                        data = cxt.CMS_DepositTransactions
    .Where(x => x.CustomerId == customerId)
    .Select(x => new CMS_DepositTransactionsModel()
    {
        CreatedBy = x.CreatedBy,
        CreatedDate = x.CreatedDate,
        CustomerId = x.CustomerId,
        CustomerName = x.CustomerName,
        ExchangeRate = x.ExchangeRate,
        Id = x.Id,
        IsActive = x.IsActive,
        PackageId = x.PackageId,
        PackageName = x.PackageName,
        PackagePrice = x.PackagePrice,
        PackageSMS = x.PackageSMS,
        PayCoin = x.PayCoin,
        PaymentMethodId = x.PaymentMethodId,
        PaymentMethodName = x.PaymentMethodName,
        SMSPrice = x.SMSPrice,
        Status = x.Status,
        WalletMoney = x.WalletMoney,
        DepositNo = x.DepositNo,
        sStatus = x.Status == (int)Commons.DepositStatus.WaitingPay ? "Waiting Pay"
    : (x.Status == (int)Commons.DepositStatus.ConfirmedPay ? "Confirmed Pay" : (x.Status == (int)Commons.DepositStatus.WaitingCustomer ? "Waiting Customer" : (x.Status == (int)Commons.DepositStatus.Completed ? "Completed" : "Cancel")))
    }).ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetListDepositTransaction", ex);
            }
            return data;
        }

        public bool ChangeStatusDepositTransaction(List<CMS_DepositTransactionsModel> model, int Status)
        {
            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var Ids = model.Select(x => x.Id).ToList();
                    var e = cxt.CMS_DepositTransactions.Where(x => Ids.Contains(x.Id)).ToList();
                    e.ForEach(x =>
                    {
                        x.Status = Status;
                        x.UpdatedDate = DateTime.Now;
                    });

                    cxt.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ChangeStatusDepositTransaction", ex);
            }
            return result;
        }

        public List<CMS_DepositTransactionsModel> GetList()
        {
            var data = new List<CMS_DepositTransactionsModel>();
            try
            {
                using (var cxt = new CMS_Context())
                {
                    data = cxt.CMS_DepositTransactions
                        .Select(x => new CMS_DepositTransactionsModel()
                        {
                            CreatedBy = x.CreatedBy,
                            CreatedDate = x.CreatedDate,
                            CustomerId = x.CustomerId,
                            CustomerName = x.CustomerName,
                            ExchangeRate = x.ExchangeRate,
                            Id = x.Id,
                            IsActive = x.IsActive,
                            PackageId = x.PackageId,
                            PackageName = x.PackageName,
                            PackagePrice = x.PackagePrice,
                            PackageSMS = x.PackageSMS,
                            PayCoin = x.PayCoin,
                            PaymentMethodId = x.PaymentMethodId,
                            PaymentMethodName = x.PaymentMethodName,
                            SMSPrice = x.SMSPrice,
                            Status = x.Status,
                            WalletMoney = x.WalletMoney,
                            DepositNo = x.DepositNo
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetListDepositTransaction", ex);
            }
            return data;
        }
        public CMS_DepositTransactionsModel GetDetail(string depId)
        {
            var data = new CMS_DepositTransactionsModel();
            try
            {
                using (var cxt = new CMS_Context())
                {
                    data = cxt.CMS_DepositTransactions
                        .Where(x => x.Id.Equals(depId))
                        .Select(x => new CMS_DepositTransactionsModel()
                        {
                            CreatedBy = x.CreatedBy,
                            CreatedDate = x.CreatedDate,
                            CustomerId = x.CustomerId,
                            CustomerName = x.CustomerName,
                            ExchangeRate = x.ExchangeRate,
                            Id = x.Id,
                            IsActive = x.IsActive,
                            PackageId = x.PackageId,
                            PackageName = x.PackageName,
                            PackagePrice = x.PackagePrice,
                            PackageSMS = x.PackageSMS,
                            PayCoin = x.PayCoin,
                            PaymentMethodId = x.PaymentMethodId,
                            PaymentMethodName = x.PaymentMethodName,
                            SMSPrice = x.SMSPrice,
                            Status = x.Status,
                            WalletMoney = x.WalletMoney,
                            DepositNo = x.DepositNo
                        }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetDetailDepositTransaction", ex);
            }
            return data;
        }
        public bool Delete(string Id, ref string msg)
        {
            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_DepositTransactions.Find(Id);
                    cxt.CMS_DepositTransactions.Remove(e);
                    cxt.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = "Không thể xóa Transactions";
                result = false;
            }
            return result;
        }
        public bool ChangeStatus(CMS_DepositTransactionsModel model, string userId)
        {
            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    using (var trans = cxt.Database.BeginTransaction())
                    {
                        try
                        {
                            var e = cxt.CMS_DepositTransactions.Where(x => x.Id.Equals(model.Id)).ToList();
                            e.ForEach(x =>
                            {
                                x.Status = model.Status;
                                x.UpdatedDate = DateTime.Now;
                                x.UpdatedBy = userId;
                            });
                            var c = cxt.CMS_Customers.Where(x => x.Id.Equals(model.CustomerId)).FirstOrDefault();
                            if (c != null)
                            {
                                c.TotalCredit += model.PackageSMS;// change after confirm
                                c.UpdatedBy = userId;
                                c.UpdatedDate = DateTime.Now;
                            }
                            cxt.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            NSLog.Logger.Error("ChangeStatusDepositTransaction", ex);
                            trans.Rollback();
                        }
                        finally
                        {
                            cxt.Dispose();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ChangeStatusDepositTransaction", ex);
            }
            return result;
        }
    }
}
