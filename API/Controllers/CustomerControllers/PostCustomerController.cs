using API.Contract.Customer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerControllers;

public partial class CustomerController : ControllerBase
{
      [HttpPost]
      public async Task<ActionResult<Guid>> AddCustomer([FromBody] CustomerRequest request, CancellationToken cancellationToken)
      {
            try
            {
                  var id = await _customerService.AddCustomer(request, cancellationToken);
                  return Ok(id);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Add Customer " + e);
            }
      }

      [HttpPost("{id:Guid}/pdf")]
      public async Task<ActionResult> UploadCustomerPdf([FromRoute] Guid id, IFormFile pdfFile, CancellationToken cancellationToken)
      {
            try
            {
                  if (pdfFile == null || pdfFile.Length == 0)
                        return BadRequest("PDF file is required.");

                  if (!pdfFile.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
                        return BadRequest("Only PDF files are allowed.");

                  byte[] pdfData;
                  using (var memoryStream = new MemoryStream())
                  {
                        await pdfFile.CopyToAsync(memoryStream, cancellationToken);
                        pdfData = memoryStream.ToArray();
                  }

                  await _customerService.UploadCustomerPdf(id, pdfData, pdfFile.FileName, cancellationToken);
                  return Ok("PDF uploaded successfully.");
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Upload Customer PDF " + e);
            }
      }
}

