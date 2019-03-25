using CMS_DTO.CMSBase;
using CMS_Shared;
using CMS_Shared.CMSCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    public class HQController : Controller
    {
        public List<SelectListItem> GetListCategorySelectItem()
        {
            var _factory = new CMSCategoriesFactory();
            var data = _factory.GetList().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.CategoryName,
            }).ToList();
            return data;
        }

        public List<CategoryByCategory> GetListCategory()
        {
            var _factory = new CMSCategoriesFactory();
            //var data = _factory.GetList().Where(x => !string.IsNullOrEmpty(x.ParentId)).Select(x => new SelectListItem
            //{
            //    Value = x.Id,
            //    Text = x.CategoryName,
            //}).ToList();
            //return data;
            var models = new List<CategoryByCategory>();
            var data = _factory.GetList();
            if (data != null)
            {
                var groupCate = data.Where(x => string.IsNullOrEmpty(x.ParentId)).ToList();
                if (groupCate != null)
                {
                    groupCate.ForEach(x =>
                    {
                        var model = new CategoryByCategory();
                        model.id = x.Id;
                        model.text = x.CategoryName.ToUpper();
                        model.children = data.Where(y => !string.IsNullOrEmpty(y.ParentId) && y.ParentId.Equals(x.Id))
                                                .Select(z => new CategoryChildren
                                                {
                                                    id = z.Id,
                                                    text = z.CategoryName
                                                }).ToList();
                        models.Add(model);
                    });
                }
            }

            return models;
        }

        public List<SelectListItem> GetListExChange()
        {
            var _lstEXChange = new List<SelectListItem>() {
                new SelectListItem() {Text=Commons.ExchangeType.None.ToString(),Value=Commons.ExchangeType.None.ToString("d") },
                new SelectListItem() {Text=Commons.ExchangeType.Binance.ToString(),Value=Commons.ExchangeType.Binance.ToString("d")},
                new SelectListItem() {Text=Commons.ExchangeType.Bittrex.ToString(),Value=Commons.ExchangeType.Bittrex.ToString("d") },
                //new SelectListItem() {Text=Commons.ExchangeType.None.ToString(),Value=Commons.ETimeType.ToolIncrease.ToString("d")},
            };

            return _lstEXChange;
        }

        public List<SelectListItem> GetListAPI()
        {
            var _lstEXChange = new List<SelectListItem>() {
                new SelectListItem() {Text=Commons.APIType.APISMS.ToString(),Value=Commons.APIType.APISMS.ToString("d") },
                new SelectListItem() {Text=Commons.APIType.APISim.ToString(),Value=Commons.APIType.APISim.ToString("d")},
                new SelectListItem() {Text=Commons.APIType.APIPerfectMonney.ToString(),Value=Commons.APIType.APIPerfectMonney.ToString("d")},
            };

            return _lstEXChange;
        }
    }
}