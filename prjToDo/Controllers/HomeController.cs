using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjToDo.Models;

namespace prjToDo.Controllers
{
    public class HomeController : Controller
    {
        dbToDoEntities db = new dbToDoEntities();
        //GET: HOME
        public ActionResult Index()
        {
            var todos = db.tToDo.OrderByDescending(m => m.fDate).ToList();
            return View(todos);
        }

        //CREATE
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string fTitle,string fImage,DateTime fDate)
        {
            //建立tToDo待辦資料型別todo物件
            tToDo todo = new tToDo();
            todo.fTitle = fTitle;
            todo.fImage = fImage;
            todo.fDate = fDate;
            db.tToDo.Add(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        //DELETE
        public ActionResult Delete(int id)
        {
            var todo = db.tToDo.Where(m => m.fId == id).FirstOrDefault();
            db.tToDo.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Edit
        //傳入修改的URL參數id編號時會執行此方法
        public ActionResult Edit(int id)
        {
            //找出欲修改的待辦事項物件todo,
            var todo = db.tToDo.Where(m => m.fId == id).FirstOrDefault();
            //再將要修改的待辦事項物件todo傳入Edit.cshtml的View檢視畫面
            return View(todo);
        }

        [HttpPost]
        public ActionResult Edit(int fId, string fTitle, string fImage, DateTime fDate)
        {
            //依fId取得要修改的待辦事項todo
            var todo = db.tToDo.Where(m => m.fId == fId).FirstOrDefault();
            todo.fTitle = fTitle;
            todo.fImage = fImage;
            todo.fDate = fDate;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}