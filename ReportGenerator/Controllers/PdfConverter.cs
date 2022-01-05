
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
    public FileContentResult ConvertFromHtml()
    {
      string htmlInput = Request.Form["htmlInput"]; //read input string
      string fileNameWithoutExtension = Request.Form["fileNameWithoutExtension"]; //read file name
      string orientation = Request.Form["orientation"]; //read orientation
      bool pageNumbering = Convert.ToBoolean(Request.Form["pageNumbering"]); //read numbering toggle

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
