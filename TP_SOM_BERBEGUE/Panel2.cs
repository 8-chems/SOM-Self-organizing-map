using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using DevComponents.DotNetBar;

namespace TP_SOM_BERBEGUE
{
    class Panel2 : Panel
    {
        GroupBox gb1 = new GroupBox(), gb2 = new GroupBox(),
                 gb3 = new GroupBox(), gb4 = new GroupBox(),
                 gb5=new GroupBox(),gb6=new GroupBox();
        ButtonX b1=new ButtonX(), b2=new ButtonX(),b3=new ButtonX(),
               b4=new ButtonX(),b5=new ButtonX(),b6=new ButtonX();
        Label l1=new Label(), l2=new Label(), l3=new Label(),
              l4=new Label(),l5=new Label(),l6=new Label(),
              l7=new Label();
        ButtonItem bi1 = new ButtonItem();
        CheckBox chb1 = new CheckBox(),chb2=new CheckBox();
        ComboBox cb1 = new ComboBox(), cb2 = new ComboBox(),
                 cb3= new ComboBox(),cb4=new ComboBox()
                 ,cb5=new ComboBox();

        TextBox  tb2 = new TextBox(), 
                tb3 = new TextBox(),tb4=new TextBox(),
                tb5=new TextBox();
        public TextBox tb1 = new TextBox();
        AforgeTraining at;
        SaveFileDialog sfd = new SaveFileDialog();
        OpenFileDialog ofd = new OpenFileDialog();

        public Panel2()
        {
            this.Dock = DockStyle.Fill;

            gb1.SetBounds(340, 50, 420, 180);
            gb1.Text = "Topologie de Reseau";
           
            gb2.SetBounds(385, 250, 375, 100);
            gb2.Text = "Pas D'apprentissage";

            gb3.SetBounds(10, 360, 750, 60);
            gb3.Text = "Carte";

            gb4.SetBounds(10, 90, 320, 140);
            gb4.Text = "Configuration Permanante";

            gb5.SetBounds(10, 250, 365, 100);
            gb5.Text = "Rayon";

            gb6.SetBounds(10,20, 320, 60);
            gb6.Text = "Bibliotheque";

            this.Controls.Add(gb1);
            this.Controls.Add(gb2);
            this.Controls.Add(gb3);
            this.Controls.Add(gb4);
            this.Controls.Add(gb5);
            this.Controls.Add(gb6);

            l1.Text = "Nombre de neurone d'Entré :"; l1.SetBounds(5,30,180,20);
            l2.Text = "Type de treillis :"; l2.SetBounds(5, 70, 180, 20);
            l3.Text = "Nombre de neurone de la carte :"; l3.SetBounds(5,110,180,20);
            l4.Text = "Type de Fonction Voisinage :"; l4.SetBounds(5,150,180,20);
            l5.Text = "Valeur Initial :"; l5.SetBounds(5,20,130,30);
            l6.Text = "Valeur Initial :"; l6.SetBounds(5, 20, 130, 30);
            l7.Text = "Utilisé la bibliotheque : "; l7.SetBounds(60, 23, 120, 20);

            chb1.Text = "Utiliser une Fonction D'adaptation :";
            chb1.SetBounds(5,50,192,30);
            
            tb4.SetBounds(200,20,160,30);
           
            cb3.SetBounds(200, 50, 160, 30);
            cb3.Items.Add(bi1);
            // cb3.Items.Add("Fonction 1");
            cb3.SelectedIndex = 0;

            cb5.SetBounds(180, 20, 120, 20);
            cb5.Items.Add("Aforge.Net");
            cb5.SelectedIndex = 0;

            chb2.Text = "Utiliser une Fonction D'adaptation :"; 
            chb2.SetBounds(5, 50, 192, 30);
            
            tb5.SetBounds(200, 20, 160, 30);
            cb4.SetBounds(200, 50, 160, 30);
            cb4.Items.Add("Fonction 1");
            cb4.SelectedIndex = 0;
            
            gb2.Controls.Add(l5); gb2.Controls.Add(chb1); gb2.Controls.Add(tb4); gb2.Controls.Add(cb3);
            gb5.Controls.Add(l6); gb5.Controls.Add(chb2); gb5.Controls.Add(tb5); gb5.Controls.Add(cb4);

            gb6.Controls.Add(l7); gb6.Controls.Add(cb5);
            
            cb1.SetBounds(190, 70, 180, 20); 
            cb1.Items.Add("Rectangulaire");
            cb1.Items.Add("Hexagonale");
            cb1.SelectedIndex = 0;

            cb2.SetBounds(190, 150, 180, 20); 
            cb2.Items.Add("Fonction 1");
            cb2.SelectedIndex = 0;

            tb1.SetBounds(190, 30, 180, 20); tb1.Enabled = false;
            tb2.SetBounds(190,110,180,20);

            b1.Text = "Cree le reseau"; b1.SetBounds(40, 20, 180, 30);
            b2.Text = "Randomisizer les Poids"; b2.SetBounds(255, 20, 180, 30);
            b3.Text = "Visualiser le reseau"; b3.SetBounds(475, 20, 180, 30);
            
            b4.Text = "Exporter les parametres"; b4.SetBounds(50,20,200,30);
            b5.Text = "Importer des parametres"; b5.SetBounds(50,60, 200, 30);
            b6.Text = "Utliser les paramtres par defaut"; b6.SetBounds(50,100,200,30);

            gb4.Controls.Add(b4); gb4.Controls.Add(b5); gb4.Controls.Add(b6);

            gb1.Controls.Add(l1); gb1.Controls.Add(l2); gb1.Controls.Add(l3);
            gb1.Controls.Add(l4); gb1.Controls.Add(cb2); gb1.Controls.Add(tb1);
            gb1.Controls.Add(tb2); gb1.Controls.Add(cb1);

            gb3.Controls.Add(b1);gb3.Controls.Add(b2);gb3.Controls.Add(b3);
            

            b1.Click += new EventHandler(b1_Click);
            b2.Click += new EventHandler(b2_Click);
            b3.Click += new EventHandler(b3_Click);
            b6.Click += new EventHandler(b6_Click);
            b4.Click += new EventHandler(b4_Click);
            b5.Click += new EventHandler(b5_Click);
        }

