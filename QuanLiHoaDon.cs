using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSieuThiMini
{
    public class QuanLiHoaDon : HoaDon, IQLST
    {
        private List<HoaDon> listHoaDon = new List<HoaDon>();
        private string filePath = "hoadon.txt";
        private List<SanPham> listSanPham = new List<SanPham>();
        public QuanLiHoaDon()
        {
            listHoaDon = new List<HoaDon>();
            listSanPham = new List<SanPham>();
            DocFile();
            DocFileSanPham();
        }

        public void Nhap()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------NHẬP HÓA ĐƠN BÁN HÀNG--------------------");
            Console.ResetColor();
            MaHoaDon = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
            Console.Write("Nhập mã nhân viên: ");
            MaNhanVien = Console.ReadLine();
            MaNhanVien = MaNhanVien.ToUpper();

            NgayLap = DateTime.Now;

            DsSanPham = new List<SanPham>();
            DsSoLuong = new List<int>();
            DsDonGia = new List<double>();
            DsThanhTien = new List<double>();

            while (true)
            {
                Console.Write("Nhập mã hàng (hoặc nhấn 'Q' để huỷ đơn): ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[Nhập 'E' để tính tiền]");
                Console.ResetColor();
                string maSP = Console.ReadLine();
                maSP = maSP.ToLower();

                // Nếu người dùng nhấn 'Q' để huỷ đơn
                if (maSP == "q")
                {
                    Console.WriteLine("Bạn đã huỷ đơn hàng.");
                    return; // Quay lại menu chính
                }

                // Nếu người dùng nhấn 'E' để tính tiền
                if (maSP == "e")
                    break;

                SanPham sanPham = traThongTinSanPham(maSP);
                if (sanPham == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Mã sản phẩm không tồn tại.");
                    Console.ResetColor();
                    continue;
                }
                int soluongHangTon = sanPham.SoLuong;
                Console.Write("Nhập số lượng (hoặc nhấn 'Q' để huỷ đơn): ");
                int soLuong;
                string input = Console.ReadLine();

                // Nếu người dùng nhấn 'Q' để huỷ đơn
                if (input.ToLower() == "q")
                {
                    Console.WriteLine("Bạn đã huỷ đơn hàng.");
                    return; // Quay lại menu chính
                }

                while (!int.TryParse(input, out soLuong) || soLuong <= 0|| soLuong> soluongHangTon)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Số lượng không hợp lệ, vui lòng nhập lại (hoặc nhấn 'Q' để huỷ đơn): ");
                    Console.ResetColor();
                    input = Console.ReadLine();

                    // Nếu người dùng nhấn 'Q' để huỷ đơn
                    if (input.ToLower() == "q")
                    {
                        Console.WriteLine("Bạn đã huỷ đơn hàng.");
                        return; // Quay lại menu chính
                    }
                }

                double donGia = sanPham.GiaBan;
                double thanhTien = soLuong * donGia;

                DsSanPham.Add(sanPham);
                DsSoLuong.Add(soLuong);
                DsDonGia.Add(donGia);
                DsThanhTien.Add(thanhTien);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Đã thêm sản phẩm: {sanPham.TenSanPham} - Số lượng: {soLuong} - Đơn giá: {donGia} - Thành tiền: {thanhTien}");
                Console.WriteLine($"Tổng tiền hiện tại: {TinhTongTien()}");

                // Cập nhật số lượng sản phẩm trong kho
                sanPham.SoLuong -= soLuong;
                CapNhatFileSanPham(sanPham);
                Console.ResetColor();
            }

            Console.WriteLine($"Tổng tiền: {TinhTongTien() + TinhTienVAT()}");
            Console.ResetColor();
            double tienKhachDua;
            while (true)
            {
                Console.Write("Nhập số tiền khách đưa: ");
                string input = Console.ReadLine();

                // Nếu người dùng nhấn 'Q' để huỷ đơn
                if (input.ToLower() == "q")
                {
                    Console.WriteLine("Bạn đã huỷ đơn hàng.");
                    return; // Quay lại menu chính
                }

                // Kiểm tra xem đầu vào có phải là số hợp lệ không
                if (double.TryParse(input, out tienKhachDua))
                {
                    // Kiểm tra nếu số tiền khách đưa không đủ thanh toán
                    if (tienKhachDua < TinhTongTien() + TinhTienVAT())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Số tiền khách đưa không đủ để thanh toán hóa đơn!");
                        Console.ResetColor();
                    }
                    else
                    {
                        break; // Nếu nhập đúng và đủ tiền, thoát khỏi vòng lặp
                    }
                }
                else
                {
                    // Nếu đầu vào không phải là một số hợp lệ, yêu cầu nhập lại
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Vui lòng nhập một số hợp lệ!");
                    Console.ResetColor();
                }
            }

            TienKhachDua = tienKhachDua;

            double tienThua = TinhTienThua();

            HoaDon hoaDon = new HoaDon(MaHoaDon, MaNhanVien, NgayLap, DsSanPham, DsSoLuong, DsDonGia, DsThanhTien, TienKhachDua);
            listHoaDon.Add(hoaDon);
            GhiFile();

            Console.WriteLine("Lập hóa đơn thành công!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("                   HÓA ĐƠN BÁN HÀNG");
            Console.WriteLine("                 Siêu thị Mini FreeFire");
            Console.WriteLine($"            Mã hóa đơn: {MaHoaDon}");
            Console.WriteLine($"Nhân viên lập: {MaNhanVien}");
            Console.WriteLine($"Ngày lập: {NgayLap.ToString("dd/MM/yyyy HH:mm:ss")}");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("STT  Tên sản phẩm       Số lượng  Đơn giá  Thành tiền");
            Console.WriteLine("-------------------------------------------------------");

            for (int i = 0; i < DsSanPham.Count; i++)
            {
                Console.WriteLine($"{i + 1,-5}{DsSanPham[i]?.TenSanPham,-20}{DsSoLuong[i],-10}{DsDonGia[i],-10}{DsThanhTien[i],-10}");
            }

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine($"                           Tổng tiền: {TinhTongTien()}");
            Console.WriteLine($"                           VAT: 10%");
            Console.WriteLine($"                           Tổng tiền sau VAT: {TinhTongTien() + TinhTienVAT()}");
            Console.WriteLine($"                           Tiền khách đưa: {TienKhachDua}");
            Console.WriteLine($"                           Tiền thừa: {tienThua}");
            Console.WriteLine("-------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("Bạn có muốn xuất hóa đơn ra file không? (Y/N)");

            char userInput = Console.ReadKey(true).KeyChar;

            if (userInput == 'Y' || userInput == 'y')
            {
                XuatHoaDonVaoFile(MaHoaDon);
            }
            Console.WriteLine("Nhấn phím Enter để quay lại menu chính.");
            Console.ReadLine();
        }
        public void Xuat()
        {
            int pageSize = 1; // Số nhân viên sẽ in ra mỗi lần
            int pageNumber = 0; // Trang hiện tại

            while (pageNumber * pageSize < listHoaDon.Count)
            {
                Console.Clear(); // Xóa màn hình mỗi khi in trang mới
                int startIndex = pageNumber * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, listHoaDon.Count);

                for (int i = startIndex; i < endIndex; i++)
                {
                    var hoaDon = listHoaDon[i];
                    XuatHoaDon(hoaDon);

                    Console.WriteLine("Nhấn Y để in hoá đơn vào file!");
                    Console.WriteLine($"Trang {pageNumber + 1} / {Math.Ceiling((double)listHoaDon.Count / pageSize)}");
                    Console.WriteLine("Nhấn <Left> để xem trang trước, <Right> để xem trang sau");
                    Console.WriteLine("Nhấn Enter để quay lại menu chính.");

                    char userInput = Console.ReadKey(true).KeyChar;
                    if (userInput == 'Y' || userInput == 'y')
                    {
                        XuatHoaDonVaoFile(hoaDon.MaHoaDon);
                    }
                }

                // Điều hướng trang
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    pageNumber = Math.Max(pageNumber - 1, 0);
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    pageNumber = Math.Min(pageNumber + 1, (int)Math.Ceiling((double)listHoaDon.Count / pageSize) - 1);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break; // Quay lại menu chính
                }
            }
        }

        private void XuatHoaDon(HoaDon hoaDon)
        {
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("                   HÓA ĐƠN BÁN HÀNG");
            Console.WriteLine("                 Siêu thị Mini FreeFire");
            Console.WriteLine($"              Mã hóa đơn: {hoaDon.MaHoaDon}");
            Console.WriteLine($"Nhân viên lập: {hoaDon.MaNhanVien}");
            Console.WriteLine($"Ngày lập: {hoaDon.NgayLap:dd/MM/yyyy}");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("STT  Tên sản phẩm       Số lượng  Đơn giá  Thành tiền");
            Console.WriteLine("-------------------------------------------------------");

            for (int j = 0; j < hoaDon.DsSanPham.Count; j++)
            {
                Console.WriteLine($"{j + 1,-5}{hoaDon.DsSanPham[j]?.TenSanPham,-20}{hoaDon.DsSoLuong[j],-10}{hoaDon.DsDonGia[j],-10}{hoaDon.DsThanhTien[j],-10}");
            }

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine($"                           Tổng tiền: {hoaDon.TinhTongTien()}");
            Console.WriteLine($"                           VAT: 10%");
            Console.WriteLine($"                           Tổng tiền sau VAT: {hoaDon.TinhTongTien() + hoaDon.TinhTienVAT()}");
            Console.WriteLine($"                           Khách đưa: {hoaDon.TienKhachDua}");
            Console.WriteLine($"                           Tiền thừa: {hoaDon.TinhTienThua()}");
            Console.WriteLine("-------------------------------------------------------");
        }



        public void Sua()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------SỬA THÔNG TIN HÓA ĐƠN--------------------");
            Console.ResetColor();

            Console.Write("Nhập mã hóa đơn cần sửa: ");
            string maHoaDon = Console.ReadLine();

            HoaDon hoaDon = listHoaDon.FirstOrDefault(x => x.MaHoaDon == maHoaDon);
            if (hoaDon == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Không tìm thấy hóa đơn cần sửa.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Nhập thông tin mới!");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Nhấn Enter để giữ nguyên thông tin cũ]");
            Console.ResetColor();

            Console.Write("Nhập mã nhân viên mới: ");
            hoaDon.MaNhanVien = Console.ReadLine();

            hoaDon.NgayLap = DateTime.Now; 

            hoaDon.DsSanPham = new List<SanPham>();
            hoaDon.DsSoLuong = new List<int>();
            hoaDon.DsDonGia = new List<double>();
            hoaDon.DsThanhTien = new List<double>();

            while (true)
            {
                Console.Write("Nhập mã hàng (nhập 'exit' để dừng): ");
                string maSP = Console.ReadLine();

                if (maSP.ToLower() == "exit")
                    break;

                SanPham sanPham = traThongTinSanPham(maSP);
                if (sanPham == null)
                {
                    Console.WriteLine("Mã sản phẩm không tồn tại.");
                    continue; 
                }

                Console.Write("Nhập số lượng: ");
                int soLuong;
                while (!int.TryParse(Console.ReadLine(), out soLuong) || soLuong <= 0)
                {
                    Console.Write("Số lượng không hợp lệ, vui lòng nhập lại: ");
                }

                // Calculate the total price for this product
                double donGia = sanPham.GiaBan;
                double thanhTien = soLuong * donGia;

                // Add product details to the bill
                hoaDon.DsSanPham.Add(sanPham);
                hoaDon.DsSoLuong.Add(soLuong);
                hoaDon.DsDonGia.Add(donGia);
                hoaDon.DsThanhTien.Add(thanhTien);

                Console.WriteLine($"Đã thêm sản phẩm: {sanPham.TenSanPham} - Số lượng: {soLuong} - Đơn giá: {donGia} - Thành tiền: {thanhTien}");
            }

            double tongTien = hoaDon.DsThanhTien.Sum();
            Console.WriteLine($"Tổng tiền hóa đơn: {tongTien}");
            GhiFile(); 
            Console.WriteLine("Cập nhật hóa đơn thành công!");
            Console.WriteLine("Nhấn phím Enter để quay lại menu chính.");
            Console.ReadLine();
        }
        
        public void Xoa()
        {
            Console.Write("Nhập mã hóa đơn cần xóa: ");
            string maHoaDon = Console.ReadLine();

            HoaDon hoaDon = listHoaDon.FirstOrDefault(x => x.MaHoaDon == maHoaDon);
            if (hoaDon == null)
            {
                Console.WriteLine("Không tìm thấy hóa đơn cần xóa.");
                return;
            }

            listHoaDon.Remove(hoaDon);
            GhiFile();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Xóa hóa đơn thành công!");
            Console.ResetColor();
            Console.WriteLine("Nhấn phím Enter để quay lại menu chính.");
            Console.ReadLine();

        }

        public void TimKiem()
        {
            Console.Write("Nhập mã hóa đơn cần tìm: ");
            string maHoaDon = Console.ReadLine();

            HoaDon hoaDon = listHoaDon.FirstOrDefault(x => x.MaHoaDon == maHoaDon);
            if (hoaDon == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Không tìm thấy hóa đơn cần tìm.");
                Console.ResetColor();
                return;
            }

            XuatHoaDon(hoaDon);
            Console.ReadLine();
        }

        public void DocFile()
        {
            if (System.IO.File.Exists(filePath))
            {
                string data = System.IO.File.ReadAllText(filePath);
                string[] lines = data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string line in lines)
                {
                    string[] items = line.Split('#');
                    if (items.Length < 5) // Kiểm tra xem dòng có đủ trường dữ liệu không
                        continue;

                    HoaDon hoaDon = new HoaDon();
                    hoaDon.MaHoaDon = items[0];
                    hoaDon.MaNhanVien = items[1];

                    // Kiểm tra và phân tích ngày tháng
                    if (DateTime.TryParseExact(items[2], "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                    {
                        hoaDon.NgayLap = parsedDate;
                    }
                    else
                    {
                        Console.WriteLine("Ngày tháng không hợp lệ trong dòng: " + line);
                        continue;
                    }

                    string[] dsSanPham = items[3].Split(',');
                    foreach (string sp in dsSanPham)
                    {
                        string[] spDetails = sp.Split('-');
                        if (spDetails.Length == 5) // Mã sản phẩm - Tên sản phẩm - Số lượng - Đơn giá - Thành tiền
                        {
                            SanPham sanPham = new SanPham
                            {
                                MaSanPham = spDetails[0],
                                TenSanPham = spDetails[1]
                            };
                            hoaDon.DsSanPham.Add(sanPham);
                            hoaDon.DsSoLuong.Add(int.Parse(spDetails[2]));
                            hoaDon.DsDonGia.Add(double.Parse(spDetails[3]));
                            hoaDon.DsThanhTien.Add(double.Parse(spDetails[4]));
                        }
                    }

                    hoaDon.TienKhachDua = double.Parse(items[4]);

                    listHoaDon.Add(hoaDon);
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy file.");
            }
        }


        public void GhiFile()
        {
            if (listHoaDon == null)
            {
                Console.WriteLine("listHoaDon là null.");
                return;
            }

            string data = "";

            foreach (HoaDon hd in listHoaDon)
            {
                if (hd?.DsSanPham != null && hd?.DsSoLuong != null && hd?.DsDonGia != null && hd?.DsThanhTien != null)
                {
                    data += $"{hd.MaHoaDon}#{hd.MaNhanVien}#{hd.NgayLap:dd/MM/yyyy HH:mm:ss}#";
                    for (int i = 0; i < hd.DsSanPham.Count; i++)
                    {
                        if (i < hd.DsSoLuong.Count && i < hd.DsDonGia.Count && i < hd.DsThanhTien.Count)
                        {
                            // Lưu mã sản phẩm - tên sản phẩm - số lượng - đơn giá - thành tiền
                            data += $"{hd.DsSanPham[i]?.MaSanPham}-{hd.DsSanPham[i]?.TenSanPham}-{hd.DsSoLuong[i]}-{hd.DsDonGia[i]}-{hd.DsThanhTien[i]},";
                        }
                    }

                    data = data.TrimEnd(','); // Xóa dấu phẩy thừa ở cuối
                    data += "#" + hd.TienKhachDua + "\n";
                }
            }

            try
            {
                System.IO.File.WriteAllText(filePath, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi ghi vào file: {ex.Message}");
            }
        }





        public SanPham traThongTinSanPham(string maSP)
        {
            foreach (SanPham sp in listSanPham)
            {
                if (sp.MaSanPham == maSP)
                {
                    return sp;
                }
            }

            return null;
        }

        public void DocFileSanPham()
        {

            if (System.IO.File.Exists("sanpham.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines("sanpham.txt");

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

        public void CapNhatFileSanPham(SanPham sanPham)
        {
            SanPham sp = listSanPham.FirstOrDefault(x => x.MaSanPham == sanPham.MaSanPham);
            if (sp == null)
            {
                Console.WriteLine("Không tìm thấy sản phẩm cần cập nhật.");
                return;
            }

            // Cập nhật so luong san pham
            sp.SoLuong = sanPham.SoLuong;

            // Ghi lại toàn bộ danh sách sản phẩm vào file
            string data = "";
            foreach (SanPham item in listSanPham)
            {
                data += item.MaSanPham + "#" + item.TenSanPham + "#" + item.DonViTinh + "#" + item.SoLuong + "#" + item.GiaBan + "#" + item.GiaNhap + "#" + item.LoaiSanPham + "#" + item.NhaSanXuat + "#" + item.NgayNhap + "#" + item.HanSuDung + "#" + item.NgaySanXuat + "\n";
            }

            System.IO.File.WriteAllText("sanpham.txt", data);
        }

        public void XuatHoaDonVaoFile(string maHoaDon)
        {
            HoaDon hoaDon = listHoaDon.FirstOrDefault(x => x.MaHoaDon == maHoaDon);

            if (hoaDon == null)
            {
                Console.WriteLine("Không tìm thấy hóa đơn cần xuất.");
                return;
            }

            string fileName = maHoaDon + ".txt";
            string data = TaoDuLieuHoaDon(hoaDon);

            System.IO.File.WriteAllText(fileName, data);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Xuất hóa đơn thành công!");
            Console.ResetColor();
        }

        private string TaoDuLieuHoaDon(HoaDon hoaDon)
        {
            string data = "-------------------------------------------------------\n";
            data += "                   HÓA ĐƠN BÁN HÀNG\n";
            data += "                 Siêu thị Mini FreeFire\n";
            data += $"             Mã hóa đơn: {hoaDon.MaHoaDon}\n";
            data += $"Nhân viên lập: {hoaDon.MaNhanVien}\n";
            data += $"Ngày lập: {hoaDon.NgayLap:dd/MM/yyyy HH:mm:ss}\n";
            data += "-------------------------------------------------------\n";
            data += "STT  Tên sản phẩm       Số lượng  Đơn giá  Thành tiền\n";
            data += "-------------------------------------------------------\n";

            for (int j = 0; j < hoaDon.DsSanPham.Count; j++)
            {
                data += $"{j + 1,-5}{hoaDon.DsSanPham[j]?.TenSanPham,-20}{hoaDon.DsSoLuong[j],-10}{hoaDon.DsDonGia[j],-10}{hoaDon.DsThanhTien[j],-10}\n";
            }

            data += "-------------------------------------------------------\n";
            data += $"                           Tổng tiền: {hoaDon.TinhTongTien()}\n";
            data += $"                           VAT: 10%\n";
            data += $"                           Tổng tiền sau VAT: {hoaDon.TinhTongTien() + hoaDon.TinhTienVAT()}\n";
            data += $"                           Tiền khách đưa: {hoaDon.TienKhachDua}\n";
            data += $"                           Tiền thừa: {hoaDon.TinhTienThua()}\n";
            data += "-------------------------------------------------------\n";

            return data;
        }
        public void ThongKeDoanhThuTheoNgay()
        {
            List<DateTime> listNgay = listHoaDon.Select(x => x.NgayLap.Date).Distinct().ToList();
            string fileName = "thongkedoanhthu.txt";
            string data = "";
            data += "-------------------------------------------------------\n";
            data += "                  THỐNG KÊ DOANH THU\n";
            data += "-------------------------------------------------------\n";
            data += $"{"Ngày",-15}{"Tổng tiền",-20}{"Số hóa đơn",-10}\n";
            data += "-------------------------------------------------------\n";

            foreach (var ngay in listNgay)
            {
                double doanhThu = 0;
                int soHoaDon = 0;

                foreach (HoaDon hoaDon in listHoaDon)
                {
                    if (hoaDon.NgayLap.Date == ngay.Date)
                    {
                        doanhThu += hoaDon.TinhTongTien();
                        soHoaDon++;
                    }
                }

                string ngayStr = ngay.ToString("dd/MM/yyyy");
                string doanhthuStr = doanhThu.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
                data += $"{ngayStr,-15}{doanhthuStr,-20}{soHoaDon,-10}\n";
            }

            data += "-------------------------------------------------------\n";
            System.IO.File.WriteAllText(fileName, data);

            Console.WriteLine("Thống kê doanh thu đã được lưu vào file thongkedoanhthu.txt");
            DocFileThongKeDoanhThuTheoNgay();
            Console.WriteLine("Nhấn phím Enter để quay lại menu chính.");
            Console.ReadLine();
        }
        public void ThongKeDoanhThuTheoThang()
        {
            List<DateTime> listThang = listHoaDon.Select(x => new DateTime(x.NgayLap.Year, x.NgayLap.Month, 1)).Distinct().ToList();
            string fileName = "thongkedoanhthuthang.txt";
            string data = "";
            data += "-------------------------------------------------------\n";
            data += "              THỐNG KÊ DOANH THU THEO THÁNG\n";
            data += "-------------------------------------------------------\n";
            data += $"{"Tháng",-15}{"Tổng tiền",-20}{"Số hóa đơn",-10}\n";
            data += "-------------------------------------------------------\n";

            foreach (var thang in listThang)
            {
                double doanhThu = 0;
                int soHoaDon = 0;

                foreach (HoaDon hoaDon in listHoaDon)
                {
                    if (hoaDon.NgayLap.Year == thang.Year && hoaDon.NgayLap.Month == thang.Month)
                    {
                        doanhThu += hoaDon.TinhTongTien();
                        soHoaDon++;
                    }
                }

                string thangStr = thang.ToString("MM/yyyy");
                string doanhthuStr = doanhThu.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
                data += $"{thangStr,-15}{doanhthuStr,-20}{soHoaDon,-10}\n";
            }

            data += "-------------------------------------------------------\n";
            System.IO.File.WriteAllText(fileName, data);

            Console.WriteLine("Thống kê doanh thu đã được lưu vào file thongkedoanhthuthang.txt");
            DocFileThongKeDoanhThuTheoThang();

            Console.WriteLine("Nhấn phím Enter để quay lại menu chính.");
            Console.ReadLine();
        }

        public void DocFileThongKeDoanhThuTheoNgay()
        {
            string fileName = "thongkedoanhthu.txt";
            if (System.IO.File.Exists(fileName))
            {
                string data = System.IO.File.ReadAllText(fileName);
                Console.WriteLine(data);
            }
            else
            {
                Console.WriteLine("File không tồn tại. Vui lòng kiểm tra lại.");
            }
        }

        public void DocFileThongKeDoanhThuTheoThang()
        {
            string fileName = "thongkedoanhthuthang.txt";
            if (System.IO.File.Exists(fileName))
            {
                string data = System.IO.File.ReadAllText(fileName);
                Console.WriteLine(data);
            }
            else
            {
                Console.WriteLine("File không tồn tại. Vui lòng kiểm tra lại.");
            }
        }


    }


}
