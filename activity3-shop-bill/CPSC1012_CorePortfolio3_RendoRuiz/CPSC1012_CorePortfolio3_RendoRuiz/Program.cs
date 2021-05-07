using System;

namespace CPSC1012_CorePortfolio3_RendoRuiz
{
    class Program
    {
        /*
        *  Purpose:    Provides a simple billing system where the user can manually enter item descriptions and their prices,
        *              add an optional tip, and calculate all amounts accordingly.
        *  Input:      Item description and price.
        *              Optional tip amount or percentage
        *  Process:    All inputs will be validated accordingly. 
        *              Net amount is the sum of all prices.
        *              Tip percentage automatically gets updated.
        *              GST amount is set at 5% of net amount.
        *              Total amount is the summation of all computed prices.
        *  Output:     A representation of a (restaurant) bill including all the items, net amount,
        *              tip amount, gst amount, and total amount.
        *  
        *  Author:     Rendo Ruiz
        *  Date:       2018-11-18
        */
        static void Main(string[] args)
        {
            int maxArraySize = 5;
            double[] itemPrice = new double[maxArraySize];
            string[] itemDescription = new string[maxArraySize];
            int currentArrayLength = 0;
            int menuChoice = 0;

            double tipValue = 0;
            while (menuChoice != 6)
            {
                menuChoice = ValidatedMenuInput();
                switch (menuChoice)
                { 
                    case 1:
                        AddItem(itemPrice, itemDescription, ref currentArrayLength);
                        break;
                    case 2:
                        RemoveItem(itemPrice, itemDescription, ref currentArrayLength);
                        break;
                    case 3:
                        tipValue = AddTip(CalculateNetAmount(itemPrice, currentArrayLength), currentArrayLength);
                        break;
                    case 4:
                        DisplayBill(itemPrice, itemDescription, currentArrayLength, tipValue);
                        break;
                    case 5:
                        ClearAll(ref itemPrice, ref itemDescription, maxArraySize, ref currentArrayLength, ref tipValue);
                        break;
                    case 6:
                        break;
                }
                if (menuChoice != 6)
                {
                    PromptPressKeyToContinue();
                    Console.Clear();
                }
            }
        }

        /*
         *  Menu Methods
         */
        static void AddItem(double[] priceArray, string[] descriptionArray, ref int currentSize)
        {
            if (currentSize < priceArray.Length)
            {
                Console.Clear();
                double price = 0;
                string description = "";
                Console.WriteLine("Add New Item");
                Console.WriteLine("===================================================");
                while (description.Trim() == "")
                {
                    Console.Write("Enter description:\n\t");
                    description = Console.ReadLine().Trim();
                    if (description == "")
                    {
                        PromptErrorMessage("Invalid input. A description is required");
                    }
                }
                price = GetDouble("Enter price:\n\t");

                priceArray[currentSize] = price;
                descriptionArray[currentSize] = description;
                currentSize += 1;
            }
            else
            {
                PromptErrorMessage("Item maximum amount reached. Throw up some cash to add more items.");
            }
        }

        static void RemoveItem(double[] priceArray, string[] descriptionArray, ref int currentSize)
        {
            if (currentSize > 0)
            {
                int selectedItem = -1;
                while (selectedItem < 0 || selectedItem > currentSize)
                {
                    Console.Clear();
                    Console.WriteLine($"Remove Item");
                    Console.WriteLine("===================================================");
                    Console.WriteLine("Item No.  Description                         Price");
                    Console.WriteLine("--------  -----------------------------  ----------");
                    for (int x = 0; x < currentSize; x++)
                    {
                        Console.WriteLine($"{x + 1,8}  {descriptionArray[x],-29}  {priceArray[x],10:C}");
                    }

                    Console.WriteLine("\nEnter the item number to remove or 0 to cancel:");
                    try
                    {
                        selectedItem = int.Parse(Console.ReadLine());
                        if (selectedItem > currentSize || selectedItem < 0)
                        {
                            PromptErrorMessage();
                            PromptPressKeyToContinue();
                        }
                    }
                    catch
                    {
                        PromptErrorMessage();
                        PromptPressKeyToContinue();
                    }
                }
                if (selectedItem == 0)
                {
                    Console.WriteLine("No item has been removed.");
                }
                else if (selectedItem == 1 && currentSize == 1)
                {
                    priceArray = new double[priceArray.Length];
                    descriptionArray = new string[descriptionArray.Length];
                    currentSize = 0;
                    Console.WriteLine("Item has been removed successfully.");
                }
                else
                {
                    for (int itemIndex = selectedItem - 1; itemIndex < currentSize - 1; itemIndex++)
                    {
                        priceArray[itemIndex] = priceArray[itemIndex + 1];
                        descriptionArray[itemIndex] = descriptionArray[itemIndex + 1];
                    }
                    currentSize -= 1;
                }
                
            }
            else
            {
                PromptErrorMessage("There are no items in the bill to remove.");
            }
        }

        static double AddTip(double netAmount, int arraySize)
        {
            double tipValue = -1;
            char tipMethod = ' ';
            if (arraySize > 0)
            {
                while (tipValue < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Add Tip");
                    Console.WriteLine("===================================================");
                    Console.WriteLine($"Net Total: {netAmount:C}");
                    Console.WriteLine("Tip Methods:");
                    Console.WriteLine("  1 - Tip Percentage");
                    Console.WriteLine("  2 - Tip Amount");
                    Console.WriteLine("  3 - No Tip");
                    Console.WriteLine("\nEnter Tip Method:");
                    tipMethod = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    if (tipMethod == '1')
                        tipValue = GetDouble("Enter Tip Percentage:\n") / 100;
                    else if (tipMethod == '2')
                        tipValue = GetDouble("Enter Tip Amount:\n");
                    else if (tipMethod == '3')
                        tipValue = 0;
                    else
                    {
                        PromptErrorMessage("Invalid input. Enter a number from the selection above (1-3)");
                        PromptPressKeyToContinue();
                    }
                }
            }
            else
            {
                PromptErrorMessage("There are no items in the bill to tip for.");
            }

            return tipValue;
        }

