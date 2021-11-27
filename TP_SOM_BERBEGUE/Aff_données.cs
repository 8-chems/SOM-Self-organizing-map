using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data.OleDb;
using System.Data;
using DevComponents.DotNetBar;

namespace TP_SOM_BERBEGUE
{
    class Aff_données : Office2007Form//Form
    {
      
      
        DataGridView dgv = new DataGridView();
        DataTable dt;
       
     public  Aff_données( DataTable dt)
        {

            
                this.dt = dt;
                this.Size = new Size(400, 400);
                this.Text = "Affichage de données";
                dgv.Dock = DockStyle.Fill;
                this.Controls.Add(dgv);
                dgv.DataSource = this.dt; this.Visible = true;
     }
        }

        
    

    }

