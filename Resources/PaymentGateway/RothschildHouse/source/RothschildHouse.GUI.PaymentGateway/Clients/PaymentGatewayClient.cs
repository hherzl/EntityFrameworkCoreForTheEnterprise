﻿using System.Net.Http.Headers;
using System.Text.Json;
using RothschildHouse.GUI.PaymentGateway.Clients.Models.Common;
using RothschildHouse.GUI.PaymentGateway.Clients.Models.PaymentGateway;

namespace RothschildHouse.GUI.PaymentGateway.Clients
{
    public class PaymentGatewayClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _endpoint;

        public PaymentGatewayClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _endpoint = configuration.GetValue<string>("ClientEndpoints:PaymentGateway");
        }

        static JsonSerializerOptions DefaultJsonSerializerOptions
            => new()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

        private HttpClient CreateHttpClient()
        {
            var client = _httpClientFactory.CreateClient("RothschildHousePaymentGateway");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentTypes.ApplicationJson));

            return client;
        }

        public async Task<ListResponse<CountryItemModel>> GetCountriesAsync(GetCountriesRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/country?pageSize={request.PageSize}&pageNumber={request.PageNumber}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ListResponse<CountryItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<ListResponse<CurrencyItemModel>> GetCurrenciesAsync(GetCurrenciesRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/currency?pageSize={request.PageSize}&pageNumber={request.PageNumber}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ListResponse<CurrencyItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<ListResponse<ClientApplicationItemModel>> GetClientApplicationsAsync(GetClientApplicationsRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/client-application?pageSize={request.PageSize}&pageNumber={request.PageNumber}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ListResponse<ClientApplicationItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<SingleResponse<ClientApplicationDetailsModel>> GetClientApplicationAsync(Guid? id)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/client-application/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SingleResponse<ClientApplicationDetailsModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<PagedResponse<CardItemModel>> GetCardsAsync(GetCardsRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/card?pageSize={request.PageSize}&pageNumber={request.PageNumber}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PagedResponse<CardItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<SingleResponse<CardDetailsModel>> GetCardAsync(Guid? id)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/card/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SingleResponse<CardDetailsModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<PagedResponse<CustomerItemModel>> GetCustomersAsync(GetCustomersRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/customer?pageSize={request.PageSize}&pageNumber={request.PageNumber}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PagedResponse<CustomerItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<SingleResponse<CustomerDetailsModel>> GetCustomerAsync(Guid? id)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/customer/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SingleResponse<CustomerDetailsModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<PagedResponse<PaymentTransactionItemModel>> GetPaymentTransactionsAsync(GetPaymentTransactionsRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/payment-txn?pageSize={request.PageSize}&pageNumber={request.PageNumber}&PaymentTransactionStatusId={request.PaymentTransactionStatusId}&clientApplicationId={request.ClientApplicationId}&startDate={request.StartDate}&endDate={request.EndDate}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PagedResponse<PaymentTransactionItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<SingleResponse<PaymentTransactionDetailsModel>> GetPaymentTransactionAsync(long? id)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/payment-txn/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SingleResponse<PaymentTransactionDetailsModel>>(content, options: DefaultJsonSerializerOptions);
        }
    }
}
