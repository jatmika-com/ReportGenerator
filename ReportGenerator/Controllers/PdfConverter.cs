
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReportGenerator
{
  [AllowAnonymous]
  [Route("api/[controller]/[action]")]
  public class PdfConverterController : ControllerBase
  {
    [HttpPost]
    public FileContentResult ConvertFromHtml([FromForm] string htmlInput, [FromForm] string fileNameWithoutExtension, [FromForm] string orientation = "", [FromForm] bool pageNumbering = true)
    {
      byte[] pdfBytes = null;

      try
      {
        pdfBytes = PdfPrinting.PdfPrint(htmlInput, orientation, pageNumbering);
      }
      catch (Exception e)
      {
        Console.WriteLine("Error converting to PDF", e.Message + e.StackTrace);
      }
      return new FileContentResult(pdfBytes, "application/pdf")
      {
        FileDownloadName = fileNameWithoutExtension + ".pdf"
      };
    }
  }
}
