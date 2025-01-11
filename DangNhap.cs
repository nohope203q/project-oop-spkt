using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSieuThiMini
{
    public class User
    {
        private string taiKhoan;
        private string matKhau;
        private string quyen;

        public string TaiKhoan { get => taiKhoan; set => taiKhoan = value; }
        public string MatKhau { get => matKhau; set => matKhau = value; }
        public string Quyen { get => quyen; set => quyen = value; }
        public User()
        {
            taiKhoan = "";
            matKhau = "";
            quyen = "";
        }
        public User(string taiKhoan, string matKhau, string quyen)
        {
            this.taiKhoan = taiKhoan;
            this.matKhau = matKhau;
            this.quyen = quyen;
        }

    }
    public class DangNhap : IQLST
    {
        private List<User> listUser;
        private string filePath = "login.txt";
        public DangNhap()
        {
            listUser = new List<User>();
            DocFile();
        }

        public void AddUser(User user)
        {
            listUser.Add(user);
            GhiFile();
        }

        public void DocFile()
        {
            listUser = new List<User>();
            string[] lines = System.IO.File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] s = line.Split('#');
                User user = new User(s[0], s[1], s[2]);
                listUser.Add(user);
            }
        }
        
        public bool CheckLogin(string taiKhoan, string matKhau, string role)
        {
            foreach (User user in listUser)
            {
                if (user.TaiKhoan.Equals(taiKhoan) && user.MatKhau.Equals(matKhau) && user.Quyen.Equals(role))
                {
                    return true;
                }
            }
            return false;
        }

        public void GhiFile()
        {
            string data = "";
            foreach (User user in listUser)
            {
                data += user.TaiKhoan + "#" + user.MatKhau + "#" + user.Quyen + "\n";
            }
            System.IO.File.WriteAllText(filePath, data);
        }

        public void Xoa()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== XÓA TÀI KHOẢN ===");
            Console.ResetColor();

            Console.Write("Nhập tên tài khoản: ");
            string taiKhoan = Console.ReadLine();
            Console.Write("Nhập mật khẩu: ");
            string matKhau = Console.ReadLine();

            bool found = false; 
            for (int i = 0; i < listUser.Count; i++)
            {
                if (listUser[i].TaiKhoan.Equals(taiKhoan) && listUser[i].MatKhau.Equals(matKhau))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bạn có chắc muốn xóa tài khoản này? (Y/N): ");
                    Console.ResetColor();
                    string confirm = Console.ReadLine().Trim().ToUpper();
                    if (confirm == "Y")
                    {
                        listUser.RemoveAt(i);
                        GhiFile();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Xóa thành công!");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Hủy xóa tài khoản.");
                        Console.ResetColor();
                    }
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Không tìm thấy tài khoản hoặc mật khẩu không đúng!");
                Console.ResetColor();
            }

            Console.WriteLine("Nhấn Enter để thoát.");
            Console.ReadLine();
        }

        public void Sua()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== SỬA THÔNG TIN TÀI KHOẢN ===");
            Console.ResetColor();

            // Nhập tên tài khoản cần sửa
            Console.Write("Nhập tên tài khoản cần sửa (hoặc nhấn 'Q' để quay lại trang chủ): ");
            string taiKhoan = Console.ReadLine();

            // Nếu nhấn phím Q để quay lại
            if (taiKhoan.ToUpper() == "Q")
            {
                Console.WriteLine("Quay lại trang chủ...");
                return; // Quay lại trang chủ
            }

            // Tìm tài khoản trong danh sách
            bool found = false;
            for (int i = 0; i < listUser.Count; i++)
            {
                if (listUser[i].TaiKhoan.Equals(taiKhoan))
                {
                    found = true;

                    // Nhập mật khẩu cũ và kiểm tra (tối đa 3 lần)
                    int attempts = 0;
                    while (attempts < 3)
                    {
                        Console.Write("Nhập mật khẩu cũ (hoặc nhấn 'Q' để quay lại): ");
                        string matKhauCu = Console.ReadLine();

                        // Nếu nhấn Q để quay lại
                        if (matKhauCu.ToUpper() == "Q")
                        {
                            Console.WriteLine("Quay lại trang chủ...");
                            return; // Quay lại trang chủ
                        }

                        if (listUser[i].MatKhau.Equals(matKhauCu))
                        {
                            // Mật khẩu đúng, tiếp tục sửa
                            Console.Write("Nhập mật khẩu mới: ");
                            string matKhauMoi = Console.ReadLine();

                            Console.WriteLine("Chọn quyền cho tài khoản: ");
                            Console.WriteLine("1. Manager");
                            Console.WriteLine("2. Sales");
                            char chon = Console.ReadKey(true).KeyChar;
                            while (chon != '1' && chon != '2')
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Chọn không đúng, vui lòng chọn lại!");
                                Console.ResetColor();
                                chon = Console.ReadKey(true).KeyChar;
                            }

                            string quyen = chon == '1' ? "Manager" : "Sales";

                            // Xác nhận sửa
                            Console.WriteLine($"\nThông tin sửa đổi:");
                            Console.WriteLine($"- Tài khoản: {taiKhoan}");
                            Console.WriteLine($"- Mật khẩu mới: {matKhauMoi}");
                            Console.WriteLine($"- Quyền mới: {quyen}");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Bạn có chắc muốn sửa thông tin này? (Y/N): ");
                            Console.ResetColor();
                            string confirm = Console.ReadLine().Trim().ToUpper();

                            if (confirm == "Y")
                            {
                                // Cập nhật thông tin
                                listUser[i].MatKhau = matKhauMoi;
                                listUser[i].Quyen = quyen;
                                GhiFile();
                                Console.WriteLine("Sửa thành công!");
                            }
                            else
                            {
                                Console.WriteLine("Hủy thao tác sửa thông tin.");
                            }
                            return; // Kết thúc phương thức sau khi sửa thành công
                        }
                        else
                        {
                            attempts++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Mật khẩu cũ không đúng! Còn {3 - attempts} lần thử.");
                            Console.ResetColor();
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bạn đã nhập sai mật khẩu cũ 3 lần. Hủy thao tác sửa thông tin.");
                    Console.ResetColor();
                    Console.WriteLine("Nhấn Enter để thoát.");
                    Console.ReadLine();
                    return;
                }
            }

            if (!found)
            {
                Console.WriteLine("Không tìm thấy tài khoản!");
            }

            Console.WriteLine("Nhấn Enter để thoát.");
            Console.ReadLine();
        }


        public void Xuat()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== XUẤT THÔNG TIN TÀI KHOẢN NGƯỜI DÙNG ===");
            Console.ResetColor();
            Console.WriteLine("Danh sách người dùng: ");
            foreach (User user in listUser)
            {
                Console.WriteLine("Tài khoản: " + user.TaiKhoan + " Mật khẩu: " + user.MatKhau + " Quyền: " + user.Quyen);
            }
            Console.WriteLine("Nhấn Enter để thoát!");
            Console.ReadLine();
        }

        public void Nhap()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== THÊM TÀI KHOẢN NGƯỜI DÙNG ===");
            Console.ResetColor();

            string taiKhoan;
            while (true)
            {
                Console.Write("Nhập tên tài khoản (hoặc nhấn Enter để thoát): ");
                taiKhoan = Console.ReadLine();

                // Kiểm tra nếu người dùng muốn thoát
                if (string.IsNullOrWhiteSpace(taiKhoan))
                {
                    Console.WriteLine("Đã hủy thao tác thêm tài khoản.");
                    return;
                }

                // Kiểm tra tài khoản đã tồn tại
                if (listUser.Any(user => user.TaiKhoan.Equals(taiKhoan)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tài khoản đã tồn tại! Vui lòng nhập lại.");
                    Console.ResetColor();
                }
                else
                {
                    break;
                }
            }

            // Nhập mật khẩu
            Console.Write("Nhập mật khẩu: ");
            string matKhau = Console.ReadLine();

            // Chọn quyền cho tài khoản
            Console.WriteLine("Chọn quyền cho tài khoản:");
            Console.WriteLine("1. Manager");
            Console.WriteLine("2. Sales");

            char chon;
            do
            {
                Console.Write("Nhập lựa chọn (1/2): ");
                chon = Console.ReadKey(true).KeyChar;

                if (chon != '1' && chon != '2')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng chọn lại.");
                    Console.ResetColor();
                }
            } while (chon != '1' && chon != '2');

            string quyen = (chon == '1') ? "Manager" : "Sales";

            // Thêm người dùng mới
            listUser.Add(new User(taiKhoan, matKhau, quyen));
            GhiFile();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nThêm tài khoản thành công!");
            Console.ResetColor();
            Console.WriteLine("Nhấn Enter để thoát!");
            Console.ReadLine();
        }

        public void TimKiem()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=== TÌM KIẾM TÀI KHOẢN NGƯỜI DÙNG ===");
                Console.ResetColor();

                Console.Write("Nhập tên tài khoản cần tìm: ");
                string taiKhoan = Console.ReadLine();

                // Tìm tài khoản trong danh sách
                User foundUser = listUser.Find(user => user.TaiKhoan.Equals(taiKhoan));
                if (foundUser != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Tài khoản: {foundUser.TaiKhoan}");
                    Console.WriteLine($"Mật khẩu: {foundUser.MatKhau}");
                    Console.WriteLine($"Quyền: {foundUser.Quyen}");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Không tìm thấy tài khoản!");
                    Console.ResetColor();
                }

                // Hỏi người dùng có muốn tìm kiếm tiếp không
                Console.WriteLine("\nNhấn 'n' để tiếp tục tìm kiếm hoặc Enter để thoát.");
                char choice = Console.ReadKey(true).KeyChar;
                if (choice != 'n') break;
            }
            Console.WriteLine("\nThoát tìm kiếm. Nhấn Enter để quay lại.");
            Console.ReadLine();
        }


        public void DoiMatKhau()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== ĐỔI MẬT KHẨU ===");
            Console.ResetColor();

            // Nhập tên tài khoản
            Console.Write("Nhập tên tài khoản: ");
            string taiKhoan = Console.ReadLine();

            // Tìm tài khoản trong danh sách
            foreach (User user in listUser)
            {
                if (user.TaiKhoan.Equals(taiKhoan))
                {
                    int attempts = 0;

                    while (attempts < 3)
                    {
                        Console.Write("Nhập mật khẩu cũ (hoặc nhấn Q để quay lại): ");
                        string matKhauCu = Console.ReadLine();

                        // Nếu người dùng nhập "Q", thoát về trang chủ
                        if (matKhauCu.Equals("Q", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Quay lại trang chủ...");
                            return; // Quay lại trang chủ
                        }

                        if (user.MatKhau.Equals(matKhauCu))
                        {
                            // Mật khẩu cũ đúng, cho phép đổi mật khẩu
                            Console.Write("Nhập mật khẩu mới: ");
                            string matKhauMoi = Console.ReadLine();
                            user.MatKhau = matKhauMoi;
                            GhiFile();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Đổi mật khẩu thành công!");
                            Console.ResetColor();
                            Console.WriteLine("Nhấn Enter để thoát!");
                            Console.ReadLine();
                            return;
                        }
                        else
                        {
                            attempts++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Mật khẩu cũ không đúng! Còn {3 - attempts} lần thử.");
                            Console.ResetColor();
                        }
                    }

                    // Nếu vượt quá 3 lần nhập sai
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bạn đã nhập sai mật khẩu cũ 3 lần. Hủy thao tác đổi mật khẩu.");
                    Console.ResetColor();
                    Console.WriteLine("Nhấn Enter để thoát!");
                    Console.ReadLine();
                    return;
                }
            }

            // Nếu không tìm thấy tài khoản
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Không tìm thấy tài khoản!");
            Console.ResetColor();
            Console.WriteLine("Nhấn Enter để thoát!");
            Console.ReadLine();
        }

    }
}
