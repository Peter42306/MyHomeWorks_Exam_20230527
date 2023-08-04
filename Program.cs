using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using NLog;
using OfficeOpenXml;

// Программа для проведения драфт сюрвея.
// Возможности:
// 1. Ввод данных начальной инспекции с консоли, метод EnterInitialSurveyData() класса Survey
// 2. Данные начальной инспекции сохраняются в бинарном файле и вызываются при продолжении инспекции, метод EnterFinalSurveyData(string filePath) класса Survey
// 3. Возможность создания объектов начальной и финальной инспекции по предустановленному шаблону, для перепроверки данных
// 4. Расчёт необходимых данных по количесту груза и анализу состояния судна на момент инспекции, методы класса Calculations
// 5. Возможность сохранения в бинарный файл объектов класса Results, а так же списка объектов Results с помощью методов класса BinFileManager
// 6. Возможность записи результатов инспекции в файл Excel (для хранения, печати в удобной форме) с помощью метода ToExcel(string templateExcelFilePath, Results data) класса PrintData
// 7. Возможность создания XML файлов, выборка из списка объектов проведенных инспекций (объекты класса Results, списки объектов класса Results) по названию судна или по значению разницы в %
// 8. Возможность вывода на консоль различных конфигураций данных с помощью методов класса PrintData, 
// 9. Логгирование даных при операциями с файлами (создание, сохранение, загрузка, открытие)
// 10. Расчёт сколько времени занимает выполнение программы

// Представлены 3 варианты тестирования

namespace DraughtSurvey20230728Intertfaces
{
    internal class Program
    {
        // Инициализация логгера
        public static Logger logger = LogManager.GetCurrentClassLogger(); 

