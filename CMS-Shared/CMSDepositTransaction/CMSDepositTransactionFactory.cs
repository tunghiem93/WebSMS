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
                using(var cxt = new CMS_Context())
                {
                    var es = new List<CMS_DepositTransactions>();
                    foreach(var item in model)
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
                            PaymentMethodName  = item.PaymentMethodName,
                            SMSPrice = item.SMSPrice,
                            UpdatedDate = DateTime.Now,
                            WalletMoney = item.WalletMoney,
                            Status = (int) Commons.DepositStatus.WaitingPay
                        };
                        es.Add(e);
                    }
                    cxt.CMS_DepositTransactions.AddRange(es);
                    cxt.SaveChanges();
                    msg = "Create deposite order successful";
                }
            }
            catch(Exception ex)
            {
                result = false;
                msg = "Create deposite order not successful";
                NSLog.Logger.Error("CreateDepositTransaction", ex);
            }
            return result;
        }

        public List<CMS_DepositTransactionsModel> GetListDepositTransaction(string customerId)
        {
            var data = new List<CMS_DepositTransactionsModel>();
            try
            {
                using(var cxt = new CMS_Context())
                {
                    data = cxt.CMS_DepositTransactions
                        .Where(x => string.IsNullOrEmpty(customerId) ? 1 == 1 : x.CustomerId == customerId)
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
                        WalletMoney = x.WalletMoney
                    }).ToList();
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("GetListDepositTransaction", ex);
            }
            return data;
        }

        public bool ChangeStatusDepositTransaction(List<CMS_DepositTransactionsModel> model)
        {
            var result = true;
            try
            {
                using(var cxt = new CMS_Context())
                {
                    foreach(var item in model)
                    {
                        var e = cxt.CMS_DepositTransactions.Find(item.Id);
                        e.Status = item.Status;
                        e.UpdatedDate = DateTime.Now;
                    }
                    cxt.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("ChangeStatusDepositTransaction", ex);
            }
            return result;
        }
    }
}
