using DotnetAppWith.Hangfire.Example.DTOs.Actors;
using DotnetAppWith.Hangfire.Example.Services;
using static Azure.Core.HttpHeader;

namespace DotnetAppWith.Hangfire.Example.Hangfire.Jobs
{
    public class AutoAddActor
    {
        private readonly IActorService _actorService;
        private readonly ILogger _logger;
        public AutoAddActor(IActorService actorService, ILogger<AutoAddActor> logger)
        {
            _actorService = actorService;
            _logger = logger;
        }

        public async Task<bool> ExecuteJob()
        {
            _logger.LogInformation("Start AutoAddActor");
            try
            {
                var model = new RandomNameApiResult();
                using (var client = new HttpClient())
                {
                    var url = "https://api.namefake.com/api.name-fake.com/english-united-states";
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        model = await response.Content.ReadFromJsonAsync<RandomNameApiResult>();
                        if (model != null)
                        {
                            _logger.LogInformation($"Insert Actor : {model.name}");
                            var result = await _actorService.Insert(new CreateActorDTO
                            {
                                Name = model!.name
                            });
                            _logger.LogInformation($"Result = {result}");
                        }
                        else
                        {
                            _logger.LogInformation($"Fail to insert, model is null");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Fail AutoAddActor");
                _logger.LogInformation(ex.Message);
            }

            return true;
        }
    }

    public class RandomNameApiResult
    {
        public string name { get; set; } = string.Empty;

    }
}
