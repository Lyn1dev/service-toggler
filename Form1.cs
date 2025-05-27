using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Security.Principal;
using System.Reflection;

namespace ServiceToggler;

public partial class Form1 : Form
{
    private ListView listViewServices;
    private Label lblStatus;
    private Button btnSave;

    public Form1()
    {
        InitializeComponent();
        this.Load += Form1_Load;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        listViewServices.Columns.Add("Service Name", 150);
        listViewServices.Columns.Add("Status", 80);
        listViewServices.CheckBoxes = true;
        string[] services = { "pcasvc", "sysmain", "dps", "CDPU", "eventlog", "dcomlaunch", "BAM", "Dusmsvc" };
        foreach (string serviceName in services)
        {
            AddServiceToList(serviceName);
        }
    }

    private void AddServiceToList(string serviceName)
    {
        try
        {
            using (ServiceController sc = new ServiceController(serviceName))
            {
                ListViewItem item = new ListViewItem(serviceName);
                item.SubItems.Add(sc.Status.ToString());
                item.Checked = (sc.Status == ServiceControllerStatus.Running);
                listViewServices.Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Error checking {serviceName}: {ex.Message}";
        }
    }

    private void OnSaveButtonClick(object sender, EventArgs e)
    {
        if (!IsAdministrator())
        {
            // Restart program and run as admin
            var processInfo = new System.Diagnostics.ProcessStartInfo(Assembly.GetEntryAssembly().CodeBase);

            // The following properties run the new process as administrator
            processInfo.UseShellExecute = true;
            processInfo.Verb = "runas";

            // Start the new process
            try
            {
                System.Diagnostics.Process.Start(processInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"This program must be run as an administrator! \n\nError: {ex.Message}");
            }

            // Shut down the current process
            Application.Exit();
        }
        else
        {
            foreach (ListViewItem item in listViewServices.Items)
            {
                string serviceName = item.Text;
                try
                {
                    using (ServiceController sc = new ServiceController(serviceName))
                    {
                        if (item.Checked)
                        {
                            if (sc.Status == ServiceControllerStatus.Stopped || sc.Status == ServiceControllerStatus.Paused)
                            {
                                sc.Start();
                                sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                                lblStatus.Text = $"Started {serviceName}";
                                item.SubItems[1].Text = sc.Status.ToString();
                            }
                            else
                            {
                                lblStatus.Text = $"{serviceName} is already running.";
                            }
                        }
                        else
                        {
                            if (sc.Status == ServiceControllerStatus.Running)
                            {
                                try
                                {
                                    sc.Stop();
                                    sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                                    lblStatus.Text = $"Stopped {serviceName}";
                                    item.SubItems[1].Text = sc.Status.ToString();
                                }
                                catch (Exception ex)
                                {
                                    lblStatus.Text = $"Error stopping {serviceName}: {ex.Message}";
                                }
                            }
                            else
                            {
                                lblStatus.Text = $"{serviceName} is already stopped.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblStatus.Text = $"Error: {ex.Message}";
                }
            }
        }
    }

    private bool IsAdministrator()
    {
        return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
    }
}
