using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AnimalCard.Application.Visit.Query.GetScheduledVisits;
using AnimalCard.Application.Helpers;
using AnimalCard.Application.DTOs;

namespace AnimalCard.Application.Visit.Command.CompleteVisit
{
    public class CompleteVisitCommandHandler : IRequestHandler<CompleteVisitCommand, CompleteVisitResponse>
    {
        public async Task<CompleteVisitResponse> Handle(CompleteVisitCommand request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME_RABIES_VACCINATION = "[dbo].[AddRabiesVaccination]";
            const string PROCEDURE_NAME_OTHER_VACCINATION = "[dbo].[AddOtherVaccination]";
            const string PROCEDURE_NAME_TREATMENT = "[dbo].[AddTreatment]";
            const string PROCEDURE_NAME_TREATED_DISEASE = "[dbo].[AddTreatedDisease]";
            const string PROCEDURE_NAME_RESEARCH = "[dbo].[AddResearch]";
            const string PROCEDURE_NAME_WEIGHT = "[dbo].[AddWeight]";
            const string PROCEDURE_NAME_GET_PET_ID = "[dbo].[GetPetIdByVisitId]";
            const string PROCEDURE_NAME_COMPLETE_VISIT = "[dbo].[CompleteVisit]";
            const string PROCEDURE_NAME_SET_PET_TO_VET = "[dbo].[SetPetToVet]";

            int petId = 0;
            int vetId = request.VetId;
            ResearchResponseDTO responseVisitCard;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                if(request.VisitId != 0)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME_GET_PET_ID, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        sqlCommand.Parameters.Add("@visitId", SqlDbType.Int).Value = request.VisitId;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                petId = sqlDataReader.GetInt32("PetId");
                            }
                        }
                    }
                }
                else
                {
                    petId = request.PetId;
                }


                if(request.RabiesVaccination.Name != "")
                {
                    using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME_RABIES_VACCINATION, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = request.RabiesVaccination.Name;
                        sqlCommand.Parameters.Add("@series", SqlDbType.NVarChar).Value = request.RabiesVaccination.Series;
                        sqlCommand.Parameters.Add("@vaccinationDate", SqlDbType.Date).Value = GetDateDueToPolishTimeZone.ReturnDateNow().Date;
                        sqlCommand.Parameters.Add("@termValidity", SqlDbType.Date).Value = GetDateDueToPolishTimeZone.ReturnDate(request.RabiesVaccination.TermValidityRabies).Date;
                        sqlCommand.Parameters.Add("@termNext", SqlDbType.Date).Value = GetDateDueToPolishTimeZone.ReturnDate (request.RabiesVaccination.TermNextRabies).Date;
                        sqlCommand.Parameters.Add("@petId", SqlDbType.Int).Value = petId;
                        sqlCommand.Parameters.Add("@vetId", SqlDbType.Int).Value = vetId;

                        sqlCommand.ExecuteNonQuery();
                    }
                }
                
                if (request.OtherVaccinations.Count != 0)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME_OTHER_VACCINATION, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        foreach (var otherVaccination in request.OtherVaccinations)
                        {
                            sqlCommand.Parameters.Add("@diseaseName", SqlDbType.NVarChar).Value = otherVaccination.DiseaseName;
                            sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = otherVaccination.Name;
                            sqlCommand.Parameters.Add("@series", SqlDbType.NVarChar).Value = otherVaccination.Series;
                            sqlCommand.Parameters.Add("@vaccinationDate", SqlDbType.Date).Value = GetDateDueToPolishTimeZone.ReturnDateNow().Date;
                            sqlCommand.Parameters.Add("@petId", SqlDbType.Int).Value = petId;
                            sqlCommand.Parameters.Add("@vetId", SqlDbType.Int).Value = vetId;

                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
                
                if (request.Treatments.Count != 0)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME_TREATMENT, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        foreach (var treatment in request.Treatments)
                        {
                            sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = treatment.TreatmentName;
                            sqlCommand.Parameters.Add("@diagnosis", SqlDbType.NVarChar).Value = treatment.TreatmentDiagnosis;
                            sqlCommand.Parameters.Add("@treatmentDescription", SqlDbType.NVarChar).Value = treatment.TreatmentDescription;
                            sqlCommand.Parameters.Add("@recommendations", SqlDbType.NVarChar).Value = treatment.Recommendations;
                            sqlCommand.Parameters.Add("@treatmentDate", SqlDbType.Date).Value = GetDateDueToPolishTimeZone.ReturnDateNow().Date;
                            sqlCommand.Parameters.Add("@petId", SqlDbType.Int).Value = petId;
                            sqlCommand.Parameters.Add("@vetId", SqlDbType.Int).Value = vetId;

                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }

                if (request.TreatedDiseases.Count != 0)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME_TREATED_DISEASE, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        foreach (var treatedDisease in request.TreatedDiseases)
                        {
                            sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = treatedDisease.DiseaseName;
                            sqlCommand.Parameters.Add("@diseaseDescription", SqlDbType.NVarChar).Value = treatedDisease.DiseaseDescription;
                            sqlCommand.Parameters.Add("@treatmentDescription", SqlDbType.NVarChar).Value = treatedDisease.TreatmentDescription;
                            sqlCommand.Parameters.Add("@prescribedMedications", SqlDbType.NVarChar).Value = treatedDisease.PrescribedMedications;
                            sqlCommand.Parameters.Add("@recommendations", SqlDbType.NVarChar).Value = treatedDisease.Recommendations;
                            sqlCommand.Parameters.Add("@diagnosisDate", SqlDbType.Date).Value = GetDateDueToPolishTimeZone.ReturnDateNow().Date;
                            sqlCommand.Parameters.Add("@petId", SqlDbType.Int).Value = petId;
                            sqlCommand.Parameters.Add("@vetId", SqlDbType.Int).Value = vetId;

                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }

                if (request.Research.ResearchesList != "")
                {
                    using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME_RESEARCH, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        sqlCommand.Parameters.Add("@researchesList", SqlDbType.NVarChar).Value = request.Research.ResearchesList;
                        sqlCommand.Parameters.Add("@researchesDate", SqlDbType.Date).Value = GetDateDueToPolishTimeZone.ReturnDateNow().Date;
                        sqlCommand.Parameters.Add("@petId", SqlDbType.Int).Value = petId;
                        sqlCommand.Parameters.Add("@vetId", SqlDbType.Int).Value = vetId;

                        sqlCommand.ExecuteNonQuery();

                    }
                }

                if (request.Weight != 0)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME_WEIGHT, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        sqlCommand.Parameters.Add("@value", SqlDbType.Decimal).Value = request.Weight;
                        sqlCommand.Parameters.Add("@weighingDate", SqlDbType.Date).Value = GetDateDueToPolishTimeZone.ReturnDateNow().Date;
                        sqlCommand.Parameters.Add("@petId", SqlDbType.Int).Value = petId;

                        sqlCommand.ExecuteNonQuery();
                    }
                }

                if (request.VisitId != 0)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME_COMPLETE_VISIT, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        sqlCommand.Parameters.Add("@visitId", SqlDbType.Int).Value = request.VisitId;
                        sqlCommand.ExecuteNonQuery();
                    }
                }

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME_SET_PET_TO_VET, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@vetId", SqlDbType.Int).Value = vetId;
                    sqlCommand.Parameters.Add("@petId", SqlDbType.Int).Value = petId;
                    sqlCommand.ExecuteNonQuery();
                }

                responseVisitCard = GenerateVisitCard.GeneratePdf(request);
                await sqlConnection.CloseAsync();

            }
            CompleteVisitResponse response = new CompleteVisitResponse() { Result = true, GeneratedCardPath = responseVisitCard.Path, GeneratedCardFileName = responseVisitCard.Name };
            return response;
        }
    }
}
