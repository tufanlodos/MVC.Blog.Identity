using Blog.BLL.Repository;
using Blog.DAL.Context;
using Blog.Entity.Entity;
using Blog.Entity.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.PL.Controllers
{
    public class MakaleController : BaseController
    {
        Repository<Article> repoM = new Repository<Article>(new BlogContext());
        Repository<Comment> repoC = new Repository<Comment>(new BlogContext());
        public ActionResult Index()
        {
            return View(repoM.GetAll(null,m=>m.OrderByDescending(x=>x.CreatedDate)).Take(4)); //tarihe göre sıralamalı ilk 4
        }
        [Authorize]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Ekle(MakaleAddViewModel model)
        {
            if(model.PictureUpload != null)
            {
                //string name = Path.GetFileNameWithoutExtension(model.PictureUpload.FileName);
                //string ext = Path.GetExtension(model.PictureUpload.FileName);
                ////Resim upload edilecek.
                //name = name.Replace(" ", "");
                string filename = model.PictureUpload.FileName;
                string imagePath = Server.MapPath("/images/" + filename);
                model.PictureUpload.SaveAs(imagePath);
                //Makale eklenecek.
                Article yeni = new Article();
                yeni.Title = model.Title;
                yeni.Summary = model.Summary;
                yeni.Content = model.Content;
                yeni.Picture = filename;
                yeni.CategoryId = model.CategoryId;
                //yeni.UserId = HttpContext.User.Identity.GetUserId();
                yeni.UserId = "55b5fccd-fbd5-4f3a-9fc0-b4b0714cc96d";
                if (repoM.Add(yeni))
                    return RedirectToAction("Index");
                return View(model);
            }


            return View();
        }
        public ActionResult MakaleDetay(int Id)
        {
            MakaleDetayViewModel mdvm = new MakaleDetayViewModel();
            mdvm.Makale = repoM.GetById(Id);
            mdvm.Yorum = new Comment();
            mdvm.Yorumlar = mdvm.Makale.Comments.ToList();
            return View(mdvm);
        }
        [HttpPost]
        public ActionResult MakaleDetay(MakaleDetayViewModel model)
        {
            Comment yeniYorum = new Comment();
            yeniYorum.ArticleId = model.Makale.Id;
            yeniYorum.Content = model.Yorum.Content;
            //yeniYorum.UserId = HttpContext.User.Identity.GetUserId(); //aktif kullanıcının id sini alırız cookielerden
            yeniYorum.UserId = "55b5fccd-fbd5-4f3a-9fc0-b4b0714cc96d";
            if (repoC.Add(yeniYorum))
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}