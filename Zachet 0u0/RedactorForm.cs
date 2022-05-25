using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zachet_0u0
{
    public partial class RedactorForm : Form
    {
        public Figure figure;
        Form1 form1;
        public RedactorForm(Form1 form)
        {
            InitializeComponent();
            //this.figure = figure;
            form1 = form;
            pictureBox1.Refresh();
        }

        public void SetFigure(Figure figure)
        {
            this.figure = figure;
            pictureBox1.Refresh();
            numericUpDown1.Minimum = 0;
            numericUpDown2.Maximum = 200;
            numericUpDown2.Value = figure.h;
            numericUpDown1.Value = figure.w;
            if (figure.type == "trac")
            {
                numericUpDown1.Minimum = figure.h / 3 * 2-2;
                numericUpDown2.Maximum = figure.w / 2 * 3+2;
            }

            button1.BackColor = figure.color;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            figure.ChangeWidth(Convert.ToInt32(numericUpDown1.Value));
            pictureBox1.Refresh();
            form1.Refresh();
            if (figure.type == "trac")
            {
                numericUpDown1.Minimum = figure.h / 3 * 2;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            figure.ChangeHeigth(Convert.ToInt32(numericUpDown2.Value));
            pictureBox1.Refresh();
            form1.Refresh();
            if (figure.type == "trac")
            {
                numericUpDown2.Maximum = figure.w / 2 * 3;
                if (numericUpDown2.Maximum > 200)
                    numericUpDown2.Maximum = 200;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
                figure.ChangeColor(colorDialog.Color);
            button1.BackColor = colorDialog.Color;
            pictureBox1.Refresh();
            form1.Refresh();
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            figure.Draw(e.Graphics, true);
        }

        private void RedactorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if (figure.type == "trac")
                {
                    int p = figure.PartNumber(e.X, e.Y);
                    if (p != -1)
                    {
                        ColorDialog colorDialog = new ColorDialog();
                        DialogResult dialogResult = colorDialog.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                            figure.ChangeColor(colorDialog.Color, p);

                        pictureBox1.Refresh();
                        form1.Refresh();
                    }
                }
        }
    }
}
