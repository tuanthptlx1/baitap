//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        public int ID { get; set; }
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }
        public Nullable<decimal> Gia { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public Nullable<int> IDDanhMuc { get; set; }
    
        public virtual DanhMuc DanhMuc { get; set; }
    }
}
