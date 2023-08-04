using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;
using System.Globalization;

namespace DraughtSurvey20230728Intertfaces
{
    // Атрибут, указывает, что объекты этого класса могут быть сериализованы
    [Serializable]

    // класс Survey, который реализует несколько интерфейсов для хранения данных о судне, осадках, грузах и гидростатических параметрах
    public class Survey : IShipData, IDraughts, IDistances, IDeductibles, IHydrostatics
    {
        // Свойства для хранения данных об инспекции и основные данные по судну

        // Дата
        public DateTime DateOfSurvey { get; set; }

        // Названии судна
        public string Name { get; set; }

        // Light Ship
        public double LS { get; set; }

        // Length Between Perpendiculars
        public double LBP { get; set; }

        // Breadth Moulded
        public double BM { get; set; }

        // Summer Deadweight
        public double SDWT { get; set; }
        

        // Свойства для хранения данных по осадкам судна 
        // Нос левый борт
        public double DraughtForePS { get; set; }

        // Нос правый борт
        public double DraughtForeSS { get; set; }

        // Нос средняя
        public double DraughtForeMean { get; set; }

        // Нос коррекция
        public double DraughtForeCorrectionToPerpendicular { get; set; }

        // Нос осадка после коррекции
        public double DraughtForeCorrected { get; set; }


        // Корма левый борт
        public double DraughtAftPS { get; set; }

        // Корма правый борт
        public double DraughtAftSS { get; set; }

        // Корма средняя
        public double DraughtAftMean { get; set; }

        // Корма коррекция
        public double DraughtAftCorrectionToPerpendicular { get; set; }

        // Корма после коррекции
        public double DraughtAftCorrected { get; set; }


        // Мидель левый борт
        public double DraughtMiddlePS { get; set; }

        // Мидель правый борт
        public double DraughtMiddleSS { get; set; }

        // Мидель средняя
        public double DraughtMiddleMean { get; set; }

        // Медель коррекция
        public double DraughtMiddleCorrection { get; set; }

        // Мидель после коррекции
        public double DraughtMiddleCorrected { get; set; }


        // Свойства для хранения данных по состоянию судна, которые будут посчитаны в программе
        
        // Видимй дифферент
        public double ApparentTrim { get; set; }

        // Дифферент после коррекции
        public double CorrectedTrim { get; set; }

        // Длина судна на осадках
        public double LBM { get; set; }

        // Усреднённая осадка судна
        public double MeanAdjustedDraught { get; set; }

        // Деформация корпуса
        public double HoggingSagging { get; set; }

        // Направление деформации корпуса
        public string HoggingSaggingDirection { get; set; }

        // Крен
        public double Heel { get; set; }

        // Направление крена
        public string HeelDirection { get; set; }


        // Свойства для хранения данных по отстояниям 

        // Отстояния по носу
        public double DistanceForePerpendicularToDraughts { get; set; }

        // Отстояния по корме
        public double DistanceAftPerpendicularToDraughts { get; set; }

        // Лтстояния по миделю
        public double DistanceMiddleToDraughts { get; set; }


        // Свойства для хранения данных по Deductibles

        // Балласт
        public double BallastWater { get; set; }

        // Пресная
        public double FreshWater { get; set; }

        // Тяжёлое
        public double FuelOil { get; set; }

        // Дизель
        public double DieselOil { get; set; }

        // Масло
        public double LubricationOil { get; set; }

        // Другое
        public double Others { get; set; }

        // Всего Deductibles
        public double TotalDeductibles { get; set; }


        // Свойства для хранения данных по гиростатике судна

        // LCF
        public double LCF { get; set; }

        // TPC
        public double TPC { get; set; }

        // MTC+
        public double MTCplus50cm { get; set; }

        // MTC-
        public double MTCminus50cm { get; set; }

        // Водоизмещение
        public double Displacement { get; set; }

        // Плотность воды
        public double SeaWaterDensity { get; set; }

        // Свойства для хранения расчётных переменных

        // Первая коррекция
        public double FirstTrimCorrection { get; set; }

        // Вторая коррекция
        public double SecondTrimCorrection { get; set; }

