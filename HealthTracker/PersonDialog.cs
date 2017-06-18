using System.Windows.Forms;
using HealthTracker.Data;
using System;
using HealthTracker.API;

namespace HealthTracker
{
    public partial class PersonDialog : Form
    {
        public PersonDialog()
        {
            InitializeComponent();
            birthDayCtrl.Value = DateTime.Now - TimeSpan.FromDays(18 * 365);
            sexBox.Items.AddRange(new object[] { Sex.Male, Sex.Female });
            sexBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public Person Person { get; private set; }

        private void okBtn_Click(object sender, EventArgs e)
        {
            var person = new Person()
            {
                Birthday = birthDayCtrl.Value.Date,
                FirstName = firstNameCtrl.Text?.Trim(),
                LastName = lastNameCtrl.Text?.Trim(),
                Sex = (Sex)(sexBox.SelectedItem ?? -1),
                Height = heightBar.Value
            };
            if (person.Birthday >= DateTime.MinValue
                && !string.IsNullOrWhiteSpace(person.FirstName)
                && !string.IsNullOrWhiteSpace(person.LastName)
                && (person.Sex == Sex.Male || person.Sex == Sex.Female)
                && person.Height >= 150)
                Person = person;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void heightBar_ValueChanged(object sender, EventArgs e)
        {
            heightVal.Text = $"{heightBar.Value} cm";
        }
    }
}