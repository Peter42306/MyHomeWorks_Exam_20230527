using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtSurvey20230728Intertfaces
{
    // Интерфейс IShipData содержит свойства касательно общих данных по судну и дате инспекции
    public interface IShipData
    {
        // Свойства для хранения данных об инспекции и основные данные по судну

        // Дата
        DateTime DateOfSurvey { get; set; }

        // Названии судна
        string Name { get; set; }

        // Light Ship
        double LS { get; set; }

        // Length Between Perpendiculars
        double LBP { get; set; }

        // Breadth Moulded
        double BM { get; set; }

        // Summer Deadweight
        double SDWT { get; set; }
    }
}
