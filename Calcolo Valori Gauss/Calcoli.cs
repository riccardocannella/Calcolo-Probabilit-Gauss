/*
 Calcolo Valori Gauss - Un'app per calcolare l'area in un certo intervallo
 della funzione di distribuzione di probabilità di Gauss.
 Copyright (c) VIRICO 2017

 This program is free software: you can redistribute it and/or modify it under
 the terms of the GNU General Public License as published by the Free Software
 Foundation, either version 3 of the License, or (at your option) any later version.

 This program is distributed in the hope that it will be useful, but WITHOUT ANY
 WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A
 PARTICULAR PURPOSE. See the GNU General Public License for more details.

 You should have received a copy of the GNU General Public License along with
 this program. If not, see http://www.gnu.org/licenses/.
 */

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

        /// <summary>
        /// Funzione che calcola l'integrale tra a e b della funzione di densità di probabilità.
        /// </summary>
        /// <param name="a">L'estremo sinistro dell'intervallo. Può anche essere -∞.</param>
        /// <param name="b">L'estremo destro dell'intervallo. Può anche essere +∞.</param>
        /// <param name="sigma">Lo scarto quadratico medio. Se non passato, di default è 1.</param>
        /// <param name="mu">La media. Se non passata, di default è 0.</param>
        /// <returns>Il valore dell'area</returns>
        public static double CalcolaIntegrale(double a, double b, double sigma = 1, double mu = 0)
        {
            //check condizioni di operabilità
            if (a > b || a.Equals(double.PositiveInfinity) || b.Equals(double.NegativeInfinity) || sigma < 0 || mu < 0)
                return double.NaN;
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
            model.Series[1].Title = "Area = " + CalcolaIntegrale(a, b, sigma, mu);
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