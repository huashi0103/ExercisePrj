using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExerciseUIPrj.controls
{
    public partial class LoadControl : Control
    {
        Color beginColor = Color.Blue;
        Color endColor = Color.Red;
        int wid = 10;
        int curindex = 0;
        Timer timer;
        int instervel = 200;
        string loadStr = "loading....";

        public LoadControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint|ControlStyles.OptimizedDoubleBuffer, true);
            this.MinimumSize = new Size(40, 80);
            if (!DesignMode)
            {
                Start();
            }
        }

        public void Start()
        {
            if (timer == null)
            {
                timer = new Timer();
                timer.Interval = instervel;
                timer.Tick += Timer_Tick;
            }
            timer.Enabled = true;
        }
        public void Stop()
        {
            if (timer != null)
            {
                timer.Enabled = false;
            }
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            curindex++;
            curindex = curindex >= wid ? 0 : curindex;
            Refresh();
        }
        //计算各种圈圈相关
        Point getPoint(double d, double r, Point center)
        {
            int x = (int)(r * Math.Cos(d * Math.PI / 180.0));
            int y = (int)(r * Math.Sin(d * Math.PI / 180.0));
            return new Point(center.X + x, center.Y - y);
        }
        GraphicsPath getPath(Point a, Point b)
        {
            Point c, d, e, f;
            int h = 2;
            Vertical(a, b, h, out c, out d);
            Vertical(b, a, h, out e, out f);
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(new Point[] { c, d, e, f });
            path.CloseAllFigures();
            return path;


        }
        bool Vertical(Point pointa, Point pointb, double R, out Point pointc, out Point pointd)
        {
            pointc = new Point();
            pointd = new Point();
            try
            {
                //（X-xa)^2+(Y-ya)^2=R*R    距离公式
                //(X-xa)*(xb-xa)+(Y-ya)*(yb-ya)=0   垂直
                //解方程得两点即为所求点
                var cx = pointa.X - (pointb.Y - pointa.Y) * R / Distance(pointa, pointb);
                var cy = pointa.Y + (pointb.X - pointa.X) * R / Distance(pointa, pointb);

                var dx = pointa.X + (pointb.Y - pointa.Y) * R / Distance(pointa, pointb);
                var dy = pointa.Y - (pointb.X - pointa.X) * R / Distance(pointa, pointb);
                pointc = new Point((int)cx, (int)cy);
                pointd = new Point((int)dx, (int)dy);
                return true;
            }
            catch
            {
                //如果A,B两点重合会报错，那样就返回false
                return false;
            }
        }
        double Distance(double xa, double ya, double xb, double yb)
        {
            double L;
            L = Math.Sqrt(Math.Pow(xa - xb, 2) + Math.Pow(ya - yb, 2));
            return L;
        }
        double Distance(Point pa, Point pb)
        {
            return Distance(pa.X, pa.Y, pb.X, pb.Y);
        }
        GraphicsPath getPath(double d, double r, Point c)
        {
            var p1 = getPoint(d, r / 2.0, c);
            var p2 = getPoint(d, r, c);
            return getPath(p1, p2);
        }
        //算渐变色
        Color[] getColors()
        {
            int dr = (int)((endColor.R - beginColor.R) / (double)wid);
            int dg = (int)((endColor.G - beginColor.G) / (double)wid);
            int db = (int)((endColor.B - beginColor.B) / (double)wid);
            List<Color> colors = new List<Color>();
            for (int i = 0; i < wid; i++)
            {
                colors.Add(Color.FromArgb(beginColor.R + dr * i, beginColor.G + dg * i, beginColor.B + db * i));
            }
            return colors.ToArray();

        }

        //画圈圈
        void drawRect(Graphics g)
        {

            int r = (int)(Size.Height / 2.0);
            Point center = new Point(r, r);
            var colors = getColors();
            int findex = curindex;
            for (int i = 0; i < wid; i++)
            {
                double d = (360.0 / wid) * i;
                var p = getPath(d, r, center);
                int cindex = findex + i;
                cindex = cindex >= wid ? cindex - wid : cindex;
                g.FillPath(new SolidBrush(colors[cindex]), p);
                
            }
        }
        //画字符串
        void drawString(Graphics g)
        {
            if (Size.Height >= Size.Width) return;
            Rectangle rect = new Rectangle(new Point(Size.Height, 0), new Size(Size.Width - Size.Height, Size.Height));
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(loadStr, Font, Brushes.Black, rect,sf);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawRect(g);
            drawString(g);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (Size.Height > Size.Width)
            {
                Size = new Size(Size.Height, Size.Height);
            }
        }
    }
}
