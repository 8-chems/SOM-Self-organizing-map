using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_SOM_BERBEGUE.SOM_Self_Implmentation
{    
    class Neurone
    {
        double[] poids;
        public int vw=0;
        int order;
        public Neurone(int inter)
        {
            poids=new Double[inter];
        }

        public void randomize()
        {   Random r = new Random();
        for (int i = 0; i < poids.Length;i++ )
        {
            poids[i] = r.NextDouble() % 255;
        }
        }
    
    
    }
}
