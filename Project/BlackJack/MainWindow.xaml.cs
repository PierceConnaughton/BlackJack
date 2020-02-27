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
        bool playerFound = false;
        bool playerInFile = false;
        bool ifHit = false;





        public MainWindow()
        {
            InitializeComponent();
        }

        //end game button clicked button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameStarted == false)
            {
                //message too show records being saved
                MessageBox.Show("Saving too records file");

                //foreach player in the list save their record too a list
                foreach (Player newPlayer in players)
                {
                    //playerInFile = false;



                    //foreach (Player player in players)
                    //{
                    //    if (newPlayer.PlayerName == player.PlayerName)
                    //    {
                    //        FileStream fs = new FileStream(@"H:\Year Two\Semester 4\Programming\Project\PlayerRecords.txt", FileMode.Create, FileAccess.Write);


                    //        StreamWriter sw = new StreamWriter(fs);

                    //        sw.WriteLine("Player Name: {0,-15} Wins: {1,-15} Losses: {2,-15} Draws: {3}", newPlayer.PlayerName, newPlayer.Wins, newPlayer.Losses, newPlayer.Draws);
                    //        playerInFile = true;
                    //        sw.Close();

                    //        return;

                    //    }

                    //}

                    //if (playerInFile == false)
                    //{
                    FileStream fs = new FileStream(@"H:\Year Two\Semester 4\Programming\Project\PlayerRecords.txt", FileMode.Append, FileAccess.Write);


                    StreamWriter sw = new StreamWriter(fs);

                    sw.WriteLine("Player Name: {0,-15} Wins: {1,-15} Losses: {2,-15} Draws: {3,-15} Date Last time player played: {4}", newPlayer.PlayerName, newPlayer.Wins, newPlayer.Losses, newPlayer.Draws,newPlayer.DateOfLastGame);

                    sw.WriteLine("");

                    sw.Close();
                    //}



                }
                ReadFile();
                MainWindow1.Close();
            }
            else
            {
                MessageBox.Show("Finish game before finishing");
            }

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

                    playerFound = false;
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
            //when the main window is loaded set all the values too default
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
                //name has to be entred to use this button
                if (String.IsNullOrEmpty(txtBxEnterName.Text))
                {
                    MessageBox.Show("Enter Name First");
                }
                else
                {
                    //set player found too false

                    playerFound = false;
                    //get the name in the textbox
                    string x = txtBxEnterName.Text;

                    //check in the list if that name matches with the player that is playing
                    foreach (Player newPlayer in players)
                    {


                        if (newPlayer.PlayerName == x)
                        {
                            //if player found set too true
                            playerFound = true;

                            ifHit = true;
                            //get random card between 1 and 10 and add it too your total
                            int hit = rnd.Next(1, 11);

                            playerNum = playerNum + hit;

                            txtBlPlayerTotal.Text = playerNum.ToString();

                            //if player gets more then 21 they lose or if player gets exactly 21 they win
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
                    //if player cant be found as the same player in the txtbx get a warning
                    if (playerFound == false)
                    {
                        MessageBox.Show("Player changed, please press start");

                    }

                }
            }
            else
            {
                //warning too restart the game

                MessageBox.Show("Press Restart and then press start game");

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
                    playerFound = false;

                    string x = txtBxEnterName.Text;

                    foreach (Player newPlayer in players)
                    {


                        if (newPlayer.PlayerName == x)
                        {
                            playerFound = true;

                            //go too dealer method
                            Dealer();

                        }
                    }
                    if (playerFound == false)
                    {
                        MessageBox.Show("Player changed, please press start");
                    }
                }
            }

            else
            {
                MessageBox.Show("Restart Game and press Start");
            }
        }

        //Method if you lost
        public void Loss()
        {
            string x = txtBxEnterName.Text;

            foreach (Player newPlayer in players)
            {


                if (newPlayer.PlayerName == x)
                {
                    newPlayer.DateOfLastGame = DateTime.Now.ToString("MM/dd/yyyy H:mm");
                    gameInProgress = true;
                    newPlayer.Losses++;
                    MessageBox.Show("You Lose. You lost " + newPlayer.Losses + " games");
                    //players.Sort();
                    RefreshRecords();
                    Restart();
                    txtBlLosses.Text = newPlayer.Losses.ToString();
                    return;
                }

            }


        }

        //Method if you won
        public void Win()
        {
            string x = txtBxEnterName.Text;

            foreach (Player newPlayer in players)
            {


                if (newPlayer.PlayerName == x)
                {
                    newPlayer.DateOfLastGame = DateTime.Now.ToString("MM/dd/yyyy H:mm");
                    gameInProgress = true;
                    newPlayer.Wins++;
                    MessageBox.Show("You won. You won " + newPlayer.Wins + " games");
                    //players.Sort();
                    RefreshRecords();
                    Restart();
                    txtBlWin.Text = newPlayer.Wins.ToString();
                    return;
                }

            }

        }

        //method if you drew
        public void Draw()
        {
            string x = txtBxEnterName.Text;

            foreach (Player newPlayer in players)
            {

                if (newPlayer.PlayerName == x)
                {
                    newPlayer.DateOfLastGame = DateTime.Now.ToString("MM/dd/yyyy H:mm");
                    gameInProgress = true;
                    newPlayer.Draws++;
                    MessageBox.Show("You Draw. You drew " + newPlayer.Draws + " games");
                    //players.Sort();
                    RefreshRecords();
                    Restart();
                    txtBlDraws.Text = newPlayer.Draws.ToString();   
                    return;

                }

            }

        }

        //refreshes the records after a game
        public void RefreshRecords()
        {

            lstBxRecords.ItemsSource = null;

           
            lstBxRecords.ItemsSource = players;

        }

        //restarts game when clicked
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            //Activates restart method
            Restart();

        }

        //restarts game
        public void Restart()
        {
            //set all the values too default
            gameRestarted = true;
            gameStarted = false;
            playerReturned = false;
            ifHit = false;

            playerNum = 0;
            dealerNum = 0;



            txtBlDealerTotal.Text = "0";
            txtBlPlayerTotal.Text = "0";
        }

        //When double down is clicked
        private void btnDoubleDown_Click(object sender, RoutedEventArgs e)
        {
            if (gameRestarted == true && gameStarted == true)
            {
                if (String.IsNullOrEmpty(txtBxEnterName.Text))
                {
                    MessageBox.Show("Enter Name First");
                }
                else if (ifHit == true)
                {
                    MessageBox.Show("Cannot double down after hit was pressed");
                }
                else
                {
                    playerFound = false;
                    string x = txtBxEnterName.Text;

                    foreach (Player newPlayer in players)
                    {


                        if (newPlayer.PlayerName == x)
                        {
                            playerFound = true;
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
                    if (playerFound == false)
                    {
                        MessageBox.Show("Player changed, please press start");
                    }
                }

            }
            else
            {

                MessageBox.Show("Restart Game and press Start");
            }
        }

        //Method for when it's the dealers turn
        public void Dealer()
        {
            //get dealers second card
            int dealerCard = rnd.Next(1, 11);

            //get dealer total and display it
            dealerNum = dealerNum + dealerCard;

            dealerNumString = dealerNum.ToString();
            txtBlDealerTotal.Text = dealerNumString;

            //if dealer has exactly 21 you lose
            if (dealerNum == 21)
            {
                Loss();
                return;
            }

            //if dealer's number is below 21
            else if (dealerNum < 21)
            {
                //go through loop 10 times
                for (int i = 0; i < 10; i++)
                {
                    //if dealer number more then player number and dealer number less then or equal to 21 you lose
                    if (dealerNum > playerNum && dealerNum <= 21)
                    {
                        Loss();
                        return;
                    }

                    //if dealer number below 21 and also below the player's number
                    else if (dealerNum <= 21 && dealerNum < playerNum)
                    {
                        //give dealer a new card and add it too total
                        int newCard = rnd.Next(1, 11);

                        dealerNum = dealerNum + newCard;

                        dealerNumString = dealerNum.ToString();
                        txtBlDealerTotal.Text = dealerNumString;

                        //if dealer number = too 21 and equal too player number it's a draw
                        if (dealerNum == 21 && dealerNum == playerNum)
                        {
                            Draw();
                            return;
                        }
                        //if dealer number = to 21 and more then player number you lose
                        else if (dealerNum == 21 && dealerNum > playerNum)
                        {
                            Loss();
                            return;
                        }
                        //if dealer number more then 21 you win
                        else if (dealerNum > 21)
                        {
                            Win();
                            return;
                        }
                    }

                    //if dealer number less than = 21 and less then player num 
                    else if (dealerNum <= 21 && dealerNum < playerNum)
                    {
                        Win();
                        return;
                    }
                    //if dealer number == player number its a draw
                    else if (dealerNum == playerNum)
                    {
                        Draw();
                        return;
                    }
                    //else if dealer num is more than player num
                    else if (dealerNum > playerNum)
                    {
                        Loss();
                        return;
                    }
                    //if dealer number is more than 21
                    else if (dealerNum > 21)
                    {
                        Win();
                        return;
                    }
                }


            }
        }

        public void ReadFile()
        {
            string text = File.ReadAllText(@"H:\Year Two\Semester 4\Programming\Project\PlayerRecords.txt");
            text = text.Replace(string.Format("Player Name: {0,-15} Wins: {1,-15} Losses: {2,-15} Draws: {3}","Pierce",1,0,0), "new value");
            File.WriteAllText("test.txt", text);
        }

        


    }
}
