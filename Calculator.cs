using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtSurvey20230728Intertfaces
{
    // Класс в котором находятся методы для вычислений 
    public class Calculator
    {
        ///////////////////////////////////////////////////////////////////////////////////////////

        // методы для работы с объектами класса Survey

        // метод для усреднения осадок
        public static Survey MeanDraughts(Survey data)
        {
            data.DraughtForeMean = (data.DraughtForePS + data.DraughtForeSS) / 2;
            data.DraughtAftMean=(data.DraughtAftPS + data.DraughtAftSS)/2;
            data.DraughtMiddleMean = (data.DraughtMiddlePS + data.DraughtMiddleSS) / 2;
            return data;
        }

        // метод для подсчёта Length Between Marks
        public static Survey LBM(Survey data)
        {
            data.LBM = Math.Round((
                data.LBP + 
                data.DistanceForePerpendicularToDraughts - 
                data.DistanceAftPerpendicularToDraughts), 3);
            return data;
        }

        // метод для подсчёта Apparent Trim
        public static Survey ApparentTrim(Survey data)
        {
            data.ApparentTrim = Math.Round(
                data.DraughtAftMean - 
                data.DraughtForeMean, 3);
            return data;
        }

        // метод для подсчёта коррекций к перпендикулярам
        public static Survey CorrectionsToPerpendiculars(Survey data)
        {

            data.DraughtForeCorrectionToPerpendicular = Math.Round((
                data.ApparentTrim *
                data.DistanceForePerpendicularToDraughts /
                data.LBM), 3);

            data.DraughtAftCorrectionToPerpendicular = Math.Round((
                data.ApparentTrim *
                data.DistanceAftPerpendicularToDraughts /
                data.LBM), 3);

            data.DraughtMiddleCorrection = Math.Round((
                data.ApparentTrim *
                data.DistanceMiddleToDraughts /
                data.LBM), 3);

            return data;
        }

        // метод для подсчёта откорректированных осадок
        public static Survey CorrectedDraughts(Survey data)
        {
            data.DraughtForeCorrected = Math.Round((
                data.DraughtForeMean +
                data.DraughtForeCorrectionToPerpendicular), 3);

            data.DraughtAftCorrected = Math.Round((
                data.DraughtAftMean +
                data.DraughtAftCorrectionToPerpendicular), 3);

            data.DraughtMiddleCorrected = Math.Round((
                data.DraughtMiddleMean +
                data.DraughtMiddleCorrection), 3);
            return data;
        }

        // метод для подсчёта откорректированного дифферента
        public static Survey CorrectedTrim(Survey data)
        {
            data.CorrectedTrim = Math.Round(
                data.DraughtAftCorrected -
                data.DraughtForeCorrected, 3);
            return data;
        }

        // метод для подсчёта Mean Adjusted Draught
        public static Survey MeanAdjustedDraught(Survey data)
        {
            data.MeanAdjustedDraught = Math.Round(((
                6 * data.DraughtMiddleCorrected +
                data.DraughtForeCorrected +
                data.DraughtAftCorrected) / 8), 3);
            return data;
        }

        // метод для подсчёта деформаций корпуса судна в метрах
        public static Survey HoggingSagging (Survey data)
        {
            data.HoggingSagging = Math.Abs(Math.Round(
                data.DraughtMiddleCorrected -
                (data.DraughtForeCorrected + data.DraughtAftCorrected) / 2, 3));
            return data;
        }

        // метод для подсчёта деформаций корпуса судна, направление деформации
        public static Survey HoggingSaggingDirection (Survey data)
        {
            if (data.DraughtMiddleCorrected< (data.DraughtForeCorrected + data.DraughtAftCorrected))
            {
                data.HoggingSaggingDirection = "Hogging";
            }
            else
            {
                data.HoggingSaggingDirection = "Sagging";
            }            
            return data;
        }

        // метод для подсчёта крена судна в градусах
        public static Survey Heel(Survey data)
        {
            data.Heel = Math.Round(Math.Atan(Math.Abs(data.DraughtMiddlePS - data.DraughtMiddleSS) / data.BM * (180 / Math.PI)), 3);
            return data;
        }

        // метод для подсчёта крена судна, направление
        public static Survey HeelDirection(Survey data)
        {
            if (data.DraughtMiddlePS < data.DraughtMiddleSS)
            {
                data.HeelDirection = "SS";
            }
            else
            {
                data.HeelDirection = "PS";
            }
            return data;
        }

        // общий метод для расчёта Mean Adjusted Draught,
        // в методе используются все методы для промежуточных рассчётов осадок и выдаётся результат
        public static Survey CalculationsOfMeanAdjustedDraught(Survey data) 
        {
            MeanDraughts(data);
            LBM(data);
            ApparentTrim(data);
            CorrectionsToPerpendiculars(data);
            CorrectedDraughts(data);
            CorrectedTrim(data);
            MeanAdjustedDraught(data);
            HoggingSagging(data);
            HoggingSaggingDirection(data);
            Heel(data);
            HeelDirection(data);
            return data;
        }
        
        // метод для подсчёта первой коррекции на дифферент
        public static Survey FirstTrimCorrection(Survey data)
        {
            data.FirstTrimCorrection = Math.Round((
                data.CorrectedTrim *
                data.LCF *
                data.TPC * 100 /
                data.LBP), 3);            
            return data;
        }

        // метод для подсчёта второй коррекции на дифферент
        public static Survey SecondTrimCorrection(Survey data)
        {
            data.SecondTrimCorrection = Math.Round((
                data.CorrectedTrim *
                data.CorrectedTrim *
                Math.Abs(data.MTCplus50cm - data.MTCminus50cm) * 50 /
                data.LBP), 3);            
            return data;
        }

        // метод для подсчёта водоизмещения откорректированного на дифферент
        public static Survey DisplacementCorrectedForTrim(Survey data)
        {
            data.DisplacementCorrectedForTrim = Math.Round((
                data.Displacement +
                data.FirstTrimCorrection +
                data.SecondTrimCorrection), 3);
            return data;
        }

        // метод для подсчёта водоизмещения откорректированного на плотность забортной воды
        public static Survey DisplacementCorrectedForDensity(Survey data)
        {
            data.DisplacementCorrectedForDensity = Math.Round((
                data.DisplacementCorrectedForTrim *
                data.SeaWaterDensity /
                1.025), 3);
            return data;
        }

        // метод для подсчёта Deductibles
        public static Survey TotalDeductibles(Survey data)
        {
            data.TotalDeductibles = Math.Round((
                data.BallastWater +
                data.FreshWater +
                data.FuelOil +
                data.DieselOil +
                data.LubricationOil +
                data.Others), 3);
            return data;
        }
        
        // метод для подсчёта Net Displacement
        public static Survey NetDisplacement(Survey data)
        {
            data.NetDisplacement = Math.Round((
                data.DisplacementCorrectedForDensity -
                data.TotalDeductibles), 3);
            return data;
        }

        // метод для подсчёта константы
        public static Survey Constant(Survey data)
        {
            data.Constant = Math.Round(data.NetDisplacement - data.LS, 3);
            return data;
        }

        // общий метод для расчёта Net Displacement,
        // в методе используются все методы для промежуточных рассчётов водоизмещения и выдаётся результат
        public static Survey CalculationsOfNetDisplacement(Survey data)
        {
            FirstTrimCorrection(data);
            SecondTrimCorrection(data);
            DisplacementCorrectedForTrim(data);
            DisplacementCorrectedForDensity(data);
            TotalDeductibles(data);
            NetDisplacement(data);
            Constant(data);

            return data;
        }

        // основной метод 
        // общий метод передаются исходные данные (объект класса Survey)
        // считаются и осадки и водоизмещение
        // на выходе все результаты
        public static Survey AllCalculationsOfClassSurvey(Survey data) 
        {
            CalculationsOfMeanAdjustedDraught(data);
            CalculationsOfNetDisplacement(data);
            return data;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        // методы для работы с объектами класса Results

        // метод для подсчёта количества груза по драфту
        // в объект Result входят два объекта Survey (initialSurvey + finalSurvey)
        public static Results CargoWeightByDraughtSurvey(Results data)
        {
            if (data.FinalSurvey.NetDisplacement > data.InitialSurvey.NetDisplacement)
            {
                data.CargoWeightByDS = Math.Round((data.FinalSurvey.NetDisplacement - data.InitialSurvey.NetDisplacement), 3);
            }
            else
            {
                data.CargoWeightByDS = Math.Round((data.InitialSurvey.NetDisplacement - data.FinalSurvey.NetDisplacement), 3);
            }

            return data;
        }

        // метод для подсчёта разницы полученного результата (количества груза по драфту) с коносаментом
        public static Results DifferenceWithBillOfLading(Results data)
        {
            data.Difference = (Math.Round((data.CargoWeightByBL - data.CargoWeightByDS), 3));
            if (data.CargoWeightByBL > data.CargoWeightByDS)
            {
                data.DifferenceMoreOrLess = "less";
            }
            else
            {
                data.DifferenceMoreOrLess = "more";
            }
            return data;
        }

        // метод для подсчёта разницы в %
        public static Results DifferencePercentageByBL(Results data)
        {
            data.DifferencePercentageByBL = Math.Round((100*data.Difference/data.CargoWeightByBL), 3);            
            return data;
        }

        // метод для подсчёта всех данных  
        // в объект Result входят два объекта Survey (initialSurvey + finalSurvey)
        public static Results AllCalculationsOfClassResults(Results data)
        {
            AllCalculationsOfClassSurvey(data.InitialSurvey);
            AllCalculationsOfClassSurvey(data.FinalSurvey);
            CargoWeightByDraughtSurvey(data);
            DifferenceWithBillOfLading(data);
            DifferencePercentageByBL(data);
            return data;
        }
    }
}
