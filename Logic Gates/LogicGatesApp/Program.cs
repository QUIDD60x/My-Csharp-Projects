using System;
using System.Collections.Generic;

namespace LogicGates
{
    public class Functions
    {
        public bool keepRunning = true;
        public bool gate1 = false;
        public bool gate2 = false;
        public string result = "off";

        public string ResetGates()
        {
            if(gate1 == true || gate2 == true)
            {
                gate1 = false;
                gate2 = false;
                return "Gate 1 and 2 has been reset.";
            }
            else
            {
                return "Gate 1 and 2 are currently off.";
            }
        }
        public void AndGate()
        {
            string resetMessage = ResetGates();
            Console.WriteLine(resetMessage);
            while(keepRunning)
            {
                if(gate1 == true && gate2 == true)
                {
                    result = "on";
                }
                else
                {
                    result = "off";
                }
                Console.WriteLine(new string('-', 60));
                Console.WriteLine(new string(' ', 60));
                Console.WriteLine($"\t\tResult is currently {result}");
                Console.WriteLine(new string(' ', 60));
                Console.WriteLine($"X switch is {gate1}\tY switch is {gate2}");
                Console.WriteLine(new string('-', 60));
                Console.WriteLine("\nType 1 to change X to true/false, 2 to change Y, and 3 to exit.");
                string userInput = Console.ReadLine();
                switch(userInput)
                {
                    case "1":
                        gate1 = !gate1;
                        continue;
                    case "2":
                    gate2 = !gate2;
                        continue;
                    case "3":
                    keepRunning = false;
                        continue;
                    case "_":
                        Console.WriteLine("Inproper input, please try again.");
                        continue;
                }
            }
        }
        public void OrGate()
        {

        }
        public void InvertGate()
        {

        }
        public void BufferGate()
        {

        }
        public void NANDGate()
        {

        }
        public void NORGate()
        {

        }
        public void XORGate()
        {

        }
        public void EXNORGate()
        {

        }
    }
    public class FrontEnd
    {
        static void Main(string[] args)
        {
            Functions functions = new();
            Console.WriteLine("Quidd's Logic Gate Examples\nThis is simply to show my knowledge of how logic gates work.");
            Console.WriteLine("Please select the number associated with the logic gate you'd like to test.");
            string userInput = Console.ReadLine();
            switch(userInput)
            {
                case "1":
                functions.AndGate();
                    return;
                case "2":
                    functions.OrGate();
                    return;
                case "3":
                    functions.InvertGate();
                    return;
                case "4":
                    functions.BufferGate();
                    return;
                case "5":
                    functions.NANDGate();
                    return;
                case "6":
                    functions.NORGate();
                    return;
                case "7":
                    functions.XORGate();
                    return;
                case "8":
                    functions.EXNORGate();
                    return;
            }
        }
    }
}