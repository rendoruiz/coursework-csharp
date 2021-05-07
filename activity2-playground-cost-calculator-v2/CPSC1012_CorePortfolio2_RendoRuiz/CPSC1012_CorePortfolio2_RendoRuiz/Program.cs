using System;

namespace CPSC1012_CorePortfolio2_RendoRuiz
{
    class Program
    {
        /*
        *  Purpose:    Provides a detailed view of expenses required to build a playground project
        *              based on the given specifications.
        *  Input:      Playground site's fence and gate sizes, and post distance.
        *              Paint and Material type to be used on fences, posts, and gate.
        *  Process:    Calculates all material amount, costs, and total based on the project's various
        *              dimensions and material type.
        *  Output:     Summary of all costs involved on a playground project.
        *  
        *  Author:     Rendo Ruiz
        *  Date:       2018-10-22
        */
        static void Main(string[] args)
        {
            double menuSelection = -1;

            double fenceWidth = 0, fenceLength = 0, fenceHeight = 0, postSpacing = 0, gateWidth = 0, gateHeight = 0;

            double gateArea = 0, gateAreaSpace = 0, gateCost = 0, gateAmount = 0;
            double fencePerimeter = 0, fencePerimeterWithWaste = 0, fenceArea = 0, fenceAreaWithWaste = 0, fenceCost = 0;
            double postCount = 0, postCost = 0;
            double railingPerimeter = 0, railingPerimeterWithWaste = 0, railingCost = 0;
            double paintAmount = 0, paintCost = 0;

            double fenceMaterialCost = 0, postMaterialCost = 0, railingMaterialCost = 0, paintMaterialCost = 0, paintCoverage = 0;

            double subtotal = 0, gst = 0, totalCost = 0;

            double basicPaintCost = 11.99, basicPaintCoverage = 300;
            double premiumPaintCost = 15.99, premiumPaintCoverage = 400;
            double deluxePaintCost = 19.99, deluxePaintCoverage = 500;

            double spruceFenceMaterial = 4.50, sprucePostMaterial = 17.20, spruceRailingCost = 0.49;
            double cedarFenceMaterial = 7.25, cedarPostMaterial = 23.99, cedarRailingCost = 0.69;
            double chainFenceMaterial = 13.50, chainPostMaterial = 50.79, chainRailingCost = 2.49;

          
            while (menuSelection != 0)
            {
                if (menuSelection >= 1 && menuSelection <= 6)
                {
                    Console.Clear();
                }
                
                switch (menuSelection)
                {
                    case 0:
                        // Exit
                        break;

                    case -1:
                        // Default Values
                        Console.WriteLine("\tDefault settings include Spruce lumber and Basic paint\n");
                        fenceMaterialCost = spruceFenceMaterial;
                        postMaterialCost = sprucePostMaterial;
                        railingMaterialCost = spruceRailingCost;
                        paintMaterialCost = basicPaintCost;
                        paintCoverage = basicPaintCoverage;
                        break;

                    case 1:
                        // Playground Dimensions
                        Console.WriteLine("   Fence Dimensions");
                        fenceWidth = GetValidInput("Width");
                        fenceLength = GetValidInput("Length");
                        fenceHeight = GetValidInput("Height");
                        break;

                    case 2:
                        // Gate Dimensions
                        Console.WriteLine("   Gate Dimensions");
                        gateWidth = GetValidInput("Width");
                        gateHeight = GetValidInput("Height");
                        break;

                    case 3:
                        // Distance Between Posts
                        Console.WriteLine("   Distance Between Posts");
                        postSpacing = GetValidInput("Distance");
                        break;

                    case 4:
                        // Fence Type
                        double fenceInput = 0;
                        Console.WriteLine("   Fence Type");
                        Console.WriteLine($"\t1. Spruce\t/{spruceFenceMaterial,6:F2} ^ft.");
                        Console.WriteLine($"\t2. Cedar\t/{cedarFenceMaterial,6:F2} ^ft.");
                        Console.WriteLine($"\t3. Chain Link\t/{chainFenceMaterial,6:F2} ^ft.");
                        
                        while (fenceInput <= 0 || fenceInput >= 4)
                        {
                            fenceInput = GetValidInput("\t\tChoose your fence type");

                            switch (fenceInput)
                            {
                                case 1:
                                    fenceMaterialCost = spruceFenceMaterial;
                                    postMaterialCost = sprucePostMaterial;
                                    railingMaterialCost = spruceRailingCost;
                                    break;
                                case 2:
                                    fenceMaterialCost = cedarFenceMaterial;
                                    postMaterialCost = cedarPostMaterial;
                                    railingMaterialCost = cedarRailingCost;
                                    break;
                                case 3:
                                    fenceMaterialCost = chainFenceMaterial;
                                    postMaterialCost = chainPostMaterial;
                                    railingMaterialCost = chainRailingCost;
                                    break;
                                default:
                                    DisplayErrorMessage();
                                    break;
                            }
                        }
                        break;

                    case 5:
                        // Paint Type
                        double paintInput = 0;
                        Console.WriteLine("   Paint Type");
                        Console.WriteLine($"\t1. Basic\t/{basicPaintCost,6:F2} qts.");
                        Console.WriteLine($"\t2. Premium\t/{premiumPaintCost,6:F2} qts.");
                        Console.WriteLine($"\t3. Deluxe\t/{deluxePaintCost,6:F2} qts.");

                        while (paintInput <= 0 || paintInput >= 4)
                        {
                            paintInput = GetValidInput("\t\tChoose your paint type");

                            switch (paintInput)
                            {
                                case 1:
                                    paintMaterialCost = basicPaintCost;
                                    paintCoverage = basicPaintCoverage;
                                    break;
                                case 2:
                                    paintMaterialCost = premiumPaintCost;
                                    paintCoverage = premiumPaintCoverage;
                                    break;
                                case 3:
                                    paintMaterialCost = deluxePaintCost;
                                    paintCoverage = deluxePaintCoverage;
                                    break;
                                default:
                                    DisplayErrorMessage();
                                    break;
                            }
                        }
                        break;

                    case 6:
                        // Create Packing Slip
                        // gate components
                        gateAreaSpace = fenceHeight * gateWidth;
                        gateArea = gateWidth * gateHeight;
                        if (gateArea <= 0)
                        {
                            gateAmount = 0;
                            gateCost = 0;
                        }
                        else
                        {
                            gateAmount = 1;
                            gateCost = 120 + (gateArea * 15.75);
                        }

                        // fence components
                        fencePerimeter = (fenceLength * 2) + (fenceWidth * 2);
                        fencePerimeterWithWaste = Math.Ceiling(fencePerimeter + (fencePerimeter * 0.10));
                        fenceArea = (fencePerimeter * fenceHeight) - gateAreaSpace;
                        fenceAreaWithWaste = Math.Ceiling(fenceArea + (fenceArea * 0.10));
                        fenceCost = fenceAreaWithWaste * fenceMaterialCost;

                        // post components
                        if (postSpacing <= 0)
                        {
                            postCount = 0;
                            postCost = 0;
                        }
                        else
                        {
                            postCount = Math.Ceiling(fencePerimeter / postSpacing) + 1;
                            postCost = postCount * postMaterialCost;
                        }

                        // railing components
                        railingPerimeter = (fencePerimeter - gateWidth) * 2;
                        railingPerimeterWithWaste = Math.Ceiling(railingPerimeter + (railingPerimeter * 0.10));
                        railingCost = railingPerimeterWithWaste * railingMaterialCost;

                        // paint components
                        if (fenceMaterialCost == chainFenceMaterial)
                        {
                            paintAmount = 0;
                            paintCost = 0;
                        }
                        else
                        {
                            paintAmount = Math.Ceiling((fenceArea * 2) / (paintCoverage / 4));
                            paintCost = paintAmount * paintMaterialCost;
                        }
                        
                        subtotal = Math.Round(fenceCost + postCost + railingCost + gateCost + paintCost, 2);
                        gst = Math.Round(subtotal * 0.05, 2);
                        totalCost = Math.Round(subtotal + gst, 2);

                        Console.WriteLine("\nInvoice and Packing Slip\n");
                        Console.WriteLine($"{fenceAreaWithWaste,7:F1}  ^ft.\tFence Material\t\t@\t{fenceMaterialCost,5:F2}\t={fenceCost,10:F2}");
                        Console.WriteLine($"{postCount,7:F1}\t\tPosts\t\t\t@\t{postMaterialCost,5:F2}\t={postCost,10:F2}");
                        Console.WriteLine($"{railingPerimeterWithWaste,7:F1}   ft.\tRailing\t\t\t@\t{railingMaterialCost,5:F2}\t={railingCost,10:F2}");
                        Console.WriteLine($"{gateAmount,7:F1}\t\tGate\t\t\t\t\t={gateCost,10:F2}");

                        // Paint can only be bought in whole quarts. 1 qt. = .25 gals.
                        Console.WriteLine($"{Math.Ceiling(paintAmount),7:F1}  qts.\tPaint\t\t\t@\t{paintMaterialCost,5:F2}\t={paintCost,10:F2}");

                        Console.WriteLine();
                        Console.WriteLine($"\t\t\t\t\t{"Net Price",13}   ={subtotal,10:F2}");
                        Console.WriteLine($"\t\t\t\t\t{"GST",13}   ={gst,10:F2}");
                        Console.WriteLine($"\t\t\t\t\t{"Total",13}   ={totalCost,10:F2}");

                        Console.ReadLine();
                        break;

                    default:
                        DisplayErrorMessage();
                        break;
                }

                if ((menuSelection >= 1 && menuSelection <= 6) || menuSelection == -1)
                {
                    Console.WriteLine("  1. Playground Dimensions");
                    Console.WriteLine("  2. Gate Dimensions");
                    Console.WriteLine("  3. Distance Between Posts");
                    Console.WriteLine("  4. Fence Type");
                    Console.WriteLine("  5. Paint Type");
                    Console.WriteLine("  6. Create Packing Slip");
                    Console.WriteLine("  0. Exit");
                }
                menuSelection = GetValidInput("\tMake a selection ");
            }
            Console.Clear();
        }

        // Returns a numeric value that is greater than or equal to 0
        static double GetValidInput(string message)
        {
            double rawInput = 0;
            double convertedInput = -1;

            while (convertedInput < 0)
            {
                Console.Write($"{message,12}: ");
                try
                {
                    rawInput = double.Parse(Console.ReadLine());

                    if (rawInput >= 0)
                    {
                        convertedInput = rawInput;
                    }
                    else
                    {
                        DisplayErrorMessage();
                    }
                }
                catch
                {
                    DisplayErrorMessage();
                }
            }
            return convertedInput;
        }

        // Consolidated all error messages for consistency
        static void DisplayErrorMessage()
        {
            Console.WriteLine("Invalid input. Please try again.");
        }
    }
}
