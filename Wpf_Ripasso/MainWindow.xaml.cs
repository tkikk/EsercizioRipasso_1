using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Ripasso
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //per il bottone stop si crea questa classe
        private CancellationTokenSource token = new CancellationTokenSource();


        private void btnStart1_Click(object sender, RoutedEventArgs e)
        {

            if (token == null)//se il token è uguale  a nullo si ricrea

                token = new CancellationTokenSource();

            Task.Factory.StartNew(() => Conteggio(token, 10000, lblRis)); //parte la prima label
        }
        // viene estratto il metodo

        private void Conteggio(CancellationTokenSource token, int max, Label lbl)
        {
            for (int i = 0; i < max; i++)
            {

                Dispatcher.Invoke(() => AggiornaUI(i, lbl));
                Thread.Sleep(500);
                //come rallentare il processo
                Dispatcher.Invoke(() => AggiornaUI1(lbl));
                Thread.Sleep(500);
                if (token.Token.IsCancellationRequested)
                    break;//si blocca il ciclo se il token ha una certa condizione

            }
            Dispatcher.Invoke(() => AggiornaUI1(lbl));
        }


        private void AggiornaUI2(Label lbl)
        {
            lbl.Content = "Ho finito!";
        }

        private void AggiornaUI(int i, Label lbl)
        {
            lbl.Content = $"sto contando..{ i.ToString()}";
        }
        private void AggiornaUI1(Label lbl)
        {
            lbl.Content = "sto aspettando ..";
        }

        private void btnStop1_Click(object sender, RoutedEventArgs e)
        {
            if (token != null)
            {
                token.Cancel();
                token = null;
            }
        }

        private void btnStart2_Click(object sender, RoutedEventArgs e)
        {
            int max = Convert.ToInt32(txtStap.Text);
            if (token == null)//se il token è uguale  a nullo si ricrea

                token = new CancellationTokenSource();
            Task.Factory.StartNew(() => Conteggio(token, max, lblRis2));
        }

        private void btnStart3_Click(object sender, RoutedEventArgs e)
        {
            int massimo = Convert.ToInt32(txtStap.Text);
            if (token == null)//se il token è uguale  a nullo si ricrea

                token = new CancellationTokenSource();
            Task.Factory.StartNew(() => Conteggio(token, massimo, lblRis3));
        }
    }
}



