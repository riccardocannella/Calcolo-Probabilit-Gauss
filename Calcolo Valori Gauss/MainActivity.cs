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
            Console.OutputEncoding = System.Text.Encoding.UTF8;
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
                //Se il risultato esiste stampalo altrimenti no
                if (!risultato.ToString().Equals("NaN"))
                    TxtRisultato.Text = risultato.ToString();
                else
                    TxtRisultato.Text = "";
            };
        }

        private double EseguiParsing()
        {
            EditText A = FindViewById<EditText>(Resource.Id.txtA);
            double.TryParse(A.Text, out double a);

            EditText B = FindViewById<EditText>(Resource.Id.txtB);
            double.TryParse(B.Text, out double b);

            if (a > b)
                MessaggioDiErrore("Estremo sinistro maggiore dell'estremo destro");

            EditText Mu = FindViewById<EditText>(Resource.Id.txtMu);
            double.TryParse(Mu.Text, out double mu);

            EditText Sigma = FindViewById<EditText>(Resource.Id.txtSigma);
            double.TryParse(Sigma.Text, out double sigma);
            if(sigma == 0)
                MessaggioDiErrore("σ non può essere uguale a 0");

            return Calcoli.CalcolaIntegrale(a, b, sigma, mu);
        }

        private void MessaggioDiErrore(string stringa)
        {
            new AlertDialog.Builder(this)
                .SetTitle("Attenzione")
                .SetMessage(stringa)
                .SetNeutralButton("Ok",(senderAlert, args) => {
                    FindViewById<EditText>(Resource.Id.txtA).Text = "";
                    FindViewById<EditText>(Resource.Id.txtB).Text = "";
                    FindViewById<EditText>(Resource.Id.txtSigma).Text = "";
                    FindViewById<EditText>(Resource.Id.txtMu).Text = "";
                })
                .Show();
        }
    }
}

