using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;


namespace IndividualProject
{
    public static class Intro
    {

        public static void TypingEffect()
        {
            string welcome = "Welcome to Physics e-Lab !!!!!!!!!!!!!!!!!!!!";
            Console.WriteLine("");
            for (int i = 0; i < welcome.Length; i++)

            {
                Console.Write(welcome[i]);
                System.Threading.Thread.Sleep(350);
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }

        public static void MultiLineAnimation()
        {
            var counter = 0;
            for (int i = 0; i < 30; i++)
            {
                Console.Clear();

                switch (counter % 4)
                {
                    case 0:
                        {
                            Console.WriteLine("╔════╤╤╤╤════╗");
                            Console.WriteLine("║    │││ \\   ║");
                            Console.WriteLine("║    │││  O  ║");
                            Console.WriteLine("║    OOO     ║");
                            break;
                        };
                    case 1:
                        {
                            Console.WriteLine("╔════╤╤╤╤════╗");
                            Console.WriteLine("║    ││││    ║");
                            Console.WriteLine("║    ││││    ║");
                            Console.WriteLine("║    OOOO    ║");
                            break;
                        };
                    case 2:
                        {
                            Console.WriteLine("╔════╤╤╤╤════╗");
                            Console.WriteLine("║   / │││    ║");
                            Console.WriteLine("║  O  │││    ║");
                            Console.WriteLine("║     OOO    ║");
                            break;
                        };
                    case 3:
                        {
                            Console.WriteLine("╔════╤╤╤╤════╗");
                            Console.WriteLine("║    ││││    ║");
                            Console.WriteLine("║    ││││    ║");
                            Console.WriteLine("║    OOOO    ║");
                            break;
                        };
                }

                counter++;
                System.Threading.Thread.Sleep(200);
            }
        }

        public static void PlaySound()
        {
            SoundPlayer mySound = new SoundPlayer();
            mySound.SoundLocation = Environment.CurrentDirectory + "\\arpa.wav";
            mySound.Play();
        }
    }
}
