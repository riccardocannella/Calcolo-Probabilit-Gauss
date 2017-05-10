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
        public static double CalcolaIntegrale(double a, double b, double sigma = 1, double mu = 0)
        {
            double piGreco = Math.PI;

            double y = SimpsonRule.IntegrateComposite(x => 1 / Math.Sqrt(2 * piGreco * Math.Pow(sigma, 2)) * Math.Exp(-0.5 * Math.Pow((x - mu) / sigma, 2)), a, b, 100000);
            return y;
        }
    }
}