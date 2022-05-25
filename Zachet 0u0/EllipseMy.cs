using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Zachet_0u0
{
    class EllipseMy : Figure
    {
        public EllipseMy(Form form, int maxw, int maxh, int num, bool check = true ) : base(form, maxw, maxh, num, check)
        {
            checkBox.Location = new Point(85 + num * 17, 38);
            type = "elli";
        }

        public override void Draw(Graphics g, bool redactor = false)
        {
            if (redactor)
            {
                g.FillEllipse(new SolidBrush(color), 140 - w / 2, 140 - h / 2, w, h);
            }
            else
            {
                g.FillEllipse(new SolidBrush(color), x, y, w, h);
                if (clicked)
                    g.DrawEllipse(new Pen(taintetcolor, 3), x, y, w, h);
            }

        }
        public override bool ClickFigureCheck(double x, double y)
        {
            if (!checkBox.Checked)
                return false;
            //int h = (this.h / 2) * (this.h / 2), 
            //    w = (this.w / 2) * (this.w / 2);
            return ((double)((x - this.x - this.w / 2) * (x - this.x - this.w / 2) / ((this.w / 2) * (this.w / 2)) + (y - this.y - this.h / 2) * (y - this.y - this.h / 2) / ((this.h / 2) * (this.h / 2))) <= 1);
        }
        public override void ChangeColor(Color color, int num = -1, bool load = false)
        {
            maincolor = Color.FromArgb(255, color.R, color.G, color.B);
            this.color = maincolor;
            taintetcolor = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
            if (load)
            {
                if (!checkBox.Checked)
                    this.color = Color.FromArgb(127, maincolor);
            }
        }

        public override int PartNumber(double x, double y)
        {
            throw new NotImplementedException();
        }
    }
}
