using System;

namespace CarpetCalculator
{
    class RoomDimension
    {
        private double mLength;
        private double mWidth;

        public double Length
        {
            get
            {
                return mLength;
            }
            set
            {
                if (value > 0)
                    mLength = value;
                else
                    throw new Exception("Input must be greater than 0.");
            }
        }
        public double Width
        {
            get
            {
                return mWidth;
            }
            set
            {
                if (value > 0)
                    mWidth = value;
                else
                    throw new Exception("Input must be greater than 0.");
            }
        }
        
        public RoomDimension(double length, double width)
        {
            Length = length;
            Width = width;
        }

        public double Area()
        {
            return (Width > 0 && Length > 0)
                ? Length * Width
                : 0;
        }

        public string ToString()
        {
            return $"Length = {Length}, Width = {Width}, Area = {Area()}";
        }
    }


    class RoomCarpet
    {
        private RoomDimension mSize;
        private double mCarpetCost;

        public RoomDimension Size
        {
            get
            {
                return mSize;
            }
            set
            {
                mSize = value;
            }
        }
        public double CarpetCost
        {
            get
            {
                return mCarpetCost;
            }
            set
            {
                if (value > 0)
                {
                    mCarpetCost = value;
                }
                else
                {
                    throw new Exception("Input must be greater than 0.");
                }
            }
        }
        
        public RoomCarpet(RoomDimension dim, double cost)
        {
            Size = dim;
            CarpetCost = cost;
        }

        public double TotalCost()
        {
            return CarpetCost * Size.Area();
        }

        public string ToString()
        {
            return $"The total cost of the carpet is {TotalCost():C}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*********************");
            Console.WriteLine("* Carpet Calculator *");
            Console.WriteLine("*********************");

            string input = "";
            bool inputCheck = false;
            
            RoomDimension dimension = new RoomDimension(1, 1);
            RoomCarpet carpet = new RoomCarpet(dimension, 1);

            while (!inputCheck)
            {
                Console.Write("Enter the room length in feet: ");
                input = Console.ReadLine();

                try
                {
                    carpet.Size.Length = double.Parse(input);
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
                Console.Write("Enter the room width in feet: ");
                input = Console.ReadLine();

                try
                {
                    carpet.Size.Width = double.Parse(input);
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
                Console.Write("Enter the carpet cost per square feet: ");
                input = Console.ReadLine();

                try
                {
                    carpet.CarpetCost = double.Parse(input);
                    inputCheck = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine(carpet.Size.ToString());
            Console.WriteLine(carpet.ToString());

        }
    }
}
