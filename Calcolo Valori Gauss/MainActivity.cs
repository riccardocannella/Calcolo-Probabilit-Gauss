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
using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using System.Globalization;
using Android.Content.PM;

namespace Calcolo_Valori_Gauss
{
    [Activity(Label = "Calcolo Valori Gauss", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private double mu, sigma, a, b;
        protected override void OnCreate(Bundle bundle)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            // catturo il bottone per calcolare l'area
            Button BtnCalcola = FindViewById<Button>(Resource.Id.btnCalcola);
            double risultato = double.NaN;
            // mi iscrivo all'evento click
            BtnCalcola.Click += (object sender, EventArgs e) =>
            {
                NascondiTastiera();
                risultato = EseguiParsing();
                TextView TxtRisultato = FindViewById<TextView>(Resource.Id.txtRisultato);
                //Se il risultato esiste stampalo altrimenti no
                if (!risultato.Equals(double.NaN))
                    if (risultato.Equals(1) || risultato.ToString().Length < 7)
                        TxtRisultato.Text = "Area = " + risultato.ToString();
                    else
                        TxtRisultato.Text = "Area = " + risultato.ToString().Substring(0, 7);
                else
                    TxtRisultato.Text = "";
            };

            Button btn = FindViewById<Button>(Resource.Id.button1);
            btn.Click += (object sender, EventArgs e) =>
            {
                NascondiTastiera();
                var intent = new Intent(this, typeof(GraphLayoutActivity));
                double provaParse = EseguiParsing();
                if (provaParse.Equals(double.NaN)) { }
                else
                {
                    intent.PutExtra("mu", mu);
                    intent.PutExtra("sigma", sigma);
                    intent.PutExtra("a", a);
                    intent.PutExtra("b", b);
                    StartActivity(intent);
                }
            };
        }
        /// <summary>
        /// Metodo privato per convertire tutti i testi in valori double
        /// </summary>
        /// <returns>Il valore dell'area dell'integrale, o NaN se non è possibile calcolarla</returns>
        private double EseguiParsing()
        {
            EditText A = FindViewById<EditText>(Resource.Id.txtA);
            EditText B = FindViewById<EditText>(Resource.Id.txtB);
            EditText Mu = FindViewById<EditText>(Resource.Id.txtMu);
            EditText Sigma = FindViewById<EditText>(Resource.Id.txtSigma);

            if (A.Text.Equals(""))
                a = double.NegativeInfinity;
            else
                if (A.Text.Equals("-") || A.Text.Equals(".") || A.Text.Equals("-."))
            {
                FindViewById<EditText>(Resource.Id.txtA).Text = "";
                MessaggioDiErrore("Estremo sinistro non valido");
                return Double.NaN;
            }
            else
                double.TryParse(A.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out a);
            if (B.Text.Equals(""))
                b = double.PositiveInfinity;
            else
                if (B.Text.Equals("-") || B.Text.Equals(".") || B.Text.Equals("-."))
            {
                FindViewById<EditText>(Resource.Id.txtB).Text = "";
                MessaggioDiErrore("Estremo destro non valido");
                return Double.NaN;
            }
            else
                double.TryParse(B.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out b);

            if (a > b && (a != double.NegativeInfinity && b != double.NegativeInfinity))
            {
                FindViewById<EditText>(Resource.Id.txtA).Text = "";
                FindViewById<EditText>(Resource.Id.txtB).Text = "";
                MessaggioDiErrore("Estremo sinistro maggiore dell'estremo destro");
                return Double.NaN;
            }

            double.TryParse(Mu.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out mu);
            if (Mu.Text.Equals(""))
            {
                mu = 0;
            }

            double.TryParse(Sigma.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out sigma);
            if (Sigma.Text.Equals(""))
            {
                sigma = 1;
            }
            if (sigma == 0)
            {
                FindViewById<EditText>(Resource.Id.txtSigma).Text = "";
                MessaggioDiErrore("σ non può essere uguale a 0");
                return Double.NaN;
            }
            return Calcoli.CalcolaIntegrale(a, b, sigma, mu);
        }
        /// <summary>
        /// Metodo privato per nascondere la tastiera
        /// </summary>
        private void NascondiTastiera()
        {
            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
        }
        /// <summary>
        /// Metodo privato per generare degli alert di errore personalizzati
        /// </summary>
        /// <param name="stringa">Il messaggio di errore da visualizzare</param>
        private void MessaggioDiErrore(string stringa)
        {
            new AlertDialog.Builder(this)
                .SetTitle("Attenzione")
                .SetMessage(stringa)
                .SetNeutralButton("Ok", (senderAlert, args) =>
                {
                    //Non Fare niente
                })
                .Show();
        }
    }
}

