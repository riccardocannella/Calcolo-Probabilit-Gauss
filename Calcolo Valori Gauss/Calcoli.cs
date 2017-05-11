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
                if (b >= mu)
                    return 0.5 + SimpsonRule.IntegrateComposite(funzioneDensita(mu, sigma), mu, b, 100000);
                else
                    return 0.5 - SimpsonRule.IntegrateComposite(funzioneDensita(mu, sigma), mu, -b, 100000);
            // valore e +inf
            else if (b == double.PositiveInfinity)
                if (a >= mu)
                    return 0.5 - SimpsonRule.IntegrateComposite(funzioneDensita(mu, sigma), mu, a, 100000);
                else
                    return 0.5 + SimpsonRule.IntegrateComposite(funzioneDensita(mu, sigma), mu, -a, 100000);

            return SimpsonRule.IntegrateComposite(funzioneDensita(mu, sigma), a, b, 100000);
        }

        public static PlotModel CreatePlotModel(double a, double b, double mu, double sigma)
        {
            var model = new PlotModel { };
            var fnDensita = Calcoli.funzioneDensita(mu, sigma);
            model.Series.Add(new FunctionSeries(fnDensita, (mu - 5 * sigma), (mu + 5 * sigma), 0.0001) { Color = OxyColors.Red, Background = OxyColor.FromRgb(220, 220, 220) });
            model.Series[0].Title = "Fn Densità, µ = " + mu + ", σ = " + sigma;
            model.IsLegendVisible = true;

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1, AbsoluteMinimum = 0 });


            // linea per colorare la porzione di funzione delimitata da a e b
            double aVero, bVero;
            if (a.Equals(double.NegativeInfinity)) aVero = mu - 5 * sigma;
            else aVero = a;
            if (b.Equals(double.PositiveInfinity)) bVero = mu + 5 * sigma;
            else bVero = b;

            var area = new FunctionSeries(fnDensita, aVero, bVero, 0.0001);
            // linea verticale per il punto a
            var lineaA = new LineSeries();
            lineaA.Points.Add(new DataPoint(aVero, 0));
            lineaA.Points.Add(new DataPoint(aVero, fnDensita(a)));
            // linea verticale per il punto b
            var lineaB = new LineSeries();
            lineaB.Points.Add(new DataPoint(bVero, 0));
            lineaB.Points.Add(new DataPoint(bVero, fnDensita(b)));
            var lineaAsse = new LineSeries();
            //linea orizzontale da a a b
            lineaAsse.Points.Add(new DataPoint(aVero, 0));
            lineaAsse.Points.Add(new DataPoint(bVero, 0));
            // setto i colori per l'area
            area.Color = OxyColors.Black;
            lineaA.Color = OxyColors.Black;
            lineaB.Color = OxyColors.Black;
            lineaAsse.Color = OxyColors.Black;
            // aggiungo tutte le linee 
            model.Series.Add(area);
            model.Series[1].Title = "Area = " + CalcolaIntegrale(a,b,sigma,mu);
            model.IsLegendVisible = true;
            // model.Series.Add(lineaCentro);
            model.Series.Add(lineaA);
            model.Series.Add(lineaB);
            model.Series.Add(lineaAsse);

            model.InvalidatePlot(true);
            return model;
        }

        public static Func<double, double> funzioneDensita(double mu, double sigma)
        {
            return (x) => 1 / Math.Sqrt(2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-0.5 * Math.Pow((x - mu) / sigma, 2));
        }
    }
}