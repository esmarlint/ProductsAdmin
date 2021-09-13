using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using ProductsAdmin.Controllers;
using ProductsAdmin.Database.Contexts;
using ProductsAdmin.Database.Models;
using ProductsAdmin.Models.Api;
using ProductsAdmin.Models.Api.Requests;
using ProductsAdmin.Models.Api.Responses;
using ProductsAdmin.Models.Request.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProductsAdmin.Test
{
    public class TestProductController
    {
        ApplicationDbContext context;
        DbContextOptionsBuilder<ApplicationDbContext> options => new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "MovieListDatabase");

        public TestProductController()
        {
            context = new ApplicationDbContext(options.Options);
            PrepareDatabase();
        }

        [Fact]
        public async Task ShouldNotReturnOneProductWhenIdExist()
        {
            var configurationMock = new Mock<IConfiguration>();
            int notExistingId = -1;

            var controller = new ProductController(context, configurationMock.Object);

            var objectResult = (NotFoundObjectResult)(await controller.GetById(notExistingId));

            var response = (RestErrorResponse)objectResult.Value;

            Assert.IsType<NotFoundObjectResult>(objectResult);
            Assert.Equal(404, objectResult.StatusCode);
            Assert.Equal(response.ErrorType, "resource_not_found");
            Assert.Equal(response.Error, $"Product with <Id> -> <{notExistingId}> doesn´t exists");
        }

        [Fact]
        public async Task ShouldReturnOneProductWhenIdExist()
        {
            var configurationMock = new Mock<IConfiguration>();
            int existingProductId = 1;

            var controller = new ProductController(context, configurationMock.Object);

            var objectResult = (OkObjectResult)(await controller.GetById(existingProductId));

            var response = (RestOkResponse<ProductRestResponse>)objectResult.Value;

            Assert.Equal(200, objectResult.StatusCode);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.IsType<RestOkResponse<ProductRestResponse>>(response);
        }

        [Fact]
        public async Task ShouldReturnPaginatedResultForAllExistingProducs()
        {
            var configurationMock = new Mock<IConfiguration>();
            var paginationParameters = new PaginationParameters
            {
                Page = 1,
                PageSize = 100
            };
            var searchProductQuery = new SearchProductQuery
            {
                Description = "",
                Name = ""
            };

            var controller = new ProductController(context, configurationMock.Object);


            var objectResult = (OkObjectResult)(await controller.GetAll(paginationParameters,searchProductQuery));

            var response = (RestPaginatedResponse<ProductRestResponse>)objectResult.Value;

            Assert.Equal(200, objectResult.StatusCode);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.IsType<RestPaginatedResponse<ProductRestResponse>>(response);
        }

        [Fact]
        public async Task ShouldReturnJustProductWithNameProductWithExpecificNameCoincidence()
        {
            var configurationMock = new Mock<IConfiguration>();
            var paginationParameters = new PaginationParameters
            {
                Page = 1,
                PageSize = 100
            };
            var searchProductQuery = new SearchProductQuery
            {
                Description = "",
                Name = "PRODUCTO 1"
            };

            var controller = new ProductController(context, configurationMock.Object);


            var objectResult = (OkObjectResult)(await controller.GetAll(paginationParameters, searchProductQuery));

            var response = (RestPaginatedResponse<ProductRestResponse>)objectResult.Value;

            Assert.Equal(200, objectResult.StatusCode);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.IsType<RestPaginatedResponse<ProductRestResponse>>(response);
            Assert.Equal(response.Payload[0].Name, searchProductQuery.Name);
        }

        [Fact]
        public async Task ShouldReturnJustProductWithNameProductWithExpecificDescriptionCoincidence()
        {
            var configurationMock = new Mock<IConfiguration>();
            var paginationParameters = new PaginationParameters
            {
                Page = 1,
                PageSize = 100
            };
            var searchProductQuery = new SearchProductQuery
            {
                Description = "DESCRIPCION 1",
                Name = ""
            };

            var controller = new ProductController(context, configurationMock.Object);


            var objectResult = (OkObjectResult)(await controller.GetAll(paginationParameters, searchProductQuery));

            var response = (RestPaginatedResponse<ProductRestResponse>)objectResult.Value;

            Assert.Equal(200, objectResult.StatusCode);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.IsType<RestPaginatedResponse<ProductRestResponse>>(response);
            Assert.Equal(response.Payload[0].Description, searchProductQuery.Description);
        }

        [Fact]
        public async Task ShouldCreateProductWhenRequestIsValid()
        {
            var configurationMock = new Mock<IConfiguration>();
            var name = Guid.NewGuid().ToString();
            var validRequest = new CreateProductRequest {
                Name = name,
                Description = "DESCRIPCION 3",
                StatusId = 1,
                Prices = new List<CreatePriceRequest>
                {
                    new CreatePriceRequest{ ColorId =1,IsDefaultPrice=true,Price=100,StatusId=1}
                }
            };


            var controller = new ProductController(context, configurationMock.Object);

            var objectResult = (ObjectResult)(await controller.Create(validRequest));

            var response = (RestOkResponse<ProductRestResponse>)objectResult.Value;

            Assert.Equal(201, objectResult.StatusCode);
            Assert.IsType<ObjectResult>(objectResult);
            Assert.IsType<RestOkResponse<ProductRestResponse>>(response);
            Assert.Equal(name,response.Payload.Name);
        }

        [Fact]
        public async Task ShouldUpdateProductWhenUpdateProductRequestIsValid()
        {
            var configurationMock = new Mock<IConfiguration>();
            var name = Guid.NewGuid().ToString();
            var productWithId1 = 1;
            var validRequest = new UpdateProductRequest
            {
                Name = name,
                Description = "DESCRIPCION 3",
                StatusId = 1,
            };


            var controller = new ProductController(context, configurationMock.Object);

            var objectResult = (OkObjectResult)(await controller.Update(productWithId1,validRequest));

            var response = (RestOkResponse<ProductRestResponse>)objectResult.Value;

            Assert.Equal(200, objectResult.StatusCode);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.IsType<RestOkResponse<ProductRestResponse>>(response);
            Assert.Equal(name, response.Payload.Name);
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenProductIdToUpdateDoesntExists()
        {
            var configurationMock = new Mock<IConfiguration>();
            var name = Guid.NewGuid().ToString();
            var notExistingId = -1;
            var validRequest = new UpdateProductRequest
            {
                Name = name,
                Description = "DESCRIPCION 3",
                StatusId = 1,
            };


            var controller = new ProductController(context, configurationMock.Object);

            var objectResult = (NotFoundObjectResult)(await controller.Update(notExistingId, validRequest));

            var response = (RestErrorResponse)objectResult.Value;

            Assert.Equal(404, objectResult.StatusCode);
            Assert.IsType<NotFoundObjectResult>(objectResult);
            Assert.IsType<RestErrorResponse>(response);
            Assert.Equal(response.ErrorType, "resource_not_found");
            Assert.Equal(response.Error, $"Product with <Id> -> <{notExistingId}> doesn´t exists");
        }

        [Fact]
        public async Task ShouldThrowAndExceptionWhenCreatProducRequestIsInvalid()
        {
            //TODO: validate request for 400 request
            var configurationMock = new Mock<IConfiguration>();
            var name = Guid.NewGuid().ToString();
            var validRequest = new CreateProductRequest
            {
                Name = name,
                Description = "DESCRIPCION 3",
                StatusId = 1,
            };


            var controller = new ProductController(context, configurationMock.Object);

            var objectResult = (ObjectResult)(await controller.Create(validRequest));

            var response = (RestErrorResponse)objectResult.Value;

            Assert.Equal(500, objectResult.StatusCode);
            Assert.IsType<ObjectResult>(objectResult);
            Assert.IsType<RestErrorResponse>(response);
            Assert.Equal(response.ErrorType, "server_error");
        }

        private void PrepareDatabase()
        {
            context.Statuses.AddRange(new Status[] {
                new Status{Type = "ENTRY",Name="ACTIVE"},
                new Status{Type = "ENTRY",Name="CANCELLED"},
                new Status{Type = "ENTRY",Name="DELETED"},
            });


            context.Colors.AddRange(new Color[] {
                new Color{Name="AZUL",Value="0000FF",Status = 1,Format="HEX"},
                new Color{Name="ROJO",Value="FF0000",Status = 1,Format="HEX"},
                new Color{Name="VERDE",Value="00FF00",Status = 1,Format="HEX"},
            });

            context.Products.AddRange(new Product[] {
                new Product{Name="PRODUCTO 1",Description="DESCRIPCION 1",Status = 1},
                new Product{Name="PRODUCTO 2",Description="DESCRIPCION 2",Status = 1},
            });

            context.SaveChanges();
        }
    }
}
