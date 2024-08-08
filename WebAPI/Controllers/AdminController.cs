using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AdminController : Controller
    {
        // Phương thức hiển thị danh sách sách
        public ActionResult Index()
        {
            List<Sach> listBook = new List<Sach>();
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
            return View(listBook); // Truyền danh sách sách vào view
        }

        private KhachHang db = new KhachHang();

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // GET: Admin/Create
        [HttpPost]
        public ActionResult Create(Sach sach, HttpPostedFileBase AnhBia)
        {
            try
            {
                if (AnhBia != null && AnhBia.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(AnhBia.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                    AnhBia.SaveAs(path);
                    sach.AnhBia = fileName;
                }

                using (SqlConnection con = new SqlConnection("workstation id=WebBanSach.mssql.somee.com;packet size=4096;user id=HoangHai_SQLLogin_1;pwd=z9cjmba912;data source=WebBanSach.mssql.somee.com;persist security info=False;initial catalog=WebBanSach;TrustServerCertificate=True"))
                {
                    string sql = "INSERT INTO Sach (TenSach, GiaBan, AnhBia) VALUES (@TenSach, @GiaBan, @AnhBia)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@TenSach", sach.TenSach);
                    cmd.Parameters.AddWithValue("@GiaBan", sach.GiaBan);
                    cmd.Parameters.AddWithValue("@AnhBia", sach.AnhBia);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(sach);
            }
        }




        // Phương thức chỉnh sửa sách
        [HttpPost]
        public ActionResult Edit(Sach sach, HttpPostedFileBase AnhBia)
        {
            try
            {
                if (AnhBia != null && AnhBia.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(AnhBia.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                    AnhBia.SaveAs(path);
                    sach.AnhBia = fileName;
                }

                using (SqlConnection con = new SqlConnection("workstation id=WebBanSach.mssql.somee.com;packet size=4096;user id=HoangHai_SQLLogin_1;pwd=z9cjmba912;data source=WebBanSach.mssql.somee.com;persist security info=False;initial catalog=WebBanSach;TrustServerCertificate=True"))
                {
                    string sql = "UPDATE Sach SET TenSach = @TenSach, GiaBan = @GiaBan, AnhBia = @AnhBia WHERE MaSach = @MaSach";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@MaSach", sach.MaSach);
                    cmd.Parameters.AddWithValue("@TenSach", sach.TenSach);
                    cmd.Parameters.AddWithValue("@GiaBan", sach.GiaBan);
                    cmd.Parameters.AddWithValue("@AnhBia", sach.AnhBia);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(sach);
            }
        }


        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            Sach sach = null;
            using (SqlConnection con = new SqlConnection("workstation id=WebBanSach.mssql.somee.com;packet size=4096;user id=HoangHai_SQLLogin_1;pwd=z9cjmba912;data source=WebBanSach.mssql.somee.com;persist security info=False;initial catalog=WebBanSach;TrustServerCertificate=True"))
            {
                string sql = "SELECT * FROM Sach WHERE MaSach = @MaSach";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@MaSach", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    sach = new Sach
                    {
                        MaSach = Convert.ToInt32(reader["MaSach"]),
                        TenSach = reader["TenSach"].ToString(),
                        GiaBan = Convert.ToDecimal(reader["GiaBan"]),
                        AnhBia = reader["AnhBia"].ToString()
                    };
                }
                con.Close();
            }
            return View(sach);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("workstation id=WebBanSach.mssql.somee.com;packet size=4096;user id=HoangHai_SQLLogin_1;pwd=z9cjmba912;data source=WebBanSach.mssql.somee.com;persist security info=False;initial catalog=WebBanSach;TrustServerCertificate=True"))
                {
                    string sql = "DELETE FROM Sach WHERE MaSach = @MaSach";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@MaSach", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() != "admin")
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "Home", action = "Login" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
