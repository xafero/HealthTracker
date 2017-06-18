using HealthTracker.Data;
using System.Windows.Forms;

namespace HealthTracker
{
    internal static class Accounts
    {
        internal static Person GetFirstPerson()
        {
            var dialog = new PersonDialog();
            return dialog.ShowDialog() == DialogResult.OK ? dialog.Person : null;
        }
    }
}