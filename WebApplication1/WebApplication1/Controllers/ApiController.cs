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
        public JsonResult GetTatCaSanPham()
        {
            var danhMucs = db.DanhMucs
                .Select(dm => new
                {
                    ID = dm.ID,
                    TenDanhMuc = dm.TenDanhMuc,
                    SanPhams = dm.SanPhams.Select(sp => new
                    {
                        sp.ID,
                        sp.TenSanPham,
                        sp.MoTa,
                        sp.Gia,
                        sp.HinhAnh
                    }).ToList()
                }).ToList();

            return Json(danhMucs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}