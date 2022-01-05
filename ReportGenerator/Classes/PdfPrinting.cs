using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WkHtmlToPdfDotNet;

namespace ReportGenerator
{
  class PdfPrinting
  {
    public static byte[] PdfPrint(string printString, string orientation = "", bool pageCount = true)
    {
      var doc = new HtmlToPdfDocument()
      {
        GlobalSettings = {
        ColorMode = ColorMode.Color,
        Orientation = Orientation.Landscape,
        PaperSize = PaperKind.A4Plus,
        },
        Objects = {
          new ObjectSettings() {
              PagesCount = pageCount,
              HtmlContent = printString,
              WebSettings = { DefaultEncoding = "utf-8" },
              HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
          }
        }
      };
      if (orientation == "landscape")
      {
        doc.GlobalSettings.Orientation = Orientation.Landscape;
      }
      else
      {
        doc.GlobalSettings.Orientation = Orientation.Portrait;
      }

      var pdfByte = PdfPrinterHelper.Instance.Convert(doc);
      return pdfByte;
    }
  }

  //using lazy init to generate single instance of PDF Converter across the application life cycle
  public class PdfPrinterHelper
  {
    private static Lazy<SynchronizedConverter> lazyConverter;

    static PdfPrinterHelper()
    {
      lazyConverter = new Lazy<SynchronizedConverter>(() =>
      {
        return new SynchronizedConverter(new PdfTools());
      });
    }

    public static SynchronizedConverter Instance
    {
      get
      {
        return lazyConverter.Value;
      }
    }
  }
}
