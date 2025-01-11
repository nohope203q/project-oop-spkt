using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSieuThiMini
{
    public class QuanLiNhaCungCap: NhaCC, IQLST
    {
        List<NhaCC> listNhaCC = new List<NhaCC>();
        private string fileName = "nhacungcap.txt";

        public QuanLiNhaCungCap()
        {
            listNhaCC = new List<NhaCC>();
            DocFile();
        }
        public void Nhap()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------NHẬP THÔNG TIN NHÀ CUNG CẤP--------------------");
            Console.ResetColor();
            NhaCC ncc = new NhaCC();
            Console.WriteLine("Nhập tên nhà cung cấp: ");
            ncc.TenNhaCC = Console.ReadLine();
            Console.WriteLine("Nhập địa chỉ: ");
            ncc.DiaChi = Console.ReadLine();
            Console.WriteLine("Nhập số điện thoại: ");
            ncc.SoDienThoai = Console.ReadLine();
            Console.WriteLine("Nhập email: ");
            ncc.Email = Console.ReadLine();
            Console.WriteLine("Nhập tên người đại diện: ");
            ncc.TenNguoiDaiDien = Console.ReadLine();
            Console.WriteLine("NHập chức vụ: ");
            ncc.ChucVu = Console.ReadLine();

            listNhaCC.Add(ncc);
            GhiFile();
            Console.WriteLine("Bạn có muốn nhập tiếp không? (Y/N)");
            string choice = Console.ReadLine();
            if (choice == "Y" || choice == "y")
            {
                Nhap();
            }
            else
            {
                return;
            }
            Console.ReadLine();


        }

        public void Xuat()
        {
            int pageSize = 10;  // Số nhân viên sẽ in ra mỗi lần
            int pageNumber = 0; // Trang hiện tại

            while (pageNumber * pageSize < listNhaCC.Count)
            {
                Console.Clear(); 
                int startIndex = pageNumber * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, listNhaCC.Count);

                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{"Tên nhà cung cấp",-20}{"Địa chỉ",-20}{"Số điện thoại",-20}{"Email",-20}{"Tên người đại diện",-20}{"Chức vụ",-20}");
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
                for (int i = startIndex; i < endIndex; i++)
                {
                    Console.WriteLine($"{listNhaCC[i].TenNhaCC,-20}{listNhaCC[i].DiaChi,-20}{listNhaCC[i].SoDienThoai,-20}{listNhaCC[i].Email,-20}{listNhaCC[i].TenNguoiDaiDien,-20}{listNhaCC[i].ChucVu,-20}");

                }
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"Trang {pageNumber + 1}/{(int)Math.Ceiling((double)listNhaCC.Count / pageSize)}");
                Console.WriteLine("Bấm phím mũi tên trái/phải để chuyển trang");
                Console.WriteLine("Bấm phím khác để thoát");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    pageNumber = Math.Max(0, pageNumber - 1);
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    pageNumber = Math.Min((int)Math.Ceiling((double)listNhaCC.Count / pageSize) - 1, pageNumber + 1);
                }
                else
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
            Console.WriteLine("--------------------SỬA THÔNG TIN NHÀ CUNG CẤP--------------------");
            Console.ResetColor();
            //tim ten nha cung cap
            Console.WriteLine("Nhập tên nhà cung cấp cần sửa: ");
            string tenNCC = Console.ReadLine();
            NhaCC ncc = listNhaCC.Find(x => x.TenNhaCC == tenNCC);
            if (ncc == null)
            {
                Console.WriteLine("Không tìm thấy nhà cung cấp");
                return;
            }
            Console.WriteLine("Nhập địa chỉ: ");
            ncc.DiaChi = Console.ReadLine();
            Console.WriteLine("Nhập số điện thoại: ");
            ncc.SoDienThoai = Console.ReadLine();
            Console.WriteLine("Nhập email: ");
            ncc.Email = Console.ReadLine();
            Console.WriteLine("Nhập tên người đại diện: ");
            ncc.TenNguoiDaiDien = Console.ReadLine();
            Console.WriteLine("Nhập chức vụ: ");
            ncc.ChucVu = Console.ReadLine();
            GhiFile();
        }

        public void Xoa()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------XÓA THÔNG TIN NHÀ CUNG CẤP--------------------");
            Console.ResetColor();
            Console.WriteLine(
                "Nhập tên nhà cung cấp cần xoá: ");
            string tenNCC = Console.ReadLine();
            NhaCC ncc = listNhaCC.Find(x => x.TenNhaCC == tenNCC);
            if (ncc == null)
            {
                Console.WriteLine("Không tìm thấy nhà cung cấp!");
                return;
            }
            Console.WriteLine("Bạn có chắc muốn xoá nhà cung cấp này? (Y/N)");
            string choice = Console.ReadLine();
            if (choice == "Y" || choice == "y")
            {
                Console.WriteLine("Đã xoá thành công!");
                listNhaCC.Remove(ncc);
                GhiFile();
            }
            else
            {
                return;
            }

            Console.ReadLine();
        }

        public void TimKiem()
        {
            Console.WriteLine("Nhập tên nhà cung cấp cần tìm: ");
            string tenNCC = Console.ReadLine();
            NhaCC ncc = listNhaCC.Find(x => x.TenNhaCC == tenNCC);
            if (ncc == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Không tìm thấy!");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Tên nhà cung cấp",-20}{"Địa chỉ",-20}{"Số điện thoại",-20}{"Email",-20}{"Tên người đại diện",-20}{"Chức vụ",-20}");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{ncc.TenNhaCC,-20}{ncc.DiaChi,-20}{ncc.SoDienThoai,-20}{ncc.Email,-20}{ncc.TenNguoiDaiDien,-20}{ncc.ChucVu,-20}");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(
                "Bấm phím bất kì để thoát!");
            Console.ReadLine();
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
                        if (data.Length != 6)
                        {
                            Console.WriteLine($"Dòng dữ liệu không hợp lệ, không đủ trường: {line}");
                            continue;
                        }

                        NhaCC ncc = new NhaCC();
                        ncc.TenNhaCC = data[0];
                        ncc.DiaChi = data[1];
                        ncc.SoDienThoai = data[2];
                        ncc.Email = data[3];
                        ncc.TenNguoiDaiDien = data[4];
                        ncc.ChucVu = data[5];

                        listNhaCC.Add(ncc);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Dòng dữ liệu không hợp lệ: {line}");
                    }
                }

            }
            else
            {
                Console.WriteLine("File không tồn tại.");
            }
        }

        public void GhiFile()
        {
            string data = "";
            foreach (NhaCC ncc in listNhaCC)
            {
                data += ncc.TenNhaCC + "#" + ncc.DiaChi + "#" + ncc.SoDienThoai + "#" + ncc.Email + "#" + ncc.TenNguoiDaiDien + "#" + ncc.ChucVu + "\n";
            }
            System.IO.File.WriteAllText(fileName, data);
        }
    }
}
