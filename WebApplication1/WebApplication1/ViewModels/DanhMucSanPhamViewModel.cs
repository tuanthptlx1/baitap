using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;


namespace WebApplication1.ViewModels
{
    public class DanhMucSanPhamViewModel
    {
        public string TenDanhMuc { get; set; }
        public List<SanPham> SanPhams { get; set; }
    }
}