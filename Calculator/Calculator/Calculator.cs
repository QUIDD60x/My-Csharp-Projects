using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp
{
    class Calculator // So this was a demo calculator I made early January or so, it's nothing super crazy just a basic old calculator.
    {
        static void Main(string[] args)
        {   
            // Could repeat using bool keeprunning = true; but too lazy to tab everything for that.
            Console.WriteLine("Would you like addition, subtraction, multiplication, or division?\n1 - Addition\n2 - Subtraction\n3 - Multiplication\n4 - Division\n");
            string choice = Console.ReadLine();
            if (float.TryParse(choice, out float mathType))
            {
                if (mathType == 1)
                {
                    Console.WriteLine("You selected Addition!");
                }

                if (mathType == 2)
                {
                    Console.WriteLine("You selected Subtraction!");
                }

                    if (mathType == 3)
                {
                    Console.WriteLine("You selected Multiplication!");
                }

                    if (mathType == 4)
                {
                    Console.WriteLine("You selected Division!");
                }   

                if (mathType >= 5)
                {
                    Console.WriteLine("You didn't select an option.");
                    // if I added the keeprunning function, put keepRunning = false and continue here.
                }   
            }
            else 
            {
                Console.WriteLine("You didn't input a number.");
                // I don't know how to break the operation yet but I'd add it here.
            }
            
            Console.WriteLine("Please insert your first number:");
            string firstNum = Console.ReadLine();

            Console.WriteLine("Now write your second number:");
            string secondNum = Console.ReadLine();

            if (float.TryParse(firstNum, out float result1) && float.TryParse(secondNum, out float result2 ))
            {
                if(mathType == 1)
                {
                    float finalNum = result1 + result2;
                    Console.WriteLine($" {result1} + {result2} is {finalNum}");      
                }
                if(mathType == 2)
                {
                    float finalNum = result1 - result2;
                    Console.WriteLine($" {result1} - {result2} is {finalNum}");      
                }
                if(mathType == 3)
                {
                    float finalNum = result1 * result2;
                    Console.WriteLine($" {result1} * {result2} is {finalNum}");      
                }
                if(mathType == 4)
                {
                    float finalNum = result1 / result2;
                    Console.WriteLine($" {result1} / {result2} is {finalNum}");      
                }
                if(mathType >= 5 && result1 >= 999 && result2 >= 999)
                {
                    Console.WriteLine("Congrats on this secret!");
                }
                else if (mathType >= 5 && result1 <= 999 && result2 <= 999)
                {
                    Console.WriteLine("You didn't give an operation to work with, what am I supposed to do?");
                }

                /*
                MORE EFFECTIVE WAY OF DOING THIS-
                try switch and case methods. For this it'd be something like:
                switch (mathType)
                {
                    case 1: //addition
                        Console.WriteLine("yadda yadda);
                        break;
                }

                ORRRR if you're feeling really fancy, you can do:
                string operation = mathType switch
                {
                    1 => "Addition",
                    2 => "You get the idea",
                };
                Sleeker, more compact, really fuckin cool.
                */

            }
            else
            {
                Console.WriteLine("You either didn't input a number or it's stupidly big.");  
            }
        }
    }
}