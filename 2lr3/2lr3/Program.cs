using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var thread1 = new Thread(Thread1);
        var thread2 = new Thread(Thread2);
        var thread3 = new Thread(Thread3);

        thread1.Start();
        thread2.Start();
        thread3.Start();

        thread1.Join();
        thread2.Join();
        thread3.Join();

        await AsyncMethod1();
        await AsyncMethod2();
    }

    static void Thread1()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Rocket starts in (Thread 1): " + (10 - i) + "...");
            Thread.Sleep(200);
        }

        Console.WriteLine("ROCKET STARTED (Thread 1)");
    }

    static void Thread2()
    {
        ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

        foreach (var color in colors)
        {
            Console.ForegroundColor = color;
            Thread.Sleep(200);
        }

        Console.WriteLine("Coloring foreground of console is finished (Thread 2)");
    }

    static void Thread3()
    {
        ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

        foreach (var color in colors)
        {
            Console.BackgroundColor = color;
            Thread.Sleep(300);
        }

        Console.ResetColor();
        Console.WriteLine("Coloring background of console is finished (Thread 3)");
    }

    static async Task AsyncMethod1()
    {
        await Task.Delay(1000);

        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(" Ukraine! ");

        Console.WriteLine("");

        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(" Ukraine! ");

        Console.ResetColor();
    }

    static async Task AsyncMethod2()
    {
        int frames = 240;
        int res = 55; //Window rendering width and height in the console.
        int framerate = 100;
        char[] asciiTable = new char[9] { '.', ':', '-', '=', '+', '*', '#', '%', '@' };
        double radius1 = 100;
        double radius2 = 150;
        double distance = 5000;
        double distance2 = res * distance * (3 / (8 * (radius1 + radius2)));
        double rotatingAngleX = 0.12 * 10 / framerate;
        double rotatingAngleZ = 0.08 * 10 / framerate;
        int x_offset = res / 2;
        int y_offset = res / 2;
        double currentAngleX = 1;
        double currentAngleZ = 1;

        for (int i = 0; i < 3; i++)
        {
            await Task.Delay(1000);
            Console.WriteLine("Donut in " + (i + 1) + "...");
        }

        Console.Clear();

        while (frames > 0)
        {
            double cosAngleZ = Math.Cos(currentAngleZ);
            double sinAngleZ = Math.Sin(currentAngleZ);
            double cosAngleX = Math.Cos(currentAngleX);
            double sinAngleX = Math.Sin(currentAngleX);

            char[,] grid = new char[res, res];
            double[,] zBuffer = new double[res, res];
            for (int x = 0; x < res; x++)
            {
                for (int y = 0; y < res; y++)
                {
                    grid[y, x] = ' ';
                    zBuffer[y, x] = 0;
                }
            }
            for (double i = 0; i < 6.28; i += 0.07)
            {
                double cosI = Math.Cos(i);
                double sinI = Math.Sin(i);
                double x1 = radius2 + radius1 * cosI;
                double y1 = radius1 * sinI;
                for (double j = 0; j < 6.28; j += 0.02)
                {
                    double cosJ = Math.Cos(j);
                    double sinJ = Math.Sin(j);
                    double x2 = x1 * (cosAngleZ * cosJ + sinAngleX * sinAngleZ * sinJ) - (y1 * cosAngleX * sinAngleZ);
                    double y2 = x1 * (cosJ * sinAngleZ - cosAngleZ * sinAngleX * sinJ) + (y1 * cosAngleX * cosAngleZ);
                    double z = distance + radius1 * sinAngleX * sinI + cosAngleX * sinJ * x1;
                    double inverseZ = 1 / z;
                    int xPosition = (int)Math.Floor(x2 * distance2 * inverseZ);
                    int yPosition = (int)Math.Floor(y2 * distance2 * inverseZ);
                    double luminance = cosJ * cosI * sinAngleZ - cosAngleX * cosI * sinJ - sinAngleX * sinI + cosAngleZ * (cosAngleX * sinI - cosI * sinAngleX * sinJ);
                    if (luminance > -0.8)
                    {
                        luminance = Math.Abs(luminance);
                        if (inverseZ > zBuffer[yPosition + y_offset, xPosition + x_offset])
                        {
                            zBuffer[yPosition + y_offset, xPosition + x_offset] = inverseZ;
                            int charIndex = (int)(Math.Round(luminance * ((asciiTable.Length - 1) / 1.414)));
                            grid[yPosition + y_offset, xPosition + x_offset] = asciiTable[charIndex];
                        }
                    }
                }
            }
            currentAngleX = (currentAngleX + rotatingAngleX) % 6.28;
            currentAngleZ = (currentAngleZ + rotatingAngleZ) % 6.28;

            StringBuilder frameOutput = new StringBuilder();
            for (int y = res - 1; y >= 0; y--)
            {
                for (int x = 0; x < res; x++)
                {
                    frameOutput.Append(grid[y, x]);
                }
                frameOutput.Append('\n');
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(frameOutput);
            await Task.Delay(1000 / framerate);

            frames--;
        }
    }
}