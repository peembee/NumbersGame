using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NumbersGame
{
    /* Mohammed Boukhedimi
       Klass: NET22 */
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] priceMoney = new int[] { 1000, 600, 400, 200, 60 };

            int entry = 50; // price to entry the lottery
            int userGuess; // user guesses saves in this variable
            int guesses = 1; // number of times user can guess the right number
            int winningNumber; ; // variable that contains the winning number
            int wallet = 200; // user has a startup wallet with 200 sec.
            Random spinNumber = new Random(); // variable for a random number

            // Game Introduktion
            Console.WriteLine("Välkommen till");
            Console.WriteLine("  LOTTERIET");
            Console.WriteLine("Inträde: " + entry + " kr");
            Console.WriteLine("Vinn upp till 1000 kr");
            Console.WriteLine("---------------");
            Console.WriteLine("Regler:");
            Console.WriteLine("Gissa på ett tal mellan 1 - 20");
            Console.WriteLine("Du får gissa upp till 5 gånger");
            Console.WriteLine("För varje använd gissning minskar ditt pris \nVinststegar:");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("-----------------------------");
            Console.WriteLine(" 1 gissning   = Vinst " + priceMoney[0] + " kr");
            Console.WriteLine(" 2 gissningar = Vinst  " + priceMoney[1] + " kr");
            Console.WriteLine(" 3 gissningar = Vinst  " + priceMoney[2] + " kr");
            Console.WriteLine(" 4 gissningar = Vinst  " + priceMoney[3] + " kr");
            Console.WriteLine(" 5 gissningar = Vinst   " + priceMoney[4] + " kr");
            Console.WriteLine("-----------------------------");
            Console.ResetColor();
            displayWallet(wallet);
            Console.WriteLine("Tryck enter för att spela");
            Console.ReadKey();
            Console.Clear();

            winningNumber = spinNumber.Next(1, 21);  // The winning number has been implemented in the variable.
            wallet = wallet - entry; // 50 sec entry
            while (true)
            {
                if (guesses > 5)  // if guesses is out, user can buy another entry.
                {
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("Det finns inte några fler gissningar");
                    Console.Write("Vill du köpa en ny biljett till lotteriet? (Y / N / 1): ");
                    string restartOrQuitGame = Console.ReadLine();
                    restartOrQuitGame = restartOrQuitGame.ToLower();
                    if (restartOrQuitGame == "y" || restartOrQuitGame == "yes" || restartOrQuitGame == "1") // asking user to buy another entry
                    {
                        if (wallet >= 50)
                        {                            
                            Console.Clear();
                            wallet = wallet - entry; // 50 sec entry
                            guesses = 1;
                            winningNumber = spinNumber.Next(1, 21); // generate a new winner number
                        }
                        else  // user don't have enough money - game is closing
                        {
                            Console.WriteLine("Tyvärr har du inte pengar till en ny biljett");
                            break;
                        }
                    }
                    else  // user don't want to play anymore, game is closing
                    {
                        break;
                    }
                }             

                displayWallet(wallet); // displays user-wallet
                while (true)
                {
                    Console.WriteLine("__________________________");
                    Console.WriteLine("Gissning: " + guesses);
                    Console.Write("Skriv in ditt tal: ");
                    try
                    {
                        userGuess = Convert.ToInt32(Console.ReadLine());
                        if (userGuess > 0 && userGuess <= 20) // checking if user entered the accepted number
                        {
                            break;
                        }
                        else
                        {
                            invalidInput();
                        }
                    }
                    catch (Exception)
                    {
                        invalidInput();
                    }
                }
                Console.Clear();
                
                loadingscene();
                Console.WriteLine("Din gissning: " + userGuess);
                if (userGuess == winningNumber)  // user gets the winner number
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Grattis du gissade rätt!");
                    Console.WriteLine("Det tog " + guesses + " gissningar.");
                    guesses = guesses - 1; // decreases the guesses to match arrays-index.
                    Console.WriteLine("Du vann " + priceMoney[guesses] + " kr");
                    wallet += priceMoney[guesses];
                    displayWallet(wallet);
                    guesses = 6;
                    Console.ResetColor();
                }
                else if (userGuess < winningNumber) // users number is lower than winner number
                {
                    lowerNumberThanWinningNumber(userGuess, winningNumber);
                    guesses++;
                }
                else // uses number is higher than winner number
                {
                    higherNumberThanWinningNumber(userGuess, winningNumber);
                    guesses++;
                }                
            }
            Console.WriteLine("Spelet avslutat. Tangent för att stänga..");
            Console.ReadKey();
        }

        /// <summary>
        /// If users number has a higher guess than winning number, print this
        /// </summary>
        /// <param name="userguess"></param>
        static void higherNumberThanWinningNumber(int userGuess, int winningNumber)
        {
            if (userGuess <= winningNumber + 2)
            {
                Console.WriteLine("Nu är du riktigt nära! Lite lägre bara");
            }
            else if (userGuess <= winningNumber + 4)
            {
                Console.WriteLine("Du är ganska nära, Men mitt vinnar tal är ändå lägre");
            }
            else
            {
                Console.WriteLine("Du gissade för högt");
            }
        }

        /// <summary>
        /// If users number has a lower guess than winning number, print this
        /// </summary>
        /// <param name="userguess"></param>
        static void lowerNumberThanWinningNumber(int userGuess, int winningNumber)
        {
            if (userGuess >= winningNumber - 2)
            {
                Console.WriteLine("Nu är du riktigt nära! Lite högre bara");
            }
            else if (userGuess >= winningNumber - 4)
            {
                Console.WriteLine("Du är ganska nära, Men mitt vinnar tal är ändå högre");
            }
            else
            {
                Console.WriteLine("Du gissade för lågt");
            }            
        }

        /// <summary>
        /// Display wallet in console
        /// </summary>
        /// <param name="wallet"></param>
        static void displayWallet(int wallet)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---------------");
            Console.WriteLine("Plånbok: " + wallet + " kr");
            Console.WriteLine("---------------");
            Console.ResetColor();           
        }

        /// <summary>
        /// display this if user entering an invalid input
        /// </summary>
        static void invalidInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Endast tal mellan 1 - 20 är giltigt");
            Console.WriteLine("Försök igen..");
            Console.WriteLine("--------------------------------------");
            Console.ResetColor();
        }

        /// <summary>
        /// loadingscene
        /// </summary>
        static void loadingscene()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Checking your guessed number..");
            Console.WriteLine("--");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("   --");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("      --");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("         --");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("      --");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("   --");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("--");
            System.Threading.Thread.Sleep(100);
            Console.ResetColor();
        }
    }
}
