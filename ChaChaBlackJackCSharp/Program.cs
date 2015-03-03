using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaChaBlackJackCSharp
{
    class Program
    {
        //All declarations below are used in methods outside of Main()
        static int[] deck;
        static int[] discardPile;
        static bool gameOver = false;

        static int userTotal = 0;
        static int dealerTotal = 0;
        static bool firstDraw = false;
       

        static void Main(string[] args)
        {
            //Main Deck
            deck = new int[52];
            
            //second deck used to store cards from deck[] after a card is played from deck
            
            discardPile = new int[52];
           
            
            bool userStays = false;
            bool dealerStays = false;
            bool playAgain = true;


            
            string userChoice = "";
            string userInput = "";

            //moved large block of code to own method
            PopulateDeck();
           
            //draws cards for dealer and user in alternating order
            InitialDraw();

            //Main work
            while (playAgain == true)
            {

                while (gameOver == false)
                {
                    

                    if (dealerTotal < 16)
                    {

                        //deal card to self
                        dealerTotal = dealerTotal + DrawCard("Dealer");
                        Console.WriteLine("Dealer Total: " + dealerTotal);

                    }
                    else
                    {
                        Console.WriteLine("Dealer Total: " + dealerTotal);
                        Console.WriteLine("Dealer Stays.");
                        dealerStays = true;
                    }


                    CheckWin();

                    if (userStays == false && gameOver == false)
                    {
                        //Prompt player if wish to hit or stay
                        Console.WriteLine("User total: " + userTotal);
                        Console.WriteLine(" Do you wish to hit or stay?");

                        userChoice = Console.ReadLine();

                        userChoice = userChoice.ToLower();

                        userChoice = CheckInput(userChoice);
                      

                        if (userChoice.Equals("hit"))
                        {

                            userTotal = userTotal + DrawCard("User");
                            Console.WriteLine("User Total: " + userTotal);

                        }
                        else if (userChoice.Equals("stay"))
                        {

                            Console.WriteLine("User Total: " + userTotal);
                            Console.WriteLine("User Stays.");
                            userStays = true;


                        }

                  
                    }

                    //Compares totals to decide winner
                    if (dealerStays == true && userStays == true && gameOver != true)
                    {

                        if (userTotal > dealerTotal)
                        {

                            Console.WriteLine("User Total: " + userTotal);
                            Console.WriteLine("Dealer Total: " + dealerTotal);
                            Console.WriteLine("USER WINS");

                            gameOver = true;

                        }
                        else if (dealerTotal > userTotal)
                        {
                            Console.WriteLine("User Total: " + userTotal);
                            Console.WriteLine("Dealer Total: " + dealerTotal);
                            Console.WriteLine("DEALER WINS");

                            gameOver = true;


                        }
                        else if (userTotal == dealerTotal)
                        {

                            Console.WriteLine("DRAW");

                            gameOver = true;

                        }

                    }




                }//end main While
                
                //Ask player if they want to play again. if so, reset all totals and reuse same deck
                if (gameOver == true)
                {
                    Console.WriteLine("Do you wish to play again?");
                    userInput = Console.ReadLine();

                    while (userInput.Equals("yes") == false && userInput.Equals("no") == false)
                    {

                        Console.WriteLine("command not understood, please type yes or no");
                        userInput = Console.ReadLine();

                        //DEBUG
                        Console.WriteLine("userInput is " + userInput);

                    }

                    if (userInput.Equals("yes") == true)
                    {
                       //Resets game
                        gameOver = false;
                        dealerTotal = 0;
                        userTotal = 0;
                        userStays = false;
                        dealerStays = false;
                        Console.WriteLine("New Game");
                        
                        InitialDraw();

                    }
                    else
                    {

                        playAgain = false;
                        Console.WriteLine("Game Over");

                    }
                }
           
            
            }//End play again while








        }//End of Main

                        
        
                        //METHODS

       
        
        //This method pulls a random card from deck[] and copies that value to corresponding elemnt in discardPile[] then deletes that value from deck[]
        static int DrawCard(string whoDraws)
        {
            int card = 0;
            int num = 0;
            int rand = 0;
            int numOfDraws = 0;

            int aceInput = 0;
            
            Random randomNumber = new Random();
            rand = randomNumber.Next(0, 51);

            num = deck[rand];

           String str = "";

            while (num == 0)
            {

                //retrieve card from deck
                //store that card in num
                rand = randomNumber.Next(0, 52);
                numOfDraws++;

                //Ensures that cards still hold value after adequate number of plays
                if(numOfDraws > 50)
                {

                    Array.Copy(discardPile, deck, 51);
                    
                    Console.WriteLine("Shuffling the deck");

                }

                num = deck[rand];


            } //eventually after playing 24 or so games this will never end

            //copy value to corresponding element in discard pile 

            discardPile[rand] = deck[rand];

            deck[rand] = 0;

            Console.WriteLine(whoDraws + " draws a " + num);

            if(num == 1)
            {   //Give user choice of using Ace as 11 or 1
                if (whoDraws.Equals("User") && firstDraw != true)
                {
                    Console.WriteLine(whoDraws + " has drawn an ace");
                    Console.WriteLine("Do you wish to change your aces value to 11 or keep it at 1?");
                   str = Console.ReadLine();
                    while(str.Equals("1") == false && str.Equals("11") == false)
                    {

                        Console.WriteLine("Command not understood, please input 1 or 11");
                        str = Console.ReadLine();

                    }

                    aceInput = Convert.ToInt32(str);

                    if (aceInput == 11)
                    {
                        num = 11;
                    }
                    else
                    {

                        //do nothing as num already equals 1

                    }
                }
                else if(firstDraw == true)
                {

                    Console.WriteLine(whoDraws + " has drawn an ace");
                    num = 11;

                }
                else if(whoDraws.Equals("Dealer"))
                {

                    Console.WriteLine(whoDraws + " has drawn an ace");
                    if(dealerTotal < 11)
                    {
                       
                        num = 11;

                    }

                }
            }

            Console.WriteLine("");

            card = num;

            return card;

        }// end draw card
        
        //Fills the Deck[] array
        static void PopulateDeck()
        {

            int temp = 0;

            //populates deck with appropriate values
            for (int i = 0; i < 13; i++)
            {

                deck[i] = i;

            }

            deck[9] = 10;
            deck[10] = 10;
            deck[11] = 10;
            deck[12] = 10;
            
            //temp is changed here to populate cards 13 - 22
            temp = 1;

            for (int i = 13; i < 23; i++)
            {
            
            deck[i] = temp;

            temp++;

            }

            deck[23] = 10;
            deck[24] = 10;
            deck[25] = 10;
            deck[26] = 10;

             //temp is changed here to populate cards 27 - 36
            temp = 1;

            for (int i = 27; i < 37; i++)
			{
			 
            deck[i] = temp;

            temp++;

			}

            deck[37] = 10;
            deck[38] = 10;
            deck[39] = 10;
            deck[40] = 10;

             //temp is changed here to populate cards 41 - 47
            temp = 1;

              for (int i = 41; i < 48; i++)
			{
			 
            deck[i] = temp;

            temp++;

			}

            deck[48] = 10;
            deck[49] = 10;
            deck[50] = 10;   
            deck[51] = 10;
        

            

        }

        //Ensures that correct input is taken from user
        static String CheckInput(String input)
        {

            while (input.Equals("hit") == false && input.Equals("stay") == false && input.Equals("h") == false && input.Equals("s") == false)
            {

                Console.Write("Command not understood, please type hit or stay : ");
                input = Console.ReadLine();
                input = input.ToLower();

                //DEBUG
                Console.WriteLine("User wrote " + "'" + input + "'");
                Console.WriteLine(input.Equals("hit"));

            }
          


            return input;
        }


        static void CheckWin()
        {

            //Checks totals to prevent redundant plays
            if (dealerTotal == 21)
            {

                gameOver = true;
                Console.WriteLine("Dealer has " + dealerTotal);
                Console.WriteLine("Dealer has blackjack.");
                Console.WriteLine("DEALER WINS");

            }
            else if (dealerTotal > 21)
            {

                gameOver = true;
                Console.WriteLine("Dealer Total: " + dealerTotal);
                Console.WriteLine("Dealer Busts.");
                Console.WriteLine("USER WINS");

            }
            else if (userTotal > 21)
            {

                gameOver = true;
                Console.WriteLine("User Total: " + userTotal);
                Console.WriteLine("User Busts.");
                Console.WriteLine("DEALER WINS");

            }
            else if (userTotal == 21)
            {

                gameOver = true;
                Console.WriteLine("User Total: " + userTotal);
                Console.WriteLine("User has blackjack.");
                Console.WriteLine("USER WINS");

            }

        }

        static void InitialDraw()
        {

            firstDraw = true;
            //Deal starting cards, 2 each for dealer and user in alternating order

            dealerTotal += DrawCard("Dealer");

            userTotal += DrawCard("User");

            dealerTotal += DrawCard("Dealer");

            userTotal += DrawCard("User");

            Console.WriteLine("Dealer Total: " + dealerTotal);
            Console.WriteLine("User Total: " + userTotal);
            firstDraw = false;


        }
    }//End of Class
}//End of NameSpace
