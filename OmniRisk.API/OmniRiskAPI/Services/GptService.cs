using OmniRiskAPI.Models;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;

using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;

namespace OmniRiskAPI.Services
{
    public class GptService
    {
        public async Task<List<TweetModel>> AnalyzeText(List<TweetModel> texts)
        {

            var openAIConfigurations = new OpenAIConfigurations
            {
                ApiKey = "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",
            };

            var openAIClient = new OpenAIClient(openAIConfigurations);
            var answer = new List<TweetModel>();
            foreach (var text in texts)
            {
                if (await SendRequest(text.Content, openAIClient))
                {
                    answer.Add(text);
                }
            }
            return answer;
        }

        private static async Task<bool> SendRequest(string text, OpenAIClient openAIClient)
        {
            var chatMsg = new ChatCompletionMessage()
            {
                Content = $"""
        Odpowiedz tylko "tak" lub "nie": czy to zdanie w jakikolwiek sposob wspomina o rzeczach potencjalnie sprawiajacych lokalne zagrozenie lub lokalna niedogodnosc: "${text}"
    """,
                Role = "user"
            };

            var chatCompletion = new ChatCompletion
            {
                Request = new ChatCompletionRequest
                {
                    Model = "gpt-3.5-turbo",
                    Messages = new[] { chatMsg },
                    Temperature = 0.2,
                    MaxTokens = 800
                }
            };

            var result = await openAIClient
              .ChatCompletions
              .SendChatCompletionAsync(chatCompletion);

            var answer = result.Response.Choices[0].Message.Content;

            return answer.ToLower().Contains("tak");
        }
    }
}
