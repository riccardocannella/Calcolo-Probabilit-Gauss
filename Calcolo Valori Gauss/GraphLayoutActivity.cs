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
using OxyPlot.Xamarin.Android;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Globalization;
using Android.Content.PM;

namespace Calcolo_Valori_Gauss
{
    [Activity(Label = "Grafico dell'Area", ScreenOrientation = ScreenOrientation.Portrait)]
    public class GraphLayoutActivity : Activity
    {
        double mu, sigma, a, b;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // vado a catturare le variabili
            SetVariabili();
            // aggiungo il controllo per il grafico
            var plotView = new PlotView(this);
            // aggiungo il grafico al controllo
            plotView.Model = Calcoli.CreatePlotModel(a, b, mu, sigma);
            // inizializzo la view
            SetContentView(Resource.Layout.GraphLayout);
            // aggiungo il grafico nella view
            this.AddContentView(plotView,
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
        }
        /// <summary>
        /// Metodo privato che va a catturare le variabili necessarie al disegno del grafico.
        /// Queste variabili sono state passate da un'altra Activity.
        /// </summary>
        private void SetVariabili()
        {
            // se non trovo mu la setto a 0 per avere la distribuzione standard
            mu = Intent.GetDoubleExtra("mu", 0.0);
            // se non trovo sigma la setto a 0 per avere la distribuzione standard
            sigma = Intent.GetDoubleExtra("sigma", 1.0);
            // se non trovo a la setto a meno infinito forzosamente
            a = Intent.GetDoubleExtra("a", double.NegativeInfinity);
            // se non trovo b la setto a più infinito forzosamente
            b = Intent.GetDoubleExtra("b", double.PositiveInfinity);
        }
        
    }
}