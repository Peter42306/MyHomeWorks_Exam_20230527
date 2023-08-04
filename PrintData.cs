using NLog;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DraughtSurvey20230728Intertfaces
{
    public class PrintData
    {
        // приватное статическое поле класса для логирования событий
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        // Метод для вывода данных начальной или финальной инспекции 
        public static void InitialOrFinal(Survey data)
        {
            Console.WriteLine();
            Console.WriteLine("".PadRight(90, '-'));
            Console.WriteLine($"Survey Data");
            Console.WriteLine();
            Console.WriteLine($"Vessel: {data.Name}");
            Console.WriteLine($"Date of survey: {data.DateOfSurvey.ToString("yyyyMMdd")}");
            Console.WriteLine($"SDWT: {data.SDWT:N3}, LBP: {data.LBP:N3}, BM: {data.BM:N3}, LS: {data.LS:N3}");
            Console.WriteLine("".PadRight(90, '-'));
            Console.WriteLine("{0,15}{1,15}{2,20}{3,15}{4,20}", "Apparent Draughts", "Mean Draught", "Distances to PP", "Corrections", "Corrected Draughts");
            Console.WriteLine($"{data.DraughtForePS,7:N2}{data.DraughtForeSS,6:N2}{data.DraughtForeMean,15:N3}{data.DistanceForePerpendicularToDraughts,18:N3}{data.DraughtForeCorrectionToPerpendicular,18:N3}{data.DraughtForeCorrected,15:N3}");
            Console.WriteLine($"{data.DraughtAftPS,7:N2}{data.DraughtAftSS,6:N2}{data.DraughtAftMean,15:N3}{data.DistanceAftPerpendicularToDraughts,18:N3}{data.DraughtAftCorrectionToPerpendicular,18:N3}{data.DraughtAftCorrected,15:N3}");
            Console.WriteLine($"{data.DraughtMiddlePS,7:N2}{data.DraughtMiddleSS,6:N2}{data.DraughtMiddleMean,15:N3}{data.DistanceMiddleToDraughts,18:N3}{data.DraughtMiddleCorrection,18:N3}{data.DraughtMiddleCorrected,15:N3}");
            Console.WriteLine("{0,20}{1,8:N3}{2,41}{3,10}", "Apparent Trim: ", data.ApparentTrim, "True Trim:", data.CorrectedTrim);
            Console.WriteLine("{0,20}{1,8:N3}{2,42}{3,9}", "LBM: ", data.LBM, "Mean Adjuested Draught: ", data.MeanAdjustedDraught);
            Console.WriteLine("".PadRight(90, '-'));
            Console.WriteLine("{0,27}{1,40}", "Hydrostatics: ", "Deductibles: ");
            Console.WriteLine("{0,27}{1,10:N3}{2,30}{3,10:N3}", "Displacement: ", data.Displacement, "Ballast Water: ", data.BallastWater);
            Console.WriteLine("{0,27}{1,10:N3}{2,30}{3,10:N3}", "LCF: ", data.LCF, "Fresh Water: ", data.FreshWater);
            Console.WriteLine("{0,27}{1,10:N3}{2,30}{3,10:N3}", "TPC: ", data.TPC, "Fuel Oil: ", data.FuelOil);
            Console.WriteLine("{0,27}{1,10:N3}{2,30}{3,10:N3}", "MTC +0.5 m: ", data.MTCplus50cm, "Diesel Oil: ", data.DieselOil);
            Console.WriteLine("{0,27}{1,10:N3}{2,30}{3,10:N3}", "MTC -0.5 m: ", data.MTCminus50cm, "Lub Oil: ", data.LubricationOil);
            Console.WriteLine("{0,27}{1,10:N4}{2,30}{3,10:N3}", "Sea Water Density: ", data.SeaWaterDensity, "Others: ", data.Others);
            Console.WriteLine("".PadRight(90, '-'));
            Console.WriteLine("{0,42}{1,11:N3}", "First trim correction: ", data.FirstTrimCorrection);
            Console.WriteLine("{0,42}{1,11:N3}", "Second trim correction: ", data.SecondTrimCorrection);
            Console.WriteLine("{0,42}{1,11:N3}", "Displacement corrected for trim: ", data.DisplacementCorrectedForTrim);
            Console.WriteLine("{0,27}{1,5:N3}{2,1}{3,11:N3}", "Displacement corrected for density ", data.SeaWaterDensity, ": ", data.DisplacementCorrectedForDensity);
            Console.WriteLine("{0,42}{1,11:N3}", "Total deductibles: ", data.TotalDeductibles);
            Console.WriteLine("{0,42}{1,11:N3}", "Net Displacement: ", data.NetDisplacement);
            Console.WriteLine($"");
            Console.WriteLine($"");
            Console.WriteLine($"");
            Console.WriteLine($"");
        }

        // Метод для вывода данных начальной и финальной инспекции 
        public static void InitialAndFinal(Results data)
        {
            Console.WriteLine();
            Console.WriteLine("".PadRight(90, '='));
            Console.WriteLine($"Survey Data");
            Console.WriteLine();
            Console.WriteLine($"File Reference: {data.FileReference}");
            Console.WriteLine($"Vessel: {data.InitialSurvey.Name}");
            Console.WriteLine("".PadRight(90, '-'));
            Console.WriteLine($"Date of initial survey: {data.InitialSurvey.DateOfSurvey.ToString("dd-MM-yyyy")}");
            Console.WriteLine($"SDWT: {data.InitialSurvey.SDWT:N3}, LBP: {data.InitialSurvey.LBP:N3}, BM: {data.InitialSurvey.BM:N3}, LS: {data.InitialSurvey.LS:N3}");
            Console.WriteLine("".PadRight(90, '-'));
            Console.WriteLine("{0,15}{1,15}{2,20}{3,15}{4,20}", "Apparent Draughts", "Mean Draught", "Distances to PP", "Corrections", "Corrected Draughts");
            Console.WriteLine($"{data.InitialSurvey.DraughtForePS,7:N2}{data.InitialSurvey.DraughtForeSS,6:N2}{data.InitialSurvey.DraughtForeMean,15:N3}{data.InitialSurvey.DistanceForePerpendicularToDraughts,18:N3}{data.InitialSurvey.DraughtForeCorrectionToPerpendicular,18:N3}{data.InitialSurvey.DraughtForeCorrected,15:N3}");
            Console.WriteLine($"{data.InitialSurvey.DraughtAftPS,7:N2}{data.InitialSurvey.DraughtAftSS,6:N2}{data.InitialSurvey.DraughtAftMean,15:N3}{data.InitialSurvey.DistanceAftPerpendicularToDraughts,18:N3}{data.InitialSurvey.DraughtAftCorrectionToPerpendicular,18:N3}{data.InitialSurvey.DraughtAftCorrected,15:N3}");
            Console.WriteLine($"{data.InitialSurvey.DraughtMiddlePS,7:N2}{data.InitialSurvey.DraughtMiddleSS,6:N2}{data.InitialSurvey.DraughtMiddleMean,15:N3}{data.InitialSurvey.DistanceMiddleToDraughts,18:N3}{data.InitialSurvey.DraughtMiddleCorrection,18:N3}{data.InitialSurvey.DraughtMiddleCorrected,15:N3}");
            Console.WriteLine($"");
            Console.WriteLine("{0,20}{1,8:N3}{2,40}{3,11}", "Apparent Trim: ", data.InitialSurvey.ApparentTrim, "True Trim:", data.InitialSurvey.CorrectedTrim);
            Console.WriteLine("{0,20}{1,8:N3}{2,41}{3,10}", "LBM: ", data.InitialSurvey.LBM, "Mean Adjuested Draught: ", data.InitialSurvey.MeanAdjustedDraught);
            Console.WriteLine("{0,16}{1,1:N3}{2,2}{3,8}{4,39}{5,0}{6,10}", "Heel to ", data.InitialSurvey.HeelDirection, ": ", data.InitialSurvey.Heel, data.InitialSurvey.HoggingSaggingDirection, ": ", data.InitialSurvey.HoggingSagging);
            Console.WriteLine("".PadRight(90, '-'));
            Console.WriteLine("{0,29}{1,40}", "Hydrostatics: ", "Deductibles: ");
            Console.WriteLine("{0,29}{1,10:N3}{2,30}{3,10:N3}", "Displacement: ", data.InitialSurvey.Displacement, "Ballast Water: ", data.InitialSurvey.BallastWater);
            Console.WriteLine("{0,29}{1,10:N3}{2,30}{3,10:N3}", "LCF: ", data.InitialSurvey.LCF, "Fresh Water: ", data.InitialSurvey.FreshWater);
            Console.WriteLine("{0,29}{1,10:N3}{2,30}{3,10:N3}", "TPC: ", data.InitialSurvey.TPC, "Fuel Oil: ", data.InitialSurvey.FuelOil);
            Console.WriteLine("{0,29}{1,10:N3}{2,30}{3,10:N3}", "MTC +0.5 m: ", data.InitialSurvey.MTCplus50cm, "Diesel Oil: ", data.InitialSurvey.DieselOil);
            Console.WriteLine("{0,29}{1,10:N3}{2,30}{3,10:N3}", "MTC -0.5 m: ", data.InitialSurvey.MTCminus50cm, "Lub Oil: ", data.InitialSurvey.LubricationOil);
            Console.WriteLine("{0,29}{1,10:N4}{2,30}{3,10:N3}", "Sea Water Density: ", data.InitialSurvey.SeaWaterDensity, "Others: ", data.InitialSurvey.Others);
            Console.WriteLine("".PadRight(90, '-'));
            Console.WriteLine("{0,69}{1,11:N3}", "First trim correction: ", data.InitialSurvey.FirstTrimCorrection);
            Console.WriteLine("{0,69}{1,11:N3}", "Second trim correction: ", data.InitialSurvey.SecondTrimCorrection);
            Console.WriteLine("{0,69}{1,11:N3}", "Displacement corrected for trim: ", data.InitialSurvey.DisplacementCorrectedForTrim);
            Console.WriteLine("{0,62}{1,5:N3}{2,1}{3,11:N3}", "Displacement corrected for density ", data.InitialSurvey.SeaWaterDensity, ": ", data.InitialSurvey.DisplacementCorrectedForDensity);
            Console.WriteLine("{0,69}{1,11:N3}", "Total deductibles: ", data.InitialSurvey.TotalDeductibles);
            Console.WriteLine("{0,69}{1,11:N3}", "Net Displacement: ", data.InitialSurvey.NetDisplacement);
            Console.WriteLine("".PadRight(90, '-'));

            Console.WriteLine($"Date of final survey: {data.FinalSurvey.DateOfSurvey.ToString("dd-MM-yyyy")}");
            Console.WriteLine($"SDWT: {data.FinalSurvey.SDWT:N3}, LBP: {data.FinalSurvey.LBP:N3}, BM: {data.FinalSurvey.BM:N3}, LS: {data.FinalSurvey.LS:N3}");
            Console.WriteLine("".PadRight(90, '-'));
            Console.WriteLine("{0,15}{1,15}{2,20}{3,15}{4,20}", "Apparent Draughts", "Mean Draught", "Distances to PP", "Corrections", "Corrected Draughts"); Console.WriteLine($"");
            Console.WriteLine($"{data.FinalSurvey.DraughtForePS,7:N2}{data.FinalSurvey.DraughtForeSS,6:N2}{data.FinalSurvey.DraughtForeMean,15:N3}{data.FinalSurvey.DistanceForePerpendicularToDraughts,18:N3}{data.FinalSurvey.DraughtForeCorrectionToPerpendicular,18:N3}{data.FinalSurvey.DraughtForeCorrected,15:N3}");
            Console.WriteLine($"{data.FinalSurvey.DraughtAftPS,7:N2}{data.FinalSurvey.DraughtAftSS,6:N2}{data.FinalSurvey.DraughtAftMean,15:N3}{data.FinalSurvey.DistanceAftPerpendicularToDraughts,18:N3}{data.FinalSurvey.DraughtAftCorrectionToPerpendicular,18:N3}{data.FinalSurvey.DraughtAftCorrected,15:N3}");
            Console.WriteLine($"{data.FinalSurvey.DraughtMiddlePS,7:N2}{data.FinalSurvey.DraughtMiddleSS,6:N2}{data.FinalSurvey.DraughtMiddleMean,15:N3}{data.FinalSurvey.DistanceMiddleToDraughts,18:N3}{data.FinalSurvey.DraughtMiddleCorrection,18:N3}{data.FinalSurvey.DraughtMiddleCorrected,15:N3}");
            Console.WriteLine($"");
            Console.WriteLine("{0,20}{1,8:N3}{2,40}{3,11}", "Apparent Trim: ", data.FinalSurvey.ApparentTrim, "True Trim:", data.FinalSurvey.CorrectedTrim);
            Console.WriteLine("{0,20}{1,8:N3}{2,41}{3,10}", "LBM: ", data.FinalSurvey.LBM, "Mean Adjuested Draught: ", data.FinalSurvey.MeanAdjustedDraught);
            Console.WriteLine("{0,16}{1,1:N3}{2,2}{3,8}{4,39}{5,0}{6,10}", "Heel to ", data.FinalSurvey.HeelDirection, ": ", data.FinalSurvey.Heel, data.FinalSurvey.HoggingSaggingDirection, ": ", data.FinalSurvey.HoggingSagging);
            Console.WriteLine("".PadRight(90, '-'));
            Console.WriteLine("{0,29}{1,40}", "Hydrostatics: ", "Deductibles: ");
            Console.WriteLine("{0,29}{1,10:N3}{2,30}{3,10:N3}", "Displacement: ", data.FinalSurvey.Displacement, "Ballast Water: ", data.FinalSurvey.BallastWater);
            Console.WriteLine("{0,29}{1,10:N3}{2,30}{3,10:N3}", "LCF: ", data.FinalSurvey.LCF, "Fresh Water: ", data.FinalSurvey.FreshWater);
            Console.WriteLine("{0,29}{1,10:N3}{2,30}{3,10:N3}", "TPC: ", data.FinalSurvey.TPC, "Fuel Oil: ", data.FinalSurvey.FuelOil);
            Console.WriteLine("{0,29}{1,10:N3}{2,30}{3,10:N3}", "MTC +0.5 m: ", data.FinalSurvey.MTCplus50cm, "Diesel Oil: ", data.FinalSurvey.DieselOil);
            Console.WriteLine("{0,29}{1,10:N3}{2,30}{3,10:N3}", "MTC -0.5 m: ", data.FinalSurvey.MTCminus50cm, "Lub Oil: ", data.FinalSurvey.LubricationOil);
            Console.WriteLine("{0,29}{1,10:N4}{2,30}{3,10:N3}", "Sea Water Density: ", data.FinalSurvey.SeaWaterDensity, "Others: ", data.FinalSurvey.Others);
            Console.WriteLine("".PadRight(90, '-'));
            Console.WriteLine("{0,69}{1,10:N3}", "First trim correction: ", data.FinalSurvey.FirstTrimCorrection);
            Console.WriteLine("{0,69}{1,10:N3}", "Second trim correction: ", data.FinalSurvey.SecondTrimCorrection);
            Console.WriteLine("{0,69}{1,10:N3}", "Displacement corrected for trim: ", data.FinalSurvey.DisplacementCorrectedForTrim);
            Console.WriteLine("{0,62}{1,5:N3}{2,1}{3,10:N3}", "Displacement corrected for density ", data.FinalSurvey.SeaWaterDensity, ": ", data.FinalSurvey.DisplacementCorrectedForDensity);
            Console.WriteLine("{0,69}{1,10:N3}", "Total deductibles: ", data.FinalSurvey.TotalDeductibles);
            Console.WriteLine("{0,69}{1,10:N3}", "Net Displacement: ", data.FinalSurvey.NetDisplacement);
            Console.WriteLine("".PadRight(90, '-'));
            Console.WriteLine("{0,69}{1,10:N3}", "Cargo as per Draught Survey: ", data.CargoWeightByDS);
            Console.WriteLine("{0,69}{1,10:N3}{2,1}", "Difference: ", data.Difference, " less");
            Console.WriteLine("{0,79:N3}{1,1}", data.DifferencePercentageByBL, " %");
            Console.WriteLine("".PadRight(90, '='));
            Console.WriteLine($"");
        }

        // Метод для вывода краткой сводки (удобнее ипсользовать для вывода одного объекта класса Results)
        public static void Short(Results data)
        {
            Console.WriteLine("{0,5}{1,18}{2,20}{3,17}{4,17}{5,21}{6,18}{7,14}{8,8}", "No.", "File Reference", "Vessel", "Initial Date", "Final Date", "Bill of Lading", "Draught Survey", "Difference", "%");
            Console.WriteLine("{0,5}{1,18}{2,20}{3,17}{4,17}{5,21}{6,18}{7,14}{8,8}", data.Index, data.FileReference, data.InitialSurvey.Name, data.InitialSurvey.DateOfSurvey.ToString("dd-MM-yyyy"), data.FinalSurvey.DateOfSurvey.ToString("dd-MM-yyyy"), data.CargoWeightByBL, data.CargoWeightByDS, data.Difference, data.DifferencePercentageByBL);
        }

        // Метод для вывода краткой сводки (удобнее использовать для вывода списка объектов класса Results)
        public static void ShortForList(Results data)
        {
            //Console.WriteLine("{0,5}{1,18}{2,20}{3,17}{4,17}{5,21}{6,18}{7,14}{8,8}", "No.", "File Reference", "Vessel", "Initial Date", "Final Date", "Bill of Lading", "Draught Survey", "Difference", "%");
            Console.WriteLine("{0,5}{1,18}{2,20}{3,17}{4,17}{5,21}{6,18}{7,14}{8,8}", data.Index, data.FileReference, data.InitialSurvey.Name, data.InitialSurvey.DateOfSurvey.ToString("dd-MM-yyyy"), data.FinalSurvey.DateOfSurvey.ToString("dd-MM-yyyy"), data.CargoWeightByBL, data.CargoWeightByDS, data.Difference, data.DifferencePercentageByBL);
        }

        // Метод для вывода списка объектов Results 
        public static void ListOfListResults(List<Results> listResults)
        {
            Console.WriteLine("{0,5}{1,18}{2,20}{3,17}{4,17}{5,21}{6,18}{7,14}{8,8}", "No.", "File Reference", "Vessel", "Initial Date", "Final Date", "Bill of Lading", "Draught Survey", "Difference", "%");
            int count = 0;
            foreach (var item in listResults)
            {
                count++;
                
                Console.WriteLine("{0,5}{1,18}{2,20}{3,17}{4,17}{5,21}{6,18}{7,14}{8,8}", count, item.FileReference, item.InitialSurvey.Name, item.InitialSurvey.DateOfSurvey.ToString("dd-MM-yyyy"), item.FinalSurvey.DateOfSurvey.ToString("dd-MM-yyyy"), item.CargoWeightByBL, item.CargoWeightByDS, item.Difference, item.DifferencePercentageByBL);
            }
        }
        // Метод для создания и вывода данных инспекции в файл Excel прикаждом вызовы будет создаваться новый файл Excel
        public static void ToExcel(string templateExcelFilePath, Results data)
        {
            if (!File.Exists(templateExcelFilePath))
            {
                Console.WriteLine("Template file for Excel document was not found");
                logger.Warn("Template file for Excel document was not found");
                return;
            }
            
            var outputFilePath=Path.Combine(Path.GetDirectoryName(templateExcelFilePath), $"Draught Survey {data.InitialSurvey.Name} {DateTime.Now.ToString("yyyy MM dd HH mm")}.xlsx");
            File.Copy(templateExcelFilePath, outputFilePath, true);

            using (var package = new ExcelPackage(new FileInfo(outputFilePath)))
            {
                if(package.Workbook!=null)
                {
                    var worksheet = package.Workbook.Worksheets["Sheet1"];
                    worksheet.Cells["B3"].Value = data.InitialSurvey.Name;
                    worksheet.Cells["F4"].Value = data.CargoWeightByBL;

                    worksheet.Cells["I7"].Value = data.InitialSurvey.LS;
                    worksheet.Cells["I8"].Value = data.InitialSurvey.LBP;
                    worksheet.Cells["I9"].Value = data.InitialSurvey.BM;

                    worksheet.Cells["C7"].Value = data.InitialSurvey.DraughtAftPS;
                    worksheet.Cells["C8"].Value = data.InitialSurvey.DraughtAftSS;
                    worksheet.Cells["C9"].Value = data.InitialSurvey.DraughtAftMean;
                    worksheet.Cells["C10"].Value = data.InitialSurvey.DraughtAftCorrectionToPerpendicular;
                    worksheet.Cells["C11"].Value = data.InitialSurvey.DraughtAftCorrected;

                    worksheet.Cells["E7"].Value = data.InitialSurvey.DraughtForePS;
                    worksheet.Cells["E8"].Value = data.InitialSurvey.DraughtForeSS;
                    worksheet.Cells["E9"].Value = data.InitialSurvey.DraughtForeMean;
                    worksheet.Cells["E10"].Value = data.InitialSurvey.DraughtForeCorrectionToPerpendicular;
                    worksheet.Cells["E11"].Value = data.InitialSurvey.DraughtForeCorrected;

                    worksheet.Cells["D7"].Value = data.InitialSurvey.DraughtMiddlePS;
                    worksheet.Cells["D8"].Value = data.InitialSurvey.DraughtMiddleSS;
                    worksheet.Cells["D9"].Value = data.InitialSurvey.DraughtMiddleMean;
                    worksheet.Cells["D10"].Value = data.InitialSurvey.DraughtMiddleCorrection;
                    worksheet.Cells["D11"].Value = data.InitialSurvey.DraughtMiddleCorrected;

                    worksheet.Cells["D12"].Value = data.InitialSurvey.MeanAdjustedDraught;
                    worksheet.Cells["D13"].Value = data.InitialSurvey.Displacement;
                    worksheet.Cells["D14"].Value = data.InitialSurvey.FirstTrimCorrection;
                    worksheet.Cells["D15"].Value = data.InitialSurvey.SecondTrimCorrection;
                    worksheet.Cells["D16"].Value = data.InitialSurvey.DisplacementCorrectedForTrim;
                    worksheet.Cells["C17"].Value = data.InitialSurvey.SeaWaterDensity;
                    worksheet.Cells["D18"].Value = data.InitialSurvey.DisplacementCorrectedForDensity;
                    worksheet.Cells["D19"].Value = data.InitialSurvey.TotalDeductibles;
                    worksheet.Cells["D20"].Value = data.InitialSurvey.NetDisplacement;                    

                    worksheet.Cells["I10"].Value = data.InitialSurvey.TPC;
                    worksheet.Cells["I11"].Value = data.InitialSurvey.LCF;
                    worksheet.Cells["I12"].Value = data.InitialSurvey.CorrectedTrim;
                    worksheet.Cells["I13"].Value = data.InitialSurvey.MTCplus50cm;
                    worksheet.Cells["I14"].Value = data.InitialSurvey.MTCminus50cm;
                    worksheet.Cells["I15"].Value = data.InitialSurvey.MTCplus50cm - data.InitialSurvey.MTCminus50cm;
                    worksheet.Cells["I16"].Value = data.InitialSurvey.FuelOil;
                    worksheet.Cells["I17"].Value = data.InitialSurvey.FreshWater;
                    worksheet.Cells["I18"].Value = data.InitialSurvey.BallastWater;
                    worksheet.Cells["I19"].Value = data.InitialSurvey.Others;
                    worksheet.Cells["I20"].Value = data.InitialSurvey.TotalDeductibles;

                    worksheet.Cells["C23"].Value = data.FinalSurvey.DraughtAftPS;
                    worksheet.Cells["C24"].Value = data.FinalSurvey.DraughtAftSS;
                    worksheet.Cells["C25"].Value = data.FinalSurvey.DraughtAftMean;
                    worksheet.Cells["C26"].Value = data.FinalSurvey.DraughtAftCorrectionToPerpendicular;
                    worksheet.Cells["C27"].Value = data.FinalSurvey.DraughtAftCorrected;

                    worksheet.Cells["E23"].Value = data.FinalSurvey.DraughtForePS;
                    worksheet.Cells["E24"].Value = data.FinalSurvey.DraughtForeSS;
                    worksheet.Cells["E25"].Value = data.FinalSurvey.DraughtForeMean;
                    worksheet.Cells["E26"].Value = data.FinalSurvey.DraughtForeCorrectionToPerpendicular;
                    worksheet.Cells["E27"].Value = data.FinalSurvey.DraughtForeCorrected;

                    worksheet.Cells["D23"].Value = data.FinalSurvey.DraughtMiddlePS;
                    worksheet.Cells["D24"].Value = data.FinalSurvey.DraughtMiddleSS;
                    worksheet.Cells["D25"].Value = data.FinalSurvey.DraughtMiddleMean;
                    worksheet.Cells["D26"].Value = data.FinalSurvey.DraughtMiddleCorrection;
                    worksheet.Cells["D27"].Value = data.FinalSurvey.DraughtMiddleCorrected;

                    worksheet.Cells["D28"].Value = data.FinalSurvey.MeanAdjustedDraught;
                    worksheet.Cells["D29"].Value = data.FinalSurvey.Displacement;
                    worksheet.Cells["D30"].Value = data.FinalSurvey.FirstTrimCorrection;
                    worksheet.Cells["D31"].Value = data.FinalSurvey.SecondTrimCorrection;
                    worksheet.Cells["D32"].Value = data.FinalSurvey.DisplacementCorrectedForTrim;
                    worksheet.Cells["D33"].Value = data.FinalSurvey.SeaWaterDensity;
                    worksheet.Cells["D34"].Value = data.FinalSurvey.DisplacementCorrectedForDensity;
                    worksheet.Cells["D35"].Value = data.FinalSurvey.TotalDeductibles;
                    worksheet.Cells["D36"].Value = data.FinalSurvey.NetDisplacement;

                    worksheet.Cells["I23"].Value = data.FinalSurvey.TPC;
                    worksheet.Cells["I24"].Value = data.FinalSurvey.LCF;
                    worksheet.Cells["I25"].Value = data.FinalSurvey.CorrectedTrim;
                    worksheet.Cells["I26"].Value = data.FinalSurvey.MTCplus50cm;
                    worksheet.Cells["I27"].Value = data.FinalSurvey.MTCminus50cm;
                    worksheet.Cells["I28"].Value = data.FinalSurvey.MTCplus50cm - data.FinalSurvey.MTCminus50cm;
                    worksheet.Cells["I29"].Value = data.FinalSurvey.FuelOil;
                    worksheet.Cells["I30"].Value = data.FinalSurvey.FreshWater;
                    worksheet.Cells["I31"].Value = data.FinalSurvey.BallastWater;
                    worksheet.Cells["I32"].Value = data.FinalSurvey.Others;
                    worksheet.Cells["I33"].Value = data.FinalSurvey.TotalDeductibles;

                    worksheet.Cells["D37"].Value = data.CargoWeightByDS;

                    worksheet.Cells["C39"].Value = data.InitialSurvey.DistanceAftPerpendicularToDraughts;
                    worksheet.Cells["C40"].Value = data.InitialSurvey.DistanceMiddleToDraughts;
                    worksheet.Cells["C41"].Value = data.InitialSurvey.DistanceForePerpendicularToDraughts;
                    worksheet.Cells["D39"].Value = data.FinalSurvey.DistanceAftPerpendicularToDraughts;
                    worksheet.Cells["D40"].Value = data.FinalSurvey.DistanceMiddleToDraughts;
                    worksheet.Cells["D41"].Value = data.FinalSurvey.DistanceForePerpendicularToDraughts;

                    package.Save();
                    Console.WriteLine($"All data were printed to Excel file Draught Survey {data.InitialSurvey.Name} {DateTime.Now.ToString("yyyy MM dd HH mm")}.xlsx");
                    logger.Info($"All data were printed to Excel file Draught Survey {data.InitialSurvey.Name} {DateTime.Now.ToString("yyyy MM dd HH mm")}.xlsx");
                }
                else
                {
                    Console.WriteLine("Error!!! Failed to open Excel document");
                    logger.Warn("Error!!! Failed to open Excel document");
                    return;
                }
            }
        }
    }
}
