using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Zachet_0u0
{
    public class RectangleMy : Figure
    {
        public RectangleMy(Form form, int maxw, int maxh, int num, bool check = true) : base(form, maxw, maxh, num, check)
        {
            checkBox.Location = new Point(85 + num * 17, 12);
            type = "rec";
        }

        public override void Draw(Graphics g, bool redactor = false)
        {
            if (redactor)
            {
                g.FillRectangle(new SolidBrush(color), 140 - w / 2, 140 - h / 2, w, h);
            }
            else
            {
                g.FillRectangle(new SolidBrush(color), x, y, w, h);
                if (clicked)
                    g.DrawRectangle(new Pen(taintetcolor, 3), x, y, w, h);
            }
        }

        public override bool ClickFigureCheck(double x, double y)
        {
            if (!checkBox.Checked)
                return false;
            if (this.x < x && this.x + w > x && this.y < y && this.y + h > y)
                return true;
            else
                return false;
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
