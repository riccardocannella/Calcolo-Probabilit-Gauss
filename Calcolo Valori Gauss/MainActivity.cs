using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace Calcolo_Valori_Gauss
{
    [Activity(Label = "Calcolo Valori Gauss", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //<application android:label="Calcolo Valori Gauss" android:theme="@android:style/Theme.Material.Light.LightStatusBar"></application>
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button BtnCalcola = FindViewById<Button>(Resource.Id.btnCalcola);

            double risultato = double.NaN;
            BtnCalcola.Click += (object sender, EventArgs e) =>
            {
                risultato = EseguiParsing();
                TextView TxtRisultato = FindViewById<TextView>(Resource.Id.txtRisultato);
                TxtRisultato.Text = risultato.ToString();
            };
        }

        private double EseguiParsing()
        {
            EditText A = FindViewById<EditText>(Resource.Id.txtA);
            double.TryParse(A.Text, out double a);

            EditText B = FindViewById<EditText>(Resource.Id.txtB);
            double.TryParse(B.Text, out double b);

            EditText Mu = FindViewById<EditText>(Resource.Id.txtMu);
            double.TryParse(Mu.Text, out double mu);

            EditText Sigma = FindViewById<EditText>(Resource.Id.txtSigma);
            double.TryParse(Sigma.Text, out double sigma);

            return Calcoli.CalcolaIntegrale(a, b, sigma, mu);
        }
    }
}

