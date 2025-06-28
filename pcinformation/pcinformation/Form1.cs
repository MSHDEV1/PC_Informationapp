using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Management.Instrumentation;
using System.Diagnostics;
using System.Net.Configuration;
using System.Net;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace pcinformation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            label1.Text = SystemInformation.PrimaryMonitorSize.ToString();
            label2.Text = SystemInformation.ComputerName.ToString();
            label3.Text = SystemInformation.MonitorCount.ToString();
            label4.Text = Environment.SystemDirectory.ToString();
            label5.Text = Environment.OSVersion.ToString();
            label6.Text = Environment.ProcessorCount.ToString();
            //İşlemci Model Bulma
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            // Anakart Model Bulma
            ManagementObjectSearcher motherboardv2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
            ManagementObjectSearcher motherboard = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_MotherboardDevice");
            ManagementObjectSearcher ram = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
            ManagementObjectSearcher ram2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher user = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Account");
            foreach (ManagementObject mo in mos.Get())
            {
                //İşlemci Model Bulma
                label14.Text = mo["Name"].ToString();   
            }
            foreach(ManagementObject mother in motherboardv2.Get())
            {
                //Anakart Model Bulma Foreach Döngüsü
                label20.Text = mother["Product"].ToString();
                
            }
            foreach (ManagementObject mother2 in motherboard.Get())
            {
                label22.Text = mother2["PrimaryBusType"].ToString();
               
            }
            foreach (ManagementObject ramv2 in ram.Get())
            {
                label23.Text = ramv2["Speed"].ToString() + "MHz";
                
            }
            foreach (ManagementObject ramv3 in ram2.Get())
            {

                double total = Convert.ToDouble(ramv3["TotalVisibleMemorySize"]) / (1024.0*1024.0);
                label25.Text = total.ToString("0.00" + "GB");
                label27.Text = ramv3["OSArchitecture"].ToString();
                label29.Text = ramv3["Caption"].ToString();
            }


            string host = Dns.GetHostName();
            label16.Text = Dns.GetHostByName(host).AddressList[0].ToString();
            label17.Text = Dns.GetHostByName(host).AddressList[1].ToString();   
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ManagementObjectSearcher ram2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject ramv3 in ram2.Get())
            {
                double total = Convert.ToDouble(ramv3["TotalVisibleMemorySize"]) / (1024.0 * 1024.0);
                double totalv2 = Convert.ToDouble(ramv3["FreePhysicalMemory"]) / (1024.0 *1024.0);
                double totalv3 = total - totalv2;
                label32.Text = ramv3["NumberOfProcesses"].ToString();
                label34.Text = totalv3.ToString("0.00" +"GB");
                double ram =Convert.ToDouble(ramv3["FreePhysicalMemory"]) / (1024.0*1024.0);
                label36.Text = ram.ToString("0.000"+"GB");
            }
        }
    }
}