using System;
using System.Collections.Generic;
using System.IO;

namespace TriviaGame
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
            Answers = new List<string>(4) { "Answer1", "Answer2", "Answer3", "Answer4" };
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

            LoadData(questionsList, filePath);

            Console.WriteLine("***************");
            Console.WriteLine("* Trivia Game *");
            Console.WriteLine("***************");

            PlayGame(questionsList);
        }

        static void PlayGame(List<TriviaQuestion> questionsList)
        {
            int listLength = questionsList.Count;
            int playerCount = 2;
            int[] playerScores = new int[playerCount];
            string inputAnswer = "";
            List<string> indexToChar = new List<string>() { "a", "b", "c", "d" };

            if (listLength >= 2)
            {
                playerScores[0] = 0; playerScores[1] = 0;
                Console.WriteLine($"There are {listLength} in the question bank. Each player will answer {listLength / playerCount} question(s).");

                for (int index = 0; index < (listLength / playerCount) * 2; index++)
                {
                    int currentPlayer = (index < listLength / playerCount) ? 0 : 1;
                    Console.WriteLine($"\nPlayer {currentPlayer + 1} Question");
                    Console.WriteLine("------------------");
                    Console.WriteLine(questionsList[index].Question);
                    for (int answerIndex = 0; answerIndex < questionsList[index].Answers.Count; answerIndex++)
                    {
                        Console.WriteLine($"{indexToChar[answerIndex]}.  {questionsList[index].Answers[answerIndex]}");
                    }
                    while (!indexToChar.Contains(inputAnswer.ToLower()))
                    {
                        Console.Write("Your answer: ");
                        inputAnswer = Console.ReadLine().ToLower();
                    }
                    if (indexToChar.IndexOf(inputAnswer) + 1 == int.Parse(questionsList[index].AnswerNumber))
                        playerScores[currentPlayer] += 1;

                    inputAnswer = "";
                }

                Console.WriteLine($"\nPlayer 1 has {playerScores[0]} points. Player 2 has {playerScores[1]} points.");
                if (playerScores[0] == playerScores[1])
                    Console.WriteLine("It's a draw!");
                else
                    Console.WriteLine($"Player {((playerScores[0] > playerScores[1]) ? 1 : 2)} is the winner!");
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

        static void PrintColoredMessageLine(string message, bool isError)
        {
            Console.ForegroundColor = (isError) ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
