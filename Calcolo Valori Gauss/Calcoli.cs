using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MathNet.Numerics.Integration;

namespace Calcolo_Valori_Gauss
{
    class Calcoli
    {
        /*
         Funzione che calcola l'integrale della funzione di densità di probabilità gaussiana, vuole 4 parametri:
            - a: estremo sinistro dell'integrale
            - b: estremo destro dell'integrale
            - sigma: scarto quadratico medio (1 di default)
            - mu : media (0 di default)
         */
        public static double CalcolaIntegrale(double a, double b, double sigma = 1, double mu = 0)
        {
            return SimpsonRule.IntegrateComposite(x => 1 / Math.Sqrt(2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-0.5 * Math.Pow((x - mu) / sigma, 2)), a, b, 100000);
        }
    }
}