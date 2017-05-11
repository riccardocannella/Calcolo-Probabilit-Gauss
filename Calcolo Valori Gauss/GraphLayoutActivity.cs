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

            Func<double, double> fnGauss = (x) => 1 / Math.Sqrt(2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-0.5 * Math.Pow((x - mu) / sigma, 2));

            model.Series.Add(new FunctionSeries(fnGauss, (mu -5*sigma), (mu + 5*sigma), 0.0001));
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1 });
            model.Axes[1].AbsoluteMinimum = 0;
            model.InvalidatePlot(true);
            return model;
        }
    }
}