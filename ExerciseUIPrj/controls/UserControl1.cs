using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExerciseUIPrj
{
    public partial class UserControl1 : UserControl
    {
        private List<string> Context = new List<string>();
        private Font font = DefaultFont;
        public UserControl1()
        {
            InitializeComponent();
            Context = new List<string> { "this is a test string!", "    这是一句测试字符串", "       这是一句测试语句     " };
            textBox1.Visible = false;
            
        }

        public void Edit()
        {
            textBox1.Visible = true;
            if (Context.Count > 0) textBox1.Text = Context[0];
            textBox1.Location = new Point(2, 0);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var gs = e.Graphics;
            Point pt = new Point(0, 0);
            foreach (string s in Context)
            {
                gs.DrawString(s, font, Brushes.Black, pt);
                pt.Y += font.Height;
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int y = (e.Y % font.Height == 0)? e.Y / font.Height - 1 : e.Y / font.Height;
            int x = e.X % ((int)font.Height);
            textBox1.Location = new Point(2, y*font.Height);
            textBox1.Text = (y < Context.Count) ? Context[y] : "";
            textBox1.Visible = true;
            textBox1.Select(x, 0);

        }

        public override void Refresh()
        {
            base.Refresh();
        }
        
       
    }
}
