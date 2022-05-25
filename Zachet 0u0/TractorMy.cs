using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Zachet_0u0
{
    class TractorMy : Figure
    {
        //public Color[,] traccolor { get; protected set; } = new Color[2, 4];
        public TractorMy(Form form, int maxw, int maxh, int num, bool check = true) : base(form, maxw, maxh, num, check)
        {
            checkBox.Location = new Point(85 + num * 17, 64);
            type = "trac";
            for (int i = 0; i < 4; i++)
            {
                traccolor[0, i] = maincolor;
                traccolor[1, i] = taintetcolor;
            }
        }

        public override void Draw(Graphics g, bool redactor = false)
        {
            if (redactor)
            {
                g.FillRectangle(new SolidBrush(traccolor[0, 0]), 140 - w/2, 140-h/2, w / 4, h / 3+2);
                g.FillRectangle(new SolidBrush(traccolor[0, 1]), 140 - w/2, 140-h/6, w, h / 3);
                g.FillEllipse(new SolidBrush(traccolor[0, 2]), 140-w/2, 140 +h/6, h / 3, h / 3);
                g.FillEllipse(new SolidBrush(traccolor[0, 3]), 140 + w/2 - h/3, 140+h/6, h / 3, h / 3);
            }
            else
            {
                g.FillRectangle(new SolidBrush(traccolor[0, 0]), x, y, w / 4, h / 3+2);
                g.FillRectangle(new SolidBrush(traccolor[0, 1]), x, y + h / 3, w, h / 3);
                g.FillEllipse(new SolidBrush(traccolor[0, 2]), x, y + h / 3 * 2 , h / 3, h / 3);
                g.FillEllipse(new SolidBrush(traccolor[0, 3]), x + w - h / 3, y + h / 3 * 2, h / 3, h / 3);
                if (clicked)
                {
                    g.DrawRectangle(new Pen(traccolor[1, 0], 3), x, y, w / 4, h / 3);
                    g.DrawRectangle(new Pen(traccolor[1, 1], 3), x, y + h / 3, w, h / 3);
                    g.DrawEllipse(new Pen(traccolor[1, 2], 3), x, y + h / 3 * 2, h / 3, h / 3);
                    g.DrawEllipse(new Pen(traccolor[1, 3], 3), x + w -h/3, y + h / 3 * 2, h / 3, h / 3);
                }
            }
        }
        public override int PartNumber(double x, double y)
        {
            if (140 - w / 2 < x && 140 - w / 2 + w / 4 > x && 140 - h / 2 < y && 140 - h / 2 + h / 3 > y)
                return 0;
            else if (140 - w / 2 < x && 140 + w / 2 > x && 140 - h / 6 < y && 140 + h / 6 > y)
                return 1;
            else if ((double)(((x - (140 - w / 2 + h / 6)) * (x - (140 - w / 2 + h / 6)) + (y - (140 + h / 6 * 2)) * (y - (140 + h / 6 * 2))) / ((this.h / 6) * (this.h / 6))) <= 1) 
                return 2;
            else if ((double)(((x - (140 + w / 2 - h / 6)) * (x - (140 + w / 2 - h / 6)) + (y - (140 + h / 6 * 2)) * (y - (140 + h / 6 * 2))) / ((this.h / 6) * (this.h / 6))) <= 1)
                return 3;
            else
                return -1;
        }
        public override bool ClickFigureCheck(double x, double y)
        {
            if (!checkBox.Checked)
                return false;

            if (this.x < x && this.x + w / 4 > x && this.y < y && this.y + h / 3 > y)
                return true;
            else if (this.x < x && this.x + w > x && this.y + h / 3 < y && this.y + h / 3 * 2 > y)
                return true;
            else if ((double)(((x - this.x - this.h / 6) * (x - this.x - this.h / 6) + (y - this.y - this.h / 6 * 5) * (y - this.y - this.h / 6 * 5)) / ((this.h / 6) * (this.h / 6))) <= 1)
                return true;
            else if ((double)(((x - (this.x + this.w - this.h / 6)) * (x - (this.x + this.w - this.h / 6)) + (y - this.y - this.h / 6*5) * (y - this.y - this.h / 6*5)) / ((this.h / 6) * (this.h / 6))) <= 1) 
                return true;
            else
                return false;
        }
        public override void ChangeColor(Color color, int num = -1, bool load = false)
        {
            if (num == -1)
            {
                
                maincolor = Color.FromArgb(255, color.R, color.G, color.B);
                this.color = maincolor;
                taintetcolor = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
                for (int i = 0; i < 4; i++)
                {
                    traccolor[0, i] = maincolor;
                    traccolor[1, i] = taintetcolor;
                }
                if (load)
                {
                    if (!checkBox.Checked)
                        for (int i = 0; i < 4; i++)
                            traccolor[0, i] = Color.FromArgb(127, maincolor);
                }
            }
            else
            {
                traccolor[0, num] = color;
                traccolor[1, num] = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
            }
        }
    }
}
