using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_SOM_BERBEGUE.SOM_Self_Implmentation
{
    class Network
    {
        int type;
        Neurone[][] netw;
        public Network(int n) { 
        netw=new Neurone[n][];
        for(int i=0;i<n;i++) netw[i]=new Neurone[n]; 
        }


      public  Neurone[] getVoisinage(Neurone n) {
          Neurone[] l;
          return null;  
      }

      public void createNetw(int i) {
          switch (i) {
              case 1: break;
              case 2: break;
          }
      }

      public Neurone getWinner(double[] input) {

          return null;
      }

      public void LearnSom(double[] input) {
          Neurone winer = this.getWinner(input);
          Neurone[] vois = this.getVoisinage(winer);

          for (int i = 0; i < vois.Length; i++) { 
          // update poids
          
          }
      }

      public double distanceEc(double[] a, double[] b)
      {
          double som = 0;
          for (int i = 0; i < a.Length; i++)
          {
              som += (a[i] - b[i]) * (a[i] - b[i]);
          }
          return Math.Sqrt(som);
      }

      public double PasApp_k(double pi, double pf, double k, double kt)
      {
          return pi * Math.Pow((pf / pi), (k / kt));
      }

      public double Voisinage_f(int rc, int t, double cgm)
      {

          return Math.Exp(-Math.Abs(rc - t) / 2 * Math.Pow(cgm, 2));
      }


    }
}
