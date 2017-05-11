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
using OxyPlot.Xamarin.Android;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Globalization;

namespace Calcolo_Valori_Gauss
{
    [Activity(Label = "Grafico dell'Area")]
    public class GraphLayoutActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            double mu = Intent.GetDoubleExtra("mu", 0.0);
            double sigma = Intent.GetDoubleExtra("sigma", 1.0);
            double a = Intent.GetDoubleExtra("a", double.NaN);
            double b = Intent.GetDoubleExtra("b", double.NaN);

            var plotView = new PlotView(this);
            plotView.Model = CreatePlotModel(a, b, mu, sigma);
            SetContentView(Resource.Layout.GraphLayout);
            this.AddContentView(plotView,
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
            // Create your application here
            //PlotView view = FindViewById<PlotView>(Resource.Id.plot_view);
            //view.Model = Calcoli.CreatePlotModel();

        }

        private PlotModel CreatePlotModel(double a, double b, double mu, double sigma)
        {
            var model = new PlotModel { };
            var fnDensita = Calcoli.funzioneDensita(mu, sigma);
            model.Series.Add(new FunctionSeries(fnDensita, (mu - 5 * sigma), (mu + 5 * sigma), 0.0001) { Color = OxyColors.Red, Background = OxyColor.FromRgb(220, 220, 220) });

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
            model.Series[1].Title = "Area";
            model.IsLegendVisible = true;
            // model.Series.Add(lineaCentro);
            model.Series.Add(lineaA);
            model.Series.Add(lineaB);
            model.Series.Add(lineaAsse);

            model.InvalidatePlot(true);
            return model;
        }
    }
}