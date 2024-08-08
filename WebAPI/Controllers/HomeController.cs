using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        KhachHang db = new KhachHang();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ActionResult ShowBook()
        {
            List<Sach> listBook = new List<Sach>();
            try
            {
                using (SqlConnection con = new SqlConnection("workstation id=WebBanSach.mssql.somee.com;packet size=4096;user id=HoangHai_SQLLogin_1;pwd=z9cjmba912;data source=WebBanSach.mssql.somee.com;persist security info=False;initial catalog=WebBanSach;TrustServerCertificate=True"))
                {
                    string sql = "SELECT * FROM Sach";
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var sach = new Sach
                        {
                            MaSach = Convert.ToInt32(row["MaSach"]),
                            TenSach = row["TenSach"].ToString(),
                            GiaBan = Convert.ToDecimal(row["GiaBan"]),
                            AnhBia = row["AnhBia"].ToString()
                        };
                        listBook.Add(sach);
                    }
                }
                if (listBook == null)
                {
                    return RedirectToAction("Error", "Home", new { message = "No books found" });
                }

                return View(listBook);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }

        public ActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }

        public ActionResult SearchBooks(string searchTerm)
        {
            List<Sach> listBook = new List<Sach>();
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    string conStr = "workstation id=WebBanSach.mssql.somee.com;packet size=4096;user id=HoangHai_SQLLogin_1;pwd=z9cjmba912;data source=WebBanSach.mssql.somee.com;persist security info=False;initial catalog=WebBanSach;TrustServerCertificate=True";
                    con.ConnectionString = conStr;
                    string sql = "SELECT * FROM Sach WHERE TenSach LIKE @searchTerm";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        var sach = new Sach
                        {
                            MaSach = Convert.ToInt32(row["MaSach"]),
                            TenSach = row["TenSach"].ToString(),
                            GiaBan = Convert.ToInt32(row["GiaBan"])
                        };
                        listBook.Add(sach);
                    }
                }
                return View("ShowBook", listBook);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }


        [HttpGet]
        public ActionResult Login()
        {
            // Trả về view Login từ thư mục Views/Log
            return View("~/Views/Log/Login.cshtml");
        }

        [HttpPost]
        public ActionResult Login(string taiKhoan, string matKhau)
        {
            try
            {
                if (taiKhoan == "admin" && matKhau == "123")
                {
                    // Đăng nhập thành công với tài khoản admin
                    Session["TaiKhoan"] = taiKhoan;
                    return RedirectToAction("Index", "Admin"); // Chuyển hướng đến trang Admin
                }

                using (SqlConnection con = new SqlConnection("workstation id=WebBanSach.mssql.somee.com;packet size=4096;user id=HoangHai_SQLLogin_1;pwd=z9cjmba912;data source=WebBanSach.mssql.somee.com;persist security info=False;initial catalog=WebBanSach;TrustServerCertificate=True"))
                {
                    string sql = "SELECT * FROM KhachHang WHERE TaiKhoan = @TaiKhoan AND MatKhau = @MatKhau";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Session["MaKH"] = reader["MaKH"];
                        Session["TaiKhoan"] = reader["TaiKhoan"];
                        Session["HoTen"] = reader["HoTen"];
                        return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không đúng.";
                        return View("~/Views/Log/Login.cshtml");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult Logout()
        {
            // Xóa thông tin người dùng khỏi Session
            Session["TaiKhoan"] = null;

            // Chuyển hướng về trang chủ
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            // Trả về view Register từ thư mục Views/Log
            return View("~/Views/Log/Register.cshtml");
        }

        [HttpPost]
        public ActionResult Register(string taiKhoan, string matKhau, string email, string hoTen)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("workstation id=WebBanSach.mssql.somee.com;packet size=4096;user id=HoangHai_SQLLogin_1;pwd=z9cjmba912;data source=WebBanSach.mssql.somee.com;persist security info=False;initial catalog=WebBanSach;TrustServerCertificate=True"))
                {
                    string sql = "INSERT INTO KhachHang (TaiKhoan, MatKhau, Email, HoTen) VALUES (@TaiKhoan, @MatKhau, @Email, @HoTen)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }

    }
}

