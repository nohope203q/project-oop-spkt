using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSieuThiMini
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool logLoop = true;
            while (logLoop)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("--------------------TRANG CHỦ--------------------");
                Console.ResetColor();
                Console.WriteLine("[1] Đăng nhập với vai trò Quản Lí");
                Console.WriteLine("[2] Đăng nhập với vai trò Nhân Viên Bán Hàng");
                Console.WriteLine("[3] Đổi mật khẩu!");
                Console.WriteLine("[4] Thoát");
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
                        QuanLi quanLi = new QuanLi();
                        quanLi.Login();
                        break;
                    case '2':
                        Console.Clear();
                        NhanVienBanHang nhanVienBanHang = new NhanVienBanHang();
                        nhanVienBanHang.Login();
                        break;
                    case '3':
                        Console.Clear();
                        DangNhap doiMatKhau = new DangNhap();
                        doiMatKhau.DoiMatKhau();
                        break;
                    case '4':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lựa chọn không hợp lệ vui lòng chọn lại!!!");
                        System.Threading.Thread.Sleep(1000);
                        break;
                }
            }
        }
    }
}
