using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using MyClass.Model;
using MyClasss.DAO;
using UDW.Library;

namespace PTUDW2.Areas.Admin.Controllers
{
    public class SupplierController : Controller
    {
        SupplierDAO suppliersDAO = new SupplierDAO();

        // GET: Admin/Supplier
        public ActionResult Index()
        {
            //Hien thi cac mau tin trang Index(status=1,2 | DAO=Index
            return View(suppliersDAO.getList("Index"));
        }

        // GET: Admin/Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("success", "Khong tim thay mau tin");
            }
            Suppliers suppliers = suppliersDAO.getRow(id);
            if (suppliers == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("success", "Khong tim thay mau tin");
            }
            return View(suppliers);
        }

        // GET: Admin/Supplier/Create
        public ActionResult Create()
        {
            ViewBag.ListOrder = new SelectList(suppliersDAO.getList("Index"), "Order", "Name");
            return View();
        }

        // POST: Admin/Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                //XU LI TU DONG

                //Xu ly tu dong: CreateAt
                suppliers.CreateAt = DateTime.Now;
                //Xu ly tu dong: UpdateAt
                suppliers.UpdateAt = DateTime.Now;
                //Xu ly tu dong: Order
                if (suppliers.Order == null)
                {
                    suppliers.Order = 1;
                }
                else
                {
                    suppliers.Order += 1;
                }
                //Xu ly tu dong: Slug
                suppliers.Slug = XString.Str_Slug(suppliers.Name);

                //xu ly cho phan upload hình ảnh
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = suppliers.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + "-" + suppliers.Id + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        suppliers.Image = imgName;
                        //upload hinh
                        string PathDir = "~/Public/img/supplier/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh

                //Them mau tin vao db
                suppliersDAO.Insert(suppliers);
                //Thong bao thanh cong
                TempData["message"] = new XMessage("success", "Them mau tin thanh cong");

            }
            ViewBag.ListOrder = new SelectList(suppliersDAO.getList("Index"), "Order", "Name");
            return View(suppliers);
        }

        // GET: Admin/Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("success", "Khong tim thay mau tin");
            }
            Suppliers suppliers = suppliersDAO.getRow(id);
            if (suppliers == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("success", "Khong tim thay mau tin");
            }
            return View(suppliers);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Img,Slug,Order,FullName,Phone,Email,UrlSite,MeteDesc,MetaKey,CreateBy,CreateAt,UpdateBy,UpdateAt,Status")] Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                //XU LI TU DONG

                //Xu ly tu dong: CreateAt
                suppliers.CreateAt = DateTime.Now;
                //Xu ly tu dong: UpdateAt
                suppliers.UpdateAt = DateTime.Now;
                //Xu ly tu dong: Order
                if (suppliers.Order == null)
                {
                    suppliers.Order = 1;
                }
                else
                {
                    suppliers.Order += 1;
                }
                //Xu ly tu dong: Slug
                suppliers.Slug = XString.Str_Slug(suppliers.Name);

                //truoc khi up anh moi thi xoa anh cu
                var img = Request.Files["img"];//lay thong tin file
                string PathDir = "~/Public/img/supplier/";
                if (suppliers.Image != null && img.ContentLength != 0)//ton tai logo NCC tu truoc
                {
                    //xoa anh cu
                    string DelPath = Path.Combine(Server.MapPath(PathDir), suppliers.Image);
                    System.IO.File.Delete(DelPath);

                }

                //up anh moi cho NCC
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = suppliers.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + "-" + suppliers.Id + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        suppliers.Image = imgName;
                        //upload hinh
                        string PathDir = "~/Public/img/supplier/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh

                //cap nhat mau tin vao db
                suppliersDAO.Update(suppliers);
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(suppliersDAO.getList("Index"), "Order", "Name");
            return View(suppliers);
        }

        // GET: Admin/Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("success", "Khong tim thay mau tin");
            }
            Suppliers suppliers = suppliersDAO.getRow(id);
            if (suppliers == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("success", "Khong tim thay mau tin");
            }
            return View(suppliers);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Suppliers suppliers = suppliersDAO.getRow(id);
            //xoa anh
            string DelPath = Path.Combine(Server.MapPath(PathDir), suppliers.Image);
            System.IO.File.Delete(DelPath);
            //Thong bao thanh cong
            TempData["message"] = new XMessage("success", "Xoa mau tin thanh cong");
            return RedirectToAction("Index");
        }

        ///////////////////////////////////////////////////////////////////
        /// STATUS
        // GET: Admin/Supplier/Status/5
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }

            //tim row co id == id cua loai SP can thay doi Status
            Suppliers Suppliers = suppliersDAO.getRow(id);
            if (Suppliers == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //kiem tra trang thai cua status, neu hien tai la 1 ->2 va nguoc lai
            Suppliers.Status = (Suppliers.Status == 1) ? 2 : 1;
            //cap nhat gia tri cho UpdateAt
            Suppliers.UpdateAt = DateTime.Now;
            //cap nhat lai DB
            suppliersDAO.Update(Suppliers);
            //thong bao thanh cong
            TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");
            //tra ket qua ve Index
            return RedirectToAction("Index");
        }

        ///////////////////////////////////////////////////////////////////
        /// MoveTrash
        // GET: Admin/Supplier/MoveTrash/5
        public ActionResult MoveTrash(int? id)
        {
            if (id == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }

            //tim row co id == id cua loai SP can thay doi Status
            Suppliers Suppliers = suppliersDAO.getRow(id);
            if (Suppliers == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //trang thai cua status = 0
            Suppliers.Status = 0;
            //cap nhat gia tri cho UpdateAt
            Suppliers.UpdateAt = DateTime.Now;

            //cap nhat lai DB
            suppliersDAO.Update(Suppliers);
            //thong bao thanh cong
            TempData["message"] = new XMessage("success", "Mẩu tin được chuyển vào thùng rác");
            //tra ket qua ve Index
            return RedirectToAction("Index");
        }

        ///////////////////////////////////////////////////////////////////
        /// TRASH
        // GET: Admin/Supplier/Trash
        public ActionResult Trash()
        {
            return View(suppliersDAO.getList("Trash"));//chi hien thi cac dong co status 0
        }

        ///////////////////////////////////////////////////////////////////
        /// Recover
        // GET: Admin/Supplier/Recover/5
        public ActionResult Recover(int? id)
        {
            if (id == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //tim row co id == id cua loai SP can thay doi Status
            Suppliers Suppliers = suppliersDAO.getRow(id);
            if (Suppliers == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //trang thai cua status = 2
            Suppliers.Status = 2;//truoc recover=0
            //cap nhat gia tri cho UpdateAt
            Suppliers.UpdateAt = DateTime.Now;

            //cap nhat lai DB
            suppliersDAO.Update(Suppliers);
            //thong bao thanh cong
            TempData["message"] = new XMessage("success", "Phục hồi mẩu tin thành công");
            //tra ket qua ve Index
            return RedirectToAction("Trash");//action trong SupplierDAO
        }
    }
}
