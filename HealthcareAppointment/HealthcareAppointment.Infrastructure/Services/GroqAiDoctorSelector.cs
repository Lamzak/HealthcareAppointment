using HealthcareAppointment.Application.Interfaces;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using Microsoft.Extensions.Configuration;
using HealthcareAppointment.Domain.Constants;

namespace HealthcareAppointment.Infrastructure.Services
{
    public class GroqAiDoctorSelector(IConfiguration configuration) : IAiDoctorSelector
    {
        public async Task<string> GetSpecializationFromSymptomsAsync(string symptoms)
        {
            var apiKey = configuration["GroqSettings:ApiKey"];
            var model = configuration["GroqSettings:Model"];
            var endpoint = configuration["GroqSettings:Endpoint"];

            if (string.IsNullOrEmpty(apiKey))
                return DoctorSpecializations.General;

            var client = new ChatClient(
                model: model,
                credential: new ApiKeyCredential(apiKey),
                options: new OpenAIClientOptions { Endpoint = new Uri(endpoint) }
            );

            var validSpecializations = DoctorSpecializations.GetAllAsString();

            var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                $"You are a medical receptionist. " +
                $"We ONLY have doctors with these specializations: [{validSpecializations}]. " +
                $"Map the patient's symptoms to the single most relevant specialization from that list. " +
                $"If the symptoms are unclear or don't fit, return '{DoctorSpecializations.General}'. " +
                $"Return ONLY the name of the specialization, nothing else."
            ),
            new UserChatMessage(symptoms)
        };

            try
            {
                ChatCompletion completion = await client.CompleteChatAsync(messages);
                var result = completion.Content[0].Text.Trim().Replace(".", "").Replace("\"", "");
                return result;
            }
            catch
            {
                return DoctorSpecializations.General;
            }
        }
    }
}
