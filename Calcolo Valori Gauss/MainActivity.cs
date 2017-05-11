using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using System.Globalization;

namespace Calcolo_Valori_Gauss
{
    [Activity(Label = "Calcolo Valori Gauss", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private double mu, sigma, a, b;
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
                NascondiTastiera();
                risultato = EseguiParsing();
                TextView TxtRisultato = FindViewById<TextView>(Resource.Id.txtRisultato);
                //Se il risultato esiste stampalo altrimenti no
                if (!risultato.ToString().Equals("NaN"))
                    TxtRisultato.Text = risultato.ToString();
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

        private double EseguiParsing()
        {
            EditText A = FindViewById<EditText>(Resource.Id.txtA);
            EditText B = FindViewById<EditText>(Resource.Id.txtB);
            EditText Mu = FindViewById<EditText>(Resource.Id.txtMu);
            EditText Sigma = FindViewById<EditText>(Resource.Id.txtSigma);

            if (A.Text.Equals(""))
                a = double.NegativeInfinity;
            else
                if (A.Text.Equals("-") || A.Text.Equals(".") || A.Text.Equals("-.")) {
                FindViewById<EditText>(Resource.Id.txtA).Text = "";
                MessaggioDiErrore("Estremo sinistro non valido");
                return Double.NaN; }
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

        private void NascondiTastiera()
        {
            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
        }

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

