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
        double mu, sigma, a, b;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetVariabili();
            var plotView = new PlotView(this);
            plotView.Model = Calcoli.CreatePlotModel(a, b, mu, sigma);
            SetContentView(Resource.Layout.GraphLayout);
            this.AddContentView(plotView,
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
            // Create your application here
            //PlotView view = FindViewById<PlotView>(Resource.Id.plot_view);
            //view.Model = Calcoli.CreatePlotModel();

        }
        private void SetVariabili()
        {
            mu = Intent.GetDoubleExtra("mu", 0.0);
            sigma = Intent.GetDoubleExtra("sigma", 1.0);
            a = Intent.GetDoubleExtra("a", double.NaN);
            b = Intent.GetDoubleExtra("b", double.NaN);
        }
        
    }
}