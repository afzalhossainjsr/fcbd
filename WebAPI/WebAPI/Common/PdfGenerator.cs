using DinkToPdf;
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
        
    }
}
