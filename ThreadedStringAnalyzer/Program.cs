using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// Name:   Cory Siebler
/// ASUID:  1000832292
/// Email:  csiebler@asu.edu
/// Class:  ASU CSE 445 (#11845)
namespace ThreadedStringAnalyzer
{
    /// <summary>
    /// 
    /// </summary>
    public class DigitCount
    {
        private string input;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public DigitCount(string input)
        {
            this.input = input;
        }

        /// <summary>
        /// 
        /// </summary>
        public void run()
        {
            Console.WriteLine("RUNNING: THREAD DIGIT COUNT");

            Console.WriteLine("RESULT: DIGIT COUNT - " + input.Length);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UppercaseCount
    {
        private string input;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public UppercaseCount(string input)
        {
            this.input = input;
        }

        /// <summary>
        /// 
        /// </summary>
        public void run()
        {
            Console.WriteLine("RUNNING: THREAD UPPERCASE COUNT");

            int count = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                if (char.IsUpper(input[i]))
                {
                    count++;
                }
            }

            Console.WriteLine("RESULT: UPPERCASE COUNT - " + count);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Palindrome
    {
        private string input;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public Palindrome(string input)
        {
            this.input = input;
        }

        /// <summary>
        /// 
        /// </summary>
        public void run()
        {
            Console.WriteLine("RUNNING: THREAD PALINDROME");

            if (input.SequenceEqual(input.Reverse()))
            {
                Console.WriteLine("RESULT: PALINDROME - TRUE");
            }
            else
            {
                Console.WriteLine("RESULT: PALINDROME - FALSE");
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo keyPressed;

            do
            {
                string input;

                Console.WriteLine("Enter the string to analyze: ");
                input = Console.ReadLine();
                Console.WriteLine();

                DigitCount digitCount = new DigitCount(input);
                UppercaseCount uppercaseCount = new UppercaseCount(input);
                Palindrome palindrome = new Palindrome(input);

                Thread digitCountThread = new Thread(new ThreadStart(digitCount.run));
                Thread uppercaseCountThread = new Thread(new ThreadStart(uppercaseCount.run));
                Thread palindromeThread = new Thread(new ThreadStart(palindrome.run));

                digitCountThread.Start();
                uppercaseCountThread.Start();
                palindromeThread.Start();

                while (!digitCountThread.IsAlive && !uppercaseCountThread.IsAlive && !palindromeThread.IsAlive)
                {
                    Thread.Sleep(1);
                }

                while (digitCountThread.IsAlive || uppercaseCountThread.IsAlive || palindromeThread.IsAlive)
                {
                    Thread.Sleep(1);
                }

                Console.WriteLine();
                Console.WriteLine("Would you like to test another string (Y/N): ");
                keyPressed = Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine();
            } while (keyPressed.KeyChar == 'Y' || keyPressed.KeyChar == 'y');
        }
    }
}