        static void DisplayBill(double[] priceArray, string[] descriptionArray, int currentArraySize, double tipValue)
        {
            if (currentArraySize > 0)
            {
                double netAmount = CalculateNetAmount(priceArray, currentArraySize);
                double tipAmount = CalculateTip(tipValue, netAmount, currentArraySize);
                double gstAmount = CalculateGST(netAmount);
                double totalAmount = CalculateTotalAmount(netAmount, tipAmount, gstAmount);
                Console.Clear();
                Console.WriteLine("Display Bill");
                Console.WriteLine("===================================================\n");
                Console.WriteLine("Description                           Price");
                Console.WriteLine("-----------------------------  ------------");
                for (int x = 0; x < currentArraySize; x++)
                {
                    Console.WriteLine($"{descriptionArray[x],-29}  {priceArray[x],12:C}");
                }
                Console.WriteLine("-----------------------------  ------------");
                Console.WriteLine($"{"Net Total",29}  {netAmount,12:C}");
                Console.WriteLine($"{"Tip Amount",29}  {tipAmount,12:C}");
                Console.WriteLine($"{"Total GST",29}  {gstAmount,12:C}");
                Console.WriteLine($"{"Total Amount",29}  {totalAmount,12:C}");
                Console.WriteLine();
            }
            else
            {
                PromptErrorMessage("There are no items in the bill to display.");
            }
        }

        static void ClearAll(ref double[] priceArray, ref string[] descriptionArray, int maxArraySize, ref int currentArraySize, ref double tipValue)
        {
            if (currentArraySize > 0)
            {
                Console.WriteLine("\nSuccessfully cleared all items.");
                priceArray = new double[maxArraySize];
                descriptionArray = new string[maxArraySize];
                currentArraySize = 0;
                tipValue = 0;
            }
            else
            {
                PromptErrorMessage("There are no items in the bill to clear.");
            }
        }
        
        /*
         *  Validator Method(s)
         */
        static double GetDouble(string prompt)
        {
            double doubleValue = -1;
            while (doubleValue < 0)
            {
                Console.Write(prompt);
                try
                {
                    doubleValue = double.Parse(Console.ReadLine());
                    if (doubleValue < 0)
                    {
                        PromptErrorMessage("Invalid input. Please enter a positive or zero value.");
                    }
                }
                catch
                {
                    PromptErrorMessage("Invalid input. Please enter a numeric value.");
                }
            }
            return doubleValue;
        }

        /*
         *  Menu Screen
         */
        static int ValidatedMenuInput()
        {
            int menuChoice = 0;
            while (menuChoice < 1 || menuChoice > 6)
            {
                DisplayMenu();
                Console.Write("Enter your choice (1-6): ");
                try
                {
                    menuChoice = int.Parse(Console.ReadLine());
                }
                catch { }
                if (menuChoice < 1 || menuChoice > 6)
                {
                    PromptErrorMessage("Invalid menu choice. Try again.");
                    PromptPressKeyToContinue();
                    Console.Clear();
                }
            }
            return menuChoice;
        }
        static void DisplayMenu()
        {
            /// src: http://www.ascii-art.de/ascii/c/coffee.txt
            string menuLogo =
                "         ...\n" +
                "       ..   ..\n" +
                "              ..\n" +
                "               ..\n" +
                "              ..\n" +
                "            ..\n" +
                " ##       ..    ####\n" +
                " ##.............##  ##\n" +
                " ##.. Monk's ...##   ##\n" +
                " ##.... Cafe ...## ##\n" +
                " ##.............###\n" +
                "  ##...........##\n" +
                "   #############\n" +
                " .###############.";

            Console.WriteLine(menuLogo);
            Console.WriteLine("\n====== Menu ======");
            Console.WriteLine(" 1. Add Item");
            Console.WriteLine(" 2. Remove Item");
            Console.WriteLine(" 3. Add Tip");
            Console.WriteLine(" 4. Display Bill");
            Console.WriteLine(" 5. Clear All");
            Console.WriteLine(" 6. Exit");
            Console.WriteLine("==================");
        }

        /*
         *  Helper Methods for less code redundancy/repetition
         */
        static void PromptPressKeyToContinue()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        static void PromptErrorMessage(string message= "Invalid input. Please try again.")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();
        }

        /*
         *  Business Logic / Computations for display output
         */
        static double CalculateNetAmount(double[] itemPrices, int currentArraySize)
        {
            double sum = 0;
            for (int item = 0; item < currentArraySize; item++)
            {
                sum += itemPrices[item];
            }
            return sum;
        }
        static double CalculateTip(double tipValue, double netAmount, int currentArraySize)
        {
            double tipAmount = 0;
            if (currentArraySize > 0)
            {
                if (tipValue < 1 && tipValue > 0)
                    tipAmount = tipValue * netAmount;
                else if (tipValue < 0)
                    tipAmount = 0;
                else
                    tipAmount = tipValue;
            }
            return tipAmount;
        }
        static double CalculateGST(double netAmount)
        {
            double gstPercentage = 0.05;
            return netAmount * gstPercentage;
        }
        static double CalculateTotalAmount(double netAmount, double tipAmount, double gstAmount)
        {
            return netAmount + gstAmount + tipAmount;
        }

    }
}
