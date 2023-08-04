using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtSurvey20230728Intertfaces
{
    // Интерфейс IDraughts содержит свойства касательно осадок судна
    internal interface IDraughts
    {
        // Свойства для хранения данных по осадкам судна 

        // Нос левый борт
        double DraughtForePS { get; set; }

        // Нос правый борт
        double DraughtForeSS { get; set; }

        // Нос средняя
        double DraughtForeMean { get; set; }

        // Нос коррекция
        double DraughtForeCorrectionToPerpendicular { get; set; }

        // Нос осадка после коррекции
        double DraughtForeCorrected { get; set; }


        // Корма левый борт
        double DraughtAftPS { get; set; }

        // Корма правый борт
        double DraughtAftSS { get; set; }

        // Корма средняя
        double DraughtAftMean { get; set; }

        // Корма коррекция
        double DraughtAftCorrectionToPerpendicular { get; set; }

        // Корма после коррекции
        double DraughtAftCorrected { get; set; }


        // Мидель левый борт
        double DraughtMiddlePS { get; set; }

        // Мидель правый борт
        double DraughtMiddleSS { get; set; }

        // Мидель средняя
        double DraughtMiddleMean { get; set; }

        // Мидель коррекция
        double DraughtMiddleCorrection { get; set; }

        // Мидель после коррекции
        double DraughtMiddleCorrected { get; set; }


        // Свойства для хранения данных по состоянию судна, которые будут посчитаны в программе

        // Видимй дифферент
        double ApparentTrim { get; set; }

        // Дифферент после коррекции
        double CorrectedTrim { get; set; }

        // Длина судна на осадках
        double LBM { get; set; }

        // Усреднённая осадка судна
        double MeanAdjustedDraught { get; set; }

        // Деформация корпуса
        double HoggingSagging { get; set; }

        // Направление деформации корпуса
        string HoggingSaggingDirection { get; set; }

        // Крен
        double Heel { get; set; }

        // Направление крена
        string HeelDirection { get; set; }
    }
}
