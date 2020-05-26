using IPInfo.Library.Exceptions;
using IPInfo.Library.Interfaces;
using IPInfo.Library.Response;
using RestSharp;
using Serilog;
using System;
using IPInfo.Library.Configuration;
using Newtonsoft.Json;

namespace IPInfo.Library
{
    public class IPInfoProvider : IIPInfoProvider
    {
        private readonly ILogger _logger;
        private readonly IPProviderConfiguration _configuration;

        private const string _serviceUnavailable = "Service is not Available";

        public IPInfoProvider(ILogger logger, IPProviderConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IPDetails GetIPDetails(string ip)
        {
            try
            {
                var client = new RestClient($"{_configuration?.APIRootUrl}/{ip}?access_key={_configuration?.APIKey}");
                var request = new RestRequest(Method.GET);
                var response = client.Execute(request);

                if(!response.IsSuccessful) throw new IPServiceNotAvailableException(_serviceUnavailable);

                return JsonConvert.DeserializeObject<IPResponse>(response.Content);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw new IPServiceNotAvailableException(_serviceUnavailable, ex);
            }
        }
    }
}
