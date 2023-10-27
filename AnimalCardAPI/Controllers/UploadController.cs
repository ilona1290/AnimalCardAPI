using AnimalCard.Application.DTOs;
using AnimalCard.Decryption;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalCardAPI.Controllers
{
    [Route("api/upload/{category}")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly string _azureConnectionString = "";

        public UploadController(IConfiguration configuration)
        {
            _azureConnectionString = ConnectionStrings.Azure;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(string category)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();

                BlobContainerClient container;
                if (file.Length > 0)
                {
                    container = new BlobContainerClient(_azureConnectionString, category);
                    var createResponse = await container.CreateIfNotExistsAsync();
                    if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                        await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    var blob = container.GetBlobClient(file.FileName);
                    await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                    using (var fileStream = file.OpenReadStream())
                    {
                        await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
                    }
                    
                    if(category == "researches" || category == "visitcards")
                    {
                        ResearchResponseDTO researchResponse = new ResearchResponseDTO();
                        researchResponse.Path = blob.Uri.ToString();
                        researchResponse.Name = blob.Name;
                        return Ok(researchResponse);
                    }

                    return Ok(blob.Uri.ToString());
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
