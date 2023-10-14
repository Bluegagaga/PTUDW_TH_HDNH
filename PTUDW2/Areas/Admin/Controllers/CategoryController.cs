using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Model;
using MyClasss.DAO;

namespace PTUDW2.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryDAO categoryDAO=new CategoryDAO();

        //INDEX
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(categoryDAO.getList("Index"));
        }


        ////DETAIL
        //// GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = categoryDAO.getRow(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }
        //CREATE
        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"),"Id","Name");
            ViewBag.ListOrder = new SelectList(categoryDAO.getList("Index"), "Order", "Name");
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Categories categories)
        {
            if (ModelState.IsValid)
            {
                //Xu li dong CreateAt
                categories.CreateAt = DateTime.Now;
                //Xu li dong UpdateAt
                categories.UpdateAt = DateTime.Now;
                //Xu li dong ParentId
                if(categories.ParentID == null)
                {
                    categories.ParentID = 0;
                }
                //Xu li dong Order
                if (categories.Order == null)
                {
                    categories.Order = 1;
                }
                else
                {
                    categories.Order += 1;
                }
                //Xu li dong Slug (tai file XString tren github)
                //categories.Slug = XString.Str_Slug(categories.Name);

                //them date cho DB
                categoryDAO.Insert(categories);
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoryDAO.getList("Index"), "Order", "Name");
            return View();
        }

        ////EDIT
        //// GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = categoryDAO.getRow(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                categoryDAO.Update(categories);
                return RedirectToAction("Index");
            }
            return View(categories);
        }
        //DELETE
        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = categoryDAO.getRow(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = categoryDAO.getRow(id);
            categoryDAO.Delete(categories);
            
            return RedirectToAction("Index");
        }


        //STATUS
        public ActionResult Status(int? id)
        {
            if (id==null)
            {
                //thong bao that bai
                TempData["message"] = ("Cap nhat trang thai that bai");
                return RedirectToAction("Index");
            }
            //tim row co id == id cua loai san pham
            Categories categories = categoryDAO.getRow(id);
            //KT trang thai cua status neu hien tai la 1 thi chuyen thanh 2 va nguoc lai 2->1
            categories.Status = (categories.Status == 1) ? 2 : 1;
            //cap nhat UpdateAt
            categories.UpdateAt = DateTime.Now;
            //cap nhat DB
            categoryDAO.Update(categories);
            //thong bao thanh cong 
            TempData["message"] = ("Cap nhat trang thai thanh cong");
            //tra ket qua ve trang index
            return RedirectToAction("Index");
        }
    }
}
