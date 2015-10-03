using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Radar2015
{


    public partial class RADAR : Form
    {
        //--------------------Nhung truong dung chung
        public static int splashCompleted = 1;// Da chay xong
        public static int help_enable = 0;
        public static int keyselect = 0;
        public int tocdohientai = 12;
        public static int thoigianbatdau = 0;
        public static int mt_catched = 0;
        public static int R = 160;
        public static Bitmap vungtam1 = new Bitmap(360, 360);//Khi chinh sua boi cac muc tieu thi phai 
        public static Bitmap vungtam2 = new Bitmap(360, 360);// giu nguyen gia tri da thay doi do
        public static int alpha = 0;
        public static int dotangthoigian = 14;
        public static int dotanggoc = 1;//  Tôc độ 12 vong /phut
        public static int mtselect = 0;// Mục tiêu nào đang được quét trong vòng quét trúng

        //--------------------
        TrucToaDo ttd = new TrucToaDo();

        //-----------------------
        TrucToaDo.xyzpoint[] td_Axis = new TrucToaDo.xyzpoint[500];//mang chua thanh phan quet khong gian
        int points = 0;
        //---- Thoi gian hien tai
        public int timeStartAll;
        public int startOK = 0;
        // Su dung resource
        ComponentResourceManager resources = new ComponentResourceManager(typeof(RADAR));
        //-----------------Chế độ quét góc
        public int scanAngle = 0;// chua thuc hien che do quet goc
        public int g1_scaned = 0;//Góc 1 được quét qua
        public int g2_scaned = 0;//góc 2 được quét
        public int scBefore;
        public int chamlandau = 0;
        muctieu a1 = new muctieu(1, 60, 60, 120, Color.LightPink, 1000, 3000);
        muctieu a2 = new muctieu(2, 220, 350, 120, Color.White, 3500, 3000);
        muctieu a3 = new muctieu(3, 70, 300, 120, Color.Violet, 3500, 6000);
        muctieu a4 = new muctieu(4, 342, 180, 120, Color.Yellow, 3000, 2000);
        muctieu a5 = new muctieu(5, 15, 190, 120, Color.Red, 3000, 2400);
        muctieu a6 = new muctieu(6, 320, 80, 120, Color.Orange, 3000, 5000);

        public RADAR()
        {
            InitializeComponent();
            vungtam1 = new Bitmap(360, 360);
            vungtam2 = new Bitmap(360, 360);
        }

        private void vemanhinh()
        {// Ve cac so lieu tren man hinh Radar
            #region
            Graphics g = Graphics.FromImage(vungtam1);
            g.Clear(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen T = new Pen(Color.LightGreen);
            T.Width = 1;
            Pen duongthang = new Pen(Color.MediumSpringGreen);
            duongthang.Width = 1;
            // Ve 4 vong tron .
            for (int i = 0; i <= 120; i += 40)
            {
                g.DrawEllipse(T, 20 + i, 20 + i, 320 - 2 * i, 320 - 2 * i);
            }
            //Ve 4 duong thang 
            g.DrawLine(duongthang, 180, 0, 180, 360);
            g.DrawLine(duongthang, 0, 180, 360, 180);
            g.DrawLine(duongthang, 0, 0, 360, 360);
            g.DrawLine(duongthang, 360, 0, 0, 360);
            // Viet toa do 4 huong
            SolidBrush drawBrush = new SolidBrush(Color.White);
            Font drawFont = new Font("Arial", 12, FontStyle.Bold);
            g.DrawString("S", drawFont, drawBrush, 172, 345);
            g.DrawString("N", drawFont, drawBrush, 172, 0);
            g.DrawString("W", drawFont, drawBrush, 0, 165);
            g.DrawString("E", drawFont, drawBrush, 340, 165);
            //---------------              
            SolidBrush db = new SolidBrush(Color.White);
            Font df = new Font("Arial", 6, FontStyle.Regular);
            g.DrawString("225", df, db, 60, 300);
            g.DrawString("135", df, db, 300, 290);
            g.DrawString("45", df, db, 300, 65);
            g.DrawString("315", df, db, 60, 52);
            g.DrawString("180", df, db, 182, 340);
            g.DrawString("0", df, db, 190, 6);
            g.DrawString("270", df, db, 2, 180);
            g.DrawString("90", df, db, 345, 184);
            //----------------
            T.Dispose();
            duongthang.Dispose();
            #endregion

            mtselect = 1;
            a1.vemuctieu();//lightpink
            if ((a1.khoangcachmuctieu() <= 160) && (ttd.quettrung(a1.toadox, a1.toadoy) == 1) && (a1.mt_catch == 1))
            {
                chonchedoman2();
                hienthisolieu();
            }
            mtselect = 2;
            a2.vemuctieu(); //white 
            if ((a2.khoangcachmuctieu() <= 160) && (ttd.quettrung(a2.toadox, a2.toadoy) == 1) && (a2.mt_catch == 1))
            {
                chonchedoman2();
                hienthisolieu();
            }
            mtselect = 3;
            a3.vemuctieu();//violet
            if ((a3.khoangcachmuctieu() <= 160) && (ttd.quettrung(a3.toadox, a3.toadoy) == 1) && (a3.mt_catch == 1))
            {
                chonchedoman2();
                hienthisolieu();
            }
            mtselect = 4;
            a4.vemuctieu();//yellow
            if ((a4.khoangcachmuctieu() <= 160) && (ttd.quettrung(a4.toadox, a4.toadoy) == 1) && (a4.mt_catch == 1))
            {
                chonchedoman2();
                hienthisolieu();
            }
            mtselect = 5;
            a5.vemuctieu();//red
            if ((a5.khoangcachmuctieu() <= 160) && (ttd.quettrung(a5.toadox, a5.toadoy) == 1) && (a5.mt_catch == 1))
            {
                chonchedoman2();
                hienthisolieu();
            }
            mtselect = 6;
            a6.vemuctieu();//Orange
            if ((a6.khoangcachmuctieu() <= 160) && (ttd.quettrung(a6.toadox, a6.toadoy) == 1) && (a6.mt_catch == 1))
            {
                chonchedoman2();
                hienthisolieu();
            }
            mtselect = 0;//-->khoa lai 
            thanhquet();
            tinhtocdoquay();
            xoadulieu();//Khi muc tieu vuot khoi pham vi xoa du lieu do di
            g = this.CreateGraphics();
            g.DrawImage(vungtam1, 5, 5);

        }
        //-----------THANH QUÉT

        public void thanhquet()
        {
            try
            {
                Graphics g = Graphics.FromImage(vungtam1);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Pen p = new Pen(Color.Lime, 2);
                float toadox = 0;
                float toadoy = 0;
                if (mt_catched == 0)
                {
                    if (scanAngle == 1)//-->Thuc hien quét góc nếu có
                    {//Chế độ quét góc
                        int goc1 = int.Parse(g1.Text);
                        int goc2 = int.Parse(g2.Text);
                        int tam = 0;
                        if (goc1 > goc2)
                        {// Đổi giá trị cho goc1 và góc 2
                            tam = goc1;
                            goc1 = goc2;
                            goc2 = tam;
                        }//-->Góc 2 lon hon goc 1
                        int gocquet = goc2 - goc1;
                        if (goc2 == 360)
                            goc2 = 359;//360-->0
                        if ((goc1 == goc2) || (goc1 < 0) || (goc2 < 0) || (gocquet > 360))
                        {//-->Hủy bỏ quá trình quét và thông báo lỗi
                            scanAngle = 0;
                            st.Text = "Số liệu không hợp lệ";
                            this.zoneScan.Image = ((System.Drawing.Image)(resources.GetObject("zoneScan.Image")));
                        }
                        else
                        if (((gocquet >= 180) && (deg_180.Checked == false)) || ((gocquet < 180) && (deg_180.Checked == true)))
                            if (dotanggoc > 0)
                            {//cham 1 quay ve 2
                                if (alpha == goc1)
                                {
                                    g1_scaned = 1;
                                    dotanggoc = -dotanggoc;
                                }
                                if ((g1_scaned == 1) && (alpha == goc2))
                                    dotanggoc = -dotanggoc;
                            }
                            else//Do tang goc <0
                            {//cham 2 quay ve 1
                                if (alpha == goc2)
                                {
                                    g2_scaned = 1;
                                    dotanggoc = -dotanggoc;
                                }
                                if ((g2_scaned == 1) && (alpha == goc1))
                                    dotanggoc = -dotanggoc;
                            }

                        else//goc quet nho hon 180;                                                              
                                if (dotanggoc > 0)
                        {//cham 2 quay ve 1
                            if (alpha == goc2)
                            {
                                g2_scaned = 1;
                                dotanggoc = -dotanggoc;
                            }
                            if (g2_scaned == 1 && alpha == goc1)
                                dotanggoc = -dotanggoc;
                        }
                        else// do tang goc nho hon 0.
                        {//cham 1 quay ve 2
                            if (alpha == goc1)
                            {
                                g1_scaned = 1;
                                dotanggoc = -dotanggoc;
                            }
                            if (g1_scaned == 1 && alpha == goc2)
                                dotanggoc = -dotanggoc;
                        }

                    } //------->dotanggoc phu hợp 
                    alpha = alpha + dotanggoc;
                }
                //--------------->Che do bat muc tieu duoc thiet lap-------------------
                else
                {//Bắt mục tiêu khi quét trung
                    // Kiem tra neu quet trung                    
                    if ((chamlandau == 0) && (quettrungNum(mt_catched) == 1))
                    {
                        alpha = (int)(mt_Angle(mt_catched) * 180 / Math.PI);
                        chamlandau = 1;
                    }
                    if (chamlandau == 0)//Vẫn quay
                        alpha = alpha + dotanggoc;
                    else
                    {
                        alpha = (int)(mt_Angle(mt_catched) * 180 / Math.PI);
                        if ((mt_Angle(mt_catched) == 0) && (khoangcach(mt_catched) == 0))
                            chamlandau = 0;//Quet tiep cho den khi gap lai neu muc tieu di qua Radar
                        if (khoangcach(mt_catched) > 160)
                        {//Huy bo che do quet goc                            
                            a1.qt = a2.qt = a3.qt = a4.qt = a5.qt = a6.qt = 0;
                            chamlandau = 0;
                            mt_catched = 0;
                            a1.mt_catch = a2.mt_catch = a3.mt_catch = a4.mt_catch = a5.mt_catch = a6.mt_catch = 1;
                        }
                    }
                }
                if (alpha >= 360)
                    alpha = 0;
                if (alpha < 0)
                    alpha = alpha + 360;
                toadox = (float)(R * Math.Sin((alpha * Math.PI) / 180)) + 180;
                toadoy = -(float)(R * Math.Cos((alpha * Math.PI) / 180)) + 180;
                g.DrawLine(p, 180, 180, toadox, toadoy);
                p.Dispose();
            }
            catch
            {
                st.Text = " Có lỗi của số liệu nhập vào ";
                scanAngle = 0;
                this.zoneScan.Image = ((System.Drawing.Image)(resources.GetObject("zoneScan.Image")));

            }
            finally
            {
                if (keyselect == 4)
                {
                    chonchedoman2();
                }
            }
        }



        //-----------------------MAN HINH 2----------------------------------------
        private void LineScan_Click(object sender, EventArgs e)
        {
        }
        //--------------------------VeGiaoDienLineScan
        #region
        public void veduongsin(float dodai, float gocx, float gocy)
        {
            Pen p = new Pen(Color.LightGreen, 1);
            Graphics g = Graphics.FromImage(vungtam2);
            float tdx = 0;
            float tdy = 0;
            float tdx1 = 0;
            float tdy1 = 0;
            for (int i = 0; i < dodai; i++)
            {
                tdy = (float)(3.5 * Math.Sin((float)tdx));
                tdx1 = tdx + 1;
                tdy1 = (float)(2 * Math.Sin(tdx1));
                g.DrawLine(p, gocx + tdx, gocy + tdy, gocx + tdx1, gocy + tdy1);
                tdx = (float)(tdx + 1);
            }
        }
        // Ve vach chia
        public void vevacchia()
        {
            SolidBrush drawBrush = new SolidBrush(Color.White);
            Font drawFont = new Font("Arial", 8, FontStyle.Regular);
            Graphics g = Graphics.FromImage(vungtam2);
            Pen p = new Pen(Color.Red, 1);
            for (int i = 1; i <= 11; i++)
            {
                int n = (i - 1) * 40;
                g.DrawLine(p, i * 30, 122, i * 30, 118);
                g.DrawString(n.ToString(), drawFont, drawBrush, i * 30, 125);
                n = n / 10;
                g.DrawLine(p, i * 30, 222, i * 30, 218);
                g.DrawString(n.ToString(), drawFont, drawBrush, i * 30, 225);
            }

        }
        public void vethuoc_linescan()
        {
            Graphics g = Graphics.FromImage(vungtam2);
            Pen p = new Pen(Color.Red, 1);
            g.Clear(Color.Black);
            //------------- Ve thuong do
            g.DrawLine(p, 30, 120, 330, 120);
            g.DrawLine(p, 30, 220, 330, 220);
            veduongsin(290, 40, 118);//chieu dai,toadogocx,toadogocy
            veduongsin(290, 40, 218);
            // --ve Radar
            SolidBrush greenBrush = new SolidBrush(Color.Green);
            g.FillRectangle(greenBrush, 30, 90, 10, 30);
            g.FillRectangle(greenBrush, 30, 190, 10, 30);
            vevacchia();
            //------------
        }
        #endregion
        //---------------------------VẼ giao diện CirleScan
        public void vegiaodienCircleScan()
        {
            keyselect = 2;
            float t_dx = 0;
            float t_dy = 0;
            float t_dx2 = 0;
            float t_dy2 = 0;
            float tam1 = 0;
            float tam2 = 0;
            float tam3 = 0;
            float tam4 = 0;
            Graphics g = Graphics.FromImage(vungtam2);
            int i = 1;
            g.Clear(Color.Black);
            Pen p = new Pen(Color.ForestGreen, 2);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            SolidBrush db = new SolidBrush(Color.White);
            Font df = new Font("Arial", 6, FontStyle.Regular);
            //---------------            
            g.DrawEllipse(p, 20, 20, 320, 320);
            g.DrawEllipse(p, 100, 100, 160, 160);
            // Ve cac duong thang
            Pen p2 = new Pen(Color.LightGreen, 1);
            p2.DashStyle = DashStyle.Dash;
            for (i = 1; i <= 10; i++)
            {
                t_dx = (float)(R * Math.Sin(i * Math.PI / 10)) + 180;
                t_dy = -(float)(R * Math.Cos(i * Math.PI / 10)) + 180;
                t_dx2 = (float)(R * Math.Sin(Math.PI + i * Math.PI / 10)) + 180;
                t_dy2 = -(float)(R * Math.Cos(Math.PI + i * Math.PI / 10)) + 180;
                tam1 = (float)(80 * Math.Sin(i * Math.PI / 10)) + 180;
                tam2 = -(float)(80 * Math.Cos(i * Math.PI / 10)) + 180;
                tam3 = (float)(80 * Math.Sin(Math.PI + i * Math.PI / 10)) + 180;
                tam4 = -(float)(80 * Math.Cos(Math.PI + i * Math.PI / 10)) + 180;
                g.DrawLine(p2, t_dx, t_dy, t_dx2, t_dy2);
            }
            //Viet chu len radar              
            g.DrawString("40", df, db, 280, 40);
            g.DrawString("2", df, db, 235, 110);
            g.DrawString("240", df, db, 80, 315);
            g.DrawString("12", df, db, 130, 250);
            g.DrawString("80", df, db, 340, 128);
            g.DrawString("4", df, db, 260, 155);
            g.DrawString("280", df, db, 15, 232);
            g.DrawString("14", df, db, 95, 208);
            g.DrawString("120", df, db, 332, 230);
            g.DrawString("6", df, db, 256, 210);
            g.DrawString("320", df, db, 12, 127);
            g.DrawString("16", df, db, 90, 155);
            g.DrawString("160", df, db, 274, 309);
            g.DrawString("8", df, db, 225, 250);
            g.DrawString("360", df, db, 70, 45);
            g.DrawString("18", df, db, 116, 110);
            g.DrawString("200", df, db, 175, 346);
            g.DrawString("10", df, db, 175, 265);
            g.DrawString("0", df, db, 180, 10);
            g.DrawString("0", df, db, 180, 90);
        }
        public void xoadulieu()
        {// Xóa mục tiêu >160
            //Xóa nhưng mục tiêu không năm trong danh 
            //sách bắt mục tiêu nếu chế độ này được bật            
            if ((a1.khoangcachmuctieu() > 160) || ((mt_catched != 1) && (mt_catched != 0)))
            {
                Lf1.Text = "";
                Ld1.Text = "";
                Lh1.Text = "";
                Lv1.Text = "";
            }
            if ((a2.khoangcachmuctieu() > 160) || ((mt_catched != 2) && (mt_catched != 0)))
            {
                Lf2.Text = "";
                Ld2.Text = "";
                Lh2.Text = "";
                Lv2.Text = "";
            }
            if ((a3.khoangcachmuctieu() > 160) || ((mt_catched != 3) && (mt_catched != 0)))
            {
                Lf3.Text = "";
                Ld3.Text = "";
                Lh3.Text = "";
                Lv3.Text = "";
            }
            if ((a4.khoangcachmuctieu() > 160) || ((mt_catched != 4) && (mt_catched != 0)))
            {
                Lf4.Text = "";
                Ld4.Text = "";
                Lh4.Text = "";
                Lv4.Text = "";
            }
            if ((a5.khoangcachmuctieu() > 160) || ((mt_catched != 5) && (mt_catched != 0)))
            {
                Lf5.Text = "";
                Ld5.Text = "";
                Lh5.Text = "";
                Lv5.Text = "";
            }
            if ((a6.khoangcachmuctieu() > 160) || ((mt_catched != 6) && (mt_catched != 0)))
            {
                Lf6.Text = "";
                Ld6.Text = "";
                Lh6.Text = "";
                Lv6.Text = "";
            }
        }
        //---------------THUOC XOAN
        public void vethuocxoan()
        {
            Graphics g = Graphics.FromImage(vungtam2);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            SolidBrush x_but = new SolidBrush(Color.White);
            Font x_f = new Font("Arial", 6, FontStyle.Regular);
            String s = "";
            Point vt = new Point(0, 0);
            g.Clear(Color.Black);
            Pen p = new Pen(Color.ForestGreen, 2);
            Pen m = new Pen(Color.LightGreen, 2);
            float R = 160;
            double phimax = 6 * Math.PI;
            double phi = phimax / 1000;
            float r = R / 1000;
            float x0 = 180;
            float y0 = 180;
            float a = (float)(R / phimax);
            int d = 0;
            float t_dx = 0;
            float t_dy = 0;
            float t_dx2 = 0;
            float t_dy2 = 0;
            while (phi <= phimax)
            {
                t_dx = (float)(a * phi * Math.Sin(phi)) + x0;
                t_dy = -(float)(a * phi * Math.Cos(phi)) + y0;
                phi = phi + phimax / 1000;
                t_dx2 = (float)(a * phi * Math.Sin(phi)) + x0;
                t_dy2 = -(float)(a * phi * Math.Cos(phi)) + y0;
                g.DrawLine(p, t_dx, t_dy, t_dx2, t_dy2);
            }
            // Ve vach chia 
            int hcx = 0;
            int hcy = 0;
            double phi_deg = 0;
            while (d <= 400)
            {
                phi = Math.Sqrt(2 * d * 23.58 * r / a);
                phi_deg = phi * 180 / Math.PI;
                phi_deg = phi_deg % 360;
                g.DrawLine(m, (float)(x0 + a * phi * Math.Sin(phi)), (float)(y0 - a * phi * Math.Cos(phi)), (float)(x0 + (a * phi - 4) * Math.Sin(phi)), (float)(y0 - (a * phi - 4) * Math.Cos(phi)));
                //Ve thước nhỏ
                if (d < 400)
                    for (int i = 1; i < 5; i++)
                    {
                        double phi1 = Math.Sqrt(2 * (4 * i + d) * 23.58 * r / a);
                        g.DrawLine(m, (float)(x0 + a * phi1 * Math.Sin(phi1)), (float)(y0 - a * phi1 * Math.Cos(phi1)), (float)(x0 + (a * phi1 - 2) * Math.Sin(phi1)), (float)(y0 - (a * phi1 - 2) * Math.Cos(phi1)));
                    }
                //-------------Viet chu
                //-- Chinh goc 
                if ((phi_deg < 90) && (phi_deg > 0))
                {
                    hcy = 4;
                    hcx = -14;
                }
                else
                    if (phi_deg < 180)
                {
                    hcx = -10;
                    hcy = -15;
                }
                else
                        if (phi_deg < 270)
                {
                    hcx = -2;
                    hcy = -15;
                }
                else
                {
                    hcx = 5;
                    hcy = 3;
                }
                if (phi_deg != 0)
                {
                    s = d.ToString();
                    vt = new Point((int)(x0 + a * phi * Math.Sin(phi)) + hcx, (int)(y0 - a * phi * Math.Cos(phi)) + hcy);
                    g.DrawString(s, x_f, x_but, vt);
                }
                else
                {
                    vt = new Point((int)x0 - 3, (int)(y0 - a + 15));
                    g.DrawString("0", x_f, x_but, vt);
                }
                d += 20;
            }
        }
        public void refreshmh2()
        {
            xoadulieu();//-->xoa khi muc tieu vuot pham vi quet
            chonchedoman2();
        }
        public float mt_Angle(int i)
        {
            float a = 0;
            switch (i)
            {
                case 1: a = ttd.tinhgoc(a1.toadox, a1.toadoy); break;
                case 2: a = ttd.tinhgoc(a2.toadox, a2.toadoy); break;
                case 3: a = ttd.tinhgoc(a3.toadox, a3.toadoy); break;
                case 4: a = ttd.tinhgoc(a4.toadox, a4.toadoy); break;
                case 5: a = ttd.tinhgoc(a5.toadox, a5.toadoy); break;
                case 6: a = ttd.tinhgoc(a6.toadox, a6.toadoy); break;
            }
            return (a);
        }
        public int khoangcach(int i)
        {
            int khoangcach = 0;
            switch (i)
            {
                case 1: khoangcach = (int)(Math.Sqrt((a1.toadox - 180) * (a1.toadox - 180) + (a1.toadoy - 180) * (a1.toadoy - 180))); break;
                case 2: khoangcach = (int)(Math.Sqrt((a2.toadox - 180) * (a2.toadox - 180) + (a2.toadoy - 180) * (a2.toadoy - 180))); break;
                case 3: khoangcach = (int)(Math.Sqrt((a3.toadox - 180) * (a3.toadox - 180) + (a3.toadoy - 180) * (a3.toadoy - 180))); break;
                case 4: khoangcach = (int)(Math.Sqrt((a4.toadox - 180) * (a4.toadox - 180) + (a4.toadoy - 180) * (a4.toadoy - 180))); break;
                case 5: khoangcach = (int)(Math.Sqrt((a5.toadox - 180) * (a5.toadox - 180) + (a5.toadoy - 180) * (a5.toadoy - 180))); break;
                case 6: khoangcach = (int)(Math.Sqrt((a6.toadox - 180) * (a6.toadox - 180) + (a6.toadoy - 180) * (a6.toadoy - 180))); break;
            }
            return (khoangcach);
        }


        public void vethuoc_SpaceScan()
        {
            Graphics g = Graphics.FromImage(vungtam2);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.Black);
            Pen p = new Pen(Color.ForestGreen, 1);//-->thanh quet
            p.DashStyle = DashStyle.Dash;
            Pen p2 = new Pen(Color.GreenYellow, 2);//Toa do
            Pen p3 = new Pen(Color.GreenYellow, 2);
            p3.DashStyle = DashStyle.Dash;
            TrucToaDo.xypoint O, W, S, E, N, Z, tq2;
            int x0_2 = 180;
            int y0_2 = 180;
            td_Axis[0].x = 180;//N
            td_Axis[0].y = 20;
            td_Axis[0].z = 0;
            td_Axis[1].x = 180;//S
            td_Axis[1].y = 340;
            td_Axis[1].z = 0;
            td_Axis[2].x = 180;//O
            td_Axis[2].y = 180;
            td_Axis[2].z = 0;
            td_Axis[3].x = 20;//W
            td_Axis[3].y = 180;
            td_Axis[3].z = 0;
            td_Axis[4].x = 340;//E
            td_Axis[4].y = 180;
            td_Axis[4].z = 0;
            td_Axis[5].x = 180;//Z
            td_Axis[5].y = 180;
            td_Axis[5].z = 250;
            points = 6;
            for (int i = 0; i < 250; i += 5)//-->thanh quet
            {
                td_Axis[points].x = (int)(x0_2 + R * Math.Sin(alpha * Math.PI / 180));
                td_Axis[points].y = (int)(y0_2 - R * Math.Cos(alpha * Math.PI / 180));
                td_Axis[points].z = i;
                points++;
            }
            //--Nap xong tien hanh xoay mang
            ttd.quaytheotruc(TrucToaDo.Truc.x, (float)(-Math.PI / 30), td_Axis, points);
            ttd.quaytheotruc(TrucToaDo.Truc.z, (float)(-Math.PI / 10), td_Axis, points);
            O = ttd.map3to2(td_Axis[2]);
            N = ttd.map3to2(td_Axis[0]);
            W = ttd.map3to2(td_Axis[3]);
            S = ttd.map3to2(td_Axis[1]);
            E = ttd.map3to2(td_Axis[4]);
            Z = ttd.map3to2(td_Axis[5]);
            g.DrawLine(p3, O.x, O.y, N.x, N.y);
            g.DrawLine(p3, O.x, O.y, W.x, W.y);
            g.DrawLine(p2, O.x, O.y, E.x, E.y);
            g.DrawLine(p2, O.x, O.y, S.x, S.y);
            g.DrawLine(p2, O.x, O.y, Z.x, Z.y);
            //ve thanh quet
            for (int i = 6; i < 56; i++)
            {
                tq2 = ttd.map3to2(td_Axis[i]);
                g.DrawLine(p, O.x, O.y, tq2.x, tq2.y);
            }

            //Viet toa do
            SolidBrush db = new SolidBrush(Color.White);
            Font f = new Font("Arial", 8, FontStyle.Bold);
            g.DrawString("S", f, db, S.x, S.y);
            g.DrawString("N", f, db, N.x, N.y);
            g.DrawString("E", f, db, E.x, E.y);
            g.DrawString("W", f, db, W.x, W.y);
        }

        public void tinhtocdoquay()
        {
            double sovong_phut = Math.Abs(dotanggoc) * 500 / (3f * dotangthoigian);
            sovong_phut = lamtron(sovong_phut);
            vRadar.Text = "Tốc độ hiện tại :" + sovong_phut.ToString() + " v/p";
            vRadar.Update();
        }

        public void start()
        {
            
            if (startOK == 1)
            {
                timeStartAll =ttd.xacdinhthoigian();//Thời gian chạy Radar bắt đầu
                thoigianbatdau = timeStartAll;
            }
        }

        //------------------------VÙNG THIẾT LẬP CÁC NÚT BẤM-----------------------
        #region
        private void reset_Click(object sender, EventArgs e)
        {

        }
        private void HelpBt_Click(object sender, EventArgs e)
        {

        }
        private void deg180(object sender, EventArgs e)
        {//hủy bỏ các chế độ nếu trước đó đang sử dụng 
            //quét nhỏ hơn 180 và ngược lại
            g1_scaned = 0;
            g2_scaned = 0;
        }
        private void exit_Click(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (startOK == 1)
            {
                vemanhinh();
            }
        }

        private void speeddown_Click(object sender, EventArgs e)
        {// Giam toc do quay 
            if (startOK == 1)
            {
                if (tocdohientai > 1)
                {
                    tocdohientai--;
                    dotangthoigian = (int)(lamtron(500 / (3f * tocdohientai)));
                    this.timer1.Interval = dotangthoigian;
                    tinhtocdoquay();
                }
            }
        }
        private void defaultspeed_Click(object sender, EventArgs e)
        {
        }
        private void RADAR_Load(object sender, EventArgs e)
        {// Chạy thanh quet
            //Thread a = new Thread(new ThreadStart(loadsplass));
            //a.Start();
            this.timer1.Enabled = true;
            this.timer1.Interval = dotangthoigian;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        }
        private void revert_Click(object sender, EventArgs e)
        {
        }
        private void spiralScan_Click(object sender, EventArgs e)
        {

        }
        private void CircleScan_Click(object sender, EventArgs e)
        {
        }
        private void ScanG_Click(object sender, EventArgs e)
        {

        }
        private void bt_mt1Catch_Click(object sender, EventArgs e)
        {




        }
        private void bt_mt2Catch_Click(object sender, EventArgs e)
        {



        }
        private void bt_mt3Catch_Click(object sender, EventArgs e)
        {

        }
        private void bt_mt4Catch_Click(object sender, EventArgs e)
        {

        }
        private void bt_mt5Catch_Click(object sender, EventArgs e)
        {

        }
        private void bt_mt6Catch_Click(object sender, EventArgs e)
        {

        }
        private void all_catch_Click(object sender, EventArgs e)
        {
            if (startOK == 1)
            {
                switch (mt_catched)
                {
                    case 1: a2.qt = a3.qt = a4.qt = a5.qt = a6.qt = 0; break;
                    case 2: a1.qt = a3.qt = a4.qt = a5.qt = a6.qt = 0; break;
                    case 3: a1.qt = a2.qt = a4.qt = a5.qt = a6.qt = 0; break;
                    case 4: a1.qt = a2.qt = a3.qt = a5.qt = a6.qt = 0; break;
                    case 5: a1.qt = a2.qt = a3.qt = a4.qt = a6.qt = 0; break;
                    case 6: a1.qt = a2.qt = a3.qt = a4.qt = a5.qt = 0; break;
                }
                mt_catched = 0;
                a1.mt_catch = a2.mt_catch = a3.mt_catch = a4.mt_catch = a5.mt_catch = a6.mt_catch = 1;
                resetbtMtColor();
            }
        }
        private void startBt_Click(object sender, EventArgs e)
        {
        }
        #endregion
        public void resetbtMtColor()
        {

            this.bt1catch.Image = global::Radar2015.Properties.Resources._1_catch;
            this.bt2catch.Image = global::Radar2015.Properties.Resources._2_catch;
            this.bt3catch.Image = global::Radar2015.Properties.Resources._3_catch;
            this.bt4catch.Image = global::Radar2015.Properties.Resources._4_catch;
            this.bt5catch.Image = global::Radar2015.Properties.Resources._5_catch;
            this.bt6catch.Image = global::Radar2015.Properties.Resources._6_catch;


        }
        public void loadsplass()
        {
            /*splasscreen a = new splasscreen();
            a.Visible = true;
            a.TopMost = true;
            a.Show();
            Thread.Sleep(2500);
            splashCompleted = 1;
            a.Close();
            */
        }
        public double lamtron(double a)
        {
            return (Math.Floor(a + 0.5f));
        }

        private void Space_over(object sender, EventArgs e)
        {

        }

        private void Space_leave(object sender, EventArgs e)
        {

        }

        private void line_over(object sender, EventArgs e)
        {

        }

        private void line_leave(object sender, EventArgs e)
        {
            this.btLine.Image = ((System.Drawing.Image)(resources.GetObject("btLine.Image")));
            st.Text = " Radar đang hoạt động ";
            st.Update();
        }


        private void g1_over(object sender, EventArgs e)
        {
            this.g1.ForeColor = Color.LightGreen;
            st.Text = "Vị trí giới hạn đầu tiên của phạm vi quét";
        }

        private void g1_leave(object sender, EventArgs e)
        {
            this.g1.ForeColor = Color.Black;
            st.Text = "Radar đang hoạt động bình thường ";
        }

        private void g2_over(object sender, EventArgs e)
        {
            this.g2.ForeColor = Color.LightGreen;
            st.Text = "Giá trị giới hạn của vị trí thứ 2 trong phạm vi quét";
        }

        private void g2_leave(object sender, EventArgs e)
        {
            this.g2.ForeColor = Color.Black;
            st.Text = "Radar đang hoạt động ";
        }

        private void limit180_over(object sender, EventArgs e)
        {
            this.deg_180.ForeColor = Color.LightGreen;
            st.Text = " Chọn nó nếu muốn Radar quét góc lớn hơn 180";
        }

        private void limit180_leave(object sender, EventArgs e)
        {
            this.deg_180.ForeColor = Color.Black;
            st.Text = "Radar đang hoạt động";
        }

        private void speedup_over(object sender, EventArgs e)
        {
        }

        private void speedup_leave(object sender, EventArgs e)
        {

        }

        private void default_over(object sender, EventArgs e)
        {

        }

        private void default_leave(object sender, EventArgs e)
        {

        }

        private void speeddown_over(object sender, EventArgs e)
        {
            st.Text = "Giảm tốc độ quét";
            this.spdown.Image = ((System.Drawing.Image)(resources.GetObject("speeddown over")));
        }

        private void speeddown_leave(object sender, EventArgs e)
        {
            st.Text = "Radar đang hoạt động ";
            this.spdown.Image = ((System.Drawing.Image)(resources.GetObject("spdown.Image")));
        }


        public void chonchedoman2()
        {// Được gọi từ Quét trúng mục tiêu
            Graphics g = Graphics.FromImage(vungtam2);
            switch (keyselect)
            {
                case 1:
                    //Chế độ LineScan
                    g.Clear(Color.Black);
                    vethuoc_linescan();
                    //------------
                    a1.Line_Scan();
                    a2.Line_Scan();
                    a3.Line_Scan();
                    a4.Line_Scan();
                    a5.Line_Scan();
                    a6.Line_Scan();
                    break;
                case 2:
                    //---CircleScan
                    g.Clear(Color.Black);
                    vegiaodienCircleScan();
                    a1.Circle_Scan();
                    a2.Circle_Scan();
                    a3.Circle_Scan();
                    a4.Circle_Scan();
                    a5.Circle_Scan();
                    a6.Circle_Scan();
                    break;
                case 3:
                    //--Spiral
                    vethuocxoan();
                    a1.Spiral_Scan();
                    a2.Spiral_Scan();
                    a3.Spiral_Scan();
                    a4.Spiral_Scan();
                    a5.Spiral_Scan();
                    a6.Spiral_Scan();

                    break;
                case 4:
                    vethuoc_SpaceScan();
                    a1.Space_Scan();
                    a2.Space_Scan();
                    a3.Space_Scan();
                    a4.Space_Scan();
                    a5.Space_Scan();
                    a6.Space_Scan();
                    break;
            }
            g = this.CreateGraphics();
            g.DrawImage(vungtam2, 375, 5);
        }

        public void hienthisolieu()
        {//--KHi quét trúng mục tiêu đó -->chỉ số liệu mục tiêu đó được cập nhật
            // Được gọi từ hàm vẽ mục tiêu
            float d = 0;
            switch (mtselect)
            {
                case 1:
                    d = a1.khoangcachmuctieu();
                    d = d * 400 / 160;
                    Ld1.Text = d.ToString();
                    d = (float)(ttd.tinhgoc(a1.toadox, a1.toadoy) * 180 / Math.PI);
                    Lf1.Text = d.ToString();
                    d = a1.toadoz / 10;
                    Lh1.Text = d.ToString();
                    d = a1.vxt;
                    Lv1.Text = d.ToString();
                    break;
                case 2:
                    d = a2.khoangcachmuctieu();
                    d = d * 400 / 160;
                    Ld2.Text = d.ToString();
                    d = (float)(ttd.tinhgoc(a2.toadox, a2.toadoy) * 180 / Math.PI);
                    Lf2.Text = d.ToString();
                    d = a2.toadoz / 10;
                    Lh2.Text = d.ToString();
                    d = a2.vxt;
                    Lv2.Text = d.ToString();
                    break;
                case 3:
                    d = a3.khoangcachmuctieu();
                    d = d * 400 / 160;
                    Ld3.Text = d.ToString();
                    d = (float)(ttd.tinhgoc(a3.toadox, a3.toadoy) * 180 / Math.PI);
                    Lf3.Text = d.ToString();
                    d = a3.toadoz / 10;
                    Lh3.Text = d.ToString();
                    d = a3.vxt;
                    Lv3.Text = d.ToString();
                    break;
                case 4:
                    d = a4.khoangcachmuctieu();
                    d = d * 400 / 160;
                    Ld4.Text = d.ToString();
                    d = (float)(ttd.tinhgoc(a4.toadox, a4.toadoy) * 180 / Math.PI);
                    Lf4.Text = d.ToString();
                    d = a4.toadoz / 10;
                    Lh4.Text = d.ToString();
                    d = a4.vxt;
                    Lv4.Text = d.ToString();
                    break;
                case 5:
                    d = a5.khoangcachmuctieu();
                    d = d * 400 / 160;
                    Ld5.Text = d.ToString();
                    d = (float)(ttd.tinhgoc(a5.toadox, a5.toadoy) * 180 / Math.PI);
                    Lf5.Text = d.ToString();
                    d = a5.toadoz / 10;
                    Lh5.Text = d.ToString();
                    d = a5.vxt;
                    Lv5.Text = d.ToString();
                    break;
                case 6:
                    d = a6.khoangcachmuctieu();
                    d = d * 400 / 160;
                    Ld6.Text = d.ToString();
                    d = (float)(ttd.tinhgoc(a6.toadox, a6.toadoy) * 180 / Math.PI);
                    Lf6.Text = d.ToString();
                    d = a6.toadoz / 10;
                    Lh6.Text = d.ToString();
                    d = a6.vxt;
                    Lv6.Text = d.ToString();
                    break;
            }
        }
        public int quettrungNum(int i)
        {
            int a = 0;
            switch (i)
            {
                case 1: a = ttd.quettrung(a1.toadox, a1.toadoy); break;
                case 2: a = ttd.quettrung(a2.toadox, a2.toadoy); break;
                case 3: a = ttd.quettrung(a3.toadox, a3.toadoy); break;
                case 4: a = ttd.quettrung(a4.toadox, a4.toadoy); break;
                case 5: a = ttd.quettrung(a5.toadox, a5.toadoy); break;
                case 6: a = ttd.quettrung(a6.toadox, a6.toadoy); break;
            }
            return (a);
        }

        private void speeddownClick(object sender, EventArgs e)
        {
            //Giam toc do quay 
            if (startOK == 1)
            {
                if (tocdohientai > 1)
                {
                    tocdohientai--;
                    dotangthoigian = (int)(lamtron(500 / (3f * tocdohientai)));
                    this.timer1.Interval = dotangthoigian;
                    tinhtocdoquay();
                }
            }
        }

        private void speeddownover(object sender, EventArgs e)
        {
            st.Text = "Giảm tốc độ quét";
            this.spdown.Image = ((System.Drawing.Image)(resources.GetObject("speeddown over")));
        }

        private void speeddownleave(object sender, EventArgs e)
        {
            st.Text = "Radar đang hoạt động ";
            this.spdown.Image = ((System.Drawing.Image)(resources.GetObject("spdown.Image")));
        }

        private void btdefault_Click(object sender, EventArgs e)
        {
            // Chuyển về tốc độ mặc định
            if (startOK == 1)
            {
                dotangthoigian = 14;
                this.timer1.Interval = dotangthoigian;
            }
        }

        private void defaultover(object sender, EventArgs e)
        {

            this.btdefault.Image = ((System.Drawing.Image)(resources.GetObject("default over")));
            st.Text = "Đưa tốc độ quét của Radar về tốc độ mặt định";
        }

        private void defaultleave(object sender, EventArgs e)
        {
            this.btdefault.Image = ((System.Drawing.Image)(resources.GetObject("btdefault.Image")));
            st.Text = "Radar đang hoạt động";
        }

        private void spup_Click(object sender, EventArgs e)
        {
            // Tang nhanh toc do quay
            if (startOK == 1)
            {
                tocdohientai++;
                dotangthoigian = (int)(lamtron(500 / (3f * tocdohientai)));
                this.timer1.Interval = dotangthoigian;
                tinhtocdoquay();
            }
        }

        private void spupover(object sender, EventArgs e)
        {
            st.Text = " Nhấp vào đây để tăng nhanh tốc độ quét";
            this.spup.Image = ((System.Drawing.Image)(resources.GetObject("speedup over")));
        }

        private void spupleave(object sender, EventArgs e)
        {
            st.Text = "Radar đang hoạt động  ";
            this.spup.Image = ((System.Drawing.Image)(resources.GetObject("spup.Image")));
        }

        private void btreset_Click(object sender, EventArgs e)
        {
            //Xóa màn hình
            Graphics g = Graphics.FromImage(vungtam1);
            Graphics g2 = Graphics.FromImage(vungtam2);
            g.Clear(Color.Black);
            g2.Clear(Color.Black);
            //--> chuyển các thông sô của mục tiêu vê mặc định
            keyselect = 0;
            scanAngle = 0;
            g1_scaned = 0;
            g2_scaned = 0;
            mtselect = 0;
            scBefore = 0;
            alpha = 0;
            dotangthoigian = 14;
            dotanggoc = 1;
            mt_catched = 0;//-->Bắt tất cả
            a1.mt_catch = a2.mt_catch = a3.mt_catch = a4.mt_catch = a5.mt_catch = a6.mt_catch = 1;
            chamlandau = 0;
            a1.mt_destroyed = a2.mt_destroyed = a3.mt_destroyed = a4.mt_destroyed = a5.mt_destroyed = a6.mt_destroyed = 0;
            a1.qt = a2.qt = a3.qt = a4.qt = a5.qt = a6.qt = 0;
            a1.toadox = 60;
            a1.toadoy = 60;
            a2.toadox = 220;
            a2.toadoy = 350;
            a3.toadox = 70;
            a3.toadoy = 300;
            a4.toadox = 342;
            a4.toadoy = 180;
            a5.toadox = 15;
            a5.toadoy = 190;
            a6.toadox = a6.x06 = 320;
            a6.toadoy = a6.y06 = 80;
            a1.bd = a2.bd = a3.bd = a4.bd = a5.bd = a6.bd = 0;
            a1.counted = a2.counted = a3.counted = a4.counted = a5.counted = a6.bd = 0;
            a1.qdmt = a2.qdmt = a3.qdmt = a4.qdmt = a5.qdmt = a6.qdmt = 0;
            a1.mt_Started = a2.mt_Started = a3.mt_Started = a4.mt_Started = a5.mt_Started = a6.mt_Started = 0;
            a1.tangtoado_mt = a2.tangtoado_mt = a3.tangtoado_mt = a4.tangtoado_mt = a5.tangtoado_mt = a6.tangtoado_mt = 0;
            a3.mapmt3 = 0;
            a4.mapmt4 = 0;
            a4.mtve4 = 0;
            a5.mtve5 = a6.mtve6 = 0;
            a5.x05 = 18;
            a5.y05 = 180;
            a6.r06 = 0;
            timeStartAll = ttd.xacdinhthoigian();
            thoigianbatdau = timeStartAll;
            g2 = this.CreateGraphics();
            g2.DrawImage(vungtam2, 375, 5);
            xoadulieu();
        }

        private void btresetover(object sender, EventArgs e)
        {
            st.Text = "Mô phỏng lại toàn bộ quá trình";
            this.btreset.Image = ((System.Drawing.Image)(resources.GetObject("reset over")));
        }

        private void btresetleave(object sender, EventArgs e)
        {
            st.Text = "Radar đang hoạt động";
            this.btreset.Image = ((System.Drawing.Image)(resources.GetObject("btreset.Image")));

        }

        private void btRv_Click(object sender, EventArgs e)
        {
            // Đảo chiều quét
            if (startOK == 1)
            {
                dotanggoc = -dotanggoc;
            }
        }

        private void revertover(object sender, EventArgs e)
        {
            st.Text = " Đảo ngược chiều quét hiện tại của Radar";
            this.btRv.Image = ((System.Drawing.Image)(resources.GetObject("revert over")));

        }

        private void revertleave(object sender, EventArgs e)
        {
            st.Text = "Radar đang hoạt động ";
            this.btRv.Image = ((System.Drawing.Image)(resources.GetObject("btRv.Image")));

        }

        private void btstart_Click(object sender, EventArgs e)
        {
            // chay chuong trinh
           
             if (splashCompleted == 1)
             {
                 startOK = 1;
                 start();
                 this.btstart.Enabled = false;
             }
           
           
        }

        private void btstartover(object sender, EventArgs e)
        {
            st.Text = "Bắt đầu quá trình quét";
            this.btstart.Image = ((System.Drawing.Image)(resources.GetObject("start over")));

        }

        private void btstartleave(object sender, EventArgs e)
        {
            st.Text = "";

            this.btstart.Image = ((System.Drawing.Image)(resources.GetObject("btstart.Image")));

        }

        private void bthelp_Click(object sender, EventArgs e)
        {/*
            if (help_enable == 0)
            {
                help_enable = 1;
                help a = new help();
                a.Show();
            }
            */
        }

        private void bthelpover(object sender, EventArgs e)
        {

            st.Text = " Để có nhiều thông tin về chương trình này nhấp vào đây";
            this.bthelp.Image = ((System.Drawing.Image)(resources.GetObject("help over")));

        }

        private void bthelpleave(object sender, EventArgs e)
        {
            st.Text = "Radar đang hoạt động";
            this.bthelp.Image = ((System.Drawing.Image)(resources.GetObject("bthelp.Image")));

        }

        private void zoneScan_Click(object sender, EventArgs e)
        {
            //Quet trong Pham vi G1-->G2 tinh theo chieu Quet hien hanh
            if (startOK == 1)
            {
                if (mt_catched == 0)
                    if (scanAngle == 0)
                    {
                        scBefore = dotanggoc;
                        scanAngle = 1;
                        this.zoneScan.Image = ((System.Drawing.Image)(resources.GetObject("stop")));

                    }
                    else
                    {
                        dotanggoc = scBefore;
                        scanAngle = 0;
                        st.Text = "Chế độ quét góc vừa được hủy bỏ";
                        this.zoneScan.Image = ((System.Drawing.Image)(resources.GetObject("zoneScan.Image")));
                        // xoa toan bo du lieu ve doi tuong truoc do
                        this.g1.Text = "";
                        this.g2.Text = "";
                        g1_scaned = 0;
                        g2_scaned = 0;

                    }
                else
                    st.Text = "Không sử dụng được chế độ quét góc khi chế độ bắt mục tiêu đã bật ";
            }
        }

        private void zoneover(object sender, EventArgs e)
        {
            if (scanAngle == 0)
            {
                this.zoneScan.Image = ((System.Drawing.Image)(resources.GetObject("ZoneScan over")));
                st.Text = " Sử dụng để quét trong phạm vi 2 góc ";
                st.Update();
            }
            else
            {
                this.zoneScan.Image = ((System.Drawing.Image)(resources.GetObject("stopover")));
                st.Text = " Tắt chức năng quét góc  ";
                st.Update();
            }
        }

        private void zoneleave(object sender, EventArgs e)
        {
            if (scanAngle == 0)
            {
                this.zoneScan.Image = ((System.Drawing.Image)(resources.GetObject("zoneScan.Image")));
                st.Text = " Radar đang hoạt động";
                st.Update();
            }
            else
            {
                this.zoneScan.Image = ((System.Drawing.Image)(resources.GetObject("stop")));
                st.Text = " Radar đang hoạt động ";
                st.Update();
            }
        }

        private void bt1catch_Click(object sender, EventArgs e)
        {
            //--------- Mục tiêu chưa xuất hiện, nhưng đã nằm trong list catch
                resetbtMtColor();// Đưa tất cả các buton về màu mặc định
            this.all.Image = global::Radar2015.Properties.Resources.all1;
            this.bt1catch.Image = global::Radar2015.Properties.Resources._1_over;// đổi màu buton
                mt_catched = 1;
                a1.mt_catch = 1;

            if ((a1.khoangcachmuctieu() <= 160)&&(a1.mt_Started == 1) && (startOK == 1) && (scanAngle == 0))
            {
                chamlandau = 0;                
                a2.mt_catch = a3.mt_catch = a4.mt_catch = a5.mt_catch = a6.mt_catch = 0;
                refreshmh2();
            }
            else
            {
                st.Text = "Bắt mục tiêu vô hiệu lực vào lúc này";
                st.Update();
            }
        }

        private void bt1over(object sender, EventArgs e)
        {
           
            st.Text = " Nhấp vào đây sẽ chỉ có mình mục tiêu thứ nhất được theo dõi";
        }

        private void bt1leave(object sender, EventArgs e)
        {
            st.Text = "Radar đang hoạt động ";
        }

        private void bt2catch_Click(object sender, EventArgs e)
        {
           
                resetbtMtColor();
            this.all.Image = global::Radar2015.Properties.Resources.all1;
            this.bt2catch.Image = global::Radar2015.Properties.Resources._2_over;
                mt_catched = 2;
                a2.mt_catch = 1;  
            if ((a2.khoangcachmuctieu() <= 160)&&(a2.mt_Started == 1) && (startOK == 1) && (scanAngle == 0))
            {
                chamlandau = 0;                
                a1.mt_catch = a6.mt_catch = a3.mt_catch = a4.mt_catch = a5.mt_catch = 0;
                refreshmh2();
            }
            else
            {
                st.Text = "Bắt mục tiêu vô hiệu lực vào lúc này";
                st.Update();
            }

        }

        private void bt2over(object sender, EventArgs e)
        {
            
            st.Text = "Nhấp vào đây , sẽ chỉ có mình mục tiêu thứ 2 được theo dõi";
        }

        private void bt2leave(object sender, EventArgs e)
        {
           
            st.Text = " Radar đang hoạt động ";
        }

        private void bt3_Click(object sender, EventArgs e)
        {
                mt_catched = 3;
                a3.mt_catch = 1;
                resetbtMtColor();
            this.all.Image = global::Radar2015.Properties.Resources.all1;
            this.bt3catch.Image = global::Radar2015.Properties.Resources._3_over;
           
            if ((a3.khoangcachmuctieu() <= 160)&&(a3.mt_Started == 1) && (startOK == 1) && (scanAngle == 0))
            {
                chamlandau = 0;                
                a1.mt_catch = a2.mt_catch = a6.mt_catch = a4.mt_catch = a5.mt_catch = 0; this.bt3catch.Image = global::Radar2015.Properties.Resources._3_catch;
               refreshmh2();
            }
            else
            {
                st.Text = "Bắt mục tiêu vô hiệu lực vào lúc này";
                st.Update();
            }
        }

        private void bt3over(object sender, EventArgs e)
        {
            st.Text = "Nhấp vào đây , sẽ chỉ có mình mục tiêu thứ 3 được theo dõi";
        }

        private void bt3leave(object sender, EventArgs e)
        {
            st.Text = "Radar đang hoạt động";
        }

        private void bt4catch_Click(object sender, EventArgs e)
        {
                mt_catched = 4;
                a4.mt_catch = 1;
                resetbtMtColor();
            this.all.Image = global::Radar2015.Properties.Resources.all1;
            this.bt4catch.Image = global::Radar2015.Properties.Resources._4_over;
            
            if ((a4.khoangcachmuctieu() <= 160)&&(a4.mt_Started == 1) && (startOK == 1) && (scanAngle == 0))
            {
                chamlandau = 0;                
                a1.mt_catch = a2.mt_catch = a3.mt_catch = a5.mt_catch = a6.mt_catch = 0; this.bt3catch.Image = global::Radar2015.Properties.Resources._3_catch;
                refreshmh2();
            }
            else
            {
                st.Text = "Bắt mục tiêu vô hiệu lực vào lúc này";
                st.Update();
            }
        }

        private void bt4over(object sender, EventArgs e)
        {
            
            st.Text = "Nhấp vào đây,sẽ chỉ có mình mục tieu thứ 4 được theo dõi";
        }

        private void bt4leave(object sender, EventArgs e)
        {
            
            st.Text = "Radar đang hoạt động";
        }

        private void bt5catch_Click(object sender, EventArgs e)
        {
                mt_catched = 5;
                a5.mt_catch = 1;
                resetbtMtColor();
            this.all.Image = global::Radar2015.Properties.Resources.all1;
            this.bt5catch.Image = global::Radar2015.Properties.Resources._5_over;
            
            if ((a5.khoangcachmuctieu() <= 160)&&(a5.mt_Started == 1) && (startOK == 1) && (scanAngle == 0))
            {
                chamlandau = 0;               
                a1.mt_catch = a2.mt_catch = a3.mt_catch = a4.mt_catch = a6.mt_catch = 0;               
                refreshmh2();
            }
            else
            {
                st.Text = "Bắt mục tiêu vô hiệu lực vào lúc này";
                st.Update();
            }
        }

        private void bt5over(object sender, EventArgs e)
        {
           
            st.Text = " Nhấp vào đây ,sẽ chỉ có mình mục tiêu thứ 5 được theo dõi ";
        }

        private void bt5leave(object sender, EventArgs e)
        {
            st.Text = "Radar đang hoạt đông";
        }

        private void bt6catch_Click(object sender, EventArgs e)
        {
                mt_catched = 6;
                a6.mt_catch = 1;
                resetbtMtColor();
            this.all.Image = global::Radar2015.Properties.Resources.all1;
            this.bt6catch.Image = global::Radar2015.Properties.Resources._6_over;
            
            if ((a6.khoangcachmuctieu() <= 160)&&(a6.mt_Started == 1) && (startOK == 1))
            {
                chamlandau = 0;                
                a2.mt_catch = a3.mt_catch = a4.mt_catch = a5.mt_catch = a1.mt_catch = 0;
                refreshmh2();
            }
            else
            {
                st.Text = "Mục tiêu ngoài phạm vi quét của RAdar";
                st.Update();
            }
        }

        private void bt6over(object sender, EventArgs e)
        {
           
            st.Text = " Nhấp vào đây ,sẽ chỉ có mình mục tiêu thứ 6 được theo dõi";
        }

        private void bt6leave(object sender, EventArgs e)
        {
            st.Text = "Radar đang hoạt đông";
        }

        private void all_Click(object sender, EventArgs e)
        {
            this.all.Image = global::Radar2015.Properties.Resources.all_over;
            if (startOK == 1)
            {
                switch (mt_catched)
                {
                    case 1: a2.qt = a3.qt = a4.qt = a5.qt = a6.qt = 0; break;
                    case 2: a1.qt = a3.qt = a4.qt = a5.qt = a6.qt = 0; break;
                    case 3: a1.qt = a2.qt = a4.qt = a5.qt = a6.qt = 0; break;
                    case 4: a1.qt = a2.qt = a3.qt = a5.qt = a6.qt = 0; break;
                    case 5: a1.qt = a2.qt = a3.qt = a4.qt = a6.qt = 0; break;
                    case 6: a1.qt = a2.qt = a3.qt = a4.qt = a5.qt = 0; break;
                }
                mt_catched = 0;
                a1.mt_catch = a2.mt_catch = a3.mt_catch = a4.mt_catch = a5.mt_catch = a6.mt_catch = 1;
                resetbtMtColor();
            }
        }

        private void allover(object sender, EventArgs e)
        {
          
            
            st.Text = "Tất cả các mục tiêu sẽ được theo dõi ";
        }

        private void leave(object sender, EventArgs e)
        {
           
            st.Text = " Radar đang hoạt động ";
        }

        private void btspiral_Click(object sender, EventArgs e)
        {
            if (startOK == 1)
            {
                keyselect = 3;
                chonchedoman2();
            }

        }

        private void spiralover(object sender, EventArgs e)
        {
            this.btspiral.Image = ((System.Drawing.Image)(resources.GetObject("spiral over")));
            st.Text = " Vị trí các mục tiêu được mô phỏng trên thước xoắn ";
            st.Update();
        }

        private void spiralleave(object sender, EventArgs e)
        {
            this.btspiral.Image = ((System.Drawing.Image)(resources.GetObject("btspiral.Image")));
            st.Text = " Radar đang hoạt động";
            st.Update();
        }

        private void btcircle_Click(object sender, EventArgs e)
        {

            if (startOK == 1)
            {
                keyselect = 2;
                chonchedoman2();
            }
        }

        private void cirleover(object sender, EventArgs e)
        {
            this.btcircle.Image = ((System.Drawing.Image)(resources.GetObject("circle over")));
            st.Text = " Vị trí các mục tiêu được mô phỏng trên thước tròn ";
            st.Update();
        }

        private void cirleleave(object sender, EventArgs e)
        {
            this.btcircle.Image = ((System.Drawing.Image)(resources.GetObject("btcircle.Image")));
            st.Text = " Radar đang hoạt động  ";
            st.Update();
        }

        private void btLine_Click(object sender, EventArgs e)
        {
            // Vẽ vị trí hiện tại
            if (startOK == 1)
            {
                keyselect = 1;// Thiết lập chế độ Hiện thị LineScan cho màn 2               
                chonchedoman2();
            }
        }

        private void lineover(object sender, EventArgs e)
        {
            this.btLine.Image = ((System.Drawing.Image)(resources.GetObject("line over")));
            st.Text = " Mô phỏng khoảng cách các mục tiêu trên thước thẳng ";
            st.Update();
        }

        private void lineleave(object sender, EventArgs e)
        {
            this.btLine.Image = ((System.Drawing.Image)(resources.GetObject("btLine.Image")));
            st.Text = " Radar đang hoạt động ";
            st.Update();
        }

        private void space_Click(object sender, EventArgs e)
        {

            if (startOK == 1)
            {
                keyselect = 4;
                chonchedoman2();
            }
        }

        private void spaceover(object sender, EventArgs e)
        {
            this.space.Image = ((System.Drawing.Image)(resources.GetObject("3d over")));
            st.Text = " Mô phỏng các mục tiêu trong không gian";
            st.Update();
        }

        private void spaceleave(object sender, EventArgs e)
        {
            this.space.Image = ((System.Drawing.Image)(resources.GetObject("space.Image")));
            st.Text = " Radar đang hoạt động ";
            st.Update();
        }

        private void btexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitover(object sender, EventArgs e)
        {
            st.Text = "Nhấp vào đây để thoát khỏi chương trình";
            this.btexit.Image = ((System.Drawing.Image)(resources.GetObject("exitover")));

        }

        private void exitleave(object sender, EventArgs e)
        {
            st.Text = "Radar đang hoạt động";
            this.btexit.Image = ((System.Drawing.Image)(resources.GetObject("btexit.Image")));

        }

        private void bt1leave(object sender, MouseEventArgs e)
        {
            this.bt1catch.Image = ((System.Drawing.Image)(resources.GetObject("1 catch")));
            st.Text = " Nhấp vào đây sẽ chỉ có mình mục tiêu thứ nhất được theo dõi";
        }

        
    }
}