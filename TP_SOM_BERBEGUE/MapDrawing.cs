using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CPI.Plot3D;
using System.Threading;
using AForge.Neuro;
using System.Drawing.Imaging;
using TP_SOM_BERBEGUE.Bitmap_Mod;

namespace TP_SOM_BERBEGUE
{
    class MapDrawing : Form
    {
        Panel mapPanel1,mapPanel2;
        private int[, ,] map;
        int networkSize;
        GroupBox gb1=new GroupBox(), gb2=new GroupBox();
        Bitmap mapBitmap;
        public MapDrawing()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ResumeLayout(false);
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Visible = true;
            this.Text = "Suiver visualement l'apprentissage des neurones";
            mapPanel2 = new BufferedPanel();
            mapPanel2.Dock = DockStyle.Fill;
            
            mapPanel1 = new Panel();
            mapPanel1.Paint+=new PaintEventHandler(mapPanel1_Paint);
            this.mapPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapPanel1.TabIndex = 0;
            mapBitmap = new Bitmap(mapPanel1.Width, mapPanel1.Height, PixelFormat.Format24bppRgb);
      


            gb1.SetBounds(10,10,350,370); gb1.Text = "Visualisation 2D";
            gb2.SetBounds(380,10,390,370);gb2.Text = "Visualisation Euclidienne de La carte";
            mapPanel1.SetBounds(10,20,330,330);
            gb1.Controls.Add(mapPanel1);
            gb2.Controls.Add(mapPanel2);

            this.Controls.Add(gb1);
            this.Controls.Add(gb2);
            mapPanel1.Invalidate();

            Benchmark.Start();
            LockBitmap lockBitmap = new LockBitmap(mapBitmap);
            lockBitmap.LockBits();

            for (int y = 0; y < lockBitmap.Height; y++)
            {
                for (int x = 0; x < lockBitmap.Width; x++)
                {
                    lockBitmap.SetPixel(x, y, Color.Red);
                }
            }
            lockBitmap.UnlockBits();
            Benchmark.End();
            mapPanel1.Invalidate();
}
        private void mapPanel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {   Graphics g = e.Graphics;
            Monitor.Enter(this);
            g.DrawImage(mapBitmap, 0, 0, mapPanel1.Width, mapPanel1.Height);
            Monitor.Exit(this);
        }
      
    }
}