        // Водоизмещение после коррекции на дифферент
        public double DisplacementCorrectedForTrim { get; set; }

        // Водоизмещение после коррекции на плотность
        public double DisplacementCorrectedForDensity { get; set; }

        // NetDisplacement (чистое водоизмещение)
        public double NetDisplacement { get; set; }

        // Константа
        public double Constant { get; set; }

        // Путь для файла
        public string FilePath { get; set; }


        // Конструктор для ввода данных судна и инициализации его осадок и гидростатических параметров

        public Survey(
            DateTime date, string name, double ls, double lbp, double bm, double sdwt, // IShipData
            double forePS, double foreSS, double aftPS, double aftSS, double midPS, double midSS, // IDraught
            double foreDistance, double aftDistance, double midDistance, // Distances
            double lcf, double tpc, double mtcPlus50, double mtcMinus50, double displacement, double density, // IHydrostatics
            double ballastWater, double freshWater, double fuelOil, double dieselOil, double lubOil, double others // IDeductibles
            )
        {
            // Инициализация свойств объекта класса Survey значениями, переданными в конструктор
            DateOfSurvey = date;
            Name = name;
            LS = ls;
            LBP = lbp;
            BM = bm;
            SDWT = sdwt;

            DraughtForePS = forePS;
            DraughtForeSS = foreSS;
            DraughtAftPS = aftPS;
            DraughtAftSS = aftSS;
            DraughtMiddlePS = midPS;
            DraughtMiddleSS = midSS;

            DistanceForePerpendicularToDraughts = foreDistance;
            DistanceAftPerpendicularToDraughts = aftDistance;
            DistanceMiddleToDraughts = midDistance;

            LCF = lcf;
            TPC = tpc;
            MTCplus50cm = mtcPlus50;
            MTCminus50cm = mtcMinus50;
            Displacement = displacement;
            SeaWaterDensity = density;

            BallastWater = ballastWater;
            FreshWater = freshWater;
            FuelOil = fuelOil;
            DieselOil = dieselOil;
            LubricationOil = lubOil;
            Others = others;
        }

