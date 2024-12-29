using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace bird2
{
    public partial class MenuForm : Form
    {
        private Dictionary<string, Image> birdSkins = new Dictionary<string, Image>();

        public MenuForm()
        {
            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            birdSkins["Default"] = Properties.Resources.yellowbird;
            birdSkins["Red Bird"] = Properties.Resources.redbird;
            birdSkins["Blue Bird"] = Properties.Resources.bluebird;
            birdSkins["Pink Bird"] = Properties.Resources.pink2;

            BirdSkins.Items.AddRange(birdSkins.Keys.ToArray());
            BirdSkins.SelectedIndex = 0;
        }
        private void Start_Click(object sender, EventArgs e)
        {
            if (BirdSkins.SelectedItem == null)
            {
                MessageBox.Show("Please select a bird skin!");
                return; 
            }
            string selectedSkin = BirdSkins.SelectedItem.ToString();
            Form1 gameForm = new Form1(selectedSkin);
            gameForm.Show();
            this.Hide();
        }

    }
}