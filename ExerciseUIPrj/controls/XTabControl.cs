using System.Windows.Forms;
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
    public partial class XTabControl : TabControl
    {
        Size defaultSize = new Size();
        const int dheight = 10;
        public XTabControl()
        {
            InitializeComponent();
            base.SetStyle(
                     ControlStyles.UserPaint |                      // 控件将自行绘制，而不是通过操作系统来绘制  
                     ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
                     ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
                     ControlStyles.ResizeRedraw |                   // 在调整控件大小时重绘控件  
                     ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明  
                     true);                                         // 设置以上值为 true  
           base.UpdateStyles();
            this.SizeMode = TabSizeMode.Fixed;  // 大小模式为固定  
            FontChanged += XTabControl_FontChanged;
            defaultSize = TextRenderer.MeasureText("tabpage1", Font);
            Margin = new System.Windows.Forms.Padding(0);
            Padding = new Point(0, 0);
            Appearance = TabAppearance.Normal;
            SetItemSize();
        }
        //public override Rectangle DisplayRectangle
        //{
        //    get
        //    {
        //        Rectangle rect = base.DisplayRectangle;
        //        return new Rectangle(rect.Left - 2, rect.Top-2, rect.Width + 4, rect.Height + 4);
        //    }
        //}
        public void SetItemSize()
        {
            if (Width >= 0 && TabCount > 0)
            {
                if (Alignment ==TabAlignment.Top || Alignment == TabAlignment.Bottom)
                {
                    int width = (int)((Size.Width - 5) / (double)this.TabCount);
                    this.ItemSize = new Size(width, defaultSize.Height + dheight);   // 设定每个标签的尺寸 
                }
                else
                {
                    int width = defaultSize.Width;
                    foreach (TabPage t in TabPages)
                    {
                        var w = TextRenderer.MeasureText(t.Name, Font).Width;
                        width = w > width ? w : width;
                    }
                    width = width + dheight;
                    this.ItemSize = new Size(defaultSize.Height + dheight, width);   // 设定每个标签的尺寸 
                }

            }
        }

        private void XTabControl_FontChanged(object sender, EventArgs e)
        {
            defaultSize = TextRenderer.MeasureText("tabpage1", Font);
            SetItemSize();
        }
        Color dbackColor = Color.FromArgb(220, 220, 220);
        protected override void OnPaint(PaintEventArgs pe)
        {
            var g = pe.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.FillRectangle(new SolidBrush(Color.White), pe.ClipRectangle);
            Rectangle rect = new Rectangle();
            
            if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)
            {
                rect = new Rectangle(0,0, ItemSize.Height+2, Height);
                g.FillRectangle(new SolidBrush(dbackColor), rect);
            }
            else
            {
                 rect = new Rectangle(0, 0, Width, ItemSize.Height+2);
               // g.FillRectangle(new SolidBrush(dbackColor), rect);
            }

            for (int i = 0; i < this.TabCount; i++)
            {
                Rectangle bounds = this.GetTabRect(i);
                if (SelectedIndex == i)
                {
                    g.FillRectangle(new SolidBrush(Color.White), bounds);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(dbackColor), bounds);
                }
                PointF textPoint = new PointF();
                SizeF textSize = TextRenderer.MeasureText(this.TabPages[i].Text, this.Font);
                textPoint.X= bounds.X + (bounds.Width - textSize.Width) / 2;
                textPoint.Y = bounds.Y + (bounds.Height - textSize.Height) / 2;
                g.DrawString( this.TabPages[i].Text,   this.Font, SystemBrushes.ControlText, textPoint.X,  textPoint.Y);
            }
        }
    }
}
