﻿using System;
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
            var plotView = new PlotView(this);
            plotView.Model = CreatePlotModel();
            SetContentView(Resource.Layout.GraphLayout);
            this.AddContentView(plotView,
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
            // Create your application here
            //PlotView view = FindViewById<PlotView>(Resource.Id.plot_view);
            //view.Model = Calcoli.CreatePlotModel();

        }

        private PlotModel CreatePlotModel()
        {
            var model = new PlotModel { Title = "Fun with Bats" };

            Func<double, double> batFn1 = (x) => 2 * Math.Sqrt(-Math.Abs(Math.Abs(x) - 1) * Math.Abs(3 - Math.Abs(x)) / ((Math.Abs(x) - 1) * (3 - Math.Abs(x)))) * (1 + Math.Abs(Math.Abs(x) - 3) / (Math.Abs(x) - 3)) * Math.Sqrt(1 - Math.Pow((x / 7), 2)) + (5 + 0.97 * (Math.Abs(x - 0.5) + Math.Abs(x + 0.5)) - 3 * (Math.Abs(x - 0.75) + Math.Abs(x + 0.75))) * (1 + Math.Abs(1 - Math.Abs(x)) / (1 - Math.Abs(x)));
            Func<double, double> batFn2 = (x) => -3 * Math.Sqrt(1 - Math.Pow((x / 7), 2)) * Math.Sqrt(Math.Abs(Math.Abs(x) - 4) / (Math.Abs(x) - 4));
            Func<double, double> batFn3 = (x) => Math.Abs(x / 2) - 0.0913722 * (Math.Pow(x, 2)) - 3 + Math.Sqrt(1 - Math.Pow((Math.Abs(Math.Abs(x) - 2) - 1), 2));
            Func<double, double> batFn4 = (x) => (2.71052 + (1.5 - .5 * Math.Abs(x)) - 1.35526 * Math.Sqrt(4 - Math.Pow((Math.Abs(x) - 1), 2))) * Math.Sqrt(Math.Abs(Math.Abs(x) - 1) / (Math.Abs(x) - 1)) + 0.9;

            model.Series.Add(new FunctionSeries(batFn1, -8, 8, 0.0001));
            model.Series.Add(new FunctionSeries(batFn2, -8, 8, 0.0001));
            model.Series.Add(new FunctionSeries(batFn3, -8, 8, 0.0001));
            model.Series.Add(new FunctionSeries(batFn4, -8, 8, 0.0001));

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1 });

            model.InvalidatePlot(true);
            return model;
        }
    }
}