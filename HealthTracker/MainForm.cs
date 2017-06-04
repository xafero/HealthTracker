using HealthTracker.API;
using HealthTracker.Linux;
using HealthTracker.WinRT;
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
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                hub = new LinuxHealthHub();
            else
                hub = new WindowsHealthHub();
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