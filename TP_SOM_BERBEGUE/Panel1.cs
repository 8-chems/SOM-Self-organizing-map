using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel; 
using Microsoft.VisualBasic;

namespace TP_SOM_BERBEGUE
{
    class Panel1 :Panel{
        Button b1 = new Button(), b2 = new Button(),
          b3 = new Button(), b4 = new Button(),
          b5 = new Button(), b6 = new Button(),
          b7 = new Button(),b8=new Button(),
          b9=new Button(),b10=new Button();
        GroupBox acd1 = new GroupBox(), acd2 = new GroupBox(),
                 acd3 = new GroupBox(), acd4 = new GroupBox();
        TextBox tb = new TextBox(), nbattv = new TextBox(), nbexv = new TextBox(), flt = new TextBox();
 
        Label l1 = new Label(), nbattr = new Label(), nbex = new Label();
       CheckedListBox clb = new CheckedListBox();
       OpenFileDialog ofd = new OpenFileDialog();
       Font font = new Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold,
                              System.Drawing.GraphicsUnit.Point, ((byte)(0))),
            font2 = new Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold,
                              System.Drawing.GraphicsUnit.Point, ((byte)(0)));
       Aff_données afd;
       DataTable dt1 = new DataTable();
       string filepath = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
       string ep = ";Extended Properties=\"Excel 8.0;HDR=YES;\"";

       OleDbConnection odbc;
       OleDbDataAdapter odbda;
       DataTable dt2 = null;
       TreeNode filtert;
       DataGridView dgv = new DataGridView();
      
       
       TreeView tv=new TreeView();
       Form filtrf = new Form();
       SaveFileDialog sfd = new SaveFileDialog();
       public Panel1()
       {
           
            this.Dock = DockStyle.Fill;
            acd1.Text = "Aquisition de données";
            acd1.SetBounds(10, 10, 750, 150);

            acd2.Text = "Caractiristiques";
            acd2.SetBounds(10, 170, 750, 260);

            acd3.Text = "Les Attributs";
            acd3.SetBounds(10, 40, 250, 180);

            acd4.Text = "Des statistiques";
            acd4.SetBounds(290, 40, 450, 180);

            l1.SetBounds(20, 80, 100, 30);
            l1.Font = font;
            l1.Text = "Source :";

            nbattr.SetBounds(10, 20, 150, 30);
            nbattr.Text = "Nombre D'attribut : ";

            nbex.SetBounds(290, 20, 150, 30);
            nbex.Text = "Nombre D'exemples : ";


            tb.AutoSize = false;
            tb.Font = font;
            tb.SetBounds(180, 75, 400, 30);
            
            flt.AutoSize = false;
            flt.SetBounds(180, 110, 400, 30);
            flt.Font = font;

            nbexv.SetBounds(400, 20, 100, 30);
            nbexv.Text = "0";
            nbexv.Enabled = false;

            nbattv.SetBounds(110, 20, 100, 30);
            nbattv.Text = "0";
            nbattv.Enabled = false;

            dgv.Dock = DockStyle.Fill;
            acd4.Controls.Add(dgv);

           ///////////////////////
            dt1.Columns.Add("ID", typeof(int));
            dt1.Columns.Add("Min", typeof(double));
            dt1.Columns.Add("Max", typeof(double));
            dt1.Columns.Add("Moyenne", typeof(double));


            dgv.DataSource = dt1;
            dt1.Rows.Add(1,25, 25, 25);
            dt1.Rows.Add(2,25, 25, 25);
            dt1.Rows.Add(3,25, 25, 25);
           /////////////////////////

            this.Controls.Add(acd1);
            this.Controls.Add(acd2);


            b1.SetBounds(20, 30, 150, 30); b2.SetBounds(180, 30, 250, 30);
            b3.SetBounds(440, 30, 150, 30); b4.SetBounds(590, 75, 150, 30);
            b5.SetBounds(20, 110, 150, 30); b6.SetBounds(10, 225, 250, 25);
            b7.SetBounds(300, 225, 120, 25); b8.SetBounds(590,110,150,30);
            b9.SetBounds(450,225,120,25);b10.SetBounds(600,225,120,25);
            b1.Text = "Charger un Fichier Excel"; b2.Text = "Connecter à une Base de données Access";
            b3.Text = "Entrer un URL"; b4.Text = "Visualiser les données"; b5.Text = "Appliquer un filter";
            b6.Text = "Enlever"; b7.Text = "Analyser les données"; b8.Text = "Exécuter";
            b9.Text = "Enregistré";  b10.Text = "Retourner";


            b1.Click += new EventHandler(b1_Click);
            b4.Click += new EventHandler(b4_Click);
            b2.Click += new EventHandler(b2_Click);
            b6.Click += new EventHandler(b6_Click);
            b5.Click+=new EventHandler(b5_Click);
            b8.Click += new EventHandler(b8_Click);
            b9.Click += new EventHandler(b9_Click);
            b10.Click += new EventHandler(b10_Click);

            acd1.Controls.Add(b1); acd1.Controls.Add(b2); acd1.Controls.Add(b3);
            acd1.Controls.Add(l1); acd1.Controls.Add(tb); acd1.Controls.Add(b4);
            acd1.Controls.Add(b5); acd1.Controls.Add(flt); acd1.Controls.Add(b8);
           

            clb.Dock = DockStyle.Fill; clb.CheckOnClick = true;
            clb.Items.Add("test 1"); clb.Items.Add("test 2");
            clb.Font=font2;

            acd2.Controls.Add(acd3); acd2.Controls.Add(acd4);
            acd2.Controls.Add(nbattv); acd2.Controls.Add(nbexv);
            acd2.Controls.Add(nbattr); acd2.Controls.Add(nbex);
            acd2.Controls.Add(b6); acd2.Controls.Add(b7);
            acd2.Controls.Add(b9); acd2.Controls.Add(b10);

            acd3.Controls.Add(clb);

           /////////////////////////////
            filtrf.Visible = false;
            filtrf.Size = new Size(150,250);
            filtrf.ControlBox = false;
            filtrf.Text = String.Empty;
            filtrf.Owner =(Form) this.Parent ;
            filtrf.StartPosition = FormStartPosition.Manual;
            tv.Dock = DockStyle.Fill;
            filtrf.Controls.Add(tv);


            TreeNode tn = new TreeNode("Filtrage de données");
            tv.Nodes.Add(tn);
            TreeNode node2 = new TreeNode("Catégorie 1");
            TreeNode node3 = new TreeNode("Catégorie 2");
            TreeNode[] array = new TreeNode[] {node2,node3 };
            tn = new TreeNode("Normalisation");
            tv.Nodes.Add(node2); tv.Nodes.Add(node3);
            node2.Nodes.Add(tn); node3.Nodes.Add(new TreeNode("Test"));
            tv.AfterSelect += new TreeViewEventHandler(tv_AfterSelect);
            tv.SelectedNode = null;
            tv.Enabled = false;

           /////////////////////////////////
        }

       void b10_Click(object sender, EventArgs e)
       {
           


       }

       void b9_Click(object sender, EventArgs e)
       {
           if (sfd.ShowDialog() == DialogResult.OK)
           {
            DataGridView dgg= new DataGridView();
            dgg.DataSource=dt2;
            Excel.Application xlApp ;
            Excel.Workbook xlWorkBook ;
            Excel.Worksheet xlWorkSheet ;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int i = 0,j = 0; 
            for (i = 0; i <= dgg.RowCount- 1; i++)
            { for (j = 0; j <= dgg.ColumnCount- 1; j++)
                {DataGridViewCell cell = dgg[j, i];
                 xlWorkSheet.Cells[i + 1, j + 1] = cell.Value;
                }
            }

            xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

           try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                xlWorkSheet = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                xlWorkBook = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlApp = null;
            }
           catch (Exception ex) { MessageBox.Show("Probleme l'hor de création de fichier esseye autre fois"); }

            MessageBox.Show("Fichier XLS est bien creé et enregistré");
        }
}


       void b8_Click(object sender, EventArgs e)
       {int i=0;
          switch(this.filtert.Text){
              case "Normalisation":
                  {
                      string[] ss = (from dc in dt2.Columns.Cast<DataColumn>()
                                     select dc.ColumnName).ToArray();
                      
                      for (int k = 0; k < ss.Length; k++)
                      {
                      foreach (DataRow dr in dt2.Rows)
                      {dr.SetField<double>(ss[k],dr.Field<double>(ss[k])/dt1.Rows[k].Field<double>("Moyenne"));
                          }
                      }

                      dt1.Clear();
                      for (int ii = 0; ii < ss.Length; ii++)
            {
                double accountLevel = 0; Boolean b = false;double min = int.MaxValue;double max = int.MinValue;double som = 0;
                 foreach (DataRow dr in dt2.Rows)
                 {
                     try
                     {
                         accountLevel = dr.Field<double>(ss[ii]);
                     } 
                     catch (Exception ee) { b=true; }
                     if(!b){
                         min = Math.Min(min, accountLevel);
                         max = Math.Max(max, accountLevel);
                         som += accountLevel;
                     }else break;
                     }


                 if (!b) { double d = (som / dt2.Rows.Count); dt1.Rows.Add(ii, min, max, d); }
                 else dt1.Rows.Add(ii, -1, -1, -1);
              
             }
           }
 break;
}
          
          
          }
       

   public  DataTable getDT() {
           
           return this.dt2;
       }

       void tv_AfterSelect(object sender, TreeViewEventArgs e)
       {
           filtrf.Visible = true;
           flt.Text = tv.SelectedNode.Text;
           filtrf.Visible = false;
           this.filtert=tv.SelectedNode;
       }

       void b5_Click(object sender, EventArgs e)
       {
          filtrf.Location = new Point(240,350);
           filtrf.Visible = true;
           tv.Enabled = true;
       }

       void b6_Click(object sender, EventArgs e)
       {
            DataTable dd = dt2;
           dd.Columns.Remove(clb.SelectedItem.ToString());
           dt1.Rows.RemoveAt(clb.SelectedIndex);
           clb.Items.RemoveAt(clb.SelectedIndex);
           nbexv.Text = dd.Rows.Count.ToString();
           nbattv.Text = dd.Columns.Count.ToString();
          
       }

       void b2_Click(object sender, EventArgs e)
       {
           new DBConnect();
       }

       void b4_Click(object sender, EventArgs e)
       {
         afd = new Aff_données(dt2);
       }

       void b1_Click(object sender, EventArgs e)
       {
           if (ofd.ShowDialog() == DialogResult.OK)
           {
               dt2 = new DataTable();
               odbc = new OleDbConnection(filepath + ofd.FileName + ep);
               odbda = new OleDbDataAdapter("Select *  from [Feuil1$]", odbc);
               odbda.Fill(dt2);

               tb.Text = ofd.FileName;
               clb.Items.Clear();
               DataTable dd=dt2;
              
              nbexv.Text = dd.Rows.Count.ToString();
              nbattv.Text = dd.Columns.Count.ToString();
              string[] ss = (from dc in dd.Columns.Cast<DataColumn>()
                             select dc.ColumnName).ToArray();
              double min = double.MaxValue, max = double.MinValue,som = 0;
              

             for (int ii=0;ii<ss.Length;ii++)
              {
                  clb.Items.Add(ss[ii]);
              }
             dt1.Rows.Clear();

            for (int ii = 0; ii < ss.Length; ii++)
            {
                double accountLevel = 0; Boolean b = false; min = int.MaxValue; max = int.MinValue; som = 0;
                 foreach (DataRow dr in dd.Rows)
                 {
                     try
                     {
                         accountLevel = dr.Field<double>(ss[ii]);
                     } 
                     catch (Exception ee) { b=true; }
                     if(!b){
                         min = Math.Min(min, accountLevel);
                         max = Math.Max(max, accountLevel);
                         som += accountLevel;
                     }else break;
                     }


                 if (!b) { double d = (som / dd.Rows.Count); dt1.Rows.Add(ii, min, max, d); }
                 else dt1.Rows.Add(ii, -1, -1, -1);
              
             }
           
               

           }
       }


    
    }
}
