using HealthTracker.API;
using System;
using System.Windows.Forms;

namespace HealthTracker
{
    public partial class MainForm : Form
    {
        private readonly IHealthHub hub;

        public MainForm()
        {
            InitializeComponent();
            Type hubType;
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                hubType = Type.GetType("HealthTracker.Linux.LinuxHealthHub, HealthTracker.Linux");
            else
                hubType = Type.GetType("HealthTracker.WinRT.WindowsHealthHub, HealthTracker.WinRT");
            hub = (IHealthHub)Activator.CreateInstance(hubType);
            hub.OnHealthEvent += Hub_OnHealthEvent;
            FormClosed += MainForm_FormClosed;
        }

        private void Hub_OnHealthEvent(IHealthHub hub, IDataEvent data)
        {
            currentMeasure.BeginInvoke((Action)(() =>
            {
                currentMeasure.Text = $"{data.Data} {data.Unit} ({(data.Status == DataKind.Final ? "!" : "~")})";
            }));
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            hub.Dispose();
        }
    }
}