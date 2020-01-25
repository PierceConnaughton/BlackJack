using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();

        int playerNum = 0;
        int dealerNum = 0;

        string dealerNumString;
        string playerNumString;
        


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (txtBxEnterName.Text != null)
            {
                MessageBox.Show("Not Null");

                int firstCard = rnd.Next(1, 11);
                int secondCard = rnd.Next(1, 12);
                int dealerFirstCard = rnd.Next(1, 11);

                playerNum = firstCard + secondCard;

                playerNumString = playerNum.ToString();

                txtBlPlayerTotal.Text = playerNumString;

                

                dealerNum = dealerFirstCard;

                dealerNumString = dealerNum.ToString();
            }
            else
            {
                MessageBox.Show("Enter Name First");
            }
        }
    }
}
