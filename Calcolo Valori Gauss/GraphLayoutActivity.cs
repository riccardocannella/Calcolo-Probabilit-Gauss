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
            SetContentView(Resource.Layout.GraphLayout);
            // Create your application here
            PlotView view = FindViewById<PlotView>(Resource.Id.plot_view);
            view.Model = Calcoli.CreatePlotModel();
            
        }

        private PlotModel CreatePlotModel()
        {
            var plotModel = new PlotModel { Title = "OxyPlot Demo" };

            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.None, Maximum = 10, Minimum = 0 });

            var series1 = new LineSeries
            {
                MarkerType = MarkerType.Square,
                StrokeThickness = 1,
                Smooth = false,
                Color = OxyColors.Black
            };

            series1.Points.Add(new DataPoint(0.0, 6.0));
            series1.Points.Add(new DataPoint(9.2, 2.1));
            series1.Points.Add(new DataPoint(2.0, 4.2));
            series1.Points.Add(new DataPoint(3.3, 8.9));
            series1.Points.Add(new DataPoint(4.7, 7.4));
            series1.Points.Add(new DataPoint(6.0, 6.2));
            series1.Points.Add(new DataPoint(0.0, 0.0));

            plotModel.Series.Add(series1);
            plotModel.InvalidatePlot(true);
            return plotModel;
        }
    }
}