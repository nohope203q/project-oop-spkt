using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSieuThiMini
{
    internal class QuanLiSanPham : SanPham, IQLST
    {
        private List<SanPham> listSanPham = new List<SanPham>();
        private string fileName = "sanpham.txt";

        public QuanLiSanPham()
        {
            listSanPham = new List<SanPham>();
            DocFile();

        }
        public void Nhap()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------NHẬP SẢN PHẨM--------------------");
            Console.ResetColor();
            SanPham sp = new SanPham();
            Random random = new Random();
            sp.MaSanPham = "sp" + random.Next(1, 10000);
            while (listSanPham.Any(nv => nv.MaSanPham == sp.MaSanPham))
            {
                sp.MaSanPham = "sp" + random.Next(1, 1000);
            }

            Console.WriteLine("Mã sản phẩm tạo tự động: " + sp.MaSanPham);

            Console.WriteLine("Nhập tên sản phẩm: ");
            sp.TenSanPham = Console.ReadLine();
            Console.WriteLine("Nhập đơn vị sản phẩm: ");
            sp.DonViTinh = Console.ReadLine();
            Console.WriteLine("Nhập số lượng sản phẩm: ");
            sp.SoLuong = int.Parse(Console.ReadLine());
            Console.WriteLine("Nhập giá bán: ");
            sp.GiaBan = double.Parse(Console.ReadLine());
            Console.WriteLine("Nhập giá nhập hàng: ");
            sp.GiaNhap = double.Parse(Console.ReadLine());
            Console.WriteLine("Nhập loại sản phẩm: ");
            sp.LoaiSanPham = Console.ReadLine();
            Console.WriteLine("Nhập nhà sản xuất: ");
            sp.NhaSanXuat = Console.ReadLine();
            Console.WriteLine("Nhập ngày nhập hàng: ");
            sp.NgayNhap = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Nhập hạn sử dụng: ");
            sp.HanSuDung = Console.ReadLine();
            Console.WriteLine("Nhập ngày sản xuất: ");
            sp.NgaySanXuat = DateTime.Parse(Console.ReadLine());
            listSanPham.Add(sp);
            GhiFile();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Đã thêm sản phẩm: {TenSanPham} - Số lượng: {SoLuong} - Giá bán: {GiaBan} - Giá nhập: {GiaNhap} - Loại sản phẩm: {LoaiSanPham} - Nhà sản xuất: {NhaSanXuat} - Ngày nhập: {NgayNhap} - Hạn sử dụng: {HanSuDung} - Ngày sản xuất: {NgaySanXuat}");
            Console.ResetColor();

            Console.WriteLine("Bạn có muốn nhập tiếp không? (Y/N)");
            string choice = Console.ReadLine();
            if (choice == "Y" || choice == "y")
            {
                Nhap();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Kết thúc nhập!");
                Console.ResetColor();
                return;
            }
            Console.ReadLine();
        }

        public void Xuat()
        {
            int pageSize = 10;  // Số nhân viên sẽ in ra mỗi lần
            int pageNumber = 0; // Trang hiện tại

            while (pageNumber * pageSize < listSanPham.Count)
            {
                Console.Clear();
                Console.Clear(); // Xóa màn hình mỗi khi in trang mới
                int startIndex = pageNumber * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, listSanPham.Count);
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{"Mã sản phẩm",-15}{"Tên sản phẩm",-20}{"Đơn vị tính",-15}{"Số lượng",-12}{"Giá bán", -12}{"Giá nhập", -15}{"Loại sản phẩm", -15}{"Nhà sản xuất", -25}{"Ngày nhập", -15}" +
                    $"{"Hạn sử dụng", -20}{"Ngày sản xuất", -20}");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                for(int i = startIndex; i < endIndex; i++) {
                    var sp = listSanPham[i];
                    Console.WriteLine($"{sp.MaSanPham,-15}{sp.TenSanPham,-20}{sp.DonViTinh,-15}{sp.SoLuong,-12}{sp.GiaBan,-12}{sp.GiaNhap,-15}{sp.LoaiSanPham,-15}{sp.NhaSanXuat,-25}{sp.NgayNhap.ToString("dd/MM/yyyy"),-15}" +
                        $"{sp.HanSuDung,-20}{sp.NgaySanXuat.ToString("dd/MM/yyyy"),-20}");
                }
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Trang " + (pageNumber + 1) + " / " + (int)Math.Ceiling((double)listSanPham.Count / pageSize));
                Console.WriteLine("Nhấn phím mũi tên trái để xem trang trước, mũi tên phải để xem trang sau");
                Console.WriteLine("Nhấn phím Enter để quay lại menu chính.");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    pageNumber = pageNumber > 0 ? pageNumber - 1 : 0;
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    pageNumber = pageNumber + 1 < (int)Math.Ceiling((double)listSanPham.Count / pageSize) ? pageNumber + 1 : pageNumber;
                }
                else if(key.Key == ConsoleKey.Enter) {
                    break;
                }
            }
            Console.ReadLine();
        }

        public void Sua()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------SỬA SẢN PHẨM--------------------");
            Console.ResetColor();
            Console.Write("Nhập mã sản phẩm cần sửa: ");
            string maSanPham = Console.ReadLine();
            maSanPham = maSanPham.ToLower();
            var sp = listSanPham.FirstOrDefault(nv => nv.MaSanPham == maSanPham);

            if (sp != null)
            {
                Console.WriteLine("Nhập tên mới cho sản phẩm (hiện tại: {0}): ", sp.TenSanPham);
                string tenMoi = Console.ReadLine();
                if (!string.IsNullOrEmpty(tenMoi)) sp.TenSanPham = tenMoi;  // Cập nhật tên sản phẩm

                Console.WriteLine("Nhập đơn vị tính mới cho sản phẩm (hiện tại: {0}): ", sp.DonViTinh);
                string donViTinhMoi = Console.ReadLine();
                if (!string.IsNullOrEmpty(donViTinhMoi)) sp.DonViTinh = donViTinhMoi;  // Cập nhật đơn vị tính

                Console.WriteLine("Nhập số lượng mới cho sản phẩm (hiện tại: {0}): ", sp.SoLuong);
                string soLuongNhap = Console.ReadLine();
                if (!string.IsNullOrEmpty(soLuongNhap)) sp.SoLuong = int.Parse(soLuongNhap);  // Cập nhật số lượng

                Console.WriteLine("Nhập giá bán mới cho sản phẩm (hiện tại: {0}): ", sp.GiaBan);
                string giaBanNhap = Console.ReadLine();
                if (!string.IsNullOrEmpty(giaBanNhap)) sp.GiaBan = double.Parse(giaBanNhap);  // Cập nhật giá bán

                Console.WriteLine("Nhập giá nhập mới cho sản phẩm (hiện tại: {0}): ", sp.GiaNhap);
                string giaNhapNhap = Console.ReadLine();
                if (!string.IsNullOrEmpty(giaNhapNhap)) sp.GiaNhap = double.Parse(giaNhapNhap);  // Cập nhật giá nhập

                Console.WriteLine("Nhập loại sản phẩm mới cho sản phẩm (hiện tại: {0}): ", sp.LoaiSanPham);
                string loaiSanPhamMoi = Console.ReadLine();
                if (!string.IsNullOrEmpty(loaiSanPhamMoi)) sp.LoaiSanPham = loaiSanPhamMoi;  // Cập nhật loại sản phẩm

                Console.WriteLine("Nhập nhà sản xuất mới cho sản phẩm (hiện tại: {0}): ", sp.NhaSanXuat);
                string nhaSanXuatMoi = Console.ReadLine();
                if (!string.IsNullOrEmpty(nhaSanXuatMoi)) sp.NhaSanXuat = nhaSanXuatMoi;  // Cập nhật nhà sản xuất

                Console.WriteLine("Nhập ngày nhập mới cho sản phẩm (hiện tại: {0}): ", sp.NgayNhap);
                string ngayNhapNhap = Console.ReadLine();
                if (!string.IsNullOrEmpty(ngayNhapNhap)) sp.NgayNhap = DateTime.Parse(ngayNhapNhap);  // Cập nhật ngày nhập

                Console.WriteLine("Nhập hạn sử dụng mới cho sản phẩm (hiện tại: {0}): ", sp.HanSuDung);
                string hanSuDungMoi = Console.ReadLine();
                if (!string.IsNullOrEmpty(hanSuDungMoi)) sp.HanSuDung = hanSuDungMoi;  // Cập nhật hạn sử dụng

                Console.WriteLine("Nhập ngày sản xuất mới cho sản phẩm (hiện tại: {0}): ", sp.NgaySanXuat);
                string ngaySanXuatNhap = Console.ReadLine();
                if (!string.IsNullOrEmpty(ngaySanXuatNhap)) sp.NgaySanXuat = DateTime.Parse(ngaySanXuatNhap);  // Cập nhật ngày sản xuất

                GhiFile();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Sửa thông tin sản phẩm thành công!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Không tìm thấy sản phẩm cần sửa!");
                Console.ResetColor();
            }
            Console.WriteLine("Nhấn phím Enter để quay lại menu chính.");
            Console.ReadLine();
        }


        public void Xoa()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------XÓA SẢN PHẨM--------------------");
            Console.Write("Nhập mã sản phẩm cần xóa: ");
            string maSanPham = Console.ReadLine();
            maSanPham = maSanPham.ToLower();
            var sp = listSanPham.FirstOrDefault(nv => nv.MaSanPham.ToString() == maSanPham);

            if (sp != null)
            {
                listSanPham.Remove(sp);
                GhiFile();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Xóa sản phẩm thành công!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Không tìm thấy sản phẩm cần xóa!");
                Console.ResetColor();
            }
            Console.WriteLine("Nhấn phím Enter để quay lại menu chính.");
            Console.ReadLine();
        }

        public void TimKiem()
        {
            Console.WriteLine("Nhập mã sản phảm cần tìm: ");
            string maSanPham = Console.ReadLine();
            maSanPham = maSanPham.ToLower();
            var sp = listSanPham.FirstOrDefault(nv => nv.MaSanPham.ToString() == maSanPham);
            if (sp != null)
            {
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{"Mã sản phẩm",-15}{"Tên sản phẩm",-20}{"Đơn vị tính",-15}{"Số lượng",-12}{"Giá bán",-12}{"Giá nhập",-15}{"Loại sản phẩm",-15}{"Nhà sản xuất",-25}{"Ngày nhập",-15}" +
                    $"{"Hạn sử dụng",-20}{"Ngày sản xuất",-20}");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{sp.MaSanPham,-15}{sp.TenSanPham,-20}{sp.DonViTinh,-15}{sp.SoLuong,-12}{sp.GiaBan,-12}{sp.GiaNhap,-15}{sp.LoaiSanPham,-15}{sp.NhaSanXuat,-25}{sp.NgayNhap.ToString("dd/MM/yyyy"),-15}" +
                    $"{sp.HanSuDung,-20}{sp.NgaySanXuat.ToString("dd/MM/yyyy"),-20}");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Không tìm thấy sản phẩm cần tìm!");
                Console.ResetColor();
            }
            Console.WriteLine("Nhấn phím Enter để quay lại menu chính.");
            Console.ReadLine();

        }

        //kiem tra san pham sap het han su dung
        public void KiemTraHanSuDung()
        {

            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Mã sản phẩm",-15}{"Tên sản phẩm",-20}{"Ngày nhập",-15}{"Ngày sản xuất",-20}{"Ngày hết hạn", -20}{"Hạn sử dụng", -20}{"Trạng thái",-15}");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------");

            foreach (var sp in listSanPham)
            {
                DateTime ngayHetHan = sp.NgaySanXuat;
                string hanSuDungStr = sp.HanSuDung;

                if (hanSuDungStr.EndsWith("ngày"))
                {
                    int soNgay = int.Parse(hanSuDungStr.Replace(" ngày", "").Trim());
                    ngayHetHan = sp.NgaySanXuat.AddDays(soNgay);
                }
                else if (hanSuDungStr.EndsWith("tháng"))
                {
                    int soThang = int.Parse(hanSuDungStr.Replace(" tháng", "").Trim());
                    ngayHetHan = sp.NgaySanXuat.AddMonths(soThang);
                }
                else if (hanSuDungStr.EndsWith("năm"))
                {
                    int soNam = int.Parse(hanSuDungStr.Replace(" năm", "").Trim());
                    ngayHetHan = sp.NgaySanXuat.AddYears(soNam);
                }
                else
                {
                    Console.WriteLine($"Định dạng hạn sử dụng không hợp lệ: {hanSuDungStr}");
                    continue;
                }

                // Check expiration status
                string trangThai = ngayHetHan >= DateTime.Now ? "Còn hạn" : "Hết hạn";

                string ngayNhap = sp.NgayNhap.ToString("dd/MM/yyyy");
                string ngaySanXuat = sp.NgaySanXuat.ToString("dd/MM/yyyy");
                string TmpngayHetHan = ngayHetHan.ToString("dd/MM/yyyy");

                if (trangThai == "Hết hạn")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ResetColor();
                }
                Console.WriteLine($"{sp.MaSanPham,-15}{sp.TenSanPham,-20}{ngayNhap,-15}{ngaySanXuat,-20}{TmpngayHetHan, -20}{sp.HanSuDung,-20}{trangThai,-15}");
            }

            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Nhấn phím Enter để quay lại menu chính.");
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
                        continue; // Bỏ qua dòng trống

                    try
                    {
                        string[] data = line.Split('#');

                        // Kiểm tra nếu dòng không đủ 11 trường
                        if (data.Length != 11)
                        {
                            Console.WriteLine($"Dòng không hợp lệ (không đủ 11 trường): {line}");
                            continue;
                        }

                        // Tạo đối tượng SanPham
                        SanPham sp = new SanPham(
                            data[0],
                            data[1],
                            data[2],
                            int.Parse(data[3]),
                            double.Parse(data[4]),
                            double.Parse(data[5]),
                            data[6],
                            data[7],
                            DateTime.Parse(data[8]),
                            data[9],
                            DateTime.Parse(data[10])
                        );

                        // Thêm vào danh sách
                        listSanPham.Add(sp);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Lỗi định dạng ở dòng: {line}. Chi tiết lỗi: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi không xác định ở dòng: {line}. Chi tiết lỗi: {ex.Message}");
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
            foreach (SanPham sp in listSanPham)
            {
                data += sp.MaSanPham + "#" + sp.TenSanPham + "#" + sp.DonViTinh + "#" + sp.SoLuong + "#" + sp.GiaBan + "#" + sp.GiaNhap + "#" + sp.LoaiSanPham + "#" + sp.NhaSanXuat + "#" + sp.NgayNhap + "#" + sp.HanSuDung + "#" + sp.NgaySanXuat + "\n";
            }
            //ghi de len du lieu cu

            System.IO.File.WriteAllText(fileName, data);
        }
    }
}
