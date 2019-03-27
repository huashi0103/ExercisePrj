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
using Shell32;
using stdole;
using SHDocVw;
using System.Runtime.InteropServices;
using System.IO;
using static ExerciseUIPrj.WinAPI;
using ExerciseUIPrj.controls;

namespace ExerciseUIPrj
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            xButton1.Click += delegate { MessageBox.Show("this is a circle button"); };
            //  button1.Click += delegate { xButton1.Enabled = xButton1.Enabled ? false : true; };

            for (int i = 0; i < 200; i++)
            {
                CustomControl1 cc = new CustomControl1();
                cc.Size = new Size(flowLayoutPanel1.Width-30, 73);
                flowLayoutPanel1.Controls.Add(cc);
            }

        }

        [DllImport("shell32.dll")]
        public static extern Int32 SHGetFolderLocation(
            IntPtr hwndOwner,         // Handle to the owner window.
            Int32 nFolder,            // A CSIDL value that identifies the folder to be
            // located.
            IntPtr hToken,            // Token that can be used to represent a particular
            // user.
            UInt32 dwReserved,        // Reserved.
            out IntPtr ppidl);        // Address of a pointer to an item identifier list
        // structure 
        // specifying the folder's location relative to the
        // root of the namespace 
        // (the desktop). 
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
    
        }
        void a(string msg)
        {
            textBox1.AppendText(msg);
        }


        void Test()
        {
            ShellWindows shellWindows = new ShellWindows();
            try
            {
                foreach (IWebBrowser2 win in shellWindows)
                {
                    WinAPI.IServiceProvider sp = win as WinAPI.IServiceProvider;
                    object sb;
                    sp.QueryService(SID_STopLevelBrowser, typeof(IShellBrowser).GUID, out sb);
                    

                    IShellBrowser shellBrowser = (IShellBrowser)sb;
                    IShellView sv;
                    shellBrowser.QueryActiveShellView(out sv);
                    Console.WriteLine(win.LocationURL + " " + win.LocationName);
                    IFolderView fv = sv as IFolderView;
                    
                    if (fv != null)
                    {
 

                        Timer t = new Timer();
                        t.Interval = 200;
                        t.Tick += (o, se) => {
                            int fitem;
                            if (0 == fv.GetFocusedItem(out fitem))
                            {
                                IntPtr pid;
                                if(0==fv.Item(fitem, out pid))
                                {
                                    //invokea(pid);
                                    
                                }
                      
                                Marshal.FreeHGlobal(pid);

                            }
                        };
                        t.Start();

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (shellWindows != null)
                {
                    Marshal.ReleaseComObject(shellWindows);
                }
            }
        }

        void invokea(object msg)
        {
            Invoke(new EventHandler(delegate {
                a(msg);
            }));
        }
        void a(object msg)
        {
            string s = msg.GetType() == typeof(string) ? msg as string : msg.ToString();
            textBox1.AppendText(s);
        }
    }
}