        // Метод для ввода с консоли данных об начальной инспекции (Initial Survey Data)
        public static Survey EnterInitialSurveyData()
        {
            //// Ввод данных начальной инспекции с консоли. Закомментирован для облегчения демонстрации работы метода. Применяем конструктор с предустановленными данными

            //Console.WriteLine("Enter Initial Survey Data: "); 
            //Console.Write("Vessel's name: "); string name=Console.ReadLine();
            //Console.Write("Date of survey (YYYY-MM-DD): "); string dateStr = Console.ReadLine(); DateTime date=DateTime.Parse(dateStr);

            //Console.Write("LPB, m: "); double lbp = double.Parse(Console.ReadLine());
            //Console.Write("BM, m: "); double bm = double.Parse(Console.ReadLine());
            //Console.Write("SDWT, mt: "); double sdwt = double.Parse(Console.ReadLine());
            //Console.Write("Light Ship, mt: "); double ls = Convert.ToDouble(Console.ReadLine());

            //Console.WriteLine("".PadRight(90, '-')); 
            //Console.WriteLine($"Vessel: {name}");
            //Console.WriteLine($"Date of survey: {date.ToString("dd-MM-yyyy")}");
            //Console.WriteLine($"SDWT: {sdwt:N3}, LBP: {lbp:N3}, BM: {bm:N3}, LS: {ls:N3}");
            //Console.WriteLine("".PadRight(90, '-'));

            //Console.Write("Draught Foreward PS: "); double forePS = double.Parse(Console.ReadLine());
            //Console.Write("Draught Foreward SS: "); double foreSS = double.Parse(Console.ReadLine());
            //Console.Write("Draught Aft PS: "); double aftPS = double.Parse(Console.ReadLine());
            //Console.Write("Draught Aft PS: "); double aftSS = double.Parse(Console.ReadLine());
            //Console.Write("Draught Middle PS: "); double midPS = double.Parse(Console.ReadLine());
            //Console.Write("Draught Middle PS: "); double midSS = double.Parse(Console.ReadLine());            

            //Console.Write("Distance from fore PP to draught: "); double foreDistance = double.Parse(Console.ReadLine());
            //Console.Write("Distance from aft PP to draught: "); double aftDistance = double.Parse(Console.ReadLine());
            //Console.Write("Distance from middle to draught: "); double midDistance = double.Parse(Console.ReadLine());

            //Console.WriteLine("Hydrostatic data:");
            //Console.Write("LCF: "); double lcf = double.Parse(Console.ReadLine());
            //Console.Write("TPC: "); double tpc = double.Parse(Console.ReadLine());
            //Console.Write("MTC +0.5 m: "); double mtcPlus50 = double.Parse(Console.ReadLine());
            //Console.Write("MTC -0.5 m: "); double mtcMinus50 = double.Parse(Console.ReadLine());
            //Console.Write("Displacement: "); double displacement = double.Parse(Console.ReadLine());
            //Console.Write("Density of Sea Water: "); double density = double.Parse(Console.ReadLine());

            //Console.WriteLine("Deductibles:");
            //Console.Write("Ballast Water: "); double ballastWater = double.Parse(Console.ReadLine());
            //Console.Write("Fresh Water: "); double freshWater = double.Parse(Console.ReadLine());
            //Console.Write("Fuel Oil: "); double fuelOil = double.Parse(Console.ReadLine());
            //Console.Write("Diesel Oil: "); double dieselOil = double.Parse(Console.ReadLine());
            //Console.Write("Lubrication Oil: "); double lubOil = double.Parse(Console.ReadLine());
            //Console.Write("Others: "); double others = double.Parse(Console.ReadLine());

            //Console.Write("");

            //Survey initialSurvey = new Survey(
            //    date, name, ls, lbp, bm, sdwt, // IShipData
            //    forePS, foreSS, aftPS, aftSS, midPS, midSS, // IDraught
            //    foreDistance, aftDistance, midDistance, // Distances
            //    lcf, tpc, mtcPlus50, mtcMinus50, displacement, density, // IHydrostatics
            //    ballastWater, freshWater, fuelOil, dieselOil, lubOil, others // IDeductibles
            //);


            // Предопределенные данные для демонстрации метода вместо ручного ввода
            Survey initialSurvey = new Survey(
                new DateTime(2023, 08, 01),
                "Isik 5", 1278, 110, 13, 3346,
                0.88, 0.9, 1.73, 1.75, 1.2, 1.22,
                -29.5, 30.4, -0.55,
                0.572, 11.796, 65.842, 79.431, 1373.856, 1,
                35, 17, 0, 35.273, 0.375, 0
                );

            // Здесь выполняется расчеты который можно сделать для класса Survey
            Calculator.AllCalculationsOfClassSurvey(initialSurvey);

            // Запись данных инспекции в бинарный файл и возврат объекта initialSurvey
            string filePath = "InitialSurvey";
            BinFileManager.SaveInitialSurveyToBinaryFile(filePath, initialSurvey);
            return initialSurvey;
        }

