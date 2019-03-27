using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExerciseUIPrj.controls
{
    public partial class CustomControl1 : Control
    {

        Rectangle picRec = new Rectangle();
        Rectangle NameRec = new Rectangle();
        Rectangle DirRec = new Rectangle();
        Rectangle BtnRec = new Rectangle();
        Rectangle BtnRec1 = new Rectangle();
        Rectangle TimeRec = new Rectangle();
        Rectangle SizeRec = new Rectangle();

        public CustomControl1()
        {
            InitializeComponent();
            BackColor = Color.White;
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Size txtSize = TextRenderer.MeasureText("abc", Font);
            int pwid = txtSize.Height * 2 + 10;
            int y =(int) ((Height - pwid) / 2.0);
            Point p = new Point(5, y);
            picRec = new Rectangle(p, new Size(pwid, pwid));
            int txtwid = (int)(Width / 2.0);
            NameRec = new Rectangle(new Point(p.X+picRec.Width + 2, p.Y), new Size(txtwid, txtSize.Height));
            DirRec = new Rectangle(new Point(p.X + picRec.Width +2, p.Y+txtSize.Height+5), new Size(txtwid, txtSize.Height));
            BtnRec = new Rectangle(new Point(NameRec.Location.X + NameRec.Width + 2, NameRec.Y + (int)(txtSize.Height/2.0)), new Size(txtSize.Height,txtSize.Height));
            BtnRec1 = new Rectangle(new Point(NameRec.Location.X + DirRec.Width + 2+txtSize.Width+2, NameRec.Y + (int)(txtSize.Height / 2.0)), new Size(txtSize.Height, txtSize.Height));
            TimeRec = new Rectangle(new Point(BtnRec1.Location.X + txtSize.Width + 4, NameRec.Y), new Size(Width-picRec.Width-NameRec.Width-BtnRec.Width*2-2*4, txtSize.Height));
            SizeRec = new Rectangle(new Point(BtnRec1.Location.X + txtSize.Width + 4, NameRec.Y+txtSize.Height+2), new Size(Width - picRec.Width - NameRec.Width - BtnRec.Width * 2 - 2 * 4, txtSize.Height));
            

        }

        protected override void OnPaint(PaintEventArgs pe)
        {

            var g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillRectangle(Brushes.Red, picRec);//图标

            var t1 = "这是一句测试文档";
            var t2 = "这是一句测试文档这是一句测试文档这是一句测试文档这是一句测试文档这是一句测试文档这是一句测试文档这是一句测试文档这是一句测试文档";
            DrawTxt(t1, g, NameRec, "这");
            DrawTxt(t2, g, DirRec, "这");
            g.FillRectangle(Brushes.Green, BtnRec);
            g.FillRectangle(Brushes.Blue, BtnRec1);
            var t3 = string.Format("修改时间:{0}",DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            var t4 = "文件大小：4555KB";
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            g.DrawString(t3, Font, Brushes.Black, TimeRec,sf);
            g.DrawString(t4, Font, Brushes.Black, SizeRec,sf);

            base.OnPaint(pe);

        }
        void DrawTxt(string s, Graphics g, Rectangle rect,string key)
        {
            string[] ress = s.Split(key.ToCharArray());

            List<string> res = new List<string>();
            if (s.StartsWith(key))
                res.Add(key);
            if (ress.Length > 1)
            {
                foreach (var r in ress)
                {
                    if (string.IsNullOrEmpty(r))
                        continue;
                    res.Add(r);
                    res.Add(key);
                }
                if (!s.EndsWith(key))
                    res.RemoveAt(res.Count - 1);
            }
            else
            {
                res.Add(s);
            }

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            
            int cwid = 0;
            for (int i = 0; i < res.Count; i++)
            {
                int wid = TextRenderer.MeasureText(g, res[i], Font,new Size(),TextFormatFlags.NoPadding|TextFormatFlags.NoPrefix).Width;
                Brush b = res[i] == key ? Brushes.Red : Brushes.Black;
                int x = cwid+wid;
                if(x>=rect.Width)
                {
                    wid = rect.Width - cwid;
                    RectangleF rec = new RectangleF(new PointF(rect.Location.X+cwid,rect.Y), new SizeF(wid, rect.Height));
                    g.DrawString(res[i], Font, b, rec, sf);
                    break;
                }
                else
                {
                    g.DrawString(res[i], Font, b, new Point(rect.Location.X + cwid, rect.Y), sf);
                }
                cwid += wid;

            }
        }
    }
}
