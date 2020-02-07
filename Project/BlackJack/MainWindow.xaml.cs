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
using System.IO;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //create a random num
        Random rnd = new Random();

        //properties for player's Card number and the dealer's card nu8mber
        int playerNum = 0;
        int dealerNum = 0;
        

        string dealerNumString;
        string playerNumString;

        //list of all the current players
        List<Player> players = new List<Player>();

        //bools for each instance of the game not being started correctley it will not start
        bool gameRestarted = true;
        bool gameInProgress = false;
        bool playerReturned = false;
        bool gameStarted = false;

        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        //if start button clicked
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //check if the game has been restarted to 0
            if (gameRestarted == true)
            {
                //check if anything is in text box
                if (String.IsNullOrEmpty(txtBxEnterName.Text))
                {
                    MessageBox.Show("Enter Name First");
                }
                else
                {
                    //else start the game thus turing the game started bool too true
                    gameStarted = true;

                    //get random cards between 2 and 21
                    int firstCard = rnd.Next(1, 11);
                    int secondCard = rnd.Next(1, 12);

                    //get dealers first card and show it
                    int dealerFirstCard = rnd.Next(1, 11);

                    //add players first too cards and turn them into a string too display
                    playerNum = firstCard + secondCard;

                    playerNumString = playerNum.ToString();

                    txtBlPlayerTotal.Text = playerNumString;

                    //show dealers first card as a string
                    dealerNum = dealerFirstCard;

                    dealerNumString = dealerNum.ToString();

                    txtBlDealerTotal.Text = dealerNumString;

                    //check if player is a returning one
                    foreach (Player returningPlayer in players)
                    {
                        //check if player name can be found in player list if so turn returning player too true and display message
                        if (returningPlayer.PlayerName == txtBxEnterName.Text)
                        {
                            playerReturned = true;
                            MessageBox.Show("Returning Player");
                        }
                    }

                    //if player returning is true
                    if (playerReturned == true)
                    {

                        //display there name
                        foreach (Player returningPlayer in players)
                        {
                            if (returningPlayer.PlayerName == txtBxEnterName.Text)
                            {
                                returningPlayer.PlayerName = txtBxEnterName.Text;

                                txtBlCurrentPlayer.Text = returningPlayer.PlayerName;
                            }
                        }
                        //get whats turned into the txt box turn it into a string
                        string x = txtBxEnterName.Text;

                        gameInProgress = false;

                        //compare the string too the list and see which matches
                        foreach (Player currentPlayer in players)
                        {

                            //if the string matches the players name
                            if (x == currentPlayer.PlayerName)
                            {
                                //if the player num is equal too 21 they win
                                if (playerNum == 21)
                                {

                                    Win();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        txtBlWin.Text = "0";
                        txtBlLosses.Text = "0";
                        txtBlDraws.Text = "0";
                        //if player is not a returning player create a new player
                        Player newPlayer = new Player();

                        players.Add(newPlayer);


                        newPlayer.PlayerName = txtBxEnterName.Text;

                        txtBlCurrentPlayer.Text = newPlayer.PlayerName;

                       

                        string x = txtBxEnterName.Text;

                        gameInProgress = false;

                        foreach (Player currentPlayer in players)
                        {

                            if (x == currentPlayer.PlayerName)
                            {
                                if (playerNum == 21)
                                {

                                    Win();
                                    return;
                                }
                            }
                        }
                    }
                    


                    
                       

                }
            }

            //if game has not been restarted display message
            else
            {
                MessageBox.Show("Restart Game and press start");
            }
                
            

            
        }
        //on window load
        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            txtBlCurrentPlayer.Text = "Unknown";
            txtBlDealerTotal.Text = "0";
            txtBlDraws.Text = "0";
            txtBlPlayerTotal.Text = "0";
            txtBlWin.Text = "0";
            txtBlLosses.Text = "0";

        }

        //if btn has been clicked
        private void btnHitMe_Click(object sender, RoutedEventArgs e)
        {
            //if game has been restarted and the game has been started
            if (gameRestarted == true && gameInProgress == false && gameStarted == true)
            {
                if (String.IsNullOrEmpty(txtBxEnterName.Text))
                {
                    MessageBox.Show("Enter Name First");
                }
                else
                {
                    string x = txtBxEnterName.Text;

                    foreach (Player newPlayer in players)
                    {


                        if (newPlayer.PlayerName == x)
                        {
                            int hit = rnd.Next(1, 11);

                            playerNum = playerNum + hit;

                            txtBlPlayerTotal.Text = playerNum.ToString();

                            if (playerNum > 21)
                            {
                                Loss();
                                return;
                            }
                            else if (playerNum == 21)
                            {
                                Win();
                                return;
                            }
                        }




                    }

                }
            }
            else
            {
                if (String.IsNullOrEmpty(txtBxEnterName.Text))
                {
                    MessageBox.Show("Enter Name First");
                }
                else
                {
                    MessageBox.Show("Press Restart and then press start game");
                }
            }
        

            




        }

        private void btnFold_Click(object sender, RoutedEventArgs e)
        {
            if (gameRestarted == true && gameStarted == true)
            {
                if (String.IsNullOrEmpty(txtBxEnterName.Text))
                {
                    MessageBox.Show("Enter Name First");
                }


                else
                {
                    string x = txtBxEnterName.Text;

                    foreach (Player newPlayer in players)
                    {
                        

                        if (newPlayer.PlayerName == x)
                        {

                            Dealer();

                        }
                    }
                }
            }

            else
            {
                MessageBox.Show("Restart Game and press Start");
            }
        }
            

        public void Loss()
        {
            string x = txtBxEnterName.Text;

            foreach (Player newPlayer in players)
            {


                if (newPlayer.PlayerName == x)
                {
                    gameInProgress = true;
                    newPlayer.Losses++;
                    MessageBox.Show("You Lose. You lost " + newPlayer.Losses + " games");
                    RefreshRecords();
                    Restart();
                    txtBlLosses.Text = newPlayer.Losses.ToString();
                    return;
                }
               
            }


        }

        public void Win()
        {
            string x = txtBxEnterName.Text;

            foreach (Player newPlayer in players)
            {
                

                if (newPlayer.PlayerName == x)
                {
                    gameInProgress = true;
                    newPlayer.Wins++;
                    MessageBox.Show("You won. You won " + newPlayer.Wins + " games");
                    RefreshRecords();
                    Restart();
                    txtBlWin.Text = newPlayer.Wins.ToString();
                    return;
                }
               
            }

        }

        public void Draw()
        {
            string x = txtBxEnterName.Text;

            foreach (Player newPlayer in players)
            {
                
                if (newPlayer.PlayerName == x)
                {
                    gameInProgress = true;
                    newPlayer.Draws++;
                    MessageBox.Show("You Draw. You drew " + newPlayer.Draws + " games");
                    RefreshRecords();
                    Restart();
                    txtBlDraws.Text = newPlayer.Draws.ToString();
                    return;
                    
                }
                
            }

        }

        public void RefreshRecords()
        {
            

            string x = txtBxEnterName.Text;

            foreach (Player newPlayer in players)
            {

                if (newPlayer.PlayerName == x)
                {
                    FileStream fs = new FileStream(@"H:\Year Two\Semester 4\Programming\Project\PlayerRecords.txt", FileMode.Append, FileAccess.Write);

                    StreamWriter sw = new StreamWriter(fs);

                    sw.WriteLine("Player Name: {0,-15} Wins: {1,-15} Losses: {2,-15} Draws: {3}", newPlayer.PlayerName, newPlayer.Wins, newPlayer.Losses, newPlayer.Draws);

                    sw.WriteLine("");

                    sw.Close();


                }

                lstBxRecords.ItemsSource = null;
                lstBxRecords.ItemsSource = players;
                return;
            }

           
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            Restart();

        }

        public void Restart()
        {
            gameRestarted = true;
            gameStarted = false;
            playerReturned = false;

            playerNum = 0;
            dealerNum = 0;

            

            txtBlDealerTotal.Text = "0";
            txtBlPlayerTotal.Text = "0";
        }

        private void btnDoubleDown_Click(object sender, RoutedEventArgs e)
        {
            if (gameRestarted == true && gameStarted == true)
            {
                if (String.IsNullOrEmpty(txtBxEnterName.Text))
                {
                    MessageBox.Show("Enter Name First");
                }
                else
                {
                    string x = txtBxEnterName.Text;

                    foreach (Player newPlayer in players)
                    {


                        if (newPlayer.PlayerName == x)
                        {
                            int doubleDown = rnd.Next(1, 11);

                            playerNum = playerNum + doubleDown;

                            txtBlPlayerTotal.Text = playerNum.ToString();

                            if (playerNum > 21)
                            {
                                Loss();
                                return;
                            }
                            else if (playerNum == 21)
                            {
                                Win();
                                return;
                            }
                            else
                            {
                                Dealer();
                            }
                        }

                    }
                }

            }
            else
            {
                MessageBox.Show("Restart Game and press Start");
            }
        }

        public void Dealer()
        {
            int dealerCard = rnd.Next(1, 11);

            dealerNum = dealerNum + dealerCard;

            dealerNumString = dealerNum.ToString();
            txtBlDealerTotal.Text = dealerNumString;


            if (dealerNum == 21)
            {
                Loss();
                return;
            }


            else if (dealerNum <= 21)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (dealerNum > playerNum && dealerNum <= 21)
                    {
                        Loss();
                        return;
                    }

                    else if (dealerNum <= 21 && dealerNum < playerNum)
                    {
                        int newCard = rnd.Next(1, 11);
                        dealerNum = dealerNum + newCard;

                        dealerNumString = dealerNum.ToString();
                        txtBlDealerTotal.Text = dealerNumString;

                        if (dealerNum == 21 && dealerNum == playerNum)
                        {
                            Draw();
                            return;
                        }
                        else if (dealerNum == 21 && dealerNum > playerNum)
                        {
                            Loss();
                            return;
                        }
                        else if (dealerNum > 21)
                        {
                            Win();
                            return;
                        }
                    }

                    else if (dealerNum <= 21 && dealerNum < playerNum)
                    {
                        Win();
                        return;
                    }
                    else if (dealerNum == playerNum)
                    {
                        Draw();
                        return;
                    }
                    else if (dealerNum >= 21)
                    {
                        Loss();
                        return;
                    }
                }


            }
        }
    }
}
