using Azure;
using Azure.AI.OpenAI;
using MeetUpWebApp.Shared.ViewModels;
using OpenAI.Chat;

namespace MeetUpWebApp.Features.AIChatBot
{
    public class AIChatBotService
    {
    private readonly string model;
    private readonly string deploymentName;
    private readonly string apiKey;
    private readonly Uri endpoint;

    public AIChatBotService(IConfiguration configuration)
    {
        // IConfiguration üzerinden değerleri constructor içinde alıyoruz
        model = configuration["AzureOpenAI:Model"];
        deploymentName = configuration["AzureOpenAI:DeploymentName"];
        apiKey = configuration["AzureOpenAI:ApiKey"];
        endpoint = new Uri(configuration["AzureOpenAI:Endpoint"]);
    }


        public async Task<string> GetAiResponseAsync(string userContent)
        {
            // Run synchronous Azure call on background thread to avoid blocking (if only sync method is available)
            return await Task.Run(() =>
            {
                try
                {
                    AzureOpenAIClient azureClient = new(
                        endpoint,
                        new AzureKeyCredential(apiKey));
                    ChatClient chatClient = azureClient.GetChatClient(deploymentName);

                    var requestOptions = new ChatCompletionOptions()
                    {
                        MaxOutputTokenCount = 4096,
                        Temperature = 1.0f,
                        TopP = 1.0f,
                    };

                    List<ChatMessage> chatMessages = new List<ChatMessage>()
                {
                    new SystemChatMessage("Sen bir etkinlik düzenleme sitesinde asistansın ama bu site meetup,kommunity gibi bir site genellikle iş dünyasından özellikle yazılım ve teknoloji alanında faaliyetler gösteren ve insalara sorulara hakkında en fazla 200 500 satırlık cevaplar vererek yardımcı oluyorsun."),
                    new UserChatMessage(userContent),
                };

                    var response = chatClient.CompleteChat(chatMessages, requestOptions);
                    return response.Value.Content[0].Text?.ToString();
                }
                catch (Exception ex)
                {
                   
                    return $"Hata: {ex.Message}";
                }
            });
        }


    }
}
