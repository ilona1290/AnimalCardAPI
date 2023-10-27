using AnimalCard.Application.Visit.Command.CompleteVisit;
using AnimalCard.Decryption;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using iText.IO.Font;
using iText.IO.Source;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using AnimalCard.Application.Owner.Queries.GetOwner;
using AnimalCard.Application.Pet.Queries.GetPetsByUserRole;
using Azure;
using iText.IO.Image;
using AnimalCard.Application.DTOs;
using System.Reflection.Metadata;

namespace AnimalCard.Application.Helpers
{
    public static class GenerateVisitCard
    {
        public static ResearchResponseDTO GeneratePdf(CompleteVisitCommand completeVisit)
        {
            string message = "";
            try
            {
                const string PROCEDURE_NAME = "[dbo].[GetInfoToGeneratedCard]";
                GetInfoToGeneratedCard info = new GetInfoToGeneratedCard();
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
                {
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        sqlCommand.Parameters.Add("@vetId", SqlDbType.NVarChar).Value = completeVisit.VetId;
                        sqlCommand.Parameters.Add("@petId", SqlDbType.NVarChar).Value = completeVisit.PetId;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                info.Vet = sqlDataReader.GetString("Vet");
                            }

                            if (sqlDataReader.NextResult())
                            {
                                while (sqlDataReader.Read())
                                {
                                    info.Pet = sqlDataReader.GetString("Pet");
                                    info.Owner = sqlDataReader.GetString("Owner");
                                }
                            }
                        }
                    }
                    sqlConnection.Close();

                }

