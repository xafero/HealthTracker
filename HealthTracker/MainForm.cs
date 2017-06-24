using HealthTracker.API;
using HealthTracker.Core;
using HealthTracker.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace HealthTracker
{
    public partial class MainForm : Form
    {
        private readonly PersonRepository persons;
        private readonly IHealthHub hub;
        private readonly IHealthHub calc;

        public MainForm()
        {
            InitializeComponent();
            
            hub.OnHealthEvent += Hub_OnHealthEvent;
            persons = new PersonRepository();
            var person = persons.List().LastOrDefault();
            if (person == null)
            {
                person = Accounts.GetFirstPerson();
                if (person == null)
                {
                    MessageBox.Show("Can't continue without at least one person!");
                    Environment.Exit(-1);
                }
                persons.Save(person);
            }
            var calcer = new CalculateHub(person);
            hub.OnHealthEvent += calcer.OnRawHealthEvent;
            calcer.OnHealthEvent += Hub_OnHealthEvent;
            calc = calcer;
            FormClosed += MainForm_FormClosed;
        }

        private void Hub_OnHealthEvent(IHealthHub hub, IDataEvent data)
        {
            if (!currentMeasure.IsHandleCreated)
                return;
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