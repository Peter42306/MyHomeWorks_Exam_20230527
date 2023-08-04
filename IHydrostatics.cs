using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtSurvey20230728Intertfaces
{
    // Интерфейс IHydrostatics содержит свойства касательно данных по гидростатика судна
    internal interface IHydrostatics
    {
        // Свойства для хранения данных по гиростатике судна

        // LCF
        double LCF { get; set; }

        // TPC
        double TPC { get; set; }

        // MTC+
        double MTCplus50cm { get; set; }

        // MTC-
        double MTCminus50cm { get; set; }

        // Водоизмещение
        double Displacement { get; set; }

        // Плотность воды
        double SeaWaterDensity { get; set; }


        // Свойства для хранения расчётных переменных

        // Первая коррекция
        double FirstTrimCorrection { get; set; }

        // Вторая коррекция
        double SecondTrimCorrection { get; set; }

        // Водоизмещение после коррекции на дифферент
        double DisplacementCorrectedForTrim { get; set; }

        // Водоизмещение после коррекции на плотность
        double DisplacementCorrectedForDensity { get; set; }

        // NetDisplacement (чистое водоизмещение)
        double NetDisplacement { get; set; }

        // Константа
        double Constant { get; set; }        
    }
}
