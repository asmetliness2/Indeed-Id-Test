using Microsoft.Extensions.Caching.Memory;
using PurseApi.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PurseApi.Providers
{
    public class CentralBankCurrencyProvider : ICurrencyProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        private static readonly string CacheKey = "currency";

        public CentralBankCurrencyProvider(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        private CentralBankResponse DeserializeResponse(string response)
        {
            using (var stream = new StringReader(response))
            {
                var serializer = new XmlSerializer(typeof(CentralBankResponse));
                var resp = (CentralBankResponse)serializer.Deserialize(stream);
                return resp;
            }
        }

        private async Task<CentralBankResponse> GetBankResponse()
        {
            var response = await _httpClient.GetAsync("");

            var responseSrt = await response.Content.ReadAsStringAsync();

            return DeserializeResponse(responseSrt);
        }

        public async Task<IEnumerable<string>> GetAvailableCurrencies()
        {
            IEnumerable<string> result = null;

            if(!_cache.TryGetValue(CacheKey, out result))
            {
                var bankResponse = await GetBankResponse();

                result = bankResponse.Cube.InnerCube.CurrencyValues.Select(s => s.Currency).ToList();

                _cache.Set(CacheKey, result, TimeSpan.FromHours(1));
            }
            return result;
        }

        public async Task<Dictionary<string, decimal>> GetCurrenciesRatio()
        {
            var result = new Dictionary<string, decimal>();

            var bankResponse = await GetBankResponse();

            foreach(var value in bankResponse.Cube.InnerCube.CurrencyValues)
            {
                result[value.Currency] = value.Value;
            }
            return result;
        }
    }


    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
    public class CentralBankResponse
    {
        [XmlElement("Cube", Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
        public Cube Cube { get; set; }
    }

    public class Cube
    {
        [XmlElement("Cube")]
        public InnerCube InnerCube { get; set; }
    }

    public class InnerCube
    {
        [XmlElement("Cube")]
        public CurrencyValue[] CurrencyValues { get; set; }
    }

    public class CurrencyValue
    {
        [XmlAttribute(AttributeName = "currency")]
        public string Currency { get; set; }
        [XmlAttribute(AttributeName = "rate")]
        public decimal Value { get; set; }
    }

}
