using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;
using System.Data.SqlClient;
using System;

namespace WebAPI.Controllers
{
    public class CartController : Controller
    {
        // Lấy giỏ hàng từ session
        private List<Sach> GetCart()
        {
            var cart = Session["Cart"] as List<Sach>;
            if (cart == null)
            {
                cart = new List<Sach>();
                Session["Cart"] = cart;
            }
            return cart;
        }

        // Thêm sách vào giỏ hàng
        public ActionResult AddToCart(int id)
        {
            var cart = GetCart();
            using (SqlConnection con = new SqlConnection("workstation id=WebBanSach.mssql.somee.com;packet size=4096;user id=HoangHai_SQLLogin_1;pwd=z9cjmba912;data source=WebBanSach.mssql.somee.com;persist security info=False;initial catalog=WebBanSach;TrustServerCertificate=True"))
            {
                string sql = "SELECT * FROM Sach WHERE MaSach = @MaSach";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@MaSach", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var sach = new Sach
                    {
                        MaSach = Convert.ToInt32(reader["MaSach"]),
                        TenSach = reader["TenSach"].ToString(),
                        GiaBan = Convert.ToDecimal(reader["GiaBan"]),
                        AnhBia = reader["AnhBia"].ToString()
                    };

                    if (!cart.Any(x => x.MaSach == sach.MaSach))
                    {
                        cart.Add(sach);
                    }
                }
                con.Close();
            }

            return RedirectToAction("Index");
        }

        // Hiển thị giỏ hàng
        public ActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // Xóa sản phẩm khỏi giỏ hàng
        [HttpPost]
        public ActionResult RemoveFromCart(int MaSach)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.MaSach == MaSach);
            if (item != null)
            {
                cart.Remove(item);
            }

            return RedirectToAction("Index");
        }
    }
}
