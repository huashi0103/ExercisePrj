using Dongger.Windows.Forms.Api;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Dongger.Windows.Forms
{
    public class DForm : Form
    {
        private DShadowForm shadow;
        public DForm()
        {
            InitializeComponent();
            this.CloseButtonImage = Properties.Resources.close;
            this.MaximumButtonImage = Properties.Resources.window_max;
            this.MaximumNormalButtonImage = Properties.Resources.windows;
            this.MinimumButtonImage = Properties.Resources.min;
            this.HelpButtonImage = Properties.Resources.help;
            this.CaptionForeColor = Color.White;
            this.CaptionBackgroundColor = Color.DimGray;
            this.CaptionHeight = 40;
            this.BackColor = Color.White;
            this.BackColorGradintTo = Color.Silver;
            this.BackColorLinearGradientMode = EGradientMode.Vertical;
            this.CenterTitle = false;
            this.ControlActivedColor = Color.FromArgb(26, 188, 156);
            this.MaxToFullScreen = false;
            this.ShadowBlur = 10;
            this.ShowTitle = true;
            this.ShowLogo = true;
            this.ShadowColor = Color.FromArgb(224, 224, 224);
            this.CustomResizeable = true;
            this.ControlBackColor = Color.Transparent;
            base.SetStyle(
             ControlStyles.UserPaint |
             ControlStyles.AllPaintingInWmPaint |
             ControlStyles.OptimizedDoubleBuffer |
             ControlStyles.ResizeRedraw |
             ControlStyles.DoubleBuffer, true);
            base.UpdateStyles();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                shadow = new DShadowForm(this)
                {
                    ShadowBlur = ShadowBlur,
                    ShadowSpread = ShadowSpread,
                    ShadowColor = Color.FromArgb(187, 187, 187)
                };

                shadow.RefreshShadow();
            }
        }

        [Browsable(true), Category("阴影"), Description("阴影模糊宽度值"), DefaultValue(10)]
        public int ShadowBlur { get; set; }

        [Category("阴影"), Description("阴影模糊值"), DefaultValue(0)]
        public int ShadowSpread { get; set; }

        [Category("阴影"), Description("阴影模糊颜色"), DefaultValue(typeof(Color), "#E0E0E0")]
        public Color ShadowColor { get; set; }

        private Image closeButtonImage;
        [Category("标题栏"), Description("关闭按钮图片")]
        public Image CloseButtonImage { get { return this.closeButtonImage; } set { this.closeButtonImage = value; this.Invalidate(this.captionRect); } }

        private Image maximumButtonImage;
        [Category("标题栏"), Description("最大化按钮图片")]
        public Image MaximumButtonImage { get { return this.maximumButtonImage; } set { this.maximumButtonImage = value; this.Invalidate(this.captionRect); } }

        private Image maximumNormalButtonImage;
        [Category("标题栏"), Description("最大化默认按钮图片")]
        public Image MaximumNormalButtonImage { get { return this.maximumNormalButtonImage; } set { this.maximumNormalButtonImage = value; this.Invalidate(this.captionRect); } }

        private Image minimumButtonImage;
        [Category("标题栏"), Description("最小化按钮图片")]
        public Image MinimumButtonImage { get { return this.minimumButtonImage; } set { this.minimumButtonImage = value; this.Invalidate(); } }

        private Image helpButtonImage;
        [Category("标题栏"), Description("帮助按钮图片")]
        public Image HelpButtonImage { get { return this.helpButtonImage; } set { this.helpButtonImage = value; this.Invalidate(this.captionRect); } }

        private Color captionForeColor;
        [Category("标题栏"), Description("标题栏文字颜色")]
        public Color CaptionForeColor { get { return this.captionForeColor; } set { this.captionForeColor = value; this.Invalidate(this.captionRect); } }

        private Color captionBackgroundColor;
        [Category("标题栏"), Description("标题栏背景颜色"), DefaultValue(typeof(Color), "White")]
        public Color CaptionBackgroundColor { get { return this.captionBackgroundColor; } set { this.captionBackgroundColor = value; this.Invalidate(this.captionRect); } }

        [Category("标题栏"), Description("标题栏按钮鼠标悬浮背景色"), DefaultValue(typeof(Color), "#1abc9c")]
        public Color ControlActivedColor { get; set; }

        private Color controlBackColor;
        [Category("标题栏"), Description("标题栏按钮默认状态背景色"), DefaultValue(typeof(Color), "Transparent")]
        public Color ControlBackColor { get { return this.controlBackColor; } set { this.controlBackColor = value; this.Invalidate(this.captionRect); } }

        private bool centerTitle;
        [Category("标题栏"), Description("标题栏文字和Icon是否居中"), DefaultValue(typeof(bool), "false")]
        public bool CenterTitle { get { return this.centerTitle; } set { this.centerTitle = value; this.Invalidate(this.captionRect); } }

        private Font titleFont;
        [Category("标题栏"), Description("标题栏文字字体样式"), DefaultValue(typeof(bool), "false")]
        public Font TitleFont { get { return this.titleFont; } set { this.titleFont = value; this.Invalidate(this.captionRect); } }

        private int captionHeight;
        [Category("标题栏"), Description("标题栏高度"), DefaultValue(typeof(int), "40")]
        public int CaptionHeight { get { return this.captionHeight; } set { this.captionHeight = value; this.SetPadding(); } }

        [Category("标题栏"), Description("是否显示标题文字"), DefaultValue(true)]
        public bool ShowTitle { get; set; }

        [Category("标题栏"), Description("是否显示标题栏图标，有可能需要显示图标，但是不需要显示到任务栏"), DefaultValue(true)]
        public bool ShowLogo { get; set; }

        private Color borderColor;
        [Category("边框"), Description("边框颜色"), DefaultValue(typeof(Color), "Gray")]
        public Color BorderColor { get { return this.borderColor; } set { this.borderColor = value; } }

        private int borderWidth;
        [Category("边框"), Description("边框宽度"), DefaultValue(typeof(int), "1")]
        public int BorderWidth { get { return this.borderWidth; } set { this.borderWidth = value; this.SetPadding(); } }

        private ButtonBorderStyle borderStyle;
        [Category("边框"), Description("边框样式"), DefaultValue(typeof(ButtonBorderStyle), "Solid")]
        public ButtonBorderStyle BorderStyle { get { return this.borderStyle; } set { this.borderStyle = value; this.SetPadding(); } }

        [Category("边框"), Description("是否允许用户调整大小"), DefaultValue(true)]
        public bool CustomResizeable { get; set; }

        [Category("样式"), Description("最大化到全屏"), DefaultValue(false)]
        public bool MaxToFullScreen { get; set; }

        private Color backColor;
        [Browsable(true), Category("样式"), Description("背景色"), DefaultValue(typeof(Color), "White")]
        public new Color BackColor { get { return backColor; } set { backColor = value; this.Invalidate(); } }

        private Color backColorGradintTo;
        [Browsable(true), Category("样式"), Description("背景渐变色"), DefaultValue(typeof(Color), "Silver")]
        public Color BackColorGradintTo { get { return backColorGradintTo; } set { backColorGradintTo = value; } }

        private EGradientMode backColorLinearGradientMode;
        [Browsable(true), Category("样式"), Description("背景颜色渐变方向"), DefaultValue(typeof(EGradientMode), "Vertical")]
        public EGradientMode BackColorLinearGradientMode { get { return backColorLinearGradientMode; } set { backColorLinearGradientMode = value; } }

        // 覆盖掉
        [Browsable(false)]
        public new Padding Padding { get; set; }

        private Rectangle closeRect = Rectangle.Empty;
        private Rectangle maxRect = Rectangle.Empty;
        private Rectangle minRect = Rectangle.Empty;
        private Rectangle helpRect = Rectangle.Empty;
        private Rectangle captionRect;
        private bool closeHover;
        private bool maxHover;
        private bool minHover;
        private bool helpHover;
        private bool captionHover;

        private void SetPadding()
        {
            if (this.BorderStyle != ButtonBorderStyle.None)
            {
                base.Padding = new Padding(this.GetBorderWidth(),
                    this.CaptionHeight + this.GetBorderWidth(), this.GetBorderWidth(), this.GetBorderWidth());
            }
            else
            {
                base.Padding = new Padding(0, this.CaptionHeight, 0, 0);
            }

            this.SetCaptionRect();
            this.Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Brush b = null;
            if (BackColorLinearGradientMode == EGradientMode.None || BackColorGradintTo.IsEmpty)
            {
                b = new SolidBrush(BackColor);
            }
            else
            {
                b = new LinearGradientBrush(e.ClipRectangle, BackColor, BackColorGradintTo, (System.Drawing.Drawing2D.LinearGradientMode)BackColorLinearGradientMode);
            }

            e.Graphics.FillRectangle(b, e.ClipRectangle);

            //base.OnPaintBackground(e);
        }

        private void DrawTitle(Graphics g)
        {
            var x = 6 + this.GetBorderWidth();
            if (this.ShowLogo)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                ImageAttributes imgAtt = new ImageAttributes();
                imgAtt.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                using (var image = this.Icon.ToBitmap())
                {
                    var rec = new Rectangle(x, (this.captionHeight - 24) / 2, 24, 24);

                    g.DrawImage(image, rec, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imgAtt);
                }

            }

            if (this.ShowTitle)
            {
                var font = this.titleFont == null ? this.Font : this.titleFont;
                var fontSize = Size.Ceiling(g.MeasureString(this.Text, font));
                if (this.CenterTitle)
                {
                    x = (this.Width - fontSize.Width) / 2;
                }
                else if (this.ShowLogo)
                {
                    x += 30;
                }

                using (var brush = new SolidBrush(this.CaptionForeColor))
                {
                    g.DrawString(this.Text, font, brush, x, (this.CaptionHeight - fontSize.Height) / 2 + this.GetBorderWidth());
                }
            }
        }

        private int GetBorderWidth()
        {
            return this.BorderStyle != ButtonBorderStyle.None && !this.BorderColor.IsEmpty ? this.BorderWidth : 0;
        }
        private void DrawControlBox(Graphics g)
        {
            if (this.ControlBox)
            {
                ImageAttributes ImgAtt = new ImageAttributes();
                ImgAtt.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                var x = this.Width - 32;
                //var rec = new Rectangle(this.Width - 32, (this.CaptionHeight - 32) / 2 + this.BorderWidth, 32, 32);
                //var rec = new Rectangle(x, this.BorderWidth, 32, 32);
                if (this.CloseButtonImage != null)
                {
                    closeRect = new Rectangle(x, 0, 32, 32);
                    using (var brush = new SolidBrush(closeHover ? this.ControlActivedColor : this.ControlBackColor))
                    {
                        g.FillRectangle(brush, closeRect);
                    }

                    g.DrawImage(this.CloseButtonImage, closeRect, 0, 0, this.CloseButtonImage.Width, this.CloseButtonImage.Height, GraphicsUnit.Pixel, ImgAtt);
                    x -= 32;
                }

                if (this.MaximizeBox && this.WindowState == FormWindowState.Maximized && this.MaximumNormalButtonImage != null)
                {
                    maxRect = new Rectangle(x, 0, 32, 32);

                    using (var brush = new SolidBrush(maxHover ? this.ControlActivedColor : this.ControlBackColor))
                    {
                        g.FillRectangle(brush, maxRect);
                    }

                    g.DrawImage(this.MaximumNormalButtonImage, maxRect, 0, 0, this.MaximumNormalButtonImage.Width, this.MaximumNormalButtonImage.Height, GraphicsUnit.Pixel, ImgAtt);
                    x -= 32;
                }
                else if (this.MaximizeBox && this.WindowState != FormWindowState.Maximized && this.MaximumButtonImage != null)
                {
                    maxRect = new Rectangle(x, 0, 32, 32);
                    using (var brush = new SolidBrush(maxHover ? this.ControlActivedColor : this.ControlBackColor))
                    {
                        g.FillRectangle(brush, maxRect);
                    }
                    g.DrawImage(this.MaximumButtonImage, maxRect, 0, 0, this.MaximumButtonImage.Width, this.MaximumButtonImage.Height, GraphicsUnit.Pixel, ImgAtt);
                    x -= 32;
                }

                if (this.MinimizeBox && this.MinimumButtonImage != null)
                {
                    minRect = new Rectangle(x, 0, 32, 32);

                    using (var brush = new SolidBrush(minHover ? this.ControlActivedColor : this.ControlBackColor))
                    {
                        g.FillRectangle(brush, minRect);
                    }
                    g.DrawImage(this.MinimumButtonImage, minRect, 0, 0, this.MinimumButtonImage.Width, this.MinimumButtonImage.Height, GraphicsUnit.Pixel, ImgAtt);
                    x -= 32;
                }

                if (base.HelpButton && this.HelpButtonImage != null)
                {
                    helpRect = new Rectangle(x, 0, 32, 32);
                    using (var brush = new SolidBrush(helpHover ? this.ControlActivedColor : this.ControlBackColor))
                    {
                        g.FillRectangle(brush, helpRect);
                    }
                    g.DrawImage(this.HelpButtonImage, helpRect, 0, 0, this.HelpButtonImage.Width, this.HelpButtonImage.Height, GraphicsUnit.Pixel, ImgAtt);
                    x -= 32;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            #region draw caption
            using (var brush = new SolidBrush(this.CaptionBackgroundColor))
            {
                e.Graphics.FillRectangle(brush, captionRect);
            }

            this.DrawTitle(e.Graphics);
            this.DrawControlBox(e.Graphics);
            #endregion

            #region draw border
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, borderColor, ButtonBorderStyle.Solid);
            #endregion
        }

        private void SetCaptionRect()
        {
            if (!this.BorderColor.IsEmpty && this.BorderStyle != ButtonBorderStyle.None && this.BorderWidth > 0)
            {
                captionRect = new Rectangle(this.GetBorderWidth(), this.GetBorderWidth(), this.Width - this.GetBorderWidth() * 2, this.CaptionHeight);
            }
            else
            {
                captionRect = new Rectangle(0, 0, this.Width, this.CaptionHeight);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.SetCaptionRect();

            if (shadow != null)
            {
                shadow.RefreshShadow(true);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            captionHover = captionRect.Contains(p);
            if (captionHover)
            {
                closeHover = closeRect != Rectangle.Empty && closeRect.Contains(p);
                minHover = minRect != Rectangle.Empty && minRect.Contains(p);
                maxHover = maxRect != Rectangle.Empty && maxRect.Contains(p);
                helpHover = helpRect != Rectangle.Empty && helpRect.Contains(p);
                this.Invalidate(captionRect);
                this.Cursor = (closeHover || minHover || maxHover || helpHover) ? Cursors.Hand : Cursors.Default;
            }
            else
            {
                if (closeHover || minHover || maxHover || helpHover)
                {
                    this.Invalidate(captionRect);
                    closeHover = minHover = maxHover = helpHover = false;
                }

                this.Cursor = Cursors.Default;
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            // 只处理标题栏，可以拖动，其他位置不处理拖动消息
            if (this.WindowState != FormWindowState.Maximized && !this.helpHover && !this.minHover && !this.closeHover && !this.maxHover && this.captionHover)
            {
                Win32.ReleaseCapture();
                Win32.SendMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MOVE + Win32.HTCAPTION, 0);//*********************调用移动无窗体控件函数  
            }

            base.OnMouseDown(e);
        }

        #region 处理无边框窗体最大化遮住任务栏的问题
        private Size nomalSize;
        private Point nomalPoint;
        private FormWindowState windowState;
        public new FormWindowState WindowState
        {
            get { return this.windowState; }
            set
            {
                if (!DesignMode)
                {
                    if (value == FormWindowState.Maximized)
                    {
                        this.nomalSize = this.Size;
                        this.nomalPoint = this.Location;

                        this.Location = new Point(0, 0);
                        if (this.MaxToFullScreen)
                        {
                            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                        }
                        else
                        {
                            this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                        }

                        this.Invalidate(this.captionRect);
                    }
                    else if (value == FormWindowState.Normal)
                    {
                        this.Size = nomalSize;
                        this.Location = nomalPoint;
                        this.Invalidate(this.captionRect);
                    }
                    else if (value == FormWindowState.Minimized)
                    {
                        base.WindowState = value;
                    }
                }

                this.windowState = value;
            }
        }
        #endregion

        #region 处理自绘按钮点击事件
        protected override void OnMouseClick(MouseEventArgs e)
        {
            var point = new Point(e.X, e.Y);
            if (this.closeRect != Rectangle.Empty && this.closeRect.Contains(point))
            {
                this.Close();
                return;
            }

            if (!this.maxRect.IsEmpty && this.maxRect.Contains(point))
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    this.WindowState = FormWindowState.Maximized;
                }

                this.maxHover = false;
                return;
            }

            if (!this.minRect.IsEmpty && this.minRect.Contains(point))
            {
                this.WindowState = FormWindowState.Minimized;
                this.minHover = false;
                return;
            }

            if (!this.helpRect.IsEmpty && this.helpRect.Contains(point))
            {
                this.helpHover = false;
                this.Invalidate(this.captionRect);
                CancelEventArgs ce = new CancelEventArgs();
                base.OnHelpButtonClicked(ce);
                return;
            }

            base.OnMouseClick(e);
        }
        #endregion
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DForm";
            this.ResumeLayout(false);
        }

        #region 调整窗口大小
        const int Guying_HTLEFT = 10;
        const int Guying_HTRIGHT = 11;
        const int Guying_HTTOP = 12;
        const int Guying_HTTOPLEFT = 13;
        const int Guying_HTTOPRIGHT = 14;
        const int Guying_HTBOTTOM = 15;
        const int Guying_HTBOTTOMLEFT = 0x10;
        const int Guying_HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            if (this.closeHover || this.minHover || this.maxHover || this.helpHover)
            {
                base.WndProc(ref m);
                return;
            }

            if (!this.CustomResizeable)
            {
                base.WndProc(ref m);
                return;
            }
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                        else m.Result = (IntPtr)Guying_HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)Guying_HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)Guying_HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)Guying_HTBOTTOM;
                    break;
                case 0x0201:                //鼠标左键按下的消息   
                    m.Msg = 0x00A1;         //更改消息为非客户区按下鼠标   
                    m.LParam = IntPtr.Zero; //默认值   
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内   
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.shadow != null)
            {
                this.shadow.Visible = this.Visible;
            }
            base.OnVisibleChanged(e);
        }

        private Panel maskLayer;
        private DRolling rollingBar;
        private Image loadingBack;

        public void StartLoading(ERollingBarStyle style = ERollingBarStyle.Default)
        {
            // 对当前界面复制快照图片
            loadingBack = new Bitmap(this.Width, this.Height);
            Graphics graphic = Graphics.FromImage(loadingBack);
            graphic.CopyFromScreen(new Point(this.Location.X, this.Location.Y + this.CaptionHeight), new Point(0, 0), new Size(this.Width, this.Height - this.CaptionHeight));
            graphic.Dispose();
            if (maskLayer == null)
            {
                maskLayer = new Panel();
                maskLayer.Size = new Size(this.Width, this.Height - this.CaptionHeight);
                maskLayer.Location = new Point(0, this.CaptionHeight + 1);
                this.Controls.Add(maskLayer);
                rollingBar = new DRolling();
                rollingBar.Size = new System.Drawing.Size(60, 60);
                rollingBar.Location = new System.Drawing.Point((this.maskLayer.Width - 60) / 2, (this.maskLayer.Height - 60) / 2 - this.CaptionHeight);
                rollingBar.SliceColor = Color.FromArgb(231, 76, 60);
                rollingBar.RadiusOut = 38;
                rollingBar.SliceNumber = 12;
                rollingBar.Location = new System.Drawing.Point((this.maskLayer.Width - 60) / 2, (this.maskLayer.Height - 60) / 2 - this.CaptionHeight);
                this.maskLayer.Controls.Add(rollingBar);
            }
            rollingBar.RollingStyle = style;
            rollingBar.RadiusIn = style == ERollingBarStyle.Default ? 20 : 38;
            rollingBar.PenWidth = style == ERollingBarStyle.Default ? 3 : 5;
            this.maskLayer.Visible = true;
            this.maskLayer.BringToFront();
            this.maskLayer.BackgroundImage = loadingBack;
            this.rollingBar.Visible = true;
            this.rollingBar.BringToFront();
            this.rollingBar.Start();
        }

        public void StopLoading()
        {
            if (this.maskLayer != null)
            {
                loadingBack.Dispose();
                loadingBack = null;
                this.maskLayer.BackgroundImage = null;
                this.maskLayer.Visible = false;
                this.rollingBar.Visible = false;
                this.rollingBar.Stop();
            }
        }
    }
}
