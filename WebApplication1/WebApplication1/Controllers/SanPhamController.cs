using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class SanPhamController : Controller
    {
        private th1Entities db = new th1Entities();



        public ActionResult Add()
        {
            ViewBag.IDDanhMuc = new SelectList(db.DanhMucs, "ID", "TenDanhMuc");
            return View();
        }

       
        [HttpPost]
        public ActionResult Add(SanPham sp, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fileUpload != null && fileUpload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Server.MapPath("~/Images/" + fileName);
                    fileUpload.SaveAs(path);
                    sp.HinhAnh = "/Images/" + fileName;
                }

                sp.NgayCapNhat = DateTime.Now;

                db.SanPhams.Add(sp);
                db.SaveChanges();

                return RedirectToAction("TrangChu");
            }

            ViewBag.IDDanhMuc = new SelectList(db.DanhMucs, "ID", "TenDanhMuc", sp.IDDanhMuc);
            return View(sp);
        }


       
        public ActionResult Edit(int id)
        {
            var sp = db.SanPhams.Find(id);
            if (sp == null)
                return HttpNotFound();

            ViewBag.DanhMucID = new SelectList(db.DanhMucs, "ID", "TenDanhMuc", sp.IDDanhMuc);
            return View(sp);
        }


        [HttpPost]
        public ActionResult Edit(SanPham sp, HttpPostedFileBase UploadHinhAnh)
        {
            if (ModelState.IsValid)
            {
                var spCu = db.SanPhams.Find(sp.ID);

                if (spCu == null)
                    return HttpNotFound();

                if (UploadHinhAnh != null && UploadHinhAnh.ContentLength > 0)
                {
                    string folderPath = Server.MapPath("~/Content/Images/");

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

       
                    string fileName = DateTime.Now.Ticks + "-" + Path.GetFileName(UploadHinhAnh.FileName);
                    string path = Path.Combine(folderPath, fileName);

        
                    UploadHinhAnh.SaveAs(path);

         
                    sp.HinhAnh = "/Content/Images/" + fileName;
                }
                else
                {
                   
                    sp.HinhAnh = spCu.HinhAnh;
                }

                sp.NgayCapNhat = DateTime.Now;

          
                db.Entry(spCu).CurrentValues.SetValues(sp);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.DanhMucID = new SelectList(db.DanhMucs, "ID", "TenDanhMuc", sp.IDDanhMuc);
            return View(sp);
        }






        public ActionResult Index()
        {
            var danhSach = db.SanPhams.ToList(); 
            return View(danhSach);            
        }

        public ActionResult Delete(int id)
        {
            var sp = db.SanPhams.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            db.SanPhams.Remove(sp);
            db.SaveChanges();

            return RedirectToAction("Index"); 
        }
        public ActionResult TrangChu()
        {
            var danhMucs = db.DanhMucs.ToList();
            var viewModel = danhMucs.Select(dm => new DanhMucSanPhamViewModel
            {
                TenDanhMuc = dm.TenDanhMuc,
                SanPhams = db.SanPhams.Where(sp => sp.IDDanhMuc == dm.ID).ToList()
            }).ToList();

            return View(viewModel);
        }
    }
}