        // Метод для ввода с консоли данных об финальнйо инспекции (Final Survey Data)
        public static Survey EnterFinalSurveyData(string filePath)
        {
            // Загрузка начальных данных (объекта initialSurvey) из бинарного файла
            Survey initialSurvey = BinFileManager.LoadInitialSurveyFromBinaryFile(filePath);

            //// Ввод данных финальной инспекции с консоли. Закомментирован для облегчения демонстрации работы метода. Применяем конструктор с предустановленными данными
            
            //Console.WriteLine("Enter Final Survey Data: ");            
            //Console.Write("Date of survey (YYYY-MM-DD): "); string dateStr = Console.ReadLine(); DateTime date = DateTime.Parse(dateStr);

            //// Принимаем повторяющиеся данные из объекта initialSurvey            
            //string name = initialSurvey.Name;
            //double ls = initialSurvey.LS;
            //double lbp = initialSurvey.LBM;
            //double bm = initialSurvey.BM;
            //double sdwt = initialSurvey.SDWT;

            //Console.WriteLine("".PadRight(90, '-'));
            //Console.WriteLine($"Vessel: {name}");
            //Console.WriteLine($"Date of survey: {date.ToString("dd-MM-yyyy")}");
            //Console.WriteLine($"SDWT: {sdwt:N3}, LBP: {lbp:N3}, BM: {bm:N3}, LS: {ls:N3}");
            //Console.WriteLine("".PadRight(90, '-'));

            //Console.Write("Draught Foreward PS: "); double forePS = double.Parse(Console.ReadLine());
            //Console.Write("Draught Foreward SS: "); double foreSS = double.Parse(Console.ReadLine());
            //Console.Write("Draught Aft PS: "); double aftPS = double.Parse(Console.ReadLine());
            //Console.Write("Draught Aft PS: "); double aftSS = double.Parse(Console.ReadLine());
            //Console.Write("Draught Middle PS: "); double midPS = double.Parse(Console.ReadLine());
            //Console.Write("Draught Middle PS: "); double midSS = double.Parse(Console.ReadLine());

            //Console.Write("Distance from fore PP to draught: "); double foreDistance = double.Parse(Console.ReadLine());
            //Console.Write("Distance from aft PP to draught: "); double aftDistance = double.Parse(Console.ReadLine());
            //Console.Write("Distance from middle to draught: "); double midDistance = double.Parse(Console.ReadLine());

            //Console.WriteLine("Hydrostatic data:");
            //Console.Write("LCF: "); double lcf = double.Parse(Console.ReadLine());
            //Console.Write("TPC: "); double tpc = double.Parse(Console.ReadLine());
            //Console.Write("MTC +0.5 m: "); double mtcPlus50 = double.Parse(Console.ReadLine());
            //Console.Write("MTC -0.5 m: "); double mtcMinus50 = double.Parse(Console.ReadLine());
            //Console.Write("Displacement: "); double displacement = double.Parse(Console.ReadLine());
            //Console.Write("Density of Sea Water: "); double density = double.Parse(Console.ReadLine());

            //Console.WriteLine("Deductibles:");
            //Console.Write("Ballast Water: "); double ballastWater = double.Parse(Console.ReadLine());
            //Console.Write("Fresh Water: "); double freshWater = double.Parse(Console.ReadLine());
            //Console.Write("Fuel Oil: "); double fuelOil = double.Parse(Console.ReadLine());
            //Console.Write("Diesel Oil: "); double dieselOil = double.Parse(Console.ReadLine());
            //Console.Write("Lubrication Oil: "); double lubOil = double.Parse(Console.ReadLine());
            //Console.Write("Others: "); double others = double.Parse(Console.ReadLine());
            //Console.Write("");

            //Survey finalSurvey = new Survey(
            //    date, name, ls, lbp, bm, sdwt, // IShipData
            //    forePS, foreSS, aftPS, aftSS, midPS, midSS, // IDraught
            //    foreDistance, aftDistance, midDistance, // Distances
            //    lcf, tpc, mtcPlus50, mtcMinus50, displacement, density, // IHydrostatics
            //    ballastWater, freshWater, fuelOil, dieselOil, lubOil, others // IDeductibles
            //);


            ////Код ниже для проверки работы метода
           


            string name = initialSurvey.Name;
            double ls = initialSurvey.LS;
            double lbp = initialSurvey.LBM;
            double bm = initialSurvey.BM;
            double sdwt = initialSurvey.SDWT;

            // Предопределенные данные для демонстрации метода вместо ручного ввода
            Survey finalSurvey = new Survey(
               new DateTime(2023, 08, 02),
               name, ls, lbp, bm, sdwt,
               3.53, 3.52, 3.6, 3.59, 3.47, 3.46,
               -29.5, 30.4, -0.55,
               1.558, 13.040, 93.034, 102.417, 4199.928, 1,
               35, 12, 0, 34.273, 0.375, 5
               );

            // Здесь выполняется расчеты которые можно сделать для класса Survey
            Calculator.AllCalculationsOfClassSurvey(finalSurvey);

            // возврат объекта finalSurvey
            return finalSurvey;
        }
        
        // Конструктор по умолчанию для класса Survey (был создан для возможности сериализации XML файлов)
        public Survey()
        {
            
        }
    }
}
