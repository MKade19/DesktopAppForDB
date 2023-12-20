using BusStation.Common.Models;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;

namespace BusStation.UI.Util
{
    public static class ReportsStore
    {
        public static void ModelsWithDistance(List<BusModelWithDistance> busModelsWithDistance)
        {
            //создаем новый документ Word
            var application = new Word.Application();
            Word.Document document = application.Documents.Add();
            //после создания экземпляра Word в приложение добавляеется новый документ 
            //создаем параграф для названия страницы
            Word.Paragraph passengerParagraph = document.Paragraphs.Add();
            Word.Range passengerRange = passengerParagraph.Range;
            passengerRange.Text = "Общий пробег автобусов модели";
            passengerRange.InsertParagraphAfter();
            passengerRange.Bold = 1;
            passengerRange.Font.Size = 20;
            passengerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            Word.Range passengerRange2 = passengerParagraph.Range;
            passengerRange2.Font.Size = 14;
            passengerRange2.ParagraphFormat.LineUnitBefore = 0.6F;
            passengerRange2.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            passengerRange2.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
            passengerRange2.InsertParagraphAfter();
            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;


            Word.Table paymentsTable = document.Tables.Add(tableRange, busModelsWithDistance.Count + 1, 3);
            paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle
                = Word.WdLineStyle.wdLineStyleSingle;
            paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            /**После создания параграфа для таблицы и получения его Range, 
             * добавляется таблица с указанием числа строк (по количеству 
             * категорий + 1) и столбцов. Последние две строчки касаются 
             * указания границ (внутренних и внешних) и выравнивания ячеек 
             * (по центру и по вертикали).
             */

            //добавляем названия колонок их форматирование
            Word.Range cellRange;
            cellRange = paymentsTable.Cell(1, 1).Range;
            cellRange.Text = "Модель";
            cellRange = paymentsTable.Cell(1, 2).Range;
            cellRange.Text = "Производитель";
            cellRange = paymentsTable.Cell(1, 3).Range;
            cellRange.Text = "Пробег";
            paymentsTable.Rows[1].Range.Bold = 1;
            paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //заполняем колонки таблицы
            for (int i = 0; i < busModelsWithDistance.Count; i++)
            {
                cellRange = paymentsTable.Cell(i + 2, 1).Range;
                cellRange.Text = busModelsWithDistance[i].Title.ToString();

                cellRange = paymentsTable.Cell(i + 2, 2).Range;
                cellRange.Text = busModelsWithDistance[i].ProducerName?.ToString();

                cellRange = paymentsTable.Cell(i + 2, 3).Range;
                cellRange.Text = busModelsWithDistance[i].TotalDistance.ToString();
            }

            application.Visible = true;
            document.SaveAs2(@"D:\Reports\ModelsWithDistanceReport.docx");
        }
        public static void TechnicalInspectionsByYearAndAllowance(List<TechnicalInspection> technicalInspections, int year, bool isAllowed)
        {
            //создаем новый документ Word
            var application = new Word.Application();
            Word.Document document = application.Documents.Add();
            //после создания экземпляра Word в приложение добавляеется новый документ 
            //создаем параграф для названия страницы
            Word.Paragraph passengerParagraph = document.Paragraphs.Add();
            Word.Range passengerRange = passengerParagraph.Range;
            string allowance = isAllowed ? "допуск" : "недопуск";
            passengerRange.Text = $"Техосмотр за {year} год ({allowance})";
            passengerRange.InsertParagraphAfter();
            passengerRange.Bold = 1;
            passengerRange.Font.Size = 20;
            passengerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            Word.Range passengerRange2 = passengerParagraph.Range;
            passengerRange2.Font.Size = 14;
            passengerRange2.ParagraphFormat.LineUnitBefore = 0.6F;
            passengerRange2.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            passengerRange2.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
            passengerRange2.InsertParagraphAfter();
            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;


            Word.Table paymentsTable = document.Tables.Add(tableRange, technicalInspections.Count + 1, 4);
            paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle
                = Word.WdLineStyle.wdLineStyleSingle;
            paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            /**После создания параграфа для таблицы и получения его Range, 
             * добавляется таблица с указанием числа строк (по количеству 
             * категорий + 1) и столбцов. Последние две строчки касаются 
             * указания границ (внутренних и внешних) и выравнивания ячеек 
             * (по центру и по вертикали).
             */

            //добавляем названия колонок их форматирование
            Word.Range cellRange;
            cellRange = paymentsTable.Cell(1, 1).Range;
            cellRange.Text = "Дата осмотра";
            cellRange = paymentsTable.Cell(1, 2).Range;
            cellRange.Text = "Автобус";
            cellRange = paymentsTable.Cell(1, 3).Range;
            cellRange.Text = "Допуск";
            cellRange = paymentsTable.Cell(1, 4).Range;
            cellRange.Text = "Прчина отказа";
            paymentsTable.Rows[1].Range.Bold = 1;
            paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //заполняем колонки таблицы
            for (int i = 0; i < technicalInspections.Count; i++)
            {
                cellRange = paymentsTable.Cell(i + 2, 1).Range;
                cellRange.Text = technicalInspections[i].InspectionDate.ToString("MM/dd/yyyy");

                cellRange = paymentsTable.Cell(i + 2, 2).Range;
                cellRange.Text = technicalInspections[i].BusNumber?.ToString();

                cellRange = paymentsTable.Cell(i + 2, 3).Range;
                cellRange.Text = technicalInspections[i].IsAllowed.ToString();

                cellRange = paymentsTable.Cell(i + 2, 4).Range;
                cellRange.Text = technicalInspections[i].DenialReason?.ToString();
            }

            application.Visible = true;
            document.SaveAs2(@"D:\Reports\TexhnicalInspectionByYearAndAllowance.docx");
        }

