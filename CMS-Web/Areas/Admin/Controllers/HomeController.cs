﻿using CMS_DTO.CMSCompany;
using CMS_DTO.CMSHome;
using CMS_Shared;
using CMS_Shared.CMSCompanies;
using CMS_Shared.Utilities;
using CMS_Web.Web.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    [NuAuth]
    public class HomeController : Controller
    {
        CMSCompaniesFactory _factory = new CMSCompaniesFactory();

        // GET: Admin/Home
        public ActionResult Index()
        {
            var model = new CMSHomeModels();
            try
            {
                var _Path = HostingEnvironment.MapPath("~/Uploads/Silder/");
                var list = Directory.GetFiles(_Path).Select(x => Path.GetFileName(x)).ToList();

                if (list != null && list.Count > 0)
                {
                    // var index = 1;
                    for (var i = 0; i < list.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                model.Silder.ImageURL1 = list[i];
                                break;
                            case 1:
                                model.Silder.ImageURL2 = list[i];
                                break;
                            case 2:
                                model.Silder.ImageURL3 = list[i];
                                break;
                            case 3:
                                model.Silder.ImageURL4 = list[i];
                                break;
                            case 4:
                                model.Silder.ImageURL5 = list[i];
                                break;
                            case 5:
                                model.Silder.ImageURL6 = list[i];
                                break;
                        }
                    }
                }


                /* get com info */
                var listCom = _factory.GetList();
                if (listCom != null && listCom.Count > 0)
                    model.ComInfo = listCom.FirstOrDefault();
                else
                    model.ComInfo = new CMS_CompanyModels();
            }
            catch (Exception ex) { }
            return View(model);
        }

        [HttpPost]
        public ActionResult UploadSilder(CMSHomeModels model)
        {
            byte[] photoByte = null;
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return RedirectToAction("Index");
            }
            if (model.Silder.PictureUpload1 != null && model.Silder.PictureUpload1.ContentLength > 0)
            {
                //Delete image 
                if (System.IO.File.Exists(Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL1)))
                {
                    ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL1));
                }

                Byte[] imgByte = new Byte[model.Silder.PictureUpload1.ContentLength];
                model.Silder.PictureUpload1.InputStream.Read(imgByte, 0, model.Silder.PictureUpload1.ContentLength);
                model.Silder.PictureByte1 = imgByte;
                model.Silder.ImageURL1 = Guid.NewGuid() + Path.GetExtension(model.Silder.PictureUpload1.FileName);
                model.Silder.PictureUpload1 = null;
                photoByte = imgByte;
                if (!string.IsNullOrEmpty(model.Silder.ImageURL1) && model.Silder.PictureByte1 != null)
                {
                    var path = Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL1);
                    MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
                    ms.Write(imgByte, 0, imgByte.Length);
                    System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);
                    photoByte = null;
                    ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.Silder.ImageURL1, ref photoByte, 400, Commons.WidthImageSilder, Commons.HeightImageSilder);
                    model.Silder.ImageURL1 = string.Empty;
                }
            }

            if (model.Silder.PictureUpload2 != null && model.Silder.PictureUpload2.ContentLength > 0)
            {
                //Delete image 
                if (System.IO.File.Exists(Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL2)))
                {
                    ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL2));
                }

                Byte[] imgByte = new Byte[model.Silder.PictureUpload2.ContentLength];
                model.Silder.PictureUpload2.InputStream.Read(imgByte, 0, model.Silder.PictureUpload2.ContentLength);
                model.Silder.PictureByte2 = imgByte;
                model.Silder.ImageURL2 = Guid.NewGuid() + Path.GetExtension(model.Silder.PictureUpload2.FileName);
                model.Silder.PictureUpload2 = null;
                photoByte = imgByte;
                if (!string.IsNullOrEmpty(model.Silder.ImageURL2) && model.Silder.PictureByte2 != null)
                {
                    var path = Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL2);
                    MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
                    ms.Write(imgByte, 0, imgByte.Length);
                    System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);
                    photoByte = null;
                    ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.Silder.ImageURL2, ref photoByte);
                    model.Silder.ImageURL2 = string.Empty;
                }
            }

            if (model.Silder.PictureUpload3 != null && model.Silder.PictureUpload3.ContentLength > 0)
            {
                //Delete image 
                if (System.IO.File.Exists(Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL3)))
                {
                    ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Silder/"+model.Silder.ImageURL3));
                }

                Byte[] imgByte = new Byte[model.Silder.PictureUpload3.ContentLength];
                model.Silder.PictureUpload3.InputStream.Read(imgByte, 0, model.Silder.PictureUpload3.ContentLength);
                model.Silder.PictureByte3 = imgByte;
                model.Silder.ImageURL3 = Guid.NewGuid() + Path.GetExtension(model.Silder.PictureUpload3.FileName);
                model.Silder.PictureUpload3 = null;
                photoByte = imgByte;
                if (!string.IsNullOrEmpty(model.Silder.ImageURL3) && model.Silder.PictureByte3 != null)
                {
                    var path = Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL3);
                    MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
                    ms.Write(imgByte, 0, imgByte.Length);
                    System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);
                    photoByte = null;
                    ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.Silder.ImageURL3, ref photoByte);
                    model.Silder.ImageURL3 = string.Empty;
                }
            }

            if (model.Silder.PictureUpload4 != null && model.Silder.PictureUpload4.ContentLength > 0)
            {
                //Delete image 
                if (System.IO.File.Exists(Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL4)))
                {
                    ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL4));
                }

                Byte[] imgByte = new Byte[model.Silder.PictureUpload4.ContentLength];
                model.Silder.PictureUpload4.InputStream.Read(imgByte, 0, model.Silder.PictureUpload4.ContentLength);
                model.Silder.PictureByte4 = imgByte;
                model.Silder.ImageURL4 = Guid.NewGuid() + Path.GetExtension(model.Silder.PictureUpload4.FileName);
                model.Silder.PictureUpload4 = null;
                photoByte = imgByte;
                if (!string.IsNullOrEmpty(model.Silder.ImageURL4) && model.Silder.PictureByte4 != null)
                {
                    var path = Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL4);
                    MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
                    ms.Write(imgByte, 0, imgByte.Length);
                    System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);
                    photoByte = null;
                    ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.Silder.ImageURL4, ref photoByte);
                    model.Silder.ImageURL4 = string.Empty;
                }
            }

            if (model.Silder.PictureUpload5 != null && model.Silder.PictureUpload5.ContentLength > 0)
            {
                //Delete image 
                if (System.IO.File.Exists(Server.MapPath("~/Uploads/Silder/"+model.Silder.ImageURL5)))
                {
                    ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL5));
                }

                Byte[] imgByte = new Byte[model.Silder.PictureUpload5.ContentLength];
                model.Silder.PictureUpload5.InputStream.Read(imgByte, 0, model.Silder.PictureUpload5.ContentLength);
                model.Silder.PictureByte5 = imgByte;
                model.Silder.ImageURL5 = Guid.NewGuid() + Path.GetExtension(model.Silder.PictureUpload5.FileName);
                model.Silder.PictureUpload5 = null;
                photoByte = imgByte;
                if (!string.IsNullOrEmpty(model.Silder.ImageURL5) && model.Silder.PictureByte5 != null)
                {
                    var path = Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL5);
                    MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
                    ms.Write(imgByte, 0, imgByte.Length);
                    System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);
                    photoByte = null;
                    ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.Silder.ImageURL5, ref photoByte);
                    model.Silder.ImageURL5 = string.Empty;
                }
            }

            if (model.Silder.PictureUpload6 != null && model.Silder.PictureUpload6.ContentLength > 0)
            {
                //Delete image 
                if (System.IO.File.Exists(Server.MapPath("~/Uploads/Silder/"+model.Silder.ImageURL6)))
                {
                    ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Silder/"+model.Silder.ImageURL6));
                }

                Byte[] imgByte = new Byte[model.Silder.PictureUpload6.ContentLength];
                model.Silder.PictureUpload6.InputStream.Read(imgByte, 0, model.Silder.PictureUpload6.ContentLength);
                model.Silder.PictureByte6 = imgByte;
                model.Silder.ImageURL6 = Guid.NewGuid() + Path.GetExtension(model.Silder.PictureUpload6.FileName);
                model.Silder.PictureUpload6 = null;
                photoByte = imgByte;
                if (!string.IsNullOrEmpty(model.Silder.ImageURL6) && model.Silder.PictureByte6 != null)
                {
                    var path = Server.MapPath("~/Uploads/Silder/" + model.Silder.ImageURL6);
                    MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
                    ms.Write(imgByte, 0, imgByte.Length);
                    System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);
                    photoByte = null;
                    ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.Silder.ImageURL6, ref photoByte);
                    model.Silder.ImageURL6 = string.Empty;
                }
            }

            //reload partial view silder
            var _Path = HostingEnvironment.MapPath("~/Uploads/Silder/");
            var list = Directory.GetFiles(_Path).Select(x => Path.GetFileName(x)).ToList();

            if (list != null && list.Count > 0)
            {
                // var index = 1;
                for (var i = 0; i < list.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            model.Silder.ImageURL1 = list[i];
                            break;
                        case 1:
                            model.Silder.ImageURL2 = list[i];
                            break;
                        case 2:
                            model.Silder.ImageURL3 = list[i];
                            break;
                        case 3:
                            model.Silder.ImageURL4 = list[i];
                            break;
                        case 4:
                            model.Silder.ImageURL5 = list[i];
                            break;
                        case 5:
                            model.Silder.ImageURL6 = list[i];
                            break;
                    }
                }
            }

            return PartialView("_Slider", model);
        }

        [HttpPost]
        public ActionResult UpdateCompanyInfo(CMSHomeModels model)
        {
            byte[] photoByte = null;
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return RedirectToAction("Index");
            }

            if (model.ComInfo.PictureUpload != null && model.ComInfo.PictureUpload.ContentLength > 0)
            {
                Byte[] imgByte = new Byte[model.ComInfo.PictureUpload.ContentLength];
                model.ComInfo.PictureUpload.InputStream.Read(imgByte, 0, model.ComInfo.PictureUpload.ContentLength);
                model.ComInfo.PictureByte = imgByte;
                model.ComInfo.ImageURL = Guid.NewGuid() + Path.GetExtension(model.ComInfo.PictureUpload.FileName);
                model.ComInfo.PictureUpload = null;
                photoByte = imgByte;
            }

            var msg = "";
            var result = _factory.CreateOrUpdate(model.ComInfo, ref msg);
            if (result)
            {
                if (!string.IsNullOrEmpty(model.ComInfo.ImageURL) && model.ComInfo.PictureByte != null)
                {
                    var path = Server.MapPath("~/Uploads/" + model.ComInfo.ImageURL);
                    MemoryStream ms = new MemoryStream(photoByte, 0, photoByte.Length);
                    ms.Write(photoByte, 0, photoByte.Length);
                    System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                    ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.ComInfo.ImageURL, ref photoByte);
                }

                /* get com info */
                var listCom = _factory.GetList();
                if (listCom != null && listCom.Count > 0)
                    model.ComInfo = listCom.FirstOrDefault();
                else
                    model.ComInfo = new CMS_CompanyModels();
            }
            else
            {
                ModelState.AddModelError("CompanyError", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return PartialView("_CompanyInfo", model);
        }
    }
}