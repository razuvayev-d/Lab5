using System;

namespace Lab5
{
    class Client
    {
        static void Main(string[] args)
        {
            // point[] arr = new point[3];

            //// arr[0] = new point(0, 0);
            //// arr[1] = new point(0, 2);
            //// arr[2] = new point(4, 2);
            ////arr[3] = new point(4, 0);
            // arr[0] = new point(2, 1);
            // arr[1] = new point(4, 5);
            // arr[2] = new point(7, 8);

            //polarpoint[] arr = new polarpoint[3];

            //arr[0] = new polarpoint(4, 0);
            //arr[1] = new polarpoint(4 * Math.Sqrt(2), 45);
            //arr[2] = new polarpoint(0, 0);
            // arr[3] = new polarpoint(0, 0);



            //PolarCoords s = new ConvertCoord();

            //Console.WriteLine(s.CalcArea(arr));
            //Console.ReadKey();

            Console.WriteLine("Введите число вершин фигуры");
            int n;
            try
            {
                if (!Int32.TryParse(Console.ReadLine(), out n)) throw new ArgumentException("Введено не число");
                // Console.WriteLine("Введите полярные координаты точки парами через пробел");
                polarpoint[] polarr = new polarpoint[n];
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("Введите полярные координаты точки через пробел");
                    string str = Console.ReadLine();
                    string[] ss = str.Split(" ");

                    if (ss.Length != 2) throw new FormatException("Введено неверное количество координат");

                    double rad, angle;
                    if (!(Double.TryParse(ss[0], out rad) | Double.TryParse(ss[1], out angle)))
                        throw new ArgumentException("Введено не число");

                    polarr[i] = new polarpoint(rad, angle);
                }

                PolarCoords Adapt = new ConvertCoord();

                Console.WriteLine("Площадь фигуры равна: ");
                Console.WriteLine(Adapt.CalcArea(polarr));
            }
            catch (ArgumentException e)            
            {
                Console.WriteLine(e.Message);
               
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Нажмите любую клавишу, чтобы завершить программу");
                Console.ReadKey();
                Environment.Exit(0);
            }




        }
    }
}
