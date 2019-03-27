using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExerciseUIPrj
{
    public partial class SkinForm : Form
    {
        public SkinForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            TransparencyKey = BColor;
            //Opacity = 0.2;

            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
        }
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        #region 窗体边框阴影效果变量申明
        //声明Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);
        #endregion


        const int HT_LEFT = 10;
        const int HT_RIGHT = 11;
        const int HT_TOP = 12;
        const int HT_TOPLEFT = 13;
        const int HT_TOPRIGHT = 14;
        const int HT_BOTTOM = 15;
        const int HT_BOTTOMLEFT = 16;
        const int HT_BOTTOMRIGHT = 17;
        const int HT_CAPTION = 2;

        #region constants

        const int WM_NCACTIVATE = 0x86;
        const int WM_NCPAINT = 0x85;
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int WM_NCRBUTTONDOWN = 0x00A4;
        const int WM_NCRBUTTONUP = 0x00A5;
        const int WM_NCMOUSEMOVE = 0x00A0;
        const int WM_NCLBUTTONUP = 0x00A2;
        const int WM_NCCALCSIZE = 0x0083;
        const int WM_NCMOUSEHOVER = 0x02A0;
        const int WM_NCMOUSELEAVE = 0x02A2;
        const int WM_NCHITTEST = 0x0084;
        const int WM_NCCREATE = 0x0081;
        //const int WM_RBUTTONUP = 0x0205;

        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_CAPTURECHANGED = 0x0215;
        const int WM_LBUTTONUP = 0x0202;
        const int WM_SETCURSOR = 0x0020;
        const int WM_CLOSE = 0x0010;
        const int WM_SYSCOMMAND = 0x0112;
        const int WM_MOUSEMOVE = 0x0200;
        const int WM_SIZE = 0x0005;
        const int WM_SIZING = 0x0214;
        const int WM_GETMINMAXINFO = 0x0024;
        const int WM_ENTERSIZEMOVE = 0x0231;
        const int WM_WINDOWPOSCHANGING = 0x0046;


        // FOR WM_SIZING MSG WPARAM
        const int WMSZ_BOTTOM = 6;
        const int WMSZ_BOTTOMLEFT = 7;
        const int WMSZ_BOTTOMRIGHT = 8;
        const int WMSZ_LEFT = 1;
        const int WMSZ_RIGHT = 2;
        const int WMSZ_TOP = 3;
        const int WMSZ_TOPLEFT = 4;
        const int WMSZ_TOPRIGHT = 5;

        // left mouse button is down.
        const int MK_LBUTTON = 0x0001;

        const int SC_CLOSE = 0xF060;
        const int SC_MAXIMIZE = 0xF030;
        const int SC_MINIMIZE = 0xF020;
        const int SC_RESTORE = 0xF120;
        const int SC_CONTEXTHELP = 0xF180;

        const int HTCAPTION = 2;
        const int HTCLOSE = 20;
        const int HTHELP = 21;
        const int HTMAXBUTTON = 9;
        const int HTMINBUTTON = 8;
        const int HTTOP = 12;

        const int SM_CYBORDER = 6;
        const int SM_CXBORDER = 5;
        const int SM_CYCAPTION = 4;

        const int CS_DropSHADOW = 0x20000;
        const int GCL_STYLE = (-26);

        #endregion

        GraphicsPath FormPath;

        Color BColor = Color.FromArgb(89,89,89);


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            Rectangle vRectangle = new Rectangle((Width - 75) / 2, 3, 75, 25);
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    //IntPtr vHandle = GetWindowDC(m.HWnd);
                   // Graphics g = Graphics.FromHdc(vHandle);
  
                    break;
                case WM_NCACTIVATE:

                    break;
                case WM_NCHITTEST:
                    #region
                    //获取鼠标位置
                    int nPosX = (m.LParam.ToInt32() & 65535);
                        int nPosY = (m.LParam.ToInt32() >> 16);
                        //右下角
                        if (nPosX >= this.Right - 6 && nPosY >= this.Bottom - 6)
                        {
                            m.Result = new IntPtr(HT_BOTTOMRIGHT);
                            return;
                        }
                        //左上角
                        else if (nPosX <= this.Left + 6 && nPosY <= this.Top + 6)
                        {
                            m.Result = new IntPtr(HT_TOPLEFT);
                            return;
                        }
                        //左下角
                        else if (nPosX <= this.Left + 6 && nPosY >= this.Bottom - 6)
                        {
                            m.Result = new IntPtr(HT_BOTTOMLEFT);
                            return;
                        }
                        //右上角
                        else if (nPosX >= this.Right - 6 && nPosY <= this.Top + 6)
                        {
                            m.Result = new IntPtr(HT_TOPRIGHT);
                            return;
                        }
                        else if (nPosX >= this.Right - 2)
                        {
                            m.Result = new IntPtr(HT_RIGHT);
                            return;
                        }
                        else if (nPosY >= this.Bottom - 2)
                        {
                            m.Result = new IntPtr(HT_BOTTOM);
                            return;
                        }
                        else if (nPosX <= this.Left + 2)
                        {
                            m.Result = new IntPtr(HT_LEFT);
                            return;
                        }
                        else if (nPosY <= this.Top + 2)
                        {
                            m.Result = new IntPtr(HT_TOP);
                            return;
                        }
                        else
                        {
                            m.Result = new IntPtr(HT_CAPTION);
                            return;
                        }
                    #endregion
            }
        }
         void SetWindowRegion()
        {
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 5);
            this.Region = new Region(FormPath);
        }
        GraphicsPath top = new GraphicsPath();

        GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            // 左上角
            path.AddArc(arcRect, 180, 90);
            // 右上角
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            // 右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            // 左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线

            top.AddArc(rect.X,rect.Y, rect.Width, 20, 180, 90);
            top.AddArc(rect.X-radius,rect.Y,rect.Width, 20, 270, 90);
            //top.AddArc(arcRect.X, arcRect.Y + 2, diameter, diameter, 0, 90);
            //top.AddArc(arcRect.X + arcRect.Width, arcRect.Y + 2, diameter, diameter, 90, 90);
            top.CloseAllFigures();
            return path;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            button1.Click += delegate { this.Close(); };
            Point start = new Point();
            bool startmove = false;
            MouseDown += (o, se) => {
                start = se.Location;
                startmove = true;
            };
            MouseUp += (o, se) => {
                startmove = false;
            };
            MouseMove += (o, se) =>
            {
                if(startmove) Location = new Point(Location.X + se.Location.X - start.X, Location.Y + se.Location.Y - start.Y);
            };
            button2.Click += delegate {
                new Form1().Show();
            };
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetWindowRegion();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetWindowRegion();
            this.Refresh();

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            ///base.OnPaint(e);
            var g = e.Graphics;
           // g.Clear(Color.FromArgb(200,BColor));
            //g.FillRegion(new SolidBrush(BColor), new Region(FormPath));
            Rectangle rect = new Rectangle(10, 10, this.Width-20, this.Height-20);
            var path = GetRoundedRectPath(rect, 5);
            g.FillRegion(new SolidBrush(Color.White), new Region(path));
            g.DrawPath(new Pen(Color.Gray), path);
           
        }

    }
    
}
