using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSieuThiMini
{
    public class QuanLi
    {

        public void Login()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------ĐĂNG NHẬP--------------------");
            Console.ResetColor();
            Console.WriteLine("Đăng nhập với vai trò Quản lí");
            Console.WriteLine("Nhập tên đăng nhập (username): ");
            string username = Console.ReadLine();
            Console.WriteLine("Nhập mật khẩu (password): ");
            string password = Console.ReadLine();
            if (new DangNhap().CheckLogin(username, password, "Manager"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Đăng nhập thành công");
                System.Threading.Thread.Sleep(1000);
                QuanLiMenu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Đăng nhập thất bại! Sai mật khẩu hoặc tên đăng nhập. Vui lòng nhập lại!");
                System.Threading.Thread.Sleep(1000);
            }
        }

        public void QuanLiMenu()
        {
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("--------------------QUẢN LÍ--------------------");
                Console.ResetColor();
                Console.WriteLine("[1] Quản lí nhân viên");
                Console.WriteLine("[2] Quản lí hàng hoá cửa hàng");
                Console.WriteLine("[3] Quản lí hoá đơn");
                Console.WriteLine("[4] Quản lí nhà cung cấp");
                Console.WriteLine("[5] Quản lí tài khoản nhân viên");
                Console.WriteLine("[6] Đăng xuất");
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
                        QuanLiNhanVien();
                        break;
                    case '2':
                        QuanLiSanPham();
                        break;
                    case '3':
                        QuanLiHoaDon();
                        break;
                    case '4':
                        QuanLiNhaCungCap();
                        break;
                    case '5':
                        QuanLiTaiKhoan();
                        break;
                    case '6':
                        loop = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lựa chọn không hợp lệ, Vui lòng nhập lại!");
                        System.Threading.Thread.Sleep(1000);
                        break;
                }
            }
        }

        public void QuanLiNhanVien()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------QUẢN LÍ--------------------");
            Console.ResetColor();
            Console.WriteLine("[1] Thêm nhân viên mới");
            Console.WriteLine("[2] Xoá nhân viên");
            Console.WriteLine("[3] Chỉnh sửa thông tin nhân viên");
            Console.WriteLine("[4] Hiển thị danh sách nhân viên");
            Console.WriteLine("[5] Tìm kiếm nhân viên");
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
                    new QuanLiNhanVien().Nhap();
                    break;
                case '2':
                    Console.Clear();
                    new QuanLiNhanVien().Xoa();
                    break;
                case '3':
                    Console.Clear();
                    new QuanLiNhanVien().Sua();
                    break;
                case '4':
                    Console.Clear();
                    new QuanLiNhanVien().Xuat();
                    break;
                case '5':
                    Console.Clear();
                    new QuanLiNhanVien().TimKiem();
                    break;
                case '6':
                    Console.Clear();
                    QuanLiMenu();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lựa chọn không hợp lệ, Vui lòng nhập lại!");
                    System.Threading.Thread.Sleep(1000);
                    break;
            }
        }

        public void QuanLiSanPham()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------QUẢN LÍ--------------------");
            Console.ResetColor();
            Console.WriteLine("[1] Thêm sản phẩm mới");
            Console.WriteLine("[2] Xoá sản phẩm");
            Console.WriteLine("[3] Chỉnh sửa thông tin sản phẩm");
            Console.WriteLine("[4] Hiển thị danh sách sản phẩm");
            Console.WriteLine("[5] Tìm kiếm sản phẩm");
            Console.WriteLine("[6] Kiểm tra hạn sử dụng ");
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
                    QuanLiMenu();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lựa chọn không hợp lệ, Vui lòng nhập lại!!!");
                    break;
            }
        }

        public void QuanLiHoaDon()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------QUẢN LÍ--------------------");
            Console.ResetColor();
            Console.WriteLine("[1] Thêm hoá đơn mới");
            Console.WriteLine("[2] Xoá hoá đơn");
            Console.WriteLine("[3] Sửa thông tin hoá đơn");
            Console.WriteLine("[4] Hiển thị danh sách hoá đơn");
            Console.WriteLine("[5] Tìm kiếm hoá đơn");
            Console.WriteLine("[6] Thống kê doanh thu theo ngày");
            Console.WriteLine("[7] Thống kê doanh thu theo tháng");
            Console.WriteLine("[8] Quay lại");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------------");
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

                    new QuanLiHoaDon().ThongKeDoanhThuTheoNgay();
                    break;
                case '7':
                    Console.Clear();

                    new QuanLiHoaDon().ThongKeDoanhThuTheoThang();
                    break;
                case '8':
                    Console.Clear();
                    QuanLiMenu();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lựa chọn không hợp lệ, Vui lòng nhập lại!!!");
                    break;
            }
        }

        public void QuanLiNhaCungCap()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------QUẢN LÍ--------------------");
            Console.ResetColor();
            Console.WriteLine("[1] Thêm nhà cung cấp mới");
            Console.WriteLine("[2] Xoá nhà cung cấp");
            Console.WriteLine("[3] Chỉnh sửa thông tin nhà cung cấp");
            Console.WriteLine("[4] Hiển thị danh sách nhà cung cấp");
            Console.WriteLine("[5] Tìm kiểm nhà cung cấp");
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
                    new QuanLiNhaCungCap().Nhap();
                    break;
                case '2':
                    Console.Clear();
                    new QuanLiNhaCungCap().Xoa();
                    break;
                case '3':
                    Console.Clear();
                    new QuanLiNhaCungCap().Sua();
                    break;
                case '4':
                    Console.Clear();
                    new QuanLiNhaCungCap().Xuat();
                    break;
                case '5':
                    Console.Clear();
                    new QuanLiNhaCungCap().TimKiem();
                    break;
                case '6':
                    Console.Clear();
                    QuanLiMenu();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lựa chọn không hợp lệ, Vui lòng nhập lại!");
                    System.Threading.Thread.Sleep(1000);
                    break;
            }
        }

        public void QuanLiTaiKhoan()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------QUẢN LÍ--------------------");
            Console.ResetColor();
            Console.WriteLine("[1] Thêm tài khoản mới");
            Console.WriteLine("[2] Xoá tài khoản");
            Console.WriteLine("[3] Chỉnh sửa thông tin tài khoản");
            Console.WriteLine("[4] Hiển thị danh sách tài khoản");
            Console.WriteLine("[5] Tìm kiếm tài khoản");
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
                    new DangNhap().Nhap();
                    break;
                case '2':
                    Console.Clear();
                    new DangNhap().Xoa();
                    break;
                case '3':
                    Console.Clear();
                    new DangNhap().Sua();
                    break;
                case '4':
                    Console.Clear();
                    new DangNhap().Xuat();
                    break;
                case '5':
                    Console.Clear();
                    new DangNhap().TimKiem();
                    break;
                case '6':
                    Console.Clear();
                    QuanLiMenu();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lựa chọn không hợp lệ, Vui lòng nhập lại!");
                    System.Threading.Thread.Sleep(1000);
                    break;
            }
        }
    }
}
