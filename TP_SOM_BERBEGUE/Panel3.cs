using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using WPFChart3D;
using System.Windows.Forms.Integration;
using System.Threading;

namespace TP_SOM_BERBEGUE
{
    
    class Panel3 : Panel
    { Button b1=new Button(),b2=new Button(),b3=new Button()
             ,b4=new Button(),b5=new Button();
    Label l1=new Label(), l2=new Label(),l3=new Label()
           ,l4=new Label(),l5=new Label();
    TextBox tb1 = new TextBox(), tb2 = new TextBox(),
            tb3 = new TextBox(), tb4 = new TextBox();
    ComboBox cb1 = new ComboBox();
    GroupBox gb1=new GroupBox(), gb2=new GroupBox(), gb3=new GroupBox();
    ProgressBar pb1 = new ProgressBar(),pb2 = new ProgressBar();
    Thread t;
    AforgeTraining at;
    public  Panel3(AforgeTraining at, DataTable dt)
    {
       

        at.setDTable(dt);
        at.setGraphics(pb1,pb2,tb3,tb4);
        this.tb1.Text = dt.Rows.Count.ToString();
        this.at = at;
          this.Dock = DockStyle.Fill;
          gb1.SetBounds(10,10,250,240); gb1.Text = "Informations";
          gb2.SetBounds(280,10,470,240); gb2.Text = "Avancement";
          gb3.SetBounds(10,260,745,100); gb3.Text = "Control";

          b1.SetBounds(40,40,130,30); b1.Text = "Commencer";
          b2.SetBounds(200,40,130,30);b2.Text="Arreter";
          b3.SetBounds(360,40,130,30); b3.Text = "Visualiser 3D";
          b4.SetBounds(530, 40, 130, 30); b4.Text = "Visualiser 2D";
          b5.SetBounds(60,380,600,30); b5.Text = "Exporter les Resultats";

          l1.SetBounds(10,50,105,30); l1.Text = "Nombre d'Exemple : ";
          l2.SetBounds(10,110,100,30); l2.Text = "Nombre d'Epoque : ";
          l3.SetBounds(10, 180, 145, 30); l3.Text = "Paramtres de L'algorithme :";
          l4.SetBounds(10, 180, 120, 30); l4.Text = "Pas D'apprentissage :";
          l5.SetBounds(260, 180, 60, 30); l5.Text = "Rayon : ";
          tb1.SetBounds(155,50,80,30); tb1.Enabled = false;
          tb2.SetBounds(155,110,80,30);
          tb3.SetBounds(150, 180, 100, 30); tb3.Enabled = false;
          tb4.SetBounds(320, 180, 100, 30); tb4.Enabled = false;
          

          cb1.Items.Add("Fixe"); cb1.Items.Add("Adapatif");
          cb1.SetBounds(155, 180, 80, 30); cb1.SelectedIndex = 1;

          pb1.SetBounds(10,40,440,40); pb2.SetBounds(10,100,440,40);

          gb1.Controls.Add(l1); gb1.Controls.Add(tb1); gb1.Controls.Add(l3);
          gb1.Controls.Add(l2); gb1.Controls.Add(tb2); gb1.Controls.Add(cb1);

          gb2.Controls.Add(pb1); gb2.Controls.Add(pb2);
          gb2.Controls.Add(l4); gb2.Controls.Add(l5);
          gb2.Controls.Add(tb3); gb2.Controls.Add(tb4);

          gb3.Controls.Add(b1); gb3.Controls.Add(b2); gb3.Controls.Add(b3); gb3.Controls.Add(b4);

          this.Controls.Add(gb1);
          this.Controls.Add(gb2);
          this.Controls.Add(gb3);
          this.Controls.Add(b5); 

          t = new Thread(new ThreadStart(at.Commencer));
          
          b3.Click += new EventHandler(b3_Click);
          b1.Click += new EventHandler(b1_Click);
          b2.Click += new EventHandler(b2_Click);
          b4.Click += new EventHandler(b4_Click);
      }

    void b4_Click(object sender, EventArgs e)
    {
        new MapDrawing();
    }

   
        
     

      
    void b2_Click(object sender, EventArgs e)
    {

       if(t.IsAlive)t.Suspend();
       tb2.Enabled = false; 
    }

    void b1_Click(object sender, EventArgs e)
    {
        at.nbEP = int.Parse(tb2.Text);
        if (t.IsAlive) t.Resume(); else t.Start();
    }

    void b3_Click(object sender, EventArgs e)
    {
        var wpfwindow = new Window1(at.wnv);
        ElementHost.EnableModelessKeyboardInterop(wpfwindow);
        wpfwindow.Show();
        
      /*  string s = ""; for (int i = 0; i < at.wnv.Length;i++) s += ","+at.wnv[i];
        MessageBox.Show(s, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
                    
    }

    }
}
