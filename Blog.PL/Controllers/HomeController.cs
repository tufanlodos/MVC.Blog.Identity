using Blog.BLL.Repository;
using Blog.DAL.Context;
using Blog.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.PL.Controllers
{
    public class HomeController : BaseController
    {
        Repository<Article> repoM = new Repository<Article>(new BlogContext());
        Repository<Tag> repoT = new Repository<Tag>(new BlogContext());
        Repository<Category> repoC = new Repository<Category>(new BlogContext());
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SonEklenenMakaleler()
        {

            return PartialView(repoM.GetAll());
        }
        public ActionResult MakalelerByKategori(int Id)
        {
            return View(repoC.Get(k => k.Id == Id).Articles);
        }
        public ActionResult MakalelerByEtiket(int etiketId)
        {
            return View(repoT.Get(e => e.Id == etiketId).Articles);
        }
    }
}