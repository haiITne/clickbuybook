﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebBanSachEntities : DbContext
    {
        public WebBanSachEntities()
            : base("name=WebBanSachEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ChiTietDonHang> ChiTietDonHang { get; set; }
        public DbSet<DonHang> DonHang { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<NhaXuatBan> NhaXuatBan { get; set; }
        public DbSet<Sach> Sach { get; set; }
        public DbSet<TacGia> TacGia { get; set; }
        public DbSet<ThamGia> ThamGia { get; set; }
        public DbSet<TheLoaiSach> TheLoaiSach { get; set; }
    }
}
