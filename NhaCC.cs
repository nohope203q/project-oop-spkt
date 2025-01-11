using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSieuThiMini
{
    public class NhaCC
    {
        private string tenNhaCC;
        private string diaChi;
        private string soDienThoai;
        private string email;
        private string tenNguoiDaiDien;
        private string chucVu;

        public string TenNhaCC { get => tenNhaCC; set => tenNhaCC = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public string Email { get => email; set => email = value; }
        public string TenNguoiDaiDien { get => tenNguoiDaiDien; set => tenNguoiDaiDien = value; }
        public string ChucVu { get => chucVu; set => chucVu = value; }

        public NhaCC()
        {
            tenNhaCC = "";
            diaChi = "";
            soDienThoai = "";
            email = "";
            tenNguoiDaiDien = "";
            chucVu = "";
        }

        public NhaCC(string tenNhaCC, string diaChi, string soDienThoai, string email, string tenNguoiDaiDien, string chucVu)
        {
            this.tenNhaCC = tenNhaCC;
            this.diaChi = diaChi;
            this.soDienThoai = soDienThoai;
            this.email = email;
            this.tenNguoiDaiDien = tenNguoiDaiDien;
            this.chucVu = chucVu;
        }


    }
}
