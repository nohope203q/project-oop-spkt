using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace QuanLiSieuThiMini
{
    public class QuanLiNhanVien: NhanVien, IQLST
    {
        List<NhanVien> listNhanVien = new List<NhanVien>();
        private string fileName = "nhanvien.txt";
        private List<string> recycledIDs = new List<string>();
        private string recycledFileName = "recycled.txt";
        public QuanLiNhanVien()
        {
            recycledIDs = new List<string>();
            listNhanVien = new List<NhanVien>();
            DocFile();
        }


    public void Nhap()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("------------------NHẬP NHÂN VIÊN------------------");
        Console.ResetColor();
        // Tự động tạo mã nhân viên
        string fisrtMaNV = "NV001";
        string MaNV = fisrtMaNV;
        if (System.IO.File.Exists(recycledFileName))
        {
            recycledIDs = System.IO.File.ReadAllLines(recycledFileName).ToList();
        }
        if (recycledIDs.Count > 0)
        {
            // Lấy mã nhân viên từ danh sách tái sử dụng
            MaNV = recycledIDs[0];
            Console.WriteLine($"Mã nhân viên tự động: {MaNV}");
            recycledIDs.RemoveAt(0); // Xóa mã này khỏi danh sách tái sử dụng
        }
        else
        {
            if (listNhanVien.Count == 0)
            {
                Console.WriteLine($"Mã nhân viên tự động: {fisrtMaNV}");
            }
            else
            {
                Console.WriteLine($"Mã nhân viên tự động: NV{listNhanVien.Count + 1:D3}");
                MaNV = $"NV{listNhanVien.Count + 1:D3}";
            }
        }

        // Nhập tên nhân viên và kiểm tra
        string TenNV;
        do
        {
            Console.WriteLine("Nhập tên nhân viên (hoặc nhấn 'Q' để huỷ): ");
            TenNV = Console.ReadLine();
            if (TenNV.ToLower() == "q") return;  // Nếu người dùng nhấn 'Q', huỷ nhập
            if (string.IsNullOrWhiteSpace(TenNV))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tên không được để trống, vui lòng nhập lại.");
                    Console.ResetColor();
                }
            else
            {
                break;
            }
        } while (true);

        // Chọn giới tính
        string GioiTinh;
        do
        {
            Console.WriteLine("Chọn giới tính (1: Nam, 2: Nữ, hoặc nhấn 'Q' để huỷ): ");
            string gtChoice = Console.ReadLine();
            if (gtChoice.ToLower() == "q") return;  // Nếu người dùng nhấn 'Q', huỷ nhập

            if (gtChoice == "1")
            {
                GioiTinh = "Nam";
                break;
            }
            else if (gtChoice == "2")
            {
                GioiTinh = "Nữ";
                break;
            }
            else
            {
                Console.WriteLine("Lựa chọn không hợp lệ, vui lòng thử lại.");
            }
        } while (true);

        // Nhập ngày sinh
        DateTime NgaySinh;
        do
        {
            Console.WriteLine("Nhập ngày sinh (dd/MM/yyyy) hoặc nhấn 'Q' để huỷ: ");
            string ngaySinhInput = Console.ReadLine();
            if (ngaySinhInput.ToLower() == "q") return;  // Nếu người dùng nhấn 'Q', huỷ nhập

            if (DateTime.TryParseExact(ngaySinhInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out NgaySinh))
            {
                break;
            }
            else
            {
                Console.WriteLine("Định dạng ngày không hợp lệ, vui lòng nhập lại.");
            }
        } while (true);

        // Kiểm tra số điện thoại hợp lệ
        string SoDienThoai;
        do
        {
            Console.WriteLine("Nhập số điện thoại (hoặc nhấn 'Q' để huỷ): ");
            SoDienThoai = Console.ReadLine();
            if (SoDienThoai.ToLower() == "q") return;  // Nếu người dùng nhấn 'Q', huỷ nhập

            if (Regex.IsMatch(SoDienThoai, @"^\d{10}$")) // Kiểm tra số điện thoại có đúng 10 chữ số
            {
                break;
            }
            else
            {
                Console.WriteLine("Số điện thoại không hợp lệ, vui lòng nhập lại.");
            }
        } while (true);

        // Kiểm tra email hợp lệ
        string Email;
        do
        {
            Console.WriteLine("Nhập email (hoặc nhấn 'Q' để huỷ): ");
            Email = Console.ReadLine();
            if (Email.ToLower() == "q") return;  // Nếu người dùng nhấn 'Q', huỷ nhập

            if (Regex.IsMatch(Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")) // Kiểm tra định dạng email
            {
                break;
            }
            else
            {
                Console.WriteLine("Email không hợp lệ, vui lòng nhập lại.");
            }
        } while (true);

        // Nhập địa chỉ
        Console.WriteLine("Nhập địa chỉ (hoặc nhấn 'Q' để huỷ): ");
        string DiaChi = Console.ReadLine();
        if (DiaChi.ToLower() == "q") return;  // Nếu người dùng nhấn 'Q', huỷ nhập

        // Chọn chức vụ
        string ChucVu;
        do
        {
            Console.WriteLine("Chọn chức vụ (1: Quản lý, 2: Nhân viên bán hàng, hoặc nhấn 'Q' để huỷ): ");
            string cvChoice = Console.ReadLine();
            if (cvChoice.ToLower() == "q") return;  // Nếu người dùng nhấn 'Q', huỷ nhập

            if (cvChoice == "1")
            {
                ChucVu = "Quản lý";
                break;
            }
            else if (cvChoice == "2")
            {
                ChucVu = "Nhân viên bán hàng";
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Lựa chọn không hợp lệ, vui lòng thử lại.");
                Console.ResetColor();
            }
        } while (true);

        // Nhập lương
        double Luong;
        do
        {
            Console.WriteLine("Nhập lương (hoặc nhấn 'Q' để huỷ): ");
            string luongInput = Console.ReadLine();
            if (luongInput.ToLower() == "q") return;  // Nếu người dùng nhấn 'Q', huỷ nhập

            if (double.TryParse(luongInput, out Luong))
            {
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Lương không hợp lệ, vui lòng nhập lại.");
                Console.ResetColor();
            }
        } while (true);

        // Thêm nhân viên vào danh sách
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Nhập thành công.");
        Console.ResetColor();
        GhiFileRecycle();
        NhanVien nv = new NhanVien(MaNV, TenNV, GioiTinh, NgaySinh, SoDienThoai, Email, DiaChi, ChucVu, Luong);
        listNhanVien.Add(nv);
        GhiFile();

        // Lựa chọn nhập tiếp hay dừng lại
        Console.WriteLine("Bạn có muốn nhập tiếp không? (Y/N): ");
        string choice = Console.ReadLine();
        if (choice.ToLower() == "y")
        {
            Nhap(); // Nhập lại nếu muốn thêm nhân viên khác
        }
        else
        {
            return; // Quay lại menu chính
        }
    }



        public void Xuat()
        {
            // Tiêu đề bảng
            int pageSize = 10;  // Số nhân viên sẽ in ra mỗi lần
            int pageNumber = 0; // Trang hiện tại

            while (pageNumber * pageSize < listNhanVien.Count)
            {
                Console.Clear(); // Xóa màn hình mỗi khi in trang mới
                int startIndex = pageNumber * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, listNhanVien.Count);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{"Mã Nhân Viên",-15}{"Họ Tên",-25}{"Giới Tính",-15}{"Ngày Sinh",-15}{"Số Điện Thoại",-15}{"Email",-28}{"Địa Chỉ",-25}{"Chức Vụ",-25}{"Lương",-15}");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.ResetColor();
                // Dữ liệu bảng
                for (int i = startIndex; i < endIndex; i++)
                {
                    var nv = listNhanVien[i];
                    // Cắt địa chỉ nếu quá dài
                    string shortDiaChi = nv.DiaChi.Length > 20 ? nv.DiaChi.Substring(0, 20) + "..." : nv.DiaChi;
                    string shortEmail = nv.Email.Length > 25 ? nv.Email.Substring(0, 25) + "..." : nv.Email;
                    string luongVND = nv.Luong.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));

                    Console.WriteLine($"{nv.MaNV,-15}{nv.TenNV,-25}{nv.GioiTinh,-15}{nv.NgaySinh.ToShortDateString(),-15}{nv.SoDienThoai,-15}{shortEmail,-28}{shortDiaChi,-25}{nv.ChucVu,-25}{luongVND,-15}");
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                        
                Console.WriteLine("Trang " + (pageNumber + 1) + " / " + (int)Math.Ceiling((double)listNhanVien.Count / pageSize));
                Console.ResetColor();
                Console.WriteLine("Nhấn mũi tên trái để quay lại trang trước, mũi tên phải để tới trang tiếp");
                Console.WriteLine("Nhấn phím Enter để quay lại menu chính.");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    pageNumber = pageNumber > 0 ? pageNumber - 1 : 0;
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    pageNumber = pageNumber + 1 < (int)Math.Ceiling((double)listNhanVien.Count / pageSize) ? pageNumber + 1 : pageNumber;
                }
                else if(key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            Console.ReadLine();
        }

        public void Sua()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------CHỈNH SỬA NHÂN VIÊN------------------");
            Console.ResetColor();
            Console.Write("Nhập mã nhân viên cần chỉnh sửa: ");
            string maNV = Console.ReadLine();
            maNV = maNV.ToUpper();
            var nhanVien = listNhanVien.FirstOrDefault(nv => nv.MaNV == maNV);

            if (nhanVien != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[Nhấn Enter để giữ nguyên giá trị cũ]");
                Console.ResetColor();
                // Hàm phụ để chọn từ danh sách
                string SelectFromOptions(string prompt, List<string> options, string currentValue)
                {
                    Console.WriteLine($"{prompt} (hiện tại: {currentValue})");
                    for (int i = 0; i < options.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {options[i]}");
                    }
                    Console.Write("Lựa chọn: ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int choice) && choice >= 1 && choice <= options.Count)
                    {
                        return options[choice - 1];
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lựa chọn không hợp lệ. Giữ nguyên giá trị cũ.");
                        Console.ResetColor();
                        return currentValue;
                    }
                }

                // Hàm phụ nhập dữ liệu với giá trị mặc định
                string InputData(string prompt, string currentValue)
                {
                    Console.Write($"{prompt} (hiện tại: {currentValue}): ");
                    string input = Console.ReadLine();
                    return string.IsNullOrEmpty(input) ? currentValue : input;
                }

                DateTime InputDate(string prompt, DateTime currentValue)
                {
                    Console.Write($"{prompt} (dd/MM/yyyy, hiện tại: {currentValue:dd/MM/yyyy}): ");
                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input)) return currentValue;

                    if (DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime newDate))
                    {
                        return newDate;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ngày sinh không hợp lệ. Giữ nguyên giá trị cũ.");
                        Console.ResetColor();
                        return currentValue;
                    }
                }

                double InputDouble(string prompt, double currentValue)
                {
                    Console.Write($"{prompt} (hiện tại: {currentValue}): ");
                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input)) return currentValue;

                    if (double.TryParse(input, out double newValue))
                    {
                        return newValue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Giá trị không hợp lệ. Giữ nguyên giá trị cũ.");
                        Console.ResetColor();
                        return currentValue;
                    }
                }

                // Danh sách các lựa chọn
                List<string> genders = new List<string> { "Nam", "Nữ" };
                List<string> roles = new List<string> {"Nhân viên bán hàng", "Quản lý" };

                nhanVien.TenNV = InputData("Nhập tên mới", nhanVien.TenNV);
                nhanVien.GioiTinh = SelectFromOptions("Chọn giới tính mới", genders, nhanVien.GioiTinh);
                nhanVien.NgaySinh = InputDate("Nhập ngày sinh mới", nhanVien.NgaySinh);
                nhanVien.SoDienThoai = InputData("Nhập số điện thoại mới", nhanVien.SoDienThoai);
                nhanVien.Email = InputData("Nhập email mới", nhanVien.Email);
                nhanVien.DiaChi = InputData("Nhập địa chỉ mới", nhanVien.DiaChi);
                nhanVien.ChucVu = SelectFromOptions("Chọn chức vụ mới", roles, nhanVien.ChucVu);
                nhanVien.Luong = InputDouble("Nhập lương mới", nhanVien.Luong);

                GhiFile();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Thông tin nhân viên đã được cập nhật.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nhân viên không tồn tại.");
                Console.ResetColor();
            }

            Console.WriteLine("Bạn có muốn cập nhật thông tin nhân viên khác không? (Y/N)");
            char userInput = Console.ReadKey(true).KeyChar;
            if (userInput == 'Y' || userInput == 'y')
            {
                Sua();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Kết thúc cập nhật.");
                Console.ResetColor();
            }
        }


        public void Xoa()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------XÓA NHÂN VIÊN------------------");
            Console.ResetColor();

            while (true)
            {
                Console.Write("Nhập mã nhân viên cần xóa: ");
                string maNV = Console.ReadLine();
                maNV = maNV.ToUpper();

                var nhanVien = listNhanVien.FirstOrDefault(nv => nv.MaNV == maNV);

                if (nhanVien != null)
                {
                    // Hỏi xác nhận trước khi xóa nhân viên
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Bạn có chắc chắn muốn xóa nhân viên với mã {maNV}? (Y/N)");
                    Console.ResetColor();
                    char confirmInput = Console.ReadKey(true).KeyChar;

                    if (confirmInput == 'Y' || confirmInput == 'y')
                    {
                        // Thêm mã nhân viên vào danh sách tái sử dụng và xóa nhân viên
                        recycledIDs.Add(maNV);
                        listNhanVien.Remove(nhanVien);

                        GhiFile();
                        GhiFileRecycle();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Nhân viên đã được xóa.");
                        Console.ResetColor();
                    }
                    else if (confirmInput == 'N' || confirmInput == 'n')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Hủy bỏ hành động xóa.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lựa chọn không hợp lệ, vui lòng nhập lại.");
                        Console.ResetColor();
                        continue;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nhân viên không tồn tại.");
                    Console.ResetColor();
                }

                // Hỏi người dùng có muốn tiếp tục xóa nhân viên khác không
                Console.WriteLine("Bạn có muốn xóa nhân viên khác không? (Y/N)");
                char userInput = Console.ReadKey(true).KeyChar;

                if (userInput == 'Y' || userInput == 'y')
                {
                    continue;  // Tiếp tục xóa nếu người dùng chọn 'Y'
                }
                else if (userInput == 'N' || userInput == 'n')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Kết thúc xóa.");
                    Console.ResetColor();
                    break;  // Kết thúc vòng lặp và trở về menu chính
                }
                else
                {
                    // Nếu người dùng nhập không hợp lệ, yêu cầu nhập lại
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lựa chọn không hợp lệ, vui lòng nhập lại.");
                    Console.ResetColor();
                }
            }
        }



        public void TimKiem()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----------------TÌM KIẾM NHÂN VIÊN----------------");
            Console.ResetColor();
            Console.Write("Nhập mã nhân viên cần tìm: ");
            string maNV = Console.ReadLine();
            maNV = maNV.ToUpper();
            var nhanVien = listNhanVien.FirstOrDefault(nv => nv.MaNV == maNV);

            if (nhanVien != null)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine(nhanVien);
                Console.WriteLine("----------------------------------");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nhân viên không tồn tại.");
                Console.ResetColor();
            }
            Console.WriteLine("Bạn có muốn tìm kiếm nhân viên khác không? (Y/N)");
            char userInput = Console.ReadKey(true).KeyChar;
            if (userInput == 'Y' || userInput == 'y')
            {
                TimKiem();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Kết thúc tìm kiếm.");
                Console.ResetColor();
                return;
            }
        }

        public void DocFile()
        {
            if (System.IO.File.Exists(fileName))
            {
                string[] lines = System.IO.File.ReadAllLines(fileName);

                if (lines.Length == 0)
                {
                    Console.WriteLine("File tồn tại nhưng không có dữ liệu.");
                    return;
                }

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue; // Bỏ qua dòng trống hoặc chỉ chứa khoảng trắng

                    try
                    {
                        string[] data = line.Split(new char[] { '#' });

                        // Kiểm tra nếu số lượng trường không đủ
                        if (data.Length != 9)
                        {
                            Console.WriteLine($"Dòng dữ liệu không hợp lệ, không đủ trường: {line}");
                            continue;
                        }

                        // Tạo đối tượng NhanVien
                        NhanVien nv = new NhanVien(
                            data[0],
                            data[1],
                            data[2],
                            DateTime.Parse(data[3]),
                            data[4],
                            data[5],
                            data[6],
                            data[7],
                            double.Parse(data[8])
                        );

                        // Thêm vào danh sách
                        listNhanVien.Add(nv);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi đọc dòng dữ liệu: {line}. Chi tiết lỗi: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("File không tồn tại. Vui lòng kiểm tra lại.");
            }
        }


        public void GhiFile()
        {
            string data = "";
            foreach (NhanVien nv in listNhanVien)
            {
                data += nv.MaNV + "#" + nv.TenNV + "#" + nv.GioiTinh + "#" + nv.NgaySinh.ToShortDateString() + "#" + nv.SoDienThoai + "#" + nv.Email + "#" + nv.DiaChi + "#" + nv.ChucVu + "#" + nv.Luong + "\n";
            }
            System.IO.File.WriteAllText(fileName, data);

        }

        public void GhiFileRecycle()
        {
            try
            {
                System.IO.File.WriteAllLines(recycledFileName, recycledIDs);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi ghi file recycled: " + ex.Message);
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
