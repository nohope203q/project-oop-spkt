using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSieuThiMini
{
    public class SanPham
    {
        private string maSanPham;
        private string tenSanPham;
        private string donViTinh;
        private int soLuong;
        private double giaBan;
        private double giaNhap;
        private string loaiSanPham;
        private string nhaSanXuat;
        private DateTime ngayNhap;
        private string hanSuDung;
        private DateTime ngaySanXuat;

        public string MaSanPham { get => maSanPham; set => maSanPham = value; }
        public string TenSanPham { get => tenSanPham; set => tenSanPham = value; }
        public string DonViTinh { get => donViTinh; set => donViTinh = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public double GiaBan { get => giaBan; set => giaBan = value; }
        public double GiaNhap { get => giaNhap; set => giaNhap = value; }
        public string LoaiSanPham { get => loaiSanPham; set => loaiSanPham = value; }
        public string NhaSanXuat { get => nhaSanXuat; set => nhaSanXuat = value; }
        public DateTime NgayNhap { get => ngayNhap; set => ngayNhap = value; }
        public string HanSuDung { get => hanSuDung; set => hanSuDung = value; }
        public DateTime NgaySanXuat { get => ngaySanXuat; set => ngaySanXuat = value; }

        public SanPham()
        {
            maSanPham = "";
            tenSanPham = "";
            donViTinh = "";
            soLuong = 0;
            giaBan = 0;
            giaNhap = 0;
            loaiSanPham = "";
            nhaSanXuat = "";
            ngayNhap = DateTime.Now;
            hanSuDung = "";
            ngaySanXuat = DateTime.Now;
        }

        public SanPham(string maSanPham, string tenSanPham, string donViTinh, int soLuong, double giaBan, double giaNhap, string loaiSanPham, string nhaSanXuat, DateTime ngayNhap, string hanSuDung, DateTime ngaySanXuat)
        {
            this.maSanPham = maSanPham;
            this.tenSanPham = tenSanPham;
            this.donViTinh = donViTinh;
            this.soLuong = soLuong;
            this.giaBan = giaBan;
            this.giaNhap = giaNhap;
            this.loaiSanPham = loaiSanPham;
            this.nhaSanXuat = nhaSanXuat;
            this.ngayNhap = ngayNhap;
            this.hanSuDung = hanSuDung;
            this.ngaySanXuat = ngaySanXuat;
        }

        public override string ToString()
        {
            return string.Format("{0,-10} {1,-20} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10} {7,-10} {8,-10} {9,-10} {10,-10}", maSanPham, tenSanPham, donViTinh, soLuong, giaBan, giaNhap, loaiSanPham, nhaSanXuat, ngayNhap, hanSuDung, ngaySanXuat);
        }
    }
}