                MemoryStream stream = new MemoryStream();
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdfDocument = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdfDocument);
                pdfDocument.SetDefaultPageSize(iText.Kernel.Geom.PageSize.A4.Rotate());

                BlobContainerClient containerFonts = new BlobContainerClient(ConnectionStrings.Azure, "fonts");
                BlobClient blobClientFont = containerFonts.GetBlobClient("Bitter-Regular.otf");
                Response<BlobDownloadInfo> response = blobClientFont.Download();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    response.Value.Content.CopyTo(memoryStream);

                    PdfFont font = PdfFontFactory.CreateFont(memoryStream.ToArray(), PdfEncodings.IDENTITY_H);

                    document.SetFont(font);
                }

                string imageUrl = "https://animalcard.blob.core.windows.net/helperimages/logo.png";

                iText.Layout.Element.Image image = new iText.Layout.Element.Image(ImageDataFactory.Create(imageUrl));
                document.Add(image);


                document.Add(new Paragraph(String.Format("Weterynarz: {0}", info.Vet)));
                document.Add(new Paragraph(String.Format("Właściciel: {0}", info.Owner)));
                document.Add(new Paragraph(String.Format("Pacjent: {0}", info.Pet)));
                document.Add(new Paragraph(String.Format("Data i godzina wizyty: {0}", GetDateDueToPolishTimeZone.ReturnDateNow().ToString("dd.MM.yyyy HH:mm:ss"))));

                if (completeVisit.RabiesVaccination.Name != "")
                {
                    document.Add(new Paragraph("Szczepienia przeciwko wściekliźnie"));
                    Table table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
                    table.AddHeaderCell("Nazwa szczepionki");
                    table.AddHeaderCell("Nr serii szczepionki");
                    table.AddHeaderCell("Data ważności szczepionki");
                    table.AddHeaderCell("Termin następnego szczepienia");

                    table.AddCell(completeVisit.RabiesVaccination.Name);
                    table.AddCell(completeVisit.RabiesVaccination.Series);
                    table.AddCell(DateOnly.FromDateTime(GetDateDueToPolishTimeZone.ReturnDate(completeVisit.RabiesVaccination.TermValidityRabies)).ToString("dd.MM.yyyy"));
                    table.AddCell(DateOnly.FromDateTime(GetDateDueToPolishTimeZone.ReturnDate(completeVisit.RabiesVaccination.TermNextRabies)).ToString("dd.MM.yyyy"));
                    document.Add(table);
                    document.Add(new Paragraph());
                }

                if (completeVisit.OtherVaccinations.Count != 0)
                {
                    document.Add(new Paragraph("Szczepienia przeciwko innych chorobom zakaźnym"));
                    Table table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();
                    table.AddHeaderCell("Nazwa choroby");
                    table.AddHeaderCell("Nazwa szczepionki");
                    table.AddHeaderCell("Nr serii szczepionki");
                    foreach (var otherVaccination in completeVisit.OtherVaccinations)
                    {
                        table.AddCell(otherVaccination.DiseaseName);
                        table.AddCell(otherVaccination.Name);
                        table.AddCell(otherVaccination.Series);
                    }
                    document.Add(table);
                    document.Add(new Paragraph());
                }

                if (completeVisit.Treatments.Count != 0)
                {
                    document.Add(new Paragraph("Zabiegi"));
                    Table table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
                    table.AddHeaderCell("Nazwa zabiegu");
                    table.AddHeaderCell("Diagnoza");
                    table.AddHeaderCell("Opis");
                    table.AddHeaderCell("Zalecenia");
                    foreach (var treatment in completeVisit.Treatments)
                    {
                        table.AddCell(treatment.TreatmentName);
                        table.AddCell(treatment.TreatmentDiagnosis);
                        table.AddCell(treatment.TreatmentDescription);
                        table.AddCell(treatment.Recommendations);
                    }
                    document.Add(table);
                    document.Add(new Paragraph());
                }

                if (completeVisit.TreatedDiseases.Count != 0)
                {
                    document.Add(new Paragraph("Leczone choroby"));
                    Table table = new Table(UnitValue.CreatePercentArray(5)).UseAllAvailableWidth();
                    table.AddHeaderCell("Nazwa choroby");
                    table.AddHeaderCell("Opis choroby");
                    table.AddHeaderCell("Opis leczenia");
                    table.AddHeaderCell("Przepisane leki");
                    table.AddHeaderCell("Zalecenia");
                    foreach (var disease in completeVisit.TreatedDiseases)
                    {
                        table.AddCell(disease.DiseaseName);
                        table.AddCell(disease.DiseaseDescription);
                        table.AddCell(disease.TreatmentDescription);
                        table.AddCell(disease.PrescribedMedications);
                        table.AddCell(disease.Recommendations);
                    }
                    document.Add(table);
                    document.Add(new Paragraph());
                }

                if (completeVisit.Research.ResearchesList != "")
                {
                    document.Add(new Paragraph("Badania"));
                    Table table = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
                    table.AddHeaderCell("Lista badanych czynników");

                    table.AddCell(completeVisit.Research.ResearchesList);
                    document.Add(table);
                    document.Add(new Paragraph());
                }

                if (completeVisit.Weight != 0)
                {
                    document.Add(new Paragraph("Waga"));
                    Table table = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
                    table.AddHeaderCell("Waga");

                    table.AddCell(completeVisit.Weight.ToString());
                    document.Add(table);
                    document.Add(new Paragraph());
                }

                document.Close();
                byte[] byteArray = stream.ToArray();

                using (MemoryStream ms = new MemoryStream(byteArray))
                {
                    ms.Position = 0;

                    BlobContainerClient container = new BlobContainerClient(ConnectionStrings.Azure, "generatedcards");
                    var createResponse = container.CreateIfNotExists();
                    if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                        container.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    // Get a reference to the blob
                    BlobClient blobClient = container.GetBlobClient(String.Format("wizyta_{0}.pdf", GetDateDueToPolishTimeZone.ReturnDateNow()
                        .ToString("dd.MM.yyyy HH:mm:ss")));

                    blobClient.UploadAsync(ms, new BlobHttpHeaders { ContentType = "application/pdf" });
                    var link = blobClient.Uri.ToString();
                    ResearchResponseDTO researchResponse = new ResearchResponseDTO();
                    researchResponse.Path = blobClient.Uri.ToString();
                    researchResponse.Name = blobClient.Name;
                    return researchResponse;
                }
            }
            catch(Exception ex)
            {
                message += " Exception: " + ex.Message;
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
                {
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("[dbo].[AddErrorMessage]", sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        sqlCommand.Parameters.Add("@message", SqlDbType.NVarChar).Value = message;
                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlConnection.Close();

                }
                return null;
            }
            
        }
    }
}
