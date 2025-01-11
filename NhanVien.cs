using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSieuThiMini
{
    
    public class NhanVien
    {
        private string maNV;
        private string tenNV;
        private string gioiTinh;
        private DateTime ngaySinh;
        private string soDienThoai;
        private string email;
        private string diaChi;
        private string chucVu;
        private double luong;
        public string MaNV { get => maNV; set => maNV = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public string Email { get => email; set => email = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string ChucVu { get => chucVu; set => chucVu = value; }
        public double Luong { get => luong; set => luong = value; }


        public NhanVien()
        {
            maNV = "";
            tenNV = "";
            gioiTinh = "";
            ngaySinh = DateTime.Now;
            soDienThoai = "";
            email = "";
            diaChi = "";
            chucVu = "";
            luong = 0;
        }

        public NhanVien(string maNV, string tenNV, string gioiTinh, DateTime ngaySinh, string soDienThoai, string email, string diaChi, string chucVu, double luong)
        {
            this.maNV = maNV;
            this.tenNV = tenNV;
            this.gioiTinh = gioiTinh;
            this.ngaySinh = ngaySinh;
            this.soDienThoai = soDienThoai;
            this.email = email;
            this.diaChi = diaChi;
            this.chucVu = chucVu;
            this.luong = luong;
        }

        public override string ToString()
        {
            string luongVND = Luong.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
            return $"Mã nhân viên: {maNV,-15} Tên: {tenNV,-25}\nGiới tính: {gioiTinh,-15}\nNgày sinh: {ngaySinh.ToString("dd/MM/yyyy"),-15}\nSđt: {soDienThoai,-15}" +
                $"\nEmail: {email,-30}\nĐịa chỉ: {diaChi,-35}\nChức vụ: {chucVu,-25}Lương: {luongVND,-15}";
        }

        







    }
}
