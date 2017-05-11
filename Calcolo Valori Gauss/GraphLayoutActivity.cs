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

namespace Calcolo_Valori_Gauss
{
    [Activity(Label = "GraphLayoutActivity")]
    public class GraphLayoutActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            double mu = Intent.GetDoubleExtra("mu", 0.0);
            double sigma = Intent.GetDoubleExtra("sigma", 2.0);
            double a = Intent.GetDoubleExtra("a", 0.0);
            double b = Intent.GetDoubleExtra("b", 0.0);

            var plotView = new PlotView(this);
            plotView.Model = CreatePlotModel(mu, sigma);
            SetContentView(Resource.Layout.GraphLayout);
            this.AddContentView(plotView,
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
            // Create your application here
            //PlotView view = FindViewById<PlotView>(Resource.Id.plot_view);
            //view.Model = Calcoli.CreatePlotModel();

        }

        private PlotModel CreatePlotModel(double a = double.NaN, double b = double.NaN, double mu = 0.0, double sigma = 1.0)
        {
            var model = new PlotModel { Title = "Grafico della distribuzione" };
<<<<<<< Updated upstream

            Func<double, double> fnGauss = (x) => 1 / Math.Sqrt(2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-0.5 * Math.Pow((x - mu) / sigma, 2));

            model.Series.Add(new FunctionSeries(fnGauss, (mu -5*sigma), (mu + 5*sigma), 0.0001));
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1 });
            model.Axes[1].AbsoluteMinimum = 0;
=======
            // questa funzione è la funzione di densità della distribuzione normale
            Func<double, double> fnDensita = (x) => 1 / Math.Sqrt(2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-0.5 * Math.Pow((x - mu) / sigma, 2));
            // creo la linea della funzione con dei limiti ampi a sufficienza
            model.Series.Add(new FunctionSeries(fnDensita, (mu - 5 * sigma), (mu + 5 * sigma), 0.0001) { Color = OxyColors.OrangeRed, Background=OxyColor.FromRgb(220, 220, 220)});
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1, AbsoluteMinimum = 0 });
            // linea per colorare la porzione di funzione delimitata da a e b
            var area = new FunctionSeries(fnDensita, a, b, 0.0001);
            // linea verticale per il punto a
            var lineaA = new LineSeries();
            lineaA.Points.Add(new DataPoint(a, 0));
            lineaA.Points.Add(new DataPoint(a, fnDensita(a)));
            // linea verticale per il punto b
            var lineaB = new LineSeries();
            lineaB.Points.Add(new DataPoint(b, 0));
            lineaB.Points.Add(new DataPoint(b, fnDensita(b)));
            var lineaAsse = new LineSeries();
            //linea orizzontale da a a b
            lineaAsse.Points.Add(new DataPoint(a, 0));
            lineaAsse.Points.Add(new DataPoint(b, 0));
            // setto i colori per l'area
            area.Color = OxyColors.Black;
            lineaA.Color = OxyColors.Black;
            lineaB.Color = OxyColors.Black;
            lineaAsse.Color = OxyColors.Black;
            // aggiungo tutte le linee 
            model.Series.Add(area);
            model.Series.Add(lineaA);
            model.Series.Add(lineaB);
            model.Series.Add(lineaAsse);
            // impongo al grafico di ridisegnarsi
>>>>>>> Stashed changes
            model.InvalidatePlot(true);
            return model;
        }
    }
}