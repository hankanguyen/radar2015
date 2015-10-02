using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Radar2015
{
    class muctieu
    {
        public int thutu;
        public int toadox;
        public int toadoy;
        public int toadoz;     
        public float vxt;//Van toc xuyen tam
        public int qt = 0;        
        public int[] vetx = new int[1000];
        public int[] vety = new int[1000];
       TrucToaDo.xyzpoint[] td_Axis = new TrucToaDo.xyzpoint[1];
        public int bd = 0;//Bien dem cho mang vet
        public Color maumuctieu;
        public int mt_catch = 1;//Bat muc tieu
        public int mt_destroyed = 0;//Huy muc tieu .
        public int thoigiancho;// thoi gian cho de bat dau chay
        public int thoigiannhay;//Thoi gian thay toa do
        public int kcmt=0;//Khoang cach muc tieu
        public double counted = 0;// Dem so lan tang toa do qua do xac dinh quan duong
        public double qdmt = 0;// Quang duong di cua muc tieu
        public int mt_Started = 0;// Xac dinh muc tieu da bat dau chua
        public int tangtoado_mt = 0;// dem so lan tang toa do
        // Cac thong so de ve muc tieu
        public int mapmt3 = 0;
        public float mapmt4 = 0;
        public int mtve4=0;
        public int x05 = 18;
        public int y05 = 180;
        public float mtve5 = 0;
        public int x06 = 320;
        public int y06 = 80;
        public float mtve6 = 0;
        public float r06 = 0;       
        
        public muctieu(int i,int tdx,int tdy,int tdz,Color maumt,int timetorun,int timewait)
        {
            thutu = i;
            toadox =tdx;
            toadoy = tdy;
            toadoz = tdz;
            maumuctieu = maumt;
            thoigiancho = timewait;
            thoigiannhay = timetorun;         
 
        }
      
        public void vemuctieu()
        {//Các mục tiêu này nằm ở màn hình chính
            int x = toadox;
            int y = toadoy;
            TrucToaDo ttd = new TrucToaDo();
            
            kcmt = khoangcachmuctieu();
            Graphics g = Graphics.FromImage(RADAR.vungtam1);
            SolidBrush mauve = new SolidBrush(maumuctieu);
            int tgnhay = thoigiannhay;//Khoảng thời gian chờ để nhảy lên vị trí mới
            int timewait = thoigiancho;// Thời gian chờ để  mục tiêu băt dầu chạy so với Radar
            int time =ttd.xacdinhthoigian();//Luôn nhân được giá trị thời gian hiện tại
            int i = 0;
            int thoigian_rd_chay = time - RADAR.thoigianbatdau;//Thời gian Radar đã chạy 
            int thoigianmuctieuchay = thoigian_rd_chay - timewait;
            int t = thoigianmuctieuchay / tgnhay;// Dùng tìm các khoảng thời gian
            if ((kcmt > 160) && (bd > 0))
                mt_destroyed = 1;//--Huy bo khi ra ngoai pham vi

  if (thoigian_rd_chay >= timewait)
     {//-----------------Tăng tọa độ theo thời gian định sẵn
                mt_Started = 1;
               // Muc tieu dau tien chon loai chuyen dong nay
                int tdxA = 0;
                int tdxB = 0;
                int tdyA = 0;
                int tdyB = 0;
                float AB = 0;
                double vantoc = 0;
      if ((t > tangtoado_mt) && (mt_destroyed == 0))
         {
                      if (thutu == 1)
                        {
                            counted++;
                            if (toadox < 150)//--> den 150 chuyen qua toa do khac
                            {
                                toadoy++;
                                toadox++;
                            }

                        else
                            if (toadox < 200)
                            {
                                toadoy--;
                                toadox++;
                            }
                            else
                            {
                                toadox++;
                                toadoy++;
                            }
                        tangtoado_mt = t;
                        qdmt += Math.Sqrt(2);
                        vantoc = (qdmt * 2.5 * 3600000 / (counted * tgnhay));
                        vxt = (float)(Math.Ceiling(vantoc));
                    }
                
                else 
                 if (thutu == 2)
                {// Ve duong di cho muc tieu thu 2
                    tangtoado_mt = t;
                   tdyA = toadoy - (toadox *toadox) / ((Math.Abs(toadox) + 1) * 60);
                    toadox--;
                    toadoy = tdyA;
                    //Tinh van toc
                   tdxA =toadox;//toa do truoc                        
                  tdxB = toadox - 1;//toa do sau
                    tdyB = tdyA - (tdxB * tdxB) / ((Math.Abs(tdxB) + 1) * 60);
                    AB = (float)(Math.Sqrt((tdxA - tdxB) * (tdxA - tdxB) + (tdyA - tdyB) * (tdyA - tdyB)));
                    qdmt += AB;
                    counted++;
                   vantoc = 3600000 * qdmt * 2.5 / (counted * tgnhay);//-->Km/h
                    vxt = (float)Math.Ceiling(vantoc);
                 }
                 else if (thutu == 3)
                 {
                     tangtoado_mt = t;
                     // Tăng tọa độ lên
                     mapmt3++;
                     //Tinh toc do
                     tdxB = toadox + (int)(5 * Math.Sin((mapmt3 - 1) * Math.PI / 180f));//Truoc do
                     tdyB = toadoy - (int)(2 * Math.Cos((mapmt3 - 1) * Math.PI / 180f));
                     toadox = toadox + (int)(5 * Math.Sin(mapmt3 * Math.PI / 180f)); //Hien tai                  
                     toadoy = toadoy - (int)(2 * Math.Cos(mapmt3 * Math.PI / 180f));
                     tdxA = toadox;
                     tdyA = toadoy;
                     AB = (float)(Math.Sqrt((tdxA - tdxB) * (tdxA - tdxB) + (tdyA - tdyB) * (tdyA - tdyB)));
                     counted++;
                     qdmt += AB;
                     vantoc = 3600000 * qdmt * 2.5 / (counted * tgnhay);//-->Km/h
                     vxt =(float)Math.Ceiling(vantoc);                    
                 }
                 else
                     if (thutu == 4)
                     {
                         tangtoado_mt = t;
                         mapmt4 += 0.07f;
                         toadox = toadox - 1;
                         if (toadox > 180)
                         {
                             mtve4 += 2;
                             tdyB = toadoy + (int)(Math.Abs(mtve4 - 2) * Math.Cos(mapmt4 - 0.07)) / 30;
                             toadoy = toadoy + (int)(Math.Abs(mtve4) * Math.Cos(mapmt4)) / 30;
                             //Tinh van toc 
                            tdxA = toadox;//Hien tai
                           tdyA = toadoy;
                           tdxB = tdxA + 1;
                            AB = (float)(Math.Sqrt((tdxA - tdxB) * (tdxA - tdxB) + (tdyA - tdyB) * (tdyA - tdyB)));
                             counted++;
                             qdmt += AB;
                             vantoc = 3600000 * qdmt * 2.5 / (counted * tgnhay);//-->Km/h
                             vxt =(float) Math.Ceiling(vantoc);

                         }
                         else
                         {
                             mtve4 -= 2;
                             tdyB = toadoy + (int)(Math.Abs(mtve4 + 2) * Math.Cos(mapmt4 - 0.07)) / 30;
                             toadoy = toadoy + (int)(Math.Abs(mtve4) * Math.Cos(mapmt4)) / 30;
                             tdxA = toadox;//Hien tai
                             tdyA = toadoy;
                             tdxB = tdxA + 1;
                             AB = (float)(Math.Sqrt((tdxA - tdxB) * (tdxA - tdxB) + (tdyA - tdyB) * (tdyA - tdyB)));
                             counted++;
                             qdmt += AB;
                             vantoc = 3600000 * qdmt * 2.5 / (counted * tgnhay);//-->Km/h
                             vxt =(float) Math.Ceiling(vantoc);
                         }
                     }
                     else 
                         if (thutu == 5)
                        {
                         tangtoado_mt = t;
                        x05++;                  
                        mtve5 += 0.1f;
                        tdxA = x05 + (int)(RADAR.R / 5 * Math.Sin((float)mtve5));
                        tdyA = y05 + (int)(RADAR.R / 5 * Math.Cos((float)mtve5));                 
                    // Tinh toc do 
                        toadox =tdxA;
                        toadoy = tdyA;
                        tdxB = x05-1+(int)(RADAR.R /5 * Math.Sin((float)(mtve5-0.1f)));
                        tdyB = y05-1+(int)(RADAR.R /5 * Math.Cos((float)(mtve5-0.1f)));
                        AB = (float)(Math.Sqrt((tdxA - tdxB) * (tdxA - tdxB) + (tdyA - tdyB) * (tdyA - tdyB)));
                        counted++;
                        qdmt += AB;
                        vantoc = 3600000 * qdmt * 2.5 /(counted*tgnhay);
                        vxt =(float) Math.Ceiling(vantoc);
                     }
                     else if (thutu == 6)
                     {
                         tangtoado_mt = t;
                         x06--;
                         y06++;
                         mtve6 += 0.08f;
                         r06 += 0.6f;
                        tdxA = x06 + (int)(r06 * Math.Sin((float)mtve6));
                        tdyA = y06 + (int)(r06 * Math.Cos((float)mtve6));
                         toadoy = tdyA;
                         toadox = tdxA;
                        tdxB = x06 + 1 + (int)((r06 - 0.6f) * Math.Sin((float)(mtve6 - 0.08f)));
                        tdyB = y06 - 1 + (int)((r06 - 0.6f) * Math.Sin((float)(mtve6 - 0.08f)));
                        AB = (float)(Math.Sqrt((tdxA - tdxB) * (tdxA - tdxB) + (tdyA - tdyB) * (tdyA - tdyB)));
                         counted++;
                         qdmt += AB;
                        vantoc = 3600000 * qdmt * 2.5 / (counted * tgnhay);//-->Km/h
                         vxt =(float) Math.Ceiling(vantoc);
                     }
     }
                //Nếu là những lần tiếp theo sau lần quét trúng
                if ((bd > 0)&&(khoangcachmuctieu()<=160))
                { //Chỉ vẽ lại vết khi mà mục tiêu nằm trong tầm kiểm soát
                    for (i = 0; i < bd; i++)
                    {
                        g.FillRectangle(mauve, vetx[i], vety[i], 2, 2);
                    }
                }
                if ((kcmt <= 160) && (ttd.quettrung(x, y) == 1) && (mt_catch == 1))
                {
                    if (bd == 0)
                    {//Lần đầu tiên quét trúng
                        g.FillRectangle(mauve, x, y, 2, 2);
                        vetx[0] = x;
                        vety[0] = y;
                        bd++;
                    }
                    else
                    {//Cac lan quet sau lan quet trung dau tien
                        // ve cac vet
                        for (i = 0; i < bd; i++)
                        {
                            g.FillRectangle(mauve, vetx[i], vety[i], 2, 2);
                        }
                        // ve diem hien tai
                        g.FillRectangle(mauve, x, y, 2, 2);
                        // Cập nhật điểm hiện tại vào vết nếu tọa độ đã thực sự thay đổi
                        if ((vetx[bd - 1] != x) || (vety[bd - 1] != y))
                        {
                            vetx[bd] = x;
                            vety[bd] = y;
                            //tang bien dem 
                            bd++;
                        }
                    }
                    qt = 1;
                    ttd.sound();
                  
                }
            }
        }

       
        public int khoangcachmuctieu()
        {
            kcmt = (int)Math.Sqrt((toadox - 180) * (toadox - 180) + (toadoy - 180) * (toadoy - 180));
            return (kcmt);
        }

        public void Line_Scan()
        {
            Graphics g = Graphics.FromImage(RADAR.vungtam2);
            kcmt = khoangcachmuctieu();
            if ((bd > 0) && (mt_catch == 1) && (mt_destroyed == 0) && (kcmt <= 160) && (qt == 1))
            {
                if (RADAR.mtselect !=thutu)
                {
                    kcmt = (int)(Math.Sqrt((vetx[bd - 1] - 180) * (vetx[bd - 1] - 180) + (vety[bd - 1] - 180) * (vety[bd - 1] - 180)));
                }
                float km_kcmt_ls = kcmt * 400 / 160;//Khoảng cách tính theo Km
                float km_kcmt_ls_tinh = km_kcmt_ls % 40;
                int px_kcmt_ls_tinh = (int)(km_kcmt_ls_tinh * 300 / 40);
                int px_kcmt_ls = (int)(km_kcmt_ls * 300 / 400);//Khoảng cách tính theo Pixel
                // Vẽ ở thước thô
                SolidBrush color_mt = new SolidBrush(maumuctieu);
                g.FillRectangle(color_mt, px_kcmt_ls + 30, 105, 5, 15);
                //vẽ ở thước tinh
                g.FillRectangle(color_mt, 30 + px_kcmt_ls_tinh, 205, 5, 15);
                

            }
        }
        public void Circle_Scan()
        {       Graphics g = Graphics.FromImage(RADAR.vungtam2);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                SolidBrush color_mt = new SolidBrush(maumuctieu);
            kcmt = khoangcachmuctieu();
            if ((bd > 0) && (mt_catch == 1) && (mt_destroyed == 0) && (kcmt <= 160) && (qt == 1))
            {   
               if (RADAR.mtselect !=thutu)
                {// Ve nguyen tai vi tri cu
                    kcmt = (int)(Math.Sqrt((vetx[bd - 1] - 180) * (vetx[bd - 1] - 180) + (vety[bd - 1] - 180) * (vety[bd - 1] - 180)));
                }
                float gocmt = (float)(2*kcmt*Math.PI/160f);// Thu duoc goc
                //--->Suy ra toa do
                int t_dxmt = (int)(RADAR.R * Math.Sin(gocmt)) + 180;
                int t_dymt = -(int)(RADAR.R * Math.Cos(gocmt)) + 180;                
                g.FillRectangle(color_mt, t_dxmt - 2, t_dymt - 2, 5, 5);
                float kcmt_tinh = kcmt % 8f;//Lay phan du cho 20Km
                float gocmt_tinh =(float)(2*Math.PI*kcmt_tinh/8f);
                t_dxmt = (int)(RADAR.R * Math.Sin(gocmt_tinh) / 2f) + 180;
                t_dymt= -(int)(RADAR.R * Math.Cos(gocmt_tinh)/ 2f ) + 180;
                g.FillRectangle(color_mt, t_dxmt - 2, t_dymt - 2,4,4);
                
            }
        }
        public void Spiral_Scan()
        {
            Graphics g = Graphics.FromImage(RADAR.vungtam2);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            double r = RADAR.R / 1000F;
            double phimax = 6 * Math.PI;
            float a = (float)(RADAR.R / phimax);
            float x0 = 180;
            float y0 = 180;
            kcmt = khoangcachmuctieu();
            ///-------------Vẽ mục tiêu 1;           
            if ((bd > 0) && (mt_catch == 1) && (mt_destroyed == 0) && (kcmt <= 160) && (qt == 1))
            {
                if (RADAR.mtselect != thutu)
                {// Ve nguyen tai vi tri cu
                    kcmt = (int)(Math.Sqrt((vetx[bd - 1] - 180) * (vetx[bd - 1] - 180) + (vety[bd - 1] - 180) * (vety[bd - 1] - 180)));
                }
                //---Chuyển qua đơn vị Km
                float kcmt_km = kcmt * 400 / 160f;
                double phi_mt = Math.Sqrt(2 * kcmt_km * 23.58 * 0.16 / a);
                SolidBrush color_mt = new SolidBrush(maumuctieu);
                int tdmt_spiralx = (int)(x0 + a * phi_mt * Math.Sin(phi_mt));
                int tdmt_spiraly = (int)(y0 - a * phi_mt * Math.Cos(phi_mt));
                g.FillRectangle(color_mt, tdmt_spiralx - 2, tdmt_spiraly - 2, 5, 5);
            }
            
        }
        public void Space_Scan()
        {//Nap toa do muc tieu
            TrucToaDo ttd = new TrucToaDo();
            Graphics g = Graphics.FromImage(RADAR.vungtam2);
            g.SmoothingMode = SmoothingMode.AntiAlias;                     
            int mt_load = 0;
           TrucToaDo.xypoint mt;
            if ((bd > 0) && (mt_catch == 1) && (mt_destroyed == 0))
            {
                if (RADAR.mtselect != thutu)
                {
                    kcmt = (int)(Math.Sqrt((vetx[bd - 1] - 180) * (vetx[bd - 1] - 180) + (vety[bd - 1] - 180) * (vety[bd - 1] - 180)));
                }
                if (kcmt <= 160)
                {
                    if (ttd.quettrung(toadox,toadoy) == 1)
                    {
                        td_Axis[0].x = toadox;
                        td_Axis[0].y = toadoy;
                        td_Axis[0].z = toadoz;                        
                        mt_load = 1;                        
                    }
                    else
                        if (qt == 1)
                        {
                            td_Axis[0].x = vetx[bd - 1];
                            td_Axis[0].y = vety[bd - 1];
                            td_Axis[0].z = toadoz;                       
                            mt_load = 1;                           
                        }
                 }
                ttd.quaytheotruc( TrucToaDo.Truc.x, (float)(-Math.PI / 30),td_Axis,1);
                ttd.quaytheotruc(TrucToaDo.Truc.z, (float)(-Math.PI / 10),td_Axis,1);
                //VE
                 if (mt_load == 1)
                 {
                     mt = ttd.map3to2(td_Axis[0]);
                     SolidBrush color_mt = new SolidBrush(maumuctieu);
                     g.FillRectangle(color_mt, mt.x - 1, mt.y - 1, 5, 5);                     
                 }
              }
        }


    }
}
