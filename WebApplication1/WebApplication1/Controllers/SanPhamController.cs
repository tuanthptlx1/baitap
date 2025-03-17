using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SanPhamController : Controller
    {
        private th1Entities db = new th1Entities();

        // GET: SanPham/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: SanPham/Add
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

                db.SanPhams.Add(sp); // SỬA chỗ này
                db.SaveChanges();

                return RedirectToAction("Info", new { id = sp.ID });
            }

            return View(sp);
        }

        // GET: SanPham/Edit/5
        public ActionResult Edit(int id)
        {
            var sp = db.SanPhams.FirstOrDefault(x => x.ID == id); // SỬA chỗ này
            if (sp == null) return HttpNotFound();
            return View(sp);
        }

        // POST: SanPham/Edit
        [HttpPost]
        public ActionResult Edit(SanPham sp)
        {
            if (ModelState.IsValid)
            {
                var spOld = db.SanPhams.FirstOrDefault(x => x.ID == sp.ID); // SỬA chỗ này
                if (spOld != null)
                {
                    spOld.TenSanPham = sp.TenSanPham;
                    spOld.MoTa = sp.MoTa;
                    spOld.Gia = sp.Gia;
                    spOld.HinhAnh = sp.HinhAnh;
                    spOld.NgayCapNhat = DateTime.Now;

                    db.SaveChanges();
                }

                return RedirectToAction("Info", new { id = sp.ID });
            }

            return View(sp);
        }

        // GET: SanPham/Info/5
        // GET: SanPham/Info
        public ActionResult Info()
        {
            var danhSach = db.SanPhams.ToList();
            return View(danhSach);
        }

        public ActionResult Index()
        {
            var danhSach = db.SanPhams.ToList(); // Lấy toàn bộ sản phẩm từ DB
            return View(danhSach);              // Truyền qua view
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

            return RedirectToAction("Index"); // hoặc "Info" nếu bạn muốn quay lại trang thông tin
        }

    }
}
