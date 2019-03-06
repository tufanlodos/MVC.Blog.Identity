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
    public class MakaleController : Controller
    {
        Repository<Article> repoM = new Repository<Article>(new BlogContext());
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MakaleDetay(int Id)
        {
            //Id'ye göre makaleyi çağır, view'a gönder...
            return View();
        }
    }
}