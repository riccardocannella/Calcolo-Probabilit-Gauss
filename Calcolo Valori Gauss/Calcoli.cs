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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

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
            // - e + inf
            if (a == double.NegativeInfinity && b == double.PositiveInfinity) return 1;
            // -inf e valore
            if (a == double.NegativeInfinity)
                if (b >= 0)
                    return 0.5 + SimpsonRule.IntegrateComposite(x => 1 / Math.Sqrt(2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-0.5 * Math.Pow((x - mu) / sigma, 2)), 0, b, 100000);
                else
                    return 0.5 - SimpsonRule.IntegrateComposite(x => 1 / Math.Sqrt(2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-0.5 * Math.Pow((x - mu) / sigma, 2)), 0, -b, 100000);
            // valore e +inf
            else if (b == double.PositiveInfinity)
                if (a >= 0)
                    return 0.5 - SimpsonRule.IntegrateComposite(x => 1 / Math.Sqrt(2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-0.5 * Math.Pow((x - mu) / sigma, 2)), 0, a, 100000);
                else
                    return 0.5 + SimpsonRule.IntegrateComposite(x => 1 / Math.Sqrt(2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-0.5 * Math.Pow((x - mu) / sigma, 2)), 0, -a, 100000);

            return SimpsonRule.IntegrateComposite(x => 1 / Math.Sqrt(2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-0.5 * Math.Pow((x - mu) / sigma, 2)), a, b, 100000);
        }

        public static double ControllaInfiniti(double valore)
        {
            if (valore == double.PositiveInfinity) return (double)int.MaxValue;
            else if (valore == double.NegativeInfinity) return (double)int.MinValue;
            return valore;
        }
    }
}