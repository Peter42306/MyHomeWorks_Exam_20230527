using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtSurvey20230728Intertfaces
{
    // Интерфейс IDistances содержит свойства отстояний осадок до перпендикуляров (один из расчётных параметров при проведении драфт сюрвея)
    internal interface IDistances
    {
        // Свойства для хранения данных по отстояниям 

        // Отстояния по носу
        double DistanceForePerpendicularToDraughts { get; set; }

        // Отстояния по корме
        double DistanceAftPerpendicularToDraughts { get; set; }

        // Отстояния по миделю
        double DistanceMiddleToDraughts { get; set; }
    }
}