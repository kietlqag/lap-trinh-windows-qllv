using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PRPR
{
    public partial class FThongKe : Form
    {
        CGiangVienDao gvdao = new CGiangVienDao();
        public FThongKe()
        {
            InitializeComponent();
        }

        private void FThongKe_Load(object sender, EventArgs e)
        {
            ThongKeDeTai();
            ThongKeDiem();

        }

        private void ThongKeDeTai()
        {
            DataTable dtProjects = gvdao.ThongKe();

            // Tính toán số lượng đề tài hoàn thành và chưa hoàn thành
            int completedCount = dtProjects.AsEnumerable().Count(row => row.Field<int>("tiendo") == 100);
            int notCompletedCount = dtProjects.Rows.Count - completedCount;

            // Tính phần trăm hoàn thành và chưa hoàn thành
            double completedPercentage = (double)completedCount / dtProjects.Rows.Count * 100;
            double notCompletedPercentage = (double)notCompletedCount / dtProjects.Rows.Count * 100;

            // Xác định loại biểu đồ tròn
            chart1.Series.Clear(); // Xóa tất cả các chuỗi dữ liệu trước đó
            chart1.Series.Add("Phần trăm"); // Thêm chuỗi dữ liệu mới
            chart1.Series["Phần trăm"].ChartType = SeriesChartType.Pie; // Thiết lập loại biểu đồ

            // Thêm các điểm dữ liệu
            chart1.Series["Phần trăm"].Points.AddXY("Hoàn thành", completedPercentage);
            chart1.Series["Phần trăm"].Points.AddXY("Chưa hoàn thành", notCompletedPercentage);

            // Cấu hình hiển thị biểu đồ tròn
            chart1.Series["Phần trăm"].IsValueShownAsLabel = true; // Hiển thị giá trị của các điểm dữ liệu như nhãn
            chart1.Series["Phần trăm"]["PieLabelStyle"] = "Outside"; // Đặt nhãn bên ngoài biểu đồ
            chart1.Series["Phần trăm"]["PieLineColor"] = "Black"; // Đặt màu đường viền cho các phần tử của biểu đồ tròn

            chart1.ChartAreas[0].Position.Width = 80; // Đặt chiều rộng của khu vực biểu đồ
            chart1.ChartAreas[0].Position.Height = 90; // Đặt chiều cao của khu vực biểu đồ

            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false; // Vô hiệu hóa lưới chính trên trục X
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false; // Vô hiệu hóa lưới chính trên trục Y
        }


        private void ThongKeDiem()
        {
            DataTable dtProjects = gvdao.ThongKe2(); // Gọi hàm để lấy dữ liệu từ cơ sở dữ liệu

            if (dtProjects != null && dtProjects.Rows.Count > 0)
            {
                Dictionary<int, int> diemCount = new Dictionary<int, int>();

                // Tính toán số lượng sinh viên theo từng điểm
                foreach (DataRow row in dtProjects.Rows)
                {
                    if (!string.IsNullOrEmpty(row["diem"].ToString()))
                    {
                        if (int.TryParse(row["diem"].ToString(), out int diem))
                        {
                            if (diemCount.ContainsKey(diem))
                            {
                                diemCount[diem]++;
                            }
                            else
                            {
                                diemCount[diem] = 1;
                            }
                        }
                    }
                }


                // Đưa dữ liệu thống kê vào biểu đồ cột
                chart2.Series.Clear();
                chart2.Series.Add("Điểm");

                foreach (var pair in diemCount)
                {
                    chart2.Series["Điểm"].Points.AddXY(pair.Key, pair.Value);
                }

                // Cấu hình biểu đồ cột
                chart2.Series["Điểm"].ChartType = SeriesChartType.Column;
                chart2.ChartAreas[0].AxisX.Title = "Điểm";
                chart2.ChartAreas[0].AxisY.Title = "Số lượng";
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.");
            }
        }
    }
}
