using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cb0t
{
    public partial class WritingPanel : UserControl
    {
        private List<String> writers = new List<String>();
        private Bitmap pen { get; set; }
        private Bitmap rec { get; set; }

        public bool IsBlack { get; set; }

        public WritingPanel()
        {
            this.InitializeComponent();
            this.pen = (Bitmap)Properties.Resources.writingpen.Clone();
            this.rec = (Bitmap)Properties.Resources.mrec.Clone();
            this.ResizeRedraw = true;
            this.Mode = WritingPanelMode.Writing;
            this.RecordingTime = 0;
            this.Paint += this.PaintWriters;
        }

        public void Free()
        {
            this.Paint -= this.PaintWriters;
            this.pen.Dispose();
            this.pen = null;
            this.rec.Dispose();
            this.rec = null;
            this.writers.Clear();
            this.writers = new List<String>();
        }

        public void ClearWriters()
        {
            this.writers.Clear();
            this.writers = new List<String>();
            this.Invalidate();
        }

        public void MyWritingStatusChanged(String name, bool writing)
        {
            if (writing)
            {
                if (!this.writers.Contains(name))
                {
                    this.writers.Add(name);
                    this.Invalidate();
                }
            }
            else
            {
                if (this.writers.Contains(name))
                {
                    this.writers.Remove(name);
                    this.Invalidate();
                }
            }
        }

        public void RemoteWritingStatusChanged(String name, bool writing)
        {
            if (writing)
            {
                if (!this.writers.Contains(name))
                {
                    this.writers.Add(name);
                    this.Invalidate();
                }
            }
            else
            {
                if (this.writers.Contains(name))
                {
                    this.writers.Remove(name);
                    this.Invalidate();
                }
            }
        }

        public WritingPanelMode Mode { get; set; }
        public int RecordingTime { get; set; }

        private void PaintWriters(object sender, PaintEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(this.IsBlack ? Color.Black : Color.White))
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, this.Width, this.Height));

            if (this.Mode == WritingPanelMode.Writing)
            {
                String names = String.Join(", ", writers.ToArray());

                if (!String.IsNullOrEmpty(names))
                {
                    e.Graphics.DrawImage(this.pen, new Point(1, 0));

                    using (SolidBrush brush = new SolidBrush(this.IsBlack ? Color.White : Color.Black))
                        e.Graphics.DrawString(StringTemplate.Get(STType.Messages, 0).Replace("+x", names), this.Font, brush, new PointF(16, 2));
                }
            }
            else
            {
                e.Graphics.DrawImage(this.rec, new Point(1, 0));

                using (SolidBrush brush = new SolidBrush(this.IsBlack ? Color.White : Color.Black))
                    e.Graphics.DrawString(StringTemplate.Get(STType.Messages, 1).Replace("+x", (15 - this.RecordingTime).ToString()), this.Font, brush, new PointF(16, 2));
            }
        }
    }

    public enum WritingPanelMode
    {
        Writing,
        Recording
    }
}
