using System;
using System.Collections.Generic;
using System.IO;

namespace TriviaGameAdministration
{
    class TriviaQuestion
    {
        List<string> mAnswers;
        private string mQuestion;
        private string mAnswerNumber;

        public List<string> Answers
        {
            get
            {
                return mAnswers;
            }
            set
            {
                if (value.Count > 0)
                {
                    mAnswers = value;
                }
                else
                {
                    throw new Exception("Error. Answers list must not be empty.");
                }
            }
        }

        public string Question
        {
            get
            {
                return mQuestion;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    mQuestion = value;
                }
                else
                {
                    throw new Exception("Error. Question value must not be empty.");
                }
            }
        }
        
        public string AnswerNumber
        {
            get
            {
                return mAnswerNumber;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    mAnswerNumber = value;
                }
                else
                {
                    throw new Exception($"Error. Input a value from 0 to 4");
                }
            }
        }

        public TriviaQuestion()
        {
            Question = "No question inserted";
            AnswerNumber = "0";
            Answers = new List<string>(4) { "Answer1", "Answer2", "Answer3", "Answer4"};
        }

        public TriviaQuestion(string question, string answerNumber, List<string> answers)
        {
            Question = question;
            AnswerNumber = answerNumber;
            Answers = answers;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<TriviaQuestion> questionsList = new List<TriviaQuestion>();
            string filePath = @"C:\CPSC\TriviaQuestons.csv";
            char operationSelection = '\0';

            // check and load if data file exists
            LoadData(questionsList, filePath);

            while (operationSelection != '4')
            {
                Console.WriteLine("******************************");
                Console.WriteLine("* Trivia Game Administration *");
                Console.WriteLine("******************************");
                Console.WriteLine("1. List Trivia Items");
                Console.WriteLine("2. Add Trivia Item");
                Console.WriteLine("3. Delete Trivia Item");
                Console.WriteLine("4. Quit");

                Console.Write("Your choice: ");
                operationSelection = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (operationSelection == '1')
                {
                    ListTriviaItems(questionsList);
                }
                else if (operationSelection == '2')
                {
                    AddTriviaItems(questionsList);
                }
                else if (operationSelection == '3')
                {
                    DeleteTriviaItem(questionsList);
                }
                else if (operationSelection == '4')
                {
                    WriteData(questionsList, filePath);
                }
                else
                {
                    PrintColoredMessageLine("Invalid input. Please choose from the choices above (1-4).", true);
                }
                Console.WriteLine();
            }
            
        }

        static void DeleteTriviaItem(List<TriviaQuestion> questionsList)
        {
            if (questionsList.Count > 0)
            {
                Console.WriteLine("Item #     Question");
                Console.WriteLine("------     ---------------------------------------------");
                for (int count = 0; count < questionsList.Count; count++)
                {
                    Console.WriteLine($"{count + 1,-6}     {questionsList[count].Question,-45}");
                }

                string input = "";
                int questionNumber = 0;
                bool isWithinRange = false;
                while (!isWithinRange)
                {
                    Console.Write($"Enter the item number to delete:\n");
                    input = Console.ReadLine();
                    if (int.TryParse(input, out questionNumber))
                    {
                        if (questionNumber >= 1 && questionNumber <= questionsList.Count)
                        {
                            PrintColoredMessageLine($"Item #{questionNumber} has been deleted from the database.", false);
                            questionsList.RemoveAt(questionNumber - 1);
                            isWithinRange = true;
                        }
                    }
                }
            }
            else
            {
                PrintColoredMessageLine("There are currently no items to delete in the database.", true);
            }
        }

        static void AddTriviaItems(List<TriviaQuestion> questionsList)
        {
            string input = "";
            string question = "";
            List<string> answerList = new List<string>(4);

            Console.WriteLine();
            while (string.IsNullOrWhiteSpace(question))
            {
                Console.Write("Enter the trivia item question:\n");
                question = Console.ReadLine().Trim();
            }
            
            for (int index = 1; index <= 4;)
            {
                Console.Write($"Enter answer #{index}:\n");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    answerList.Add(input);
                    index++;
                }
            }

            int answerNumber = 0;
            bool isWithinRange = false;
            while (!isWithinRange)
            {
                Console.Write("Enter the correct answer choice (1-4):\n");
                input = Console.ReadLine();
                if (int.TryParse(input, out answerNumber))
                {
                    if (answerNumber >= 1 && answerNumber <= 4)
                    {
                        isWithinRange = true;
                    }
                }
            }

            TriviaQuestion newQuestion = new TriviaQuestion(question, answerNumber.ToString(), answerList);
            questionsList.Add(newQuestion);
            PrintColoredMessageLine("Item has been successfuly added to the database.", false);
        }

        static void ListTriviaItems(List<TriviaQuestion> questionsList)
        {
            if (questionsList.Count > 0)
            {
                for (int index = 0; index < questionsList.Count; index++)
                {
                    Console.Write($"{ ((index > 0) ? "\n" : "")}");
                    Console.WriteLine($"{(index + 1) + "."}  {questionsList[index].Question}");
                    for (int answer = 0; answer < questionsList[index].Answers.Count; answer++)
                    {
                        Console.WriteLine($"{(answer + 1) + ".",6}  {questionsList[index].Answers[answer]}");
                    }
                    Console.WriteLine($"Current answer: {questionsList[index].AnswerNumber}");
                }
            }
            else
            {
                PrintColoredMessageLine("There are currently no items in the database.", true);
            }
        }

        static void LoadData(List<TriviaQuestion> questionsList, string filePath)
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] lineArray = line.Split(",");
                        string question = lineArray[0];
                        string answerNumber = lineArray[1];
                        List<string> answerList = new List<string>();
                        for (int index = 2; index < lineArray.Length; index++)
                        {
                            answerList.Add(lineArray[index]);
                        }

                        // Create an instance of a BabyName using the 3 values
                        TriviaQuestion currentQuestion = new TriviaQuestion(question, answerNumber, answerList);
                        // Add the baby to the babies list
                        questionsList.Add(currentQuestion);
                    }
                    
                    PrintColoredMessageLine("File loaded successfuly.", false);
                    if (questionsList.Count < 1)
                        PrintColoredMessageLine("Loaded file does not contain any data.", true);
                }
            }
            catch 
            {
                // Let the user know what went wrong.
                PrintColoredMessageLine("No existing file has been found.", true);
            }
        }

        static void WriteData(List<TriviaQuestion> questionsList, string filePath)
        {
            string stringMaker = "";
            FileInfo file = new FileInfo(filePath);

            try
            {
                if (!file.Directory.Exists)
                {
                    Directory.CreateDirectory(file.DirectoryName);
                }

                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (TriviaQuestion question in questionsList)
                    {
                        stringMaker = $"{question.Question},{question.AnswerNumber}";
                        foreach (string answer in question.Answers)
                        {
                            stringMaker += $",{answer}";
                        }

                        sw.WriteLine(stringMaker);
                    }
                }
                if (questionsList.Count > 0)
                    PrintColoredMessageLine($"The trivia items has been saved to file \"{filePath}\".", false);
                else
                    PrintColoredMessageLine($"File \"{filePath}\" has been updated.", false);
            }
            catch (Exception e)
            {
                PrintColoredMessageLine(e.ToString(), true);
            }
            Console.WriteLine("Good-bye.");
        }

        static void PrintColoredMessageLine(string message, bool isError)
        {
            Console.ForegroundColor = (isError) ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