        static void Main(string[] args)
        {
            // Логирование времени начала выполнения 
            logger.Info($"Main running was started on {DateTime.Now.ToString("yyyy MM dd")} at {DateTime.Now.ToString("HH mm ss ffff ")}");

            // Создание экземпляра секундомера для измерения времени выполнения и запуск секундомера
            Stopwatch stopwatch =new Stopwatch();
            stopwatch.Start();



            ///////////////////////////////////////////////////////////////////////////////////////////

            // Демонстрация 1
            // Демонстрация работы класса Result с предустановленными данными

            // Исходные данные - два объекта класса Survey
            // Для удобства данные предустановлены через конструктор
            // Есть возможность ввода данных вручную, через EnterInitialSurveyData и EnterFinalSurveyData

            Survey initialSurvey = new Survey(
                new DateTime(2023, 07, 15),
                "Isik 3", 1278, 110, 13, 3346,
                0.88, 0.9, 1.73, 1.75, 1.2, 1.22,
                -29.5, 30.4, -0.55,
                0.572, 11.796, 65.842, 79.431, 1373.856, 1,
                35, 17, 0, 35.273, 0.375, 0 //fgh
                );

            Survey finalSurvey = new Survey(
                           new DateTime(2023, 07, 25),
                           "Isik 3", 1278, 110, 13, 3346,
                           3.53, 3.52, 3.6, 3.59, 3.47, 3.46,
                           -29.5, 30.4, -0.55,
                           1.558, 13.040, 93.034, 102.417, 4199.928, 1,
                           35, 12, 0, 34.273, 0.375, 5
                           );

            // Создание объекта results класса Results, который принимает данные initialSurvey и finalSurvey
            Results results = new Results("155/YU", initialSurvey, finalSurvey, 3009.588);

            // Проведение расчётов
            Calculator.AllCalculationsOfClassResults(results);

            // Вывод результатов на консоль
            PrintData.InitialAndFinal(results);

            // Создается объект binFileManager класса BinFileManager, который
            // управляет сохранением и загрузкой результатов обследования в бинарный файл с именем DraughtSurveyResults.bin
            string filePath = "DraughtSurveyResults.bin";
            BinFileManager binFileManager = new BinFileManager(filePath);
            binFileManager.AddResults(results);
            binFileManager.SaveToBinaryFile();

            // Выводятся все результаты из бинарного файла с помощью метода ShowAllResultsFromBinFile,
            // и выводится список с основными данными с помощью метода ShowShortSummary
            binFileManager.ShowAllResultsFromBinFile();
            binFileManager.ShowShortSummary();




            ///////////////////////////////////////////////////////////////////////////////////////////

            //// Демонстрация 2

            //// Цели демонстрации:
            //// 1. Введение данных начальной инспекции
            //// 2. Сохранение данных начальной инспекции в бин файл
            //// 3. Выход/вход 
            //// 4. Продолжение работы с данными финальной инспекции
            //// 5. Добавление в список результатов хранящихся в бин файле
            //// 6. Демонстрация всех результатов из бин файла

            //// создается строковая переменная filePath, содержащая имя файла, в который будут сохранены начальные данные обследования судна
            //string filePath = "Initial Draught Survey 2023 08 01.bin";

            //// Создается объект initialSurvey класса Survey, и пользователю предлагается ввести начальные данные обследования вручную с помощью метода Survey.EnterInitialSurveyData().
            //Survey initialSurvey = Survey.EnterInitialSurveyData();

            //// полученные данные сохраняются в бинарный файл с помощью статического метода BinFileManager.SaveInitialSurveyToBinaryFile(filePath, initialSurvey)
            //BinFileManager.SaveInitialSurveyToBinaryFile(filePath, initialSurvey);

            //// результаты начальной инспекции выводятся на экран с помощью метода PrintData.InitialOrFinal(initialSurvey)
            //PrintData.InitialOrFinal(initialSurvey);

            ////-------------------------------------------------------------------------
            //// Далее для дальнейшей демонстрации нужно закомментировать код выше, выйти из программы и снова зайти, для ввода данных финальной инспекции
            //// Начальные данные для продолжения будут загружены из бин файла

            //// После выхода из программы и повторного входа, данные загружаются из бинарного файла с помощью метода Survey.EnterFinalSurveyData(filePath). Пользователю предлагается ввести данные финальной инспекции
            //Survey finalSurvey = Survey.EnterFinalSurveyData(filePath);

            //// Результаты начальной инспекции выводятся на экран с помощью метода PrintData.InitialOrFinal(initialSurvey)
            //PrintData.InitialOrFinal(finalSurvey);

            //// создаётся объект results класса Results, ему передаются данные начальной и финальнйо инспекции, а так же количество груза по коносаменту и номер рабочего  файла
            //Results results = new Results("741/OD", initialSurvey, finalSurvey, 3009.588);

            //// вычисления на основе входящих данных
            //Calculator.AllCalculationsOfClassResults(results);
            //PrintData.InitialAndFinal(results);
            //PrintData.Short(results);

            //// создаётся путь у файлу
            //string filePathForList = "DraughtSurveyResults.bin";

            ////Здесь создается объект binFileManager класса BinFileManager, который управляет сохранением и загрузкой результатов обследования в бинарный файл с именем "DraughtSurveyResults.bin".
            //BinFileManager binFileManager = new BinFileManager(filePathForList);

            //// операции над бинарным файлом используя методы класса BinFileManager
            //binFileManager.AddResults(results);
            //binFileManager.SaveToBinaryFile();
            //binFileManager.ShowAllResultsFromBinFile();
            //binFileManager.ShowShortSummary();




            /////////////////////////////////////////////////////////////////////////////////////////////

            //// Демонстрация 3
            //// Демонстрация работы класса PrintData, запись в файл Excel
            //// Исходные данные - два объекта класса Survey
            //// Для удобства данные вводятся через конструктор
            //// Есть возможность ввода данных вручную, через EnterInitialSurveyData и EnterFinalSurveyData


            //// Создание объектов Survey для начальной и конечной инспекции
            //// Инициализация предустановленными значениями для каждого объекта Survey для облегчения демонстрации
            //Survey initialSurvey = new Survey(
            //    new DateTime(2023, 07, 15),
            //    "Isik 3", 1278, 110, 13, 3346,
            //    0.88, 0.9, 1.73, 1.75, 1.2, 1.22,
            //    -29.5, 30.4, -0.55,
            //    0.572, 11.796, 65.842, 79.431, 1373.856, 1,
            //    35, 17, 0, 35.273, 0.375, 0 
            //    );

            //Survey finalSurvey = new Survey(
            //               new DateTime(2023, 07, 25),
            //               "Isik 3", 1278, 110, 13, 3346,
            //               3.53, 3.52, 3.6, 3.59, 3.47, 3.46,
            //               -29.5, 30.4, -0.55,
            //               1.558, 13.040, 93.034, 102.417, 4199.928, 1,
            //               35, 12, 0, 34.273, 0.375, 5
            //               );

            //// Создание объекта Results и выполнение всех расчетов
            //Results results = new Results("159/YU", initialSurvey, finalSurvey, 3009.588);
            //Calculator.AllCalculationsOfClassResults(results);

            //// Вывод данных в Excel файл
            //string templateExcelFilePath = "ExcelDraughtSurveyTemplate.xlsx";
            //PrintData.ToExcel(templateExcelFilePath, results);

            //// Работа с XML файлом
            //string xmlFilePath = "DraughtSurveyResults.xml";
            //XmlFileManager xmlFileManager = new XmlFileManager(xmlFilePath);
            //xmlFileManager.CreateOrLoadFromXmlFile();
            //xmlFileManager.AddResult(results);
            //xmlFileManager.ShowAllResultsFromXmlFile();

            //// Поиск в списке результатов по имени судна и вывод на экран
            //var resultsByVesselName = xmlFileManager.GetVesselsByName("Isik 3");
            //PrintData.ListOfListResults(resultsByVesselName);
            //Console.WriteLine("".PadRight(90, '='));

            //// Получение списка судов, где разница более половины процента и вывод на экран
            //var resultsByDifferenceMoreHalfPercent = xmlFileManager.GetVesselsIfDifferenceMoreHalfPercent();
            //PrintData.ListOfListResults(resultsByDifferenceMoreHalfPercent);

            //// Работа с бинарным файлом
            //string binFilePath = "DraughtSurveyResults.bin";
            //BinFileManager binFileManager = new BinFileManager(binFilePath);
            //binFileManager.CreateOrLoadFromBinaryFile();
            //binFileManager.AddResults(results);
            //binFileManager.SaveToBinaryFile();
            //binFileManager.ShowShortSummary();
            //binFileManager.ShowAllResultsFromBinFile();



            //////////////////////////////////////////////////////////////////////////////////



            // Остановка секундомера после выполнения кода
            stopwatch.Stop();

            // Логирование времени окончания выполнения Main()
            logger.Info($"Main running was completed on {DateTime.Now.ToString("yyyy MM dd")} at {DateTime.Now.ToString("HH mm ss ffff ")}");

            // Получение затраченного времени
            TimeSpan elapsedTime = stopwatch.Elapsed;
            
            // Логирование затраченного времени
            logger.Info($"Elapsed time: {elapsedTime.Seconds:00}.{elapsedTime.Milliseconds:0000} seconds");

            // Вывод затраченного времени на консоль
            Console.WriteLine($"Elapsed time: {elapsedTime.Seconds:00}.{elapsedTime.Milliseconds:0000} seconds"); 
        }
    }
}
