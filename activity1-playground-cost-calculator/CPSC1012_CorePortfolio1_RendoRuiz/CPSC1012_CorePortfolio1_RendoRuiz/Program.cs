using System;

namespace CPSC1012_CorePortfolio1_RendoRuiz
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            *  Purpose:    Provides material costs and total cost of a playground project.
            *  Input:      Playground fence and gate dimensions, and post spacing in feet.
            *  Process:    Calculates costs of each material based on the input.
            *  Output:     Summary of costs per material, subtotal, gst, and total cost.
            *  
            *  Author:     Rendo Ruiz
            *  Date:       2018-10-01
            */

            double fenceWidth, fenceLength, fenceHeight, postSpacing, gateWidth, gateHeight;

            double gateArea, gateAreaSpace, gateCost;
            double fencePerimeter, fencePerimeterWithWaste, fenceArea, fenceAreaWithWaste, fenceCost;
            double postCount, postCost;
            double railingPerimeter, railingPerimeterWithWaste, railingCost;
            double paintAmount, paintCost;

            double fenceMaterialCost = 7.25, postMaterialCost = 23.99, railingMaterialCost = 0.69, paintMaterialCost = 15.99;

            double subtotal, gst, totalCost;

            Console.Write("Enter the width of the playground\t: ");
            fenceWidth = double.Parse(Console.ReadLine());
            Console.Write("Enter the height of the playground\t: ");
            fenceLength = double.Parse(Console.ReadLine());
            Console.Write("Enter the height of the fence\t\t: ");
            fenceHeight = double.Parse(Console.ReadLine());
            Console.Write("Enter the space between posts\t\t: ");
            postSpacing = double.Parse(Console.ReadLine());
            Console.Write("Enter the width of the gate\t\t: ");
            gateWidth = double.Parse(Console.ReadLine());
            Console.Write("Enter the height of the gate\t\t: ");
            gateHeight = double.Parse(Console.ReadLine());
            

            gateAreaSpace = fenceHeight * gateWidth;
            gateArea = gateWidth * gateHeight;
            gateCost = 120 + (gateArea * 15.75);

            fencePerimeter = (fenceLength * 2) + (fenceWidth * 2);
            fencePerimeterWithWaste = Math.Ceiling(fencePerimeter + (fencePerimeter * 0.10));
            fenceArea = (fencePerimeter * fenceHeight) - gateAreaSpace;
            fenceAreaWithWaste = Math.Ceiling(fenceArea + (fenceArea * 0.10));
            fenceCost = fenceAreaWithWaste * fenceMaterialCost;

            postCount = Math.Ceiling(fencePerimeter / postSpacing) + 1;
            postCost = postCount * postMaterialCost;

            railingPerimeter = (fencePerimeter - gateWidth) * 2;
            railingPerimeterWithWaste = Math.Ceiling(railingPerimeter + (railingPerimeter * 0.10));
            railingCost = railingPerimeterWithWaste * railingMaterialCost;

            // in gallons; 1 quart = 0.25 gallons
            paintAmount = Math.Ceiling((fenceArea * 2) / 100);
            paintCost = paintAmount * paintMaterialCost;

            subtotal = Math.Round(fenceCost + postCost + railingCost + gateCost + paintCost, 2);
            gst = Math.Round(subtotal * 0.05, 2);
            totalCost = Math.Round(subtotal + gst, 2);

            Console.WriteLine("\nInvoice and Packing Slip\n");
            Console.WriteLine($"{fenceAreaWithWaste,7:F1}  ^ft.\tFence Material\t\t@\t{fenceMaterialCost,5:F2}\t={fenceCost,10:F2}");
            Console.WriteLine($"{postCount,7:F1}\t\tPosts\t\t\t@\t{postMaterialCost,5:F2}\t={postCost,10:F2}");
            Console.WriteLine($"{railingPerimeterWithWaste,7:F1}   ft.\tRailing\t\t\t@\t{railingMaterialCost,5:F2}\t={railingCost,10:F2}");
            Console.WriteLine($"{1,7:F1}\t\tGate\t\t\t\t\t={gateCost,10:F2}");

            // Paint can only be bought in whole quarts. 1 qt. = .25 gals.
            Console.WriteLine($"{Math.Ceiling(paintAmount),7:F1}  qts.\tPaint\t\t\t@\t{paintMaterialCost,5:F2}\t={paintCost,10:F2}");

            Console.WriteLine();
            Console.WriteLine($"\t\t\t\t\t{"Net Price",13}   ={subtotal,10:F2}");
            Console.WriteLine($"\t\t\t\t\t{"GST",13}   ={gst,10:F2}");
            Console.WriteLine($"\t\t\t\t\t{"Total",13}   ={totalCost,10:F2}");
        }
    }
}
