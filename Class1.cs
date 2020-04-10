using System;
namespace Lab5
{
    /// <summary>
    /// Точка в декартовой системе координат
    /// </summary>
    public struct point
    {
        public double X { get; }
        public double Y { get; }
        public point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}, {Y})";
    }
    /// <summary>
    /// Точка полярных координат
    /// </summary>
    public struct polarpoint
    {
        /// <summary>
        /// Радиальная координата
        /// </summary>
        public double Rad { get; }
        /// <summary>
        /// Полярный угол
        /// </summary>
        public double Angle { get; }
        public polarpoint(double rad, double angle)
        {
            Rad = rad;
            Angle = angle;
        }
    }

   
    /// <summary>
    /// Целевой класс
    /// </summary>
     interface PolarCoords
    {
        
        /// <summary>
        /// Счиатет площадь фигуры, заданной массивом полярных координат
        /// </summary>
        /// <param name="PolCoords">Массив полярных координат</param>
        /// <returns>Площадь фигуры</returns>
        public double CalcArea(polarpoint[] PolCoords);
       
    }

    
    /// <summary>
    /// Адаптер
    /// </summary>
    class ConvertCoord : PolarCoords
    {
        private DecartCoords DecCoords;
       
        public double CalcArea(polarpoint[] PolCoords)
        {
            DecCoords = new DecartCoords(PolarToDecartArr(PolCoords));         
            return DecCoords.CalkArea();
        }
        /// <summary>
        /// Преобразует полярные координаты точки в декартовы
        /// </summary>
        /// <param name="pol">Полярная точка</param>
        /// <returns>Декартова точка</returns>
        private point PolarToDecart(polarpoint pol)
        {
            double x, y;
            x = pol.Rad * Math.Cos(DegToRad(pol.Angle));
            y = pol.Rad * Math.Sin(DegToRad(pol.Angle));           
            return new point(x,y);
        }
        /// <summary>
        /// Преобразует массив полярных координат в массив декартовых координат
        /// </summary>
        /// <param name="PolArr">массив полярных координат</param>
        /// <returns>массив декартовых координат</returns>
        private point[] PolarToDecartArr(polarpoint[] PolArr)
        {
            point[] PointArr= new point[PolArr.Length];

            for (int i = 0; i < PointArr.Length; i++)
            {
                PointArr[i] = PolarToDecart(PolArr[i]); 
            }
            return PointArr;
        }
        /// <summary>
        /// Переводит градусы в радианы
        /// </summary>
        /// <param name="Deg">Градусы</param>
        /// <returns>Радианы</returns>
        private double DegToRad(double Deg)
        {
            return Deg * Math.PI / 180;
        }
    }

    // "Adaptee"
    /// <summary>
    /// Адаптируемый
    /// </summary>
    class DecartCoords
    {
        /// <summary>
        /// Расширенный массив координат. Реальные координаты индексируются 1..n-1
        /// </summary>
        point[] ExpCoords;

        public DecartCoords(point[] Coords)
        {
            ExpCoords = ExpandArr(in Coords);
        }
        /// <summary>
        /// Считает площадь фигуры по координатам
        /// </summary>
        /// <returns>Площадь фигуры</returns>
        public double CalkArea()
        {
            double sum1 = 0;
            double tmp;
            for (int i = 1; i < ExpCoords.Length-1; i++)
            {
                tmp = ExpCoords[i].X * (ExpCoords[i + 1].Y - ExpCoords[i - 1].Y);
                sum1 = sum1 + ExpCoords[i].X * (ExpCoords[i + 1].Y - ExpCoords[i - 1].Y);
            }
            return Math.Round(Math.Abs(sum1)/2,2);
        }
        /// <summary>
        /// Добавляет в массив координат по одному элементу в начало и конец.
        /// В начало добавляется последний элемент исходного массива.
        /// В конец добавляется первый элемент исходного массива.        
        /// </summary>
        /// <param name="Coords">Входной массив</param>
        /// <returns>Массив, в котором Coords лежит в индексах i=1..n-1</returns>
        private point[] ExpandArr(in point[] Coords)
        {
            point[] ExpCoords = new point[Coords.Length + 2];
            ExpCoords[0] = Coords[Coords.Length - 1];
            ExpCoords[Coords.Length+1] = Coords[0];

            for (int i = 0; i < Coords.Length; i++)
            {
                ExpCoords[i + 1] = Coords[i];
            }
            return ExpCoords;
        }
    }
}