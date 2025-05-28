using System.ServiceProcess;
using System.Security.Principal;
using System.Reflection;

namespace ServiceToggler;

public partial class Form1 : Form
{
    private ListView listViewServices;
    private TextBox txtOutputLog;
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
        listViewServices.Columns.Add("Description", 300);
        listViewServices.CheckBoxes = true;
        string[] services = { "pcasvc", "sysmain", "dps", "CDPU", "eventlog", "dcomlaunch", "BAM", "Dusmsvc" };
        foreach (string serviceName in services)
        {
            AddServiceToList(serviceName);
        }
    }

    private void AddServiceToList(string serviceName)
    {
        string description = "";
        switch (serviceName)
        {
            case "pcasvc":
                description = "Program Compatibility Assistant Service";
                break;
            case "sysmain":
                description = "Maintains and improves system performance";
                break;
            case "dps":
                description = "Enables problem detection and resolution";
                break;
            case "CDPU":
                description = "Used for Connected Devices and Universal Apps";
                break;
            case "eventlog":
                description = "Manages event logging and tracing";
                break;
            case "dcomlaunch":
                description = "Provides the launching of DCOM servers";
                break;
            case "BAM":
                description = "Moderates background activity to conserve power";
                break;
            case "Dusmsvc":
                description = "Manages data usage policies";
                break;
        }

        try
        {
            using (ServiceController sc = new ServiceController(serviceName))
            {
                ListViewItem item = new ListViewItem(serviceName);
                item.SubItems.Add(sc.Status.ToString());
                item.SubItems.Add(description);
                item.Checked = (sc.Status == ServiceControllerStatus.Running);
                listViewServices.Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            txtOutputLog.AppendText($"Error checking {serviceName}: {ex.Message}\r\n");
        }
    }

    private void OnSaveButtonClick(object sender, EventArgs e)
    {
        if (!IsAdministrator())
        {
            MessageBox.Show("This application must be run as administrator to toggle services.");
            return;
        }

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
                            txtOutputLog.AppendText($"Started {serviceName}\r\n");
                            item.SubItems[1].Text = sc.Status.ToString();
                        }
                        else
                        {
                            txtOutputLog.AppendText($"{serviceName} is already running.\r\n");
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
                                txtOutputLog.AppendText($"Stopped {serviceName}\r\n");
                                item.SubItems[1].Text = sc.Status.ToString();
                            }
                            catch (Exception ex)
                            {
                                txtOutputLog.AppendText($"Error stopping {serviceName}: {ex.Message}\r\n");
                            }
                        }
                        else
                        {
                            txtOutputLog.AppendText($"{serviceName} is already stopped.\r\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                txtOutputLog.AppendText($"Error: {ex.Message}\r\n");
            }
        }
    }

    private bool IsAdministrator()
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
}
