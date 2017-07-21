using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RpgLibrary;
using XRpgLibrary;

namespace RpgEditor
{
    public partial class FormNewGame : Form
    {
        #region Field Region

        RolePlayingGame rpg;

        #endregion

        #region Property Region

        public RolePlayingGame RolePlayingGame
        {
            get { return rpg; }
        }

        #endregion

        #region Constructor Region

        public FormNewGame()
        {
            InitializeComponent();
            btnOK.Click += new EventHandler(btnOK_Click);
        }

        #endregion

        #region Event Handler Region

        void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrEmpty(tbDescription.Text))
            {
                MessageBox.Show("You must enter a name and a description.", "Error");
                return;
            }

            rpg = new RolePlayingGame(tbName.Text, tbDescription.Text);

            this.Close();
        }

        #endregion
    }
}
