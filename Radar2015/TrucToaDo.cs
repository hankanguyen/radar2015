using System;
using System.IO;
using System.Media;

namespace Radar2015
{
    class TrucToaDo
    {
       
        public TrucToaDo()
        {
            
        }
        public enum Truc { x, y, z }
        public struct xypoint
        {
            public int x, y;
        }
        public struct xyzpoint
        {
            public double x, y, z;
        }
        public xypoint map3to2(xyzpoint p)
        {
            xyzpoint tam = p;
            xypoint convert;
            int halfwidth = 180;
            tam.y = tam.y + 400;
            tam.y = tam.y + halfwidth;
            convert.x = (int)(tam.x * tam.y / 800 + halfwidth) - 110;
            convert.y = (int)(halfwidth - tam.y * tam.z / 800) + 100;
            return (convert);
        }
        public void quaytheotruc(Truc truc, float goc, xyzpoint[] td_Axis, int points)
        {
            double hshc = 1.001D;
            switch (truc)
            {
                case Truc.x:
                    for (int i = 0; i < points; i++)
                    {
                        td_Axis[i].y = (td_Axis[i].y * Math.Cos(goc) - td_Axis[i].z * Math.Sin(goc)) * hshc;
                        td_Axis[i].z = (td_Axis[i].z * Math.Cos(goc) + td_Axis[i].y * Math.Sin(goc)) * hshc;
                    }
                    break;
                case Truc.z:
                    for (int i = 0; i < points; i++)
                    {
                        td_Axis[i].y = (td_Axis[i].y * Math.Cos(goc) - td_Axis[i].x * Math.Sin(goc)) * hshc;
                        td_Axis[i].x = (td_Axis[i].x * Math.Cos(goc) + td_Axis[i].y * Math.Sin(goc)) * hshc;
                    }
                    break;
                case Truc.y:
                    for (int i = 0; i < points; i++)
                    {
                        td_Axis[i].x = (td_Axis[i].x * Math.Cos(goc) - td_Axis[i].z * Math.Sin(goc));
                        td_Axis[i].z = (td_Axis[i].z * Math.Cos(goc) + td_Axis[i].x * Math.Sin(goc));
                    }
                    break;
            }
        }
        public int xacdinhthoigian()
        {// Tra ve thoi gian hien tai tinh theo giay
            DateTime d = DateTime.Now;
            int gio = d.Hour;
            int phut = d.Minute;
            int giay = d.Second;
            int miligiay = d.Millisecond;
            return (gio * 3600 + phut * 60 + giay) * 1000 + miligiay;
        }
        public void sound()
        {//Nếu thực hiện chế độ quét goc thì không có âm thanh phát ra
            if (RADAR.mt_catched == 0)
            {
                if (File.Exists("beep.wav"))
                {
                    SoundPlayer a = new SoundPlayer("beep.wav");
                    a.Play();
                }
                else
                {
                    Console.Beep(40, 1);
                    SystemSounds.Exclamation.Play();
                }
            }
        }

        public float tinhgoc(float x, float y)
        {// x,y (toa do cua muc tieu) -->RAd
            float b = 180;//Truc tung
            float c = (float)Math.Sqrt((x - 180) * (x - 180) + (y - 180) * (y - 180));//-->kc den mt
            float a = (float)Math.Sqrt((x - 180) * (x - 180) + y * y);
            float cos_beta = (b * b + c * c - a * a) / (2 * b * c);
            float beta = (float)Math.Acos(cos_beta);
            if (x < 180)
                beta = (float)(2 * Math.PI - beta);
            if (c == 0)
                beta = 0;
            return (beta);
        }
        public int quettrung(float x, float y)
        {//x,y la toa do cua muc tieu-->tra ve 1 ne quet trung 
            float b = 180;
            float c = (float)Math.Sqrt((x - 180) * (x - 180) + (y - 180) * (y - 180));
            float a = (float)Math.Sqrt((x - 180) * (x - 180) + y * y);
            float cos_beta = (b * b + c * c - a * a) / (2 * b * c);
            float beta = (float)Math.Acos(cos_beta);
            float alpha_rad = (float)(RADAR.alpha * Math.PI / 180);
            float dotanggoc_rad = (float)(RADAR.dotanggoc * Math.PI / 180);
            if (x < 180)
                beta = (float)(2 * Math.PI - beta);
            if (c <= 160)// Muc tieu nam trong tam quet Radar
            {
                if (RADAR.dotanggoc > 0)
                {
                    if ((beta >= alpha_rad) && (beta <= (alpha_rad + dotanggoc_rad)))
                    {
                        return (1);
                    }
                    else
                        return (0);
                }
                else
                {
                    if ((beta <= alpha_rad) && (beta >= (alpha_rad + dotanggoc_rad)))
                    {
                        return (1);
                    }
                    else
                        return (0);
                }
            }
            else
                return (0);
        }

    }
}
