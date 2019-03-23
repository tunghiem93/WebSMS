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
                            Status = (int)Commons.DepositStatus.WaitingPay
                        };
                        es.Add(e);
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
            var lstStatus = Status.Select(x => x).ToList();
            try
            {
                int? Isnull = null;
                if (Status != null)
                {
                    Isnull = 1;
                }
                using (var cxt = new CMS_Context())
                {
                    data = cxt.CMS_DepositTransactions
                        .Where(x => string.IsNullOrEmpty(customerId) ? 1 == 1 : x.CustomerId == customerId && Isnull.HasValue ? Status.Contains(x.Status) : 1 == 1)
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
                            sStatus = x.Status == (int)Commons.DepositStatus.WaitingPay ? "Waiting Pay"
                        : (x.Status == (int)Commons.DepositStatus.ConfirmedPay ? "Confirmed Pay" : (x.Status == (int)Commons.DepositStatus.WaitingCustomer ? "Waiting Customer" : (x.Status == (int)Commons.DepositStatus.Completed ? "Completed" : "Cancel")))
                        }).ToList();
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
    }
}
