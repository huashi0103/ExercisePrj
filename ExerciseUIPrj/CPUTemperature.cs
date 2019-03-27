using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace ExerciseUIPrj
{
    public partial class CPUTemperature : Form
    {
        public CPUTemperature()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }


        private dynamic GetTemperature()
        {
            List<dynamic> result = new List<dynamic>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
            foreach (ManagementObject obj in searcher.Get())
            {
                Double temp = Convert.ToDouble(obj["CurrentTemperature"].ToString());
                result.Add(new { CurrentValue = temp, InstanceName = obj["InstanceName"].ToString() });
            }
            return result;
        }
    }
}
