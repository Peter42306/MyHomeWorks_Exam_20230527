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
    // Атрибут, указывает, что объекты этого класса могут быть сериализованы
    [Serializable]

    // Класс объединяет результаты полученные на начальной и финальной инспекциях и выдаёт общий результат, объект для дальнейшего хранения инмпекции
    public class Results
    {
        // Свойства для хранения данных об инмпекции

        // Порядковый номер инспекции при выводе на консоль или при записи в файл
        public int Index { get; set; }
        
        // Номера файла (дела, инспекции)
        public string FileReference { get; set; }
        
        // Начальная инспекция
        public Survey InitialSurvey { get; set; }

        // Финальная инспекция
        public Survey FinalSurvey { get; set; }
        
        // Данных по цифре коносамента или береговой цифре
        public double CargoWeightByBL { get; set; }
        
        // ГЛАВНЫЙ РЕЗУЛЬТАТ РАСЧЁТОВ
        public double CargoWeightByDS { get; set; }
        
        // Разница с коносаментной цифрой в тоннах
        public double Difference { get; set; }

        // Разница в какую сторону (больше или меньше)
        public string DifferenceMoreOrLess { get; set; }

        // Разница с коносаментной цифрой в %
        public double DifferencePercentageByBL { get; set; }

        
        // Конструкторы с параметрами
        public Results(string fileRef, Survey initial, Survey final, double cargoBL)
        {
            FileReference = fileRef;
            InitialSurvey = initial;
            FinalSurvey = final;
            CargoWeightByBL = cargoBL;
        }

        public Results(Survey initial, Survey final, double cargoBL)
        {
            InitialSurvey = initial;
            FinalSurvey = final;
            CargoWeightByBL = cargoBL;
        }

        // Конструктор по умолчанию для класса Results (был создан для возможности сериализации XML файлов)
        public Results()
        {
            
        }
    }
}
