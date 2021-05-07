using System;

namespace FeverControl
{
    class Acetaminophen
    {
        private int mAge;
        private double mWeight;

        public int Age
        {
            get
            {
                return mAge;
            }
            set
            {
                if (value >= 2 && value <= 11)
                {
                    mAge = value;
                }
                else
                {
                    throw new Exception("Invalid age for children acetaminophen. Age must be between 2 and 11.");
                }
            }
        }

        public double Weight
        {
            get
            {
                return mWeight;
            }
            set
            {
                if (value >= 24 && value <= 95)
                {
                    mWeight = value;
                }
                else
                {
                    throw new Exception("Invalid weight for children acetaminophen. Weight must be between 24 and 95 lbs.");
                }
            }
        }

        public Acetaminophen()
        {
            Age = 2;
            Weight = 24;
        }
        public Acetaminophen(int age, double weight)
        {
            Age = age;
            Weight = weight;
        }

        public double LiquidDosageByWeight()
        {
            double liquidDosage = 0;

            if (Weight <= 35)
                liquidDosage = 5;
            else if (Weight <= 47)
                liquidDosage = 7.5;
            else if (Weight <= 59)
                liquidDosage = 10;
            else if (Weight <= 71)
                liquidDosage = 12.5;
            else
                liquidDosage = 15;

            return liquidDosage;
        }

        public double LiquidDosageByAge()
        {
            double liquidDosage = 0;

            if (Age <= 3)
                liquidDosage = 5;
            else if (Age <= 5)
                liquidDosage = 7.5;
            else if (Age <= 8)
                liquidDosage = 10;
            else if (Age <= 10)
                liquidDosage = 12.5;
            else
                liquidDosage = 15;

            return liquidDosage;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            bool inputCheck = false; 
            char infoCheck = '\0';
            Acetaminophen oralDose = new Acetaminophen();

            while (infoCheck.ToString().ToUpper() != "Y")
            {
                infoCheck = '\0';

                Console.WriteLine("*****************");
                Console.WriteLine("* Fever Control *");
                Console.WriteLine("*****************");

                inputCheck = false;
                while (!inputCheck)
                {
                    Console.Write("Enter the children’s weight in pounds: ");
                    input = Console.ReadLine();

                    try
                    {
                        oralDose.Weight = double.Parse(input);
                        inputCheck = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                inputCheck = false;
                while (!inputCheck)
                {
                    Console.Write("Enter the children’s age in years: ");
                    input = Console.ReadLine();

                    try
                    {
                        oralDose.Age = int.Parse(input);
                        inputCheck = true;
                    }
                    catch (Exception e)
                    { 
                        Console.WriteLine(e.Message);
                    }
                }
                
                Console.WriteLine($"Weight: {oralDose.Weight} lbs, Age: {oralDose.Age} years old");
                Console.Write("Is the information above about the children correct (Y/N): ");
                infoCheck = Console.ReadKey().KeyChar;
            }
            
            Console.WriteLine($"\n\nDosage by weight({oralDose.Weight}lbs) is: {oralDose.LiquidDosageByWeight()} ml");
            Console.WriteLine($"Dosage by age ({oralDose.Age} years) is: {oralDose.LiquidDosageByAge()}");
        }
    }
}
