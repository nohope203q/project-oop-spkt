using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSieuThiMini
{
    public class HoaDon{ 
        private string maHoaDon;
        private string maNhanVien;
        private DateTime ngayLap;
        private List<SanPham> dsSanPham;
        private List<int> dsSoLuong;
        private List<double> dsDonGia;
        private List<double> dsThanhTien;
        private const double VAT = 0.1;
        private double tienKhachDua;

        public string MaHoaDon { get => maHoaDon; set => maHoaDon = value; }
        public string MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public DateTime NgayLap { get => ngayLap; set => ngayLap = value; }
        public List<SanPham> DsSanPham { get => dsSanPham; set => dsSanPham = value; }
        public List<int> DsSoLuong { get => dsSoLuong; set => dsSoLuong = value; }
        public List<double> DsDonGia { get => dsDonGia; set => dsDonGia = value; }
        public List<double> DsThanhTien { get => dsThanhTien; set => dsThanhTien = value; }
        public double TienKhachDua { get => tienKhachDua; set => tienKhachDua = value; }
        
        public HoaDon()
        {
            maHoaDon = "";
            maNhanVien = "";
            ngayLap = DateTime.Now;
            dsSanPham = new List<SanPham>();
            dsSoLuong = new List<int>();
            dsDonGia = new List<double>();
            dsThanhTien = new List<double>();
            tienKhachDua = 0;
        }

        public HoaDon(string maHoaDon, string maNhanVien, DateTime ngayLap, List<SanPham> dsSanPham, List<int> dsSoLuong, List<double> dsDonGia, List<double> dsThanhTien, double tienKhachDua)
        {
            this.maHoaDon = maHoaDon;
            this.maNhanVien = maNhanVien;
            this.ngayLap = ngayLap;
            this.dsSanPham = dsSanPham;
            this.dsSoLuong = dsSoLuong;
            this.dsDonGia = dsDonGia;
            this.dsThanhTien = dsThanhTien;
            this.tienKhachDua = tienKhachDua;
        }

        public double TinhTongTien()
        {
            double tongTien = 0;
            for (int i = 0; i < dsSanPham.Count; i++)
            {
                tongTien += dsThanhTien[i];
            }
            return tongTien;
        }

        public double TinhTienVAT()
        {
            return TinhTongTien() * VAT;
        }

        public double TinhTienThua()
        {
            return tienKhachDua - (TinhTongTien() + TinhTienVAT());
        }
    }
}