        public static void VoyagesInfo(List<Voyage> voyages)
        {
            //создаем новый документ Word
            var application = new Word.Application();
            Word.Document document = application.Documents.Add();
            //после создания экземпляра Word в приложение добавляеется новый документ 
            //создаем параграф для названия страницы
            Word.Paragraph passengerParagraph = document.Paragraphs.Add();
            Word.Range passengerRange = passengerParagraph.Range;
            passengerRange.Text = "Информация о рейсах";
            passengerRange.InsertParagraphAfter();
            passengerRange.Bold = 1;
            passengerRange.Font.Size = 20;
            passengerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            Word.Range passengerRange2 = passengerParagraph.Range;
            passengerRange2.Text = $"Всего рейсов {voyages.Count}";
            passengerRange2.Font.Size = 14;
            passengerRange2.ParagraphFormat.LineUnitBefore = 0.6F;
            passengerRange2.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            passengerRange2.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
            passengerRange2.InsertParagraphAfter();
            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;


            Word.Table paymentsTable = document.Tables.Add(tableRange, voyages.Count + 1, 6);
            paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle
                = Word.WdLineStyle.wdLineStyleSingle;
            paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            /**После создания параграфа для таблицы и получения его Range, 
             * добавляется таблица с указанием числа строк (по количеству 
             * категорий + 1) и столбцов. Последние две строчки касаются 
             * указания границ (внутренних и внешних) и выравнивания ячеек 
             * (по центру и по вертикали).
             */

            //добавляем названия колонок их форматирование
            Word.Range cellRange;
            cellRange = paymentsTable.Cell(1, 1).Range;
            cellRange.Text = "Дата осмотра";
            cellRange = paymentsTable.Cell(1, 2).Range;
            cellRange.Text = "Время отправления";
            cellRange = paymentsTable.Cell(1, 3).Range;
            cellRange.Text = "Время прибытия";
            cellRange = paymentsTable.Cell(1, 4).Range;
            cellRange.Text = "Маршрут";
            cellRange = paymentsTable.Cell(1, 5).Range;
            cellRange.Text = "Сотрудник";
            cellRange = paymentsTable.Cell(1, 6).Range;
            cellRange.Text = "Автобус";
            paymentsTable.Rows[1].Range.Bold = 1;
            paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //заполняем колонки таблицы
            for (int i = 0; i < voyages.Count; i++)
            {
                cellRange = paymentsTable.Cell(i + 2, 1).Range;
                cellRange.Text = voyages[i].VoyageDate.ToString("MM/dd/yyyy");

                cellRange = paymentsTable.Cell(i + 2, 2).Range;
                cellRange.Text = voyages[i].DepartureTime.ToString("HH:mm");

                cellRange = paymentsTable.Cell(i + 2, 3).Range;
                cellRange.Text = voyages[i].ArrivalTime.ToString("HH:mm");

                cellRange = paymentsTable.Cell(i + 2, 4).Range;
                cellRange.Text = voyages[i].BusRouteNumber?.ToString();

                cellRange = paymentsTable.Cell(i + 2, 5).Range;
                cellRange.Text = voyages[i].WorkerName?.ToString();

                cellRange = paymentsTable.Cell(i + 2, 6).Range;
                cellRange.Text = voyages[i].BusNumber?.ToString();
            }

            application.Visible = true;
            document.SaveAs2(@"D:\Reports\VoyagesSummary.docx");
        }
    }
}
