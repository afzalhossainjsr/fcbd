﻿using DinkToPdf;
using DinkToPdf.Contracts;

namespace WebAPI.Common
{
    public static class PdfGenerator
    {
        private static readonly IConverter _converter = new SynchronizedConverter(new PdfTools());

        public static byte[] GeneratePdfA4(string htmlContent) 
        {
            var globalSettings = new GlobalSettings
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait,
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };

            return _converter.Convert(pdf);
        }
        public static byte[] GeneratePdfCustom(string htmlContent)
        {
            var customWidthInMillimeters = "101mm"; // 4 inches in millimeters
            var customHeightInMillimeters = "300mm"; // Set height to 0 for automatic calculation based on content

            var globalSettings = new GlobalSettings
            {
                PaperSize = new PechkinPaperSize(customWidthInMillimeters, customHeightInMillimeters),
                Orientation = Orientation.Portrait,
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };

            return _converter.Convert(pdf);
        }


    }
}
