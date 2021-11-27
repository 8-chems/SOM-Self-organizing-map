using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TP_SOM_BERBEGUE
{
    class DBConnect : Form
    {
        Button b1 = new Button(),b2=new Button();
        TextBox t1 = new TextBox(), t2 = new TextBox(),t3=new TextBox();
        MaskedTextBox  mtb = new MaskedTextBox();
        Label l1 = new Label(), l2 = new Label(), l3 = new Label(), l4 = new Label(),l5=new Label();
        OpenFileDialog ofd = new OpenFileDialog();
        public DBConnect()
        {
            this.Size = new Size(300,400);
            this.Visible = true;

            b1.Text = "Explore";
            b1.SetBounds(10,10,80,30);
           
            b2.Text = "Connect";
            b2.SetBounds(200,330,80,30);

            l1.Text = "URI :";
            l1.SetBounds(100,20,40,30);

            l2.Text = "Nom D'utilisateur :";
            l2.SetBounds(10,60,120,30);


            l3.Text = "Mot De Passe :";
            l3.SetBounds(10,100,80,30);

            l4.Text = "Requete :";
            l4.SetBounds(10,140,80,20);
           
            t1.SetBounds(140,20,140,30);
            t2.SetBounds(140,60,140,30);
            t3.SetBounds(20,160,250,150);
            t3.Multiline = true;


            mtb.SetBounds(140,100,140,30);
            mtb.PasswordChar = '*';

            this.Controls.Add(b1); this.Controls.Add(b2); this.Controls.Add(l1); this.Controls.Add(l2);
            this.Controls.Add(l3); this.Controls.Add(l4); this.Controls.Add(t1); this.Controls.Add(t2);
            this.Controls.Add(mtb); this.Controls.Add(t3);

            b1.Click += new EventHandler(b1_Click);
            b2.Click += new EventHandler(b2_Click);


        }

        void b2_Click(object sender, EventArgs e)
        {
            //connect+ get data+ fill datatable end;
            this.Close();
        }

        void b1_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                t1.Text = ofd.FileName;
            }
        }
    }
}
