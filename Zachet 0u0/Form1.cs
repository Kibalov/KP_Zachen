using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Zachet_0u0
{
    public partial class Form1 : Form
    {
        Figure[] figures = new Figure[30];
        int tempfigind;
        bool move;
        int[] deltamove = new int[2];
        RedactorForm redactor;
        bool saved = false;
        public Form1()
        {
            MessageBox.Show("Честивое орудие", "АХхахаХхАхаХ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InitializeComponent();
            redactor = new RedactorForm(this);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                move = false;
                for (int i = 29; i >= 0; i--)
                {
                    if (figures[i] != null)
                        if (figures[i].ClickFigureCheck(e.X, e.Y))
                        {
                            figures[tempfigind].Click(false);
                            tempfigind = i;
                            figures[i].Click(true);
                            deltamove[0] = e.X - figures[i].x;
                            deltamove[1] = e.Y - figures[i].y;
                            move = true;
                            break;
                        }
                }
                if (figures[tempfigind] != null)
                    if (move == false)
                        figures[tempfigind].Click(false);
                Refresh();
            }
            else if (e.Button == MouseButtons.Right)
            {
                figures[tempfigind].Click(false);
                for (int i = 29; i >= 0; i--)
                {
                    if (figures[i] != null)
                        if (figures[i].ClickFigureCheck(e.X, e.Y))
                        {
                            redactor.SetFigure(figures[i]);
                            redactor.Show();
                            saved = false;
                            break;
                        }

                }
                Refresh();
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                if (figures[tempfigind] != null)
                    figures[tempfigind].SetCoordinates(e.X - deltamove[0], e.Y - deltamove[1]);
                Refresh();
                saved = false;
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < figures.Length; i++)
            {
                if (figures[i] != null)
                    figures[i].Draw(e.Graphics);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int k = 0;
            for (int i = 0; i < figures.Length; i++)
            {
                if (figures[i] != null)
                    if (figures[i].type == "rec")
                        k++;
            }
            for (int i = 0; i < figures.Length; i++)
            {
                if (k < numericUpDown1.Value)
                {
                    if (figures[i] == null)
                    {
                        figures[i] = new RectangleMy(this, Width, Height, k);
                        k++;
                        Refresh();
                    }
                }
                else
                    break;
            }
            for (int i = 29; i >= 0; i--)
                if (k > numericUpDown1.Value)
                {
                    if (figures[i] != null)
                    {
                        if (figures[i].type == "rec")
                        {
                            figures[i].Removech();
                            figures[i] = null;
                            k--;
                            Refresh();
                        }
                    }
                }
                else
                    break;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int k = 0;
            for (int i = 0; i < figures.Length; i++)
            {
                if (figures[i] != null)
                    if (figures[i].type == "elli")
                        k++;
            }
            for (int i = 0; i < figures.Length; i++)
            {
                if (k < numericUpDown2.Value)
                {
                    if (figures[i] == null)
                    {
                        figures[i] = new EllipseMy(this, Width, Height, k);
                        k++;
                        Refresh();
                    }
                }
                else
                    break;
            }
            for (int i = 29; i >= 0; i--)
                if (k > numericUpDown2.Value)
                {
                    if (figures[i] != null)
                    {
                        if (figures[i].type == "elli")
                        {
                            figures[i].Removech();
                            figures[i] = null;
                            k--;
                            Refresh();
                        }
                    }
                }
                else
                    break;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            int k = 0;
            for (int i = 0; i < figures.Length; i++)
            {
                if (figures[i] != null)
                    if (figures[i].type == "trac")
                        k++;
            }
            for (int i = 0; i < figures.Length; i++)
            {
                if (k < numericUpDown3.Value)
                {
                    if (figures[i] == null)
                    {
                        figures[i] = new TractorMy(this, Width, Height, k);
                        k++;
                        Refresh();
                    }
                }
                else
                    break;
            }
            for (int i = 29; i >= 0; i--)
                if (k > numericUpDown3.Value)
                {
                    if (figures[i] != null)
                    {
                        if (figures[i].type == "trac")
                        {
                            figures[i].Removech();
                            figures[i] = null;
                            k--;
                            Refresh();
                        }
                    }
                }
                else
                    break;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveArt();
        }
        private void SaveArt()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            StreamWriter streamWriter = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "save.txt"));
            for (int i = 0; i < figures.Length; i++)
            {
                if (figures[i] != null)
                {
                    streamWriter.WriteLine(figures[i].type);
                    streamWriter.WriteLine(figures[i].checkBox.Checked);
                    streamWriter.WriteLine(figures[i].w);
                    streamWriter.WriteLine(figures[i].h);
                    streamWriter.WriteLine(figures[i].x);
                    streamWriter.WriteLine(figures[i].y);
                    streamWriter.WriteLine(figures[i].color.ToArgb());
                    if (figures[i].type == "trac")
                    {
                        streamWriter.WriteLine(figures[i].traccolor[0, 0].ToArgb());
                        streamWriter.WriteLine(figures[i].traccolor[0, 1].ToArgb());
                        streamWriter.WriteLine(figures[i].traccolor[0, 2].ToArgb());
                        streamWriter.WriteLine(figures[i].traccolor[0, 3].ToArgb());
                    }
                }
                else
                    streamWriter.WriteLine("null");
            }
            streamWriter.Close();
            button1.Enabled = true;
            button2.Enabled = true;
            saved = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "save.txt")))
            {
                button2.Enabled = false;
                button1.Enabled = false;
                for (int i = figures.Length-1; i >= 0; i--)
                    if (figures[i] != null)
                    {
                        figures[i].Removech();
                        figures[i] = null;
                        Refresh();
                    }
                StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "save.txt"));
                int re = 0, el = 0, tr = 0;
                string type = "";
                for (int i = 0; i < figures.Length; i++)
                {
                    type = sr.ReadLine();
                    if (type != "null")
                    {
                        if (type == "rec")
                        {
                            figures[i] = new RectangleMy(this, Width, Height, re, Convert.ToBoolean(sr.ReadLine()));
                            figures[i].ChangeWidth(Convert.ToInt32(sr.ReadLine()));
                            figures[i].ChangeHeigth(Convert.ToInt32(sr.ReadLine()));
                            figures[i].SetCoordinates(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                            figures[i].ChangeColor(Color.FromArgb(Convert.ToInt32(sr.ReadLine())), -1, true);
                            re++;
                        }
                        else if (type == "elli")
                        {
                            figures[i] = new EllipseMy(this, Width, Height, el, Convert.ToBoolean(sr.ReadLine()));
                            figures[i].ChangeWidth(Convert.ToInt32(sr.ReadLine()));
                            figures[i].ChangeHeigth(Convert.ToInt32(sr.ReadLine()));
                            figures[i].SetCoordinates(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                            figures[i].ChangeColor(Color.FromArgb(Convert.ToInt32(sr.ReadLine())), -1, true);
                            el++;
                        }
                        else if (type == "trac")
                        {
                            figures[i] = new TractorMy(this, Width, Height, tr, Convert.ToBoolean(sr.ReadLine()));
                            figures[i].ChangeWidth(Convert.ToInt32(sr.ReadLine()));
                            figures[i].ChangeHeigth(Convert.ToInt32(sr.ReadLine()));
                            figures[i].SetCoordinates(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                            figures[i].ChangeColor(Color.FromArgb(Convert.ToInt32(sr.ReadLine())), -1, true);

                            figures[i].ChangeColor(Color.FromArgb(Convert.ToInt32(sr.ReadLine())), 0, true);
                            figures[i].ChangeColor(Color.FromArgb(Convert.ToInt32(sr.ReadLine())), 1, true);
                            figures[i].ChangeColor(Color.FromArgb(Convert.ToInt32(sr.ReadLine())), 2, true);
                            figures[i].ChangeColor(Color.FromArgb(Convert.ToInt32(sr.ReadLine())), 3, true);
                            tr++;
                        }
                        Refresh();
                    }
                }
                if (re != 0)
                    numericUpDown1.Value = re;
                if (el != 0)
                    numericUpDown2.Value = el;
                if (tr != 0)
                    numericUpDown3.Value = tr;
                button2.Enabled = true;
                button1.Enabled = true;
                sr.Close();
                Refresh();
            }
            else
                MessageBox.Show("Файла нет :(", "Бибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("САМОЕ ЧЕСТИВОЕ ОРУДИЕ В МИРЕ v0.95!!! \na.k.a. HONEST UNBELIVABLE EGG PILL SCULPTURE v0.95 \n(HUEPS v0.95)\nДанная программа предназначена для спасения нечестивого люда земного (за пять тыщ). И да прибудет со спасаемыми удача, и да прибудет снисхождение богов наших. И да низвергнет бог того, кто подношением принебрег (пять тыщ), ибо возрадуется люд простой.\nОмон!", "ИнфоИнфоИнфоИнфоИнфоИнфоИнфоИнфоИнфоИнфоИнфо", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите закрыть сей шедевр?", "Выхода нет", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                e.Cancel = true;
            else if (!saved)
                {
                    result = MessageBox.Show("Желаете ли сохранить ваше творение искусства?", "Выхода нет", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                        SaveArt();
                    else if (result == DialogResult.Cancel)
                        e.Cancel = true;
                }
        }
    }
}
