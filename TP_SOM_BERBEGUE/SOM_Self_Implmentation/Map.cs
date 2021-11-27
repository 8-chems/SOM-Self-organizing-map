using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.Drawing;
using Microsoft.VisualBasic;


public class Map
{

    private Neuron[,] outputs;  // Collection of weights.

    private int iteration;      // Current iteration.

    private int length;        // Side length of output grid.

    private int dimensions;    // Number of input dimensions.

    private Random rnd = new Random();

    private DataTable dt;

    private List<string> labels = new List<string>();

    private List<double[]> patterns = new List<double[]>();



   


    public Map(int dimensions, int length, DataTable dt)
    {
        this.length = length;

        this.dimensions = dimensions;

        Initialise();

        LoadData(dt);
        Train(0.0000001/*a modifié*/);
        DumpCoordinates();

    }



    private void Initialise()
    {

        outputs = new Neuron[length, length];

        for (int i = 0; i < length; i++)
        {

            for (int j = 0; j < length; j++)
            {

                outputs[i, j] = new Neuron(i, j, length);

                outputs[i, j].Weights = new double[dimensions];

                for (int k = 0; k < dimensions; k++)
                {

                    outputs[i, j].Weights[k] = rnd.NextDouble();

                }

            }

        }

    }



    private void LoadData(DataTable dt)
    {
          string[] ss = (from dc in dt.Columns.Cast<DataColumn>()
                           select dc.ColumnName).ToArray();
        string sm="";
        for(int i=0;i<(ss.Length-1);i++){sm=ss[i]+",";};sm+=ss[ss.Length-1];
            labels.Add(sm);

            double[] inputs = new double[dimensions];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                inputs = this.getRow(i);
                patterns.Add(inputs);
            }

    }
   
    public double[] getRow(int i)
    {
        int n = this.dt.Columns.Count;
        double[] arr = new double[n];
        DataRow dr = this.dt.Rows[i];
        for (int j = 0; j < n; j++) arr[j] = dr.Field<double>(j);
        return arr;
    }


    private void Train(double maxError)
    {

        double currentError = double.MaxValue;

        while (currentError > maxError)
        {

            currentError = 0;

            List<double[]> TrainingSet = new List<double[]>();

            foreach (double[] pattern in patterns)
            {

                TrainingSet.Add(pattern);

            }

            // choisir l'exemple aléatoirement
            for (int i = 0; i < patterns.Count; i++)
            {

                double[] pattern = TrainingSet[rnd.Next(patterns.Count - i)];

                currentError += TrainPattern(pattern);

                TrainingSet.Remove(pattern);

            }
}

    }



    private double TrainPattern(double[] pattern)
    {

        double error = 0;

        Neuron winner = Winner(pattern);

        for (int i = 0; i < length; i++)
        {

            for (int j = 0; j < length; j++)
            {

                error += outputs[i, j].UpdateWeights(pattern, winner, iteration);

            }

        }

        iteration++;

        return Math.Abs(error / (length * length));

    }



    private void DumpCoordinates()
    {

        for (int i = 0; i < patterns.Count; i++)
        {

            Neuron n = Winner(patterns[i]);
}

}



    private Neuron Winner(double[] pattern)
    {

        Neuron winner = null;

        double min = double.MaxValue;

        for (int i = 0; i < length; i++)

            for (int j = 0; j < length; j++)
            {

                double d = Distance(pattern, outputs[i, j].Weights);

                if (d < min)
                {

                    min = d;

                    winner = outputs[i, j];

                }

            }

        return winner;

    }



    private double Distance(double[] vector1, double[] vector2)
    {

        double value = 0;

        for (int i = 0; i < vector1.Length; i++)
        {

            value += Math.Pow((vector1[i] - vector2[i]), 2);

        }

        return Math.Sqrt(value);

    }

}



public class Neuron
{

    public double[] Weights;

    public int X;

    public int Y;

    private int length;

    private double nf;



    public Neuron(int x, int y, int length)
    {

        X = x;

        Y = y;

        this.length = length;

        nf = 1000 / Math.Log(length);

    }



    private double Gauss(Neuron win, int it)
    {

        double distance = Math.Sqrt(Math.Pow(win.X - X, 2) + Math.Pow(win.Y - Y, 2));

        return Math.Exp(-Math.Pow(distance, 2) / (Math.Pow(Strength(it), 2)));

    }


    // it est itération actual
    private double LearningRate(int it)
    {

        return Math.Exp(-it / 1000) * 0.1;

    }



    private double Strength(int it)
    {

        return Math.Exp(-it / nf) * length;

    }



    public double UpdateWeights(double[] pattern, Neuron winner, int it)
    {

        double sum = 0;

        for (int i = 0; i < Weights.Length; i++)
        {

            double delta = LearningRate(it) * Gauss(winner, it) * (pattern[i] - Weights[i]);

            Weights[i] += delta;

            sum += delta;

        }

        return sum / Weights.Length;

    }

}