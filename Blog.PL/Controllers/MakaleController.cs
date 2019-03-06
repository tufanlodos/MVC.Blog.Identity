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
        public ActionResult Index()
        {
            return View(repoM.GetAll());
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost,ValidateInput(false)] //validate input false demezsek ckdan gelen htmlleri geçirmiyor.
        public ActionResult Ekle(MakaleAddViewModel model)
        {
            if (model.PictureUpload != null)
            {

                /*
                 * string name = Path.GetFileNameWithoutExtension(model.PictureUpload.FileName);
                name = name.Replace(" ", ""); mesela içerdeki boşlukları yoket.
                string ext = Path.GetExtension(model.PictureUpload.FileName);
                bu ikiliyle alınanı işlemeye yarıyor System.IO kullanarak
                */

                //resim upload edilecek.
                string filename = model.PictureUpload.FileName; //gelen dosya adı ne ise o şekilde kayıt
                string imagepath = Server.MapPath("/images/" + filename);
                model.PictureUpload.SaveAs(imagepath);
                //makale eklenecek
                Article yeni = new Article();
                yeni.Title = model.Title;
                yeni.Summary = model.Summary;
                yeni.Content = model.Content;
                yeni.Picture = filename;
                yeni.CategoryId = model.CategoryId;
                //yeni.UserId = HttpContext.User.Identity.GetUserId() şu an aktif yok;
                yeni.UserId = "55b5fccd-fbd5-4f3a-9fc0-b4b0714cc96d";
                if (repoM.Add(yeni))
                    return RedirectToAction("Index");
                else
                    return View(model);
            }
            return View();
        }

        public ActionResult MakaleDetay(int Id)
        {
            //Id'ye göre makaleyi çağır, view'a gönder...
            return View();
        }
    }
}