using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using SignalRDemo3ytEFC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRDemo3ytEFC.Reports
{
    public class ProductReport
    {
        IWebHostEnvironment _oHostEnvironment;

        #region Declaration
        int _maxColumn = 3;
        Document _document;
        PdfPTable _pdfPTable = new PdfPTable(3);
        PdfPCell _pdfPCell;
        Font _fontStyle;
        MemoryStream _memoryStream = new MemoryStream();
        List<Student> _students = new List<Student>();
        #endregion

        public ProductReport(IWebHostEnvironment oHostEnvironment)
        {
            _oHostEnvironment = oHostEnvironment;
        }

        public byte[] Report(List<Student> students)
        {
            _students = students;

            _document = new Document(PageSize.A4, 10f, 10f, 20f, 30f);
            _pdfPTable.WidthPercentage = 100;
            _pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();

            float[] sizes = new float[_maxColumn];
            for (int i = 0; i < _maxColumn; i++)
            {
                if (i == 0) sizes[i] = 50;
                else sizes[i] = 100;
            }

            _pdfPTable.SetWidths(sizes);

            this.ReportHeader();
            this.ReporBody();

            _pdfPTable.HeaderRows = 2;
            _document.Add(_pdfPTable);
            _document.Close();

            return _memoryStream.ToArray();
        }

        private void ReportHeader()
        {
            _pdfPCell = new PdfPCell(this.AddLogo());
            _pdfPCell.Colspan = 1;
            _pdfPCell.Border = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(this.SetPageTitle());
            _pdfPCell.Colspan = _maxColumn - 1;
            _pdfPCell.Border = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPTable.CompleteRow();
        }

        private PdfPTable AddLogo()
        {
            int maxColumn = 1;
            PdfPTable pdfPTable = new PdfPTable(maxColumn);

            string path = _oHostEnvironment.WebRootPath + "/Images";
            string imgCombine = Path.Combine(path, "ThumbIKR_Logo.png");
            Image img = Image.GetInstance(imgCombine);

            _pdfPCell = new PdfPCell(img);
            _pdfPCell.Colspan = maxColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfPCell);

            pdfPTable.CompleteRow();

            return pdfPTable;
        }
        private PdfPTable SetPageTitle()
        {
            int maxColumn = 2;
            PdfPTable pdfPTable = new PdfPTable(maxColumn);

            _fontStyle = FontFactory.GetFont("Tahoma", 18f, 1);

            _pdfPCell = new PdfPCell(new Phrase("School Name", _fontStyle));
            _pdfPCell.Colspan = maxColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfPCell);

            pdfPTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 14f, 1);

            _pdfPCell = new PdfPCell(new Phrase("School Informations", _fontStyle));
            _pdfPCell.Colspan = maxColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfPCell);

            pdfPTable.CompleteRow();

            return pdfPTable;
        }

        private void ReporBody()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 9f, 1);
            var fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);

            #region Table Header
            _pdfPCell = new PdfPCell(new Phrase("SL", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.Gray;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Name", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.Gray;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Roll", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.Gray;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPTable.CompleteRow();
            #endregion

            #region Table Body
            int nSL = 1;
            foreach (var student in _students)
            {
                _pdfPCell = new PdfPCell(new Phrase(nSL++.ToString(), _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.White;
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(student.Name, _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.White;
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(student.Roll, _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.White;
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPTable.CompleteRow();
            }

            #endregion
        }
    }
}