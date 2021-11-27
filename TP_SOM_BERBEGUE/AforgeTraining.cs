using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
///////////////////////////////////
using AForge;
using AForge.Neuro;
using AForge.Neuro.Learning;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Linq;
namespace TP_SOM_BERBEGUE
{  
    class AforgeTraining 
    {
        DistanceNetwork dn;
        SOMLearning trainer;
        double[] input;
        public int[] wnv;
        bool needToStop=false;
        DataTable dt;
        int nbe = 0,nbnc=0;
        int rif = 0;
        int pif = 0;
        public int nbEP=0;
        public int count = 0;
        ProgressBar  pb1,pb2;
        TextBox tb1, tb2;
        public delegate void MontrerProgresEX(int ni,string  k,string o);
        public delegate void MontrerProgresEPC(int ni);
    public AforgeTraining()
        {  //Neuron... = new Range(0, 255);
         // Neuron.RandRange = new DoubleRange( 0, 1 );

           this.dt = dt;
       }
    public void setDTable(DataTable dt)
    { 
        this.dt = dt;
    }
        
        /// /////////////////////////////////////////////
    public void Commencer() {
        for (int k = 0; k < nbEP;k++) // nb epok;
        {
            for (int i = 0; i < dt.Rows.Count; i++) // nombre d'ex
            {
                input = this.getRow(i);
                trainer.Run(input);
                wnv[dn.GetWinner()]++;

                // interface + itiration +....
                // update learning rate and radius continuously,
                // so networks may come steady state

                tb1.Parent.Invoke((MontrerProgresEX)ProgresEx,(100*(i+1)/dt.Rows.Count),this.trainer.LearningRadius.ToString(),this.trainer.LearningRate.ToString());
               
            }
         if(k!=( nbEP-1))   tb1.Parent.Invoke((MontrerProgresEX)ProgresEx,0,this.trainer.LearningRadius.ToString(),this.trainer.LearningRate.ToString());
            tb1.Parent.Invoke((MontrerProgresEPC)ProgresEPC, (100*(k+1)/nbEP));
        }
    
    }


    public void ProgresEx(int i,string k,string o)
    {
        this.pb1.Value = i;
        this.tb1.Text = k;
        this.tb2.Text = o;
    }
    public void ProgresEPC(int i)
    {
        this.pb2.Value = i;
    }   
        /// /////////////////////////////////////////////

    public void setGraphics(ProgressBar  pb1,ProgressBar  pb2,TextBox tb1,TextBox tb2) {
        this.pb1 = pb1; this.pb2 = pb2;
        this.tb1 = tb1; this.tb2 = tb2;
        this.tb1.Text = this.trainer.LearningRadius.ToString();
        this.tb2.Text = this.trainer.LearningRate.ToString();
}

        ////////////////////////////////////////////////////
    public double[] getRow(int i)
    {
        int n = this.dt.Columns.Count;
        double[] arr = new double[n];
        DataRow dr = this.dt.Rows[i];
        for (int j = 0; j < n; j++) arr[j] = dr.Field<double>(j);
        return arr;
    }




  public  double getPasApp()
    {
        return trainer.LearningRate;
    }

  public double getRadus()
  {
      return trainer.LearningRadius;  
  
  }


        public double PasApp_k(double pi,double pf,double k,double kt) {
             return pi * Math.Pow((pf/pi),(k/kt));  
        }

       public double  Voisinage_f(int rc, int t, double cgm){
            return Math.Exp(-Math.Abs(rc-t)/2*Math.Pow(cgm,2));
       }

     public void Randomize(){
         dn.Randomize();
         dn.Randomize();
     }

     public void setParametres(int nbe,int type_tr,int nbc,int type_fv,double ri,int rif,double pi,int pif) {
         this.nbe=nbe;
         this.nbnc = nbc;
         dn = new DistanceNetwork(nbe, nbc);
         trainer = new SOMLearning(dn);
         input = new double[nbe];
         trainer.LearningRadius = ri;
         trainer.LearningRate = pi;
         wnv = new int[nbc]; for (int i = 0; i < nbc; i++) wnv[i] = 1;
             this.pif = pif;
         this.rif = rif;
        }
       }
}




//public double RunEpoch(double[][] input)
