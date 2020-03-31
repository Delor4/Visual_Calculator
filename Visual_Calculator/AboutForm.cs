using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visual_Calculator
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            label2.Text = "Wersja " + Properties.Resources.app_version;
        }
    }
}
