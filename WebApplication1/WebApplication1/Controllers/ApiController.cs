using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class ApiController : Controller
    {
        // GET: Api
        private th1Entities db = new th1Entities();

        // Trả về danh sách sản phẩm theo ID danh mục (dạng JSON)
        public JsonResult GetSanPhamTheoDanhMuc(int danhMucId)
        {
            var sp = db.SanPhams
                      .Where(x => x.IDDanhMuc == danhMucId)
                      .Select(x => new
                      {
                          x.ID,
                          x.TenSanPham,
                          x.MoTa,
                          x.Gia,
                          x.HinhAnh
                      })
                      .ToList();

            return Json(sp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDanhMucs()
        {
            var danhMucs = db.DanhMucs
                .Select(d => new { d.ID, d.TenDanhMuc })
                .ToList();

            return Json(danhMucs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}