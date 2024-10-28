﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineStore.API.Common.IntegrationTests;
using OnlineStore.API.Common.IntegrationTests.Helpers;
using Xunit;

namespace OnlineStore.API.Warehouse.IntegrationTests
{
    public class WarehouseTests : IClassFixture<TestFixture<Startup>>
    {
        readonly HttpClient Client;

        public WarehouseTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task SearchProductsAsWarehouseManagerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWarehouseManagerAsync();
            var request = new
            {
                Url = "/api/v1/Warehouse/search-product",
                Body = new
                {
                    ProductID = 1
                }
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client
                .PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task SearchProductsAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Warehouse/search-product",
                Body = new
                {
                    ProductID = 1
                }
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client
                .PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task GetProductInventoryAsWarehouseManagerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWarehouseManagerAsync();
            var request = new
            {
                Url = "/api/v1/Warehouse/product-inventory",
                Body = new
                {
                    ProductID = 1
                }
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client
                .PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetProductInventoryAsWarehouseOperatorAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWarehouseOperatorAsync();
            var request = new
            {
                Url = "/api/v1/Warehouse/product-inventory",
                Body = new
                {
                    ProductID = 1
                }
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client
                .PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task GetProductInventoryAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Warehouse/product-inventory",
                Body = new
                {
                    ProductID = 1,
                    LocationID = ""
                }
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client
                .PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task PostProductAsWarehouseManagerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWarehouseManagerAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Warehouse/Product"),
                Body = new
                {
                    ProductName = string.Format("Test product (integration tests) {0}", DateTime.Now),
                    ProductCategoryID = 1,
                    UnitPrice = 9.99m,
                    Description = "unit tests"
                }
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client
                .PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task PutProductUnitPriceAsWarehouseManagerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWarehouseManagerAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Warehouse/update-product-unit-price/{0}", 1),
                Body = new
                {
                    UnitPrice = 14.99m
                }
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client
                .PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