        void b5_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(ofd.FileName);

                XmlNode node = doc.DocumentElement.SelectSingleNode("/Network");

                foreach (XmlNode nod  in node.ChildNodes)
                {
                    string text = nod.InnerText;
                    MessageBox.Show(text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
          
                }

             
            
            
            }
        }

        void b4_Click(object sender, EventArgs e)
        {


            sfd.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            sfd.FilterIndex = 1;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                XmlWriter writer = XmlWriter.Create(sfd.FileName, settings);
                writer.WriteStartDocument();

                writer.WriteStartElement("Network");
                writer.WriteElementString("param1", "10");
                writer.WriteElementString("param2", "10");
                writer.WriteElementString("param3", "10");
                writer.WriteElementString("param4", "10");
                writer.WriteElementString("param5", "10");
                writer.WriteElementString("param6", "10");
                writer.WriteEndDocument();

                writer.Flush();
                writer.Close();


            }
        }
        void b6_Click(object sender, EventArgs e)
        {
          //
            tb4.Text = "0,02";
            tb5.Text = "1";
            int i=int.Parse(tb1.Text);i=i*i;
            tb2.Text = i.ToString();

            b1.PerformClick();
            b2.PerformClick();
            b3.PerformClick();
        }

        void b3_Click(object sender, EventArgs e)
        {
            new MapDrawing();
            b3.BackColor = Color.Green;   
}

        void b2_Click(object sender, EventArgs e)
        {
            at.Randomize();
            b2.BackColor = Color.Green; 
        }

        void b1_Click(object sender, EventArgs e)
        {
            at = new AforgeTraining();
            at.setParametres(int.Parse(tb1.Text),cb1.SelectedIndex,int.Parse(tb2.Text), 
                1, int.Parse(tb5.Text),(chb1.Checked!=true) ? 0 : cb4.SelectedIndex, 
                    float.Parse(tb4.Text),(chb2.Checked!=true) ? 0 : cb3.SelectedIndex);
            b1.BackColor = Color.Green;   
        }

       public  AforgeTraining getAT() {

            return this.at;
        }

    }
}
