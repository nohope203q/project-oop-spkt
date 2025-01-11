using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSieuThiMini
{
    public class NhanVienBanHang
    {
        public void Login()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------ĐĂNG NHẬP--------------------");
            Console.ResetColor();
            Console.WriteLine("Đăng nhập với vai trò là Nhân viên bán hàng");
            Console.WriteLine("Nhập tên đăng nhập(username): ");
            string username = Console.ReadLine();
            Console.WriteLine("Nhập mật khẩu(password): ");
            string password = Console.ReadLine();
            if (new DangNhap().CheckLogin(username, password, "Sales"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Đăng nhập thành công!");
                Console.ResetColor();
                System.Threading.Thread.Sleep(1000);
                NhanVienBanHangMenu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Đăng nhập thất bại!");
                Console.ResetColor();
                System.Threading.Thread.Sleep(1000);
            }
        }

        public void NhanVienBanHangMenu()
        {
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("--------------------NHÂN VIÊN BÁN HÀNG--------------------");
                Console.ResetColor();
                Console.WriteLine("[1] Quản lí hàng hoá cửa hàng");
                Console.WriteLine("[2] Quản lí hoá đơn");
                Console.WriteLine("[3] Đăng xuất");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-------------------------------------------------");
                Console.ResetColor();
                Console.Write("Lựa chọn của bạn: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[Nhập số tương ứng]");
                Console.ResetColor();
                char userInput = Console.ReadKey(true).KeyChar;
                
                switch (userInput)
                {
                    case '1':
                        QuanLiSanPham();
                        break;
                    case '2':
                        QuanLiHoaDon();
                        break;
                    case '3':
                        loop = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lựa chọn không hợp lệ, Vui lòng nhập lại!!!");
                        Console.ResetColor();
                        break;
                }
            }
        }

        public void QuanLiSanPham()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------NHÂN VIÊN BÁN HÀNG--------------------");
            Console.ResetColor();
            Console.WriteLine("[1] Thêm sản phẩm mới");
            Console.WriteLine("[2] Xoá sản phẩm");
            Console.WriteLine("[3] Chỉnh sửa thông tin sản phẩm");
            Console.WriteLine("[4] Hiển thị danh sách sản phẩm");
            Console.WriteLine("[5] Tìm kiếm sản phẩm");
            Console.WriteLine("[6] Kiểm tra hạn sử dụng");
            Console.WriteLine("[7] Quay lại");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------------");
            Console.ResetColor();
            Console.Write("Lựa chọn của bạn: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Nhập số tương ứng]");
            Console.ResetColor();
            char userInput = Console.ReadKey(true).KeyChar;
            switch ((char)userInput)
            {
                case '1':
                    Console.Clear();
                    new QuanLiSanPham().Nhap();
                    break;
                case '2':
                    Console.Clear();
                    new QuanLiSanPham().Xoa();
                    break;
                case '3':
                    Console.Clear();
                    new QuanLiSanPham().Sua();
                    break;
                case '4':
                    Console.Clear();
                    new QuanLiSanPham().Xuat();
                    break;
                case '5':
                    Console.Clear();
                    new QuanLiSanPham().TimKiem();
                    break;
                case '6':
                    Console.Clear();
                    new QuanLiSanPham().KiemTraHanSuDung();
                    break;
                case '7':
                    Console.Clear();
                    NhanVienBanHangMenu();
                    break;
                default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Lựa chọn không hợp lệ, Vui lòng nhập lại!!!");
                Console.ResetColor();
                    break;
            }
        }

        public void QuanLiHoaDon()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------NHÂN VIÊN BÁN HÀNG--------------------");
            Console.ResetColor();
            Console.WriteLine("[1] Thêm hoá đơn mới");
            Console.WriteLine("[2] Xoá hoá đơn");
            Console.WriteLine("[3] Sửa thông tin hoá đơn");
            Console.WriteLine("[4] Hiển thị danh sách hoá đơn");
            Console.WriteLine("[5] Tìm kiếm hoá đơn");
            Console.WriteLine("[6] Quay lại");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------------");
            Console.ResetColor();
            Console.Write("Lựa chọn của bạn: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Nhập số tương ứng]");
            Console.ResetColor();
            char userInput = Console.ReadKey(true).KeyChar;
            switch (userInput)
            {
                case '1':
                    Console.Clear();
                    new QuanLiHoaDon().Nhap();
                    break;
                case '2':
                    Console.Clear();
                    new QuanLiHoaDon().Xoa();
                    break;
                case '3':
                    Console.Clear();
                    new QuanLiHoaDon().Sua();
                    break;
                case '4':
                    Console.Clear();
                    new QuanLiHoaDon().Xuat();
                    break;
                case '5':
                    Console.Clear();
                    new QuanLiHoaDon().TimKiem();
                    break;
                case '6':
                    Console.Clear();
                    NhanVienBanHangMenu();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lựa chọn không hợp lệ, Vui lòng nhập lại!!!");
                    break;
            }
        }
    }

}
