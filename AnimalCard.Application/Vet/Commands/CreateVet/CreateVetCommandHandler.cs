using AnimalCard.Application.Common.Interfaces;
using AnimalCard.Application.User.Commands.CreateUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using AnimalCard.Decryption;

namespace AnimalCard.Application.Vet.Commands.CreateVet
{
    public class CreateVetCommandHandler : IRequestHandler<CreateVetCommand, bool>
    {
        private readonly IIdentityService _identityService;
        public CreateVetCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<bool> Handle(CreateVetCommand request, CancellationToken cancellationToken)
        {
            string fullName = request.Name + " " + request.Surname;
            bool isVet = false;
            bool isSuccessed = false;

            using (var httpClient = new HttpClient())
            {
                using (var httpRequest = new HttpRequestMessage(new HttpMethod("POST"), "https://wetsystems.org.pl/WetSystemsInfo/Bramka"))
                {
                    httpRequest.Content = new StringContent($"op=lekarze&nr_pwz={request.NrPWZ}&imie=&nazwisko=");
                    httpRequest.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = httpClient.SendAsync(httpRequest);
                    StreamReader sr = new StreamReader(response.Result.Content.ReadAsStream());
                    string returnvalue = sr.ReadToEnd();

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(returnvalue);

                    List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table")
                    .Descendants("tr")
                    .Skip(1)
                    .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                    .ToList();
                    if (table[0].Count != 1)
                    {
                        if (table[0][1] == fullName.ToUpper())
                        {
                            isVet = true;
                        }
                    }
                    
                }
            }

            if (isVet)
            {
                var result = await _identityService.CreateUserAsync(request.Password, request.Email, fullName, request.Roles);
                const string PROCEDURE_NAME = "[dbo].[CreateVet]";
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        sqlCommand.Parameters.Add("@nrPWZ", SqlDbType.NVarChar).Value = request.NrPWZ;
                        sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = request.Name;
                        sqlCommand.Parameters.Add("@surname", SqlDbType.NVarChar).Value = request.Surname;
                        sqlCommand.Parameters.Add("@sex", SqlDbType.NChar).Value = request.Sex.First();
                        sqlCommand.Parameters.Add("@email", SqlDbType.NVarChar).Value = request.Email;
                        sqlCommand.Parameters.Add("@phoneNumber", SqlDbType.NVarChar).Value = request.PhoneNumber;

                        sqlCommand.ExecuteNonQuery();
                    }
                    await sqlConnection.CloseAsync();

                }
                isSuccessed = result.isSucceed;
            }
            return isSuccessed;
        }
    }
}
