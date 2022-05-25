using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Zachet_0u0
{
    public abstract class Figure
    {
        public int x { get; protected set; }
        public int y { get; protected set; }
        public int w { get; protected set; }
        public int h { get; protected set; }
        public Color maincolor { get; protected set; }
        public Color color { get; protected set; }
        public Color taintetcolor { get; protected set; }
        public Color[,] traccolor { get; protected set; } = new Color[2, 4];
        public CheckBox checkBox { get; protected set; } = new CheckBox()
        {
            Size = new Size(15, 14),
            Text = ""
        };
        Form form;
        public string type { get; protected set; }
        public bool clicked { get; protected set; } = false;

        public Figure(Form form, int maxw, int maxh, int num, bool check = true)
        {
            this.form = form;
            Random r = new Random();
            w = r.Next(30, 100);
            h = w;
            this.x = r.Next(maxw-w);
            this.y = r.Next(maxh-h-20);

            maincolor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
            color = maincolor;
            taintetcolor = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);

            checkBox.Checked = check;
            checkBox.Click += CheckBox_Click;
            form.Controls.Add(checkBox);
        }

        private void CheckBox_Click(object sender, EventArgs e)
        {
            if (type == "trac")
            {
                if (checkBox.Checked)
                {
                    for (int i = 0; i < 4; i++)
                        traccolor[0, i] = Color.FromArgb(255, traccolor[0, i].R, traccolor[0, i].G, traccolor[0, i].B);
                }
                else
                    for (int i = 0; i < 4; i++)
                        traccolor[0, i] = Color.FromArgb(127, traccolor[0, i].R, traccolor[0, i].G, traccolor[0, i].B);
            }
            else
            {
                if (checkBox.Checked)
                    color =Color.FromArgb(255, maincolor);
                else
                    color = Color.FromArgb(125, maincolor);
            }
            form.Refresh();
        }

        public void Removech()
        {
            form.Controls.Remove(checkBox);
        }
        public void Click(bool b)
        {
            clicked = b;
        }
        public void SetCoordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public virtual void Draw(Graphics g, bool redactor = false) { }
        public abstract bool ClickFigureCheck(double x, double y);
        public abstract int PartNumber(double x, double y);
        public abstract void ChangeColor(Color color, int num = -1, bool load = false);
        public void ChangeWidth(int w)
        {
            this.w = w;
        }
        public void ChangeHeigth(int h)
        {
            this.h = h;
        }
    }
}
