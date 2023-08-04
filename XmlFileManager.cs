using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DraughtSurvey20230728Intertfaces
{
    // Класс, который управляет сохранением и загрузкой объектов типа Results (результаты измерений) в формате XML
    // Предоставляет удобные методы для сохранения, загрузки и отображения данных в формате XML, используя сериализацию и десериализацию.
    // Также, используется логирование для отслеживания различных событий при работе с файлами.
    // Есть возможность получать список объектов Results по заданным критериям.
    public class XmlFileManager
    {
        // приватное поле класса для хранения списка результатов Results
        private List<Results> resultsList;

        // приватное поле класса для хранения пути к XML-файлу.
        private string filePath;

        // приватное статическое поле класса для логирования событий
        private static Logger logger= LogManager.GetCurrentClassLogger();

        // Конструктор,
        // 1. Создает экземпляр XmlFileManager
        // 2. Инициализирует список resultsList и путь к файлу filePath
        // 3. Вызывает метод CreateOrLoadFromXmlFile(), который проверяет наличие файла и либо загружает данные из файла, либо создает новый пустой список
        public XmlFileManager(string filePath)
        {
            resultsList = new List<Results>();
            this.filePath = filePath;
            CreateOrLoadFromXmlFile();
        }

        // Добавляет объект results типа Results в список resultsList и сохраняет изменения в XML-файле с помощью метода SaveToXmlFile()
        public void AddResult(Results results)
        {
            resultsList.Add(results);
            Console.WriteLine($"Object {results} was added to XML file {filePath}");
            logger.Info($"Object {results} was added to XML file {filePath}");
            SaveToXmlFile();
            
        }
        // Проверяет наличие файла по заданному пути filePath.
        // Если файл существует, то данные загружаются в список resultsList.
        // В противном случае, создается новый пустой список.
        public void CreateOrLoadFromXmlFile()
        {
            if (File.Exists(filePath))
            {
                LoadFromXmlFile();
                Console.WriteLine($"XML file {filePath} was loaded");
                logger.Info($"XML file {filePath} was loaded");
            }
            else
            {
                resultsList=new List<Results>();
                Console.WriteLine($"List(List<Results>) for XML file {resultsList} was created");
                logger.Info($"List(List<Results>) for XML file {resultsList} was created");
            }
        }

        // Загружает данные из XML-файла по указанному пути filePath с помощью десериализации и помещает их в список resultsList.
        public void LoadFromXmlFile()
        {
            if(File.Exists(filePath))
            {
                XmlSerializer xmlSerializer= new XmlSerializer(typeof(List<Results>));
                using(FileStream fileStream =new FileStream(filePath, FileMode.Open))
                {
                    resultsList=(List<Results>)xmlSerializer.Deserialize(fileStream);
                }
                Console.WriteLine($"XML file {filePath} was loaded");
                logger.Info($"XML file {filePath} was loaded");
            }
            else
            {
                Console.WriteLine($"Error!!! XML file {filePath} was not found");
                logger.Error($"Error!!! XML file {filePath} was not found");
            }
        }

        // Сохраняет список resultsList в формате XML в файл по указанному пути filePath
        public void SaveToXmlFile()
        {
            XmlSerializer xmlSerializer=new XmlSerializer(typeof(List<Results>));
            using(FileStream fileStream=new FileStream(filePath, FileMode.Create))
            {
                xmlSerializer.Serialize(fileStream, resultsList);
            }
            Console.WriteLine($"XML file {filePath} was saved");
            logger.Info($"XML file {filePath} was saved");
        }

        // Выводит информацию обо всех объектах типа Results из списка resultsList, который загружен из XML-файла
        public void ShowAllResultsFromXmlFile()
        {
            if(File.Exists(filePath))
            {                
                foreach (var results in resultsList)
                {
                    PrintData.InitialAndFinal(results);
                }
            }
            else
            {
                Console.WriteLine($"XML file {filePath} was not found");
                logger.Warn($"XML file {filePath} was not found");
            }
        }

        // Возвращают список объектов Results, соответствующих определенным условиям по имени судна и проценту разницы веса груза по сравнению с данными из Bill of Lading.
        public List<Results> GetVesselsByName(string name)
        {
            return resultsList.Where(result => result.InitialSurvey.Name == name).ToList();
        }

        public List<Results> GetVesselsIfDifferenceMoreHalfPercent()
        {
            return resultsList.Where(results => results.DifferencePercentageByBL >= 0.5).ToList();
        }
    }
}
