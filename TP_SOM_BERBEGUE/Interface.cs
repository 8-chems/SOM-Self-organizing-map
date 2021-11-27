using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using TP_SOM_BERBEGUE;
using System.Data;

public class Interface : Form
{

    TabControl tc = new TabControl();
    ImageList imageList1 = new ImageList();
    Panel1 p1 = new Panel1();
    Panel2 p2 = new Panel2();
    DataTable dt;
    Panel3 p3;
    public Interface()
    {
        this.Icon = new Icon(@"C:/Users/Berbegue-Pc/Desktop/Rafiqul-Hassan-Blogger-Search.ico");
        this.Size = new Size(800,550);
        this.Visible = true; 
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.Text = "TP SOM Berbegue";
        this.Location = new Point(200,100);
        

        imageList1.Images.Add("key1", Image.FromFile(@"C:/Users/Berbegue-Pc/Desktop/1.jpg"));
        imageList1.Images.Add("key2", Image.FromFile(@"C:/Users/Berbegue-Pc/Desktop/2.png"));
        imageList1.Images.Add("key2", Image.FromFile(@"C:/Users/Berbegue-Pc/Desktop/3.png"));

        tc.Dock = DockStyle.Fill; 
        imageList1.ImageSize = new System.Drawing.Size(64,64);
        tc.ImageList = imageList1;
       
        tc.TabPages.Add("tabKey1", "", 0);
        tc.TabPages.Add("tabKey2", "", 1);
        tc.TabPages.Add("tabKey3", "", 2);
        tc.Selecting += new TabControlCancelEventHandler(Onglet_Selectioné);
       
        
        this.Controls.Add(tc);
        tc.SelectedTab.Controls.Add(p1);
        tc.SelectTab("tabKey2");
        tc.SelectedTab.Controls.Add(p2);
        tc.SelectTab("tabKey1");
        

}
    void Onglet_Selectioné(object sender, TabControlCancelEventArgs e)
    {
        switch (this.tc.SelectedIndex)
        {
            case 0:
              // MessageBox.Show("a tab is clicked be sure that u did the best choice " + 0, "Tab clicked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
            case 1:
               if(p1.getDT()!=null) p2.tb1.Text = p1.getDT().Columns.Count.ToString();
                break;
            case 2:
                if (p1.getDT() == null)
                {
                    MessageBox.Show("SVP Choisir une methode et entrer les données requis","Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tc.SelectTab("tabkey1");
                }else
                
                if (p2.getAT() == null)
                {
                    MessageBox.Show("La carte n'est pas encore creé assurer la creation du carte et resseyer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   tc.SelectTab("tabkey2");
                }
                else {
                    p3 = new Panel3(p2.getAT(), p1.getDT());
                    tc.SelectedTab.Controls.Add(p3);
                    
            }
                break;
         }
    }

   
   
}