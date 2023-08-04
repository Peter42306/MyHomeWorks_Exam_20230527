using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DraughtSurvey20230728Intertfaces
{
    // Класс BinFileManager, который управляет сохранением и загрузкой объектов типа Results в бинарный файл
    // (каждый объект Result состоит из двух объектов Survey) 
    // предоставляет удобные методы для сохранения, загрузки и отображения данных в двоичном формате,
    // используя сериализацию и десериализацию.
    // Также, используется логирование для отслеживания различных событий с файлами (создание, сохранение, загрузка, открытие)
    public class BinFileManager
    {
        // приватное поле класса для хранения списка результатов Results
        private List<Results> resultsList;

        // приватное поле класса для хранения пути к бинарному файлу.
        private string filePath;
        
        // приватное статическое поле класса для логирования событий
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // конструктор BinFileManager:
        // 1. создает экземпляр BinFileManager
        // 2. инициализирует список resultsList и путь к файлу filePath
        // 3. вызывает метод CreateOrLoadFromBinaryFile(), который проверяет наличие файла и либо загружает данные из файла, либо создает новый список
        public BinFileManager(string filePath)
        {
            resultsList = new List<Results>();
            this.filePath = filePath;
            CreateOrLoadFromBinaryFile(); // Загружаем данные из файла при создании объекта
        }

        // Проверяет наличие файла по заданному пути filePath.
        // Если файл существует, то данные загружаются в список resultsList.
        // В противном случае, создается новый пустой список
        public void CreateOrLoadFromBinaryFile()
        {
            if (File.Exists(filePath))
            {
                LoadFromBinaryFile();
                Console.WriteLine($"Binary file {filePath} was loaded");
                logger.Info($"Binary file {filePath} was loaded");
            }
            else
            {
                resultsList = new List<Results>();
                Console.WriteLine($"List (List<Results>) {resultsList} was created");
                logger.Info($"List (List<Results>) {resultsList} was created");
            }
        }

        // Добавляет объект results типа Results в список resultsList.
        // Перед добавлением присваивается индекс элементу списка, который соответствует его порядковому номеру в списке
        public void AddResults(Results results)
        {            
            results.Index = resultsList.Count + 1;
            resultsList.Add(results);
            Console.WriteLine($"Object {results} was added to binary file {filePath}");
            logger.Info($"Object {results} was added to binary file {filePath}");
        }

        // Сохраняет список resultsList в двоичный файл по указанному пути filePath с помощью сериализации
        public void SaveToBinaryFile()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, resultsList);
            }
            Console.WriteLine($"Object was saved to the file {filePath}");
            logger.Info($"Object was saved to the file {filePath}");
        }

        // Загружает данные из двоичного файла по указанному пути filePath с помощью десериализации и помещает их в список resultsList
        public void LoadFromBinaryFile()
        {
            if (File.Exists(filePath))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    resultsList = (List<Results>)binaryFormatter.Deserialize(fileStream);
                }
                Console.WriteLine($"Obojects were loaded from file {filePath}");
                logger.Info($"Obojects were loaded from file {filePath}");
                
            }
            else
            {
                Console.WriteLine($"File {filePath} was not found");
                logger.Warn($"File {filePath} was not found");
            }
        }

        // Выводит информацию обо всех объектах типа Results из списка resultsList, который загружен из двоичного файла
        public void ShowAllResultsFromBinFile()
        {
            if (File.Exists(filePath))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter1 = new BinaryFormatter();
                    List<Results> resultsFromBinFile = (List<Results>)binaryFormatter1.Deserialize(fileStream);
                    //int count = 0;
                    foreach (var results in resultsFromBinFile)
                    {
                        //count++;
                        Console.WriteLine($"Object No. {results.Index} from bunary file {filePath}");
                        PrintData.InitialAndFinal(results);
                        Console.WriteLine("".PadRight(90, '='));
                        Console.WriteLine("".PadRight(90, '='));
                    }
                }
            }
            else
            {
                Console.WriteLine($"File {filePath} was not found");
                logger.Warn($"File {filePath} was not found");
            }
        }

        // Выводит краткое подведение итогов по всем объектам
        public void ShowShortSummary()
        {
            if (File.Exists(filePath))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter1 = new BinaryFormatter();
                    List<Results> resultsFromBinFile = (List<Results>)binaryFormatter1.Deserialize(fileStream);
                    Console.WriteLine("{0,5}{1,18}{2,20}{3,17}{4,17}{5,21}{6,18}{7,14}{8,8}", "No.", "File Reference", "Vessel", "Initial Date", "Final Date", "Bill of Lading", "Draught Survey", "Difference", "%");
                    
                    foreach (var results in resultsFromBinFile)
                    {                        
                        PrintData.Short(results);                        
                    }
                }
            }
            else
            {
                Console.WriteLine($"File {filePath} was not found");
                logger.Warn($"File {filePath} was not found");
            }            
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        
        // Метод для сохранения объектов типа Survey (исходные данные) в бин файл
        public static void SaveInitialSurveyToBinaryFile(string filePath, Survey initialSurvey)
        {
            //filePath = "Initial_Survey.bin";
            initialSurvey.FilePath = filePath ;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, initialSurvey);
            }
            
            Console.WriteLine($"Object was saved to the file {filePath}");
            logger.Info($"Object was saved to the file {filePath}");
            
        }


        // Методы для загрузки объектов типа Survey (исходные данные) 
        public static Survey LoadInitialSurveyFromBinaryFile(string filePath)
        {
            //string filePath = $"{initialSurvey.Name}_{initialSurvey.DateOfSurvey.ToString("yyyyMMdd")}_Initial.bin";


            if (File.Exists(filePath))
            {
                Survey initialSurvey;
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    initialSurvey=(Survey)binaryFormatter.Deserialize(fileStream);

                    //resultsList = (List<Results>)binaryFormatter.Deserialize(fileStream);
                }
                Console.WriteLine($"Obojects were loaded from file {filePath}");
                logger.Info($"Obojects were loaded from file {filePath}");
                return initialSurvey;
            }
            else
            {
                Console.WriteLine($"File {filePath} was not found");
                logger.Warn($"File {filePath} was not found");
                return null;
            }
        }
    }
}
