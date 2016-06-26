using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGSI;
using CSGSI.Nodes;

namespace Bomb_Timer
{
    class Program
    {
        static GameStateListener gsl;

        static bool BombPlanted = false;
        static int BombTimer = 40; 

        //Bomb states
        static string Exploded = "Bomb Status: Exploded";
        static string Defused = "Bomb Status: Defused";
        static string Idle = "Bomb Status: Idle";



        static void Main(string[] args)
        {
            Console.Title = "Bomb Timer";
            Console.SetWindowSize(30, 10);
            gsl = new GameStateListener(3000);
            gsl.NewGameState += new NewGameStateHandler(OnNewGameState);
            if (!gsl.Start())
            {
                Environment.Exit(0);
            }
            //Console.ForegroundColor = ConsoleColor.Green;
            Title();
            Console.WriteLine("Bomb Status: Idle");
        }


        static void OnNewGameState(GameState gs)
        {


            if (!BombPlanted && gs.Round.Bomb == BombState.Planted && gs.Round.Phase == RoundPhase.Live && gs.Previously.Round.Bomb == BombState.Undefined) //(Isplanted = false and bomb is planted and round is live. (In progress) and bomb state from the last round is cleared.)
            {
                BombPlanted = true;
                Count();
            }
            else if (BombPlanted && gs.Round.Phase == RoundPhase.Over)
            {
                BombPlanted = false;
            }


            switch (gs.Round.Bomb)
            {

                case BombState.Exploded:

                    Console.Clear();
                    Title();
                    Console.WriteLine(Exploded);
                    System.Threading.Thread.Sleep(5000);
                    BombTimer = 40;
                    Console.Clear();
                    break;

                case BombState.Defused:

                    Console.Clear();
                    Title();
                    Console.WriteLine(Defused);
                    System.Threading.Thread.Sleep(5000);
                    BombTimer = 40;
                    Console.Clear();
                    break;
            }

            switch (gs.Round.Phase)
            {

                case RoundPhase.FreezeTime:

                    Title();
                    Console.WriteLine(Idle);
                    break;
            }
        }

        static void Count()
        {
            while (BombPlanted == true)
            {
                Console.Clear();
                BombTimer--;
                Title();
                Console.WriteLine("Bomb Status: Planted");
                Console.WriteLine(BombTimer);
                System.Threading.Thread.Sleep(1000);
            }
        }


        static void Title()
        {
            Console.Clear();
            Console.WriteLine("+-------------------+\r\n| NextGenUpdate     |\r\n| CS:GO Bomb Timer  |\r\n| By Temp           |\r\n+-------------------+");
        }
    }
}