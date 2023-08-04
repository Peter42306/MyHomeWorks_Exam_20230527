using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtSurvey20230728Intertfaces
{
    // Интерфейс IDeductibles содержит свойства входящие в значение Deductibles (один из расчётных параметров при проведении драфт сюрвея)
    public interface IDeductibles
    {
        // Свойства для хранения данных по Deductibles

        // Балласт
        double BallastWater { get; set; }

        // Пресная
        double FreshWater { get; set; }
        
        // Тяжёлое
        double FuelOil { get; set; }

        // Дизель
        double DieselOil { get; set; }

        // Масло
        double LubricationOil { get; set; }

        // Другое
        double Others { get; set; }

        // Всего Deductibles
        double TotalDeductibles { get; set; }
    }
}