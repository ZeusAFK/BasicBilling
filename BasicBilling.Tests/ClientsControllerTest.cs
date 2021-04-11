using System.Collections.Generic;
using BasicBilling.Controllers;
using BasicBilling.Data.Interfaces;
using BasicBilling.Data.Repositories;
using BasicBilling.Profiles;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BasicBilling.Data.Models;

namespace BasicBilling.Tests
{
  public class ClientsControllerTest
  {
    ClientsController controller;
    IBasicBillingRepo repository;
    IMapper mapper;

    public ClientsControllerTest()
    {
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new BillingProfile());
      });

      mapper = mappingConfig.CreateMapper();
      repository = new MockBasicBillingRepoImpl();
      controller = new ClientsController(repository, mapper);
    }

    [Fact]
    public void GetAllClients_WhenCalled_ReturnsOkObjectResult()
    {
      var okResult = controller.GetAllClients();
      Assert.IsType<OkObjectResult>(okResult.Result);
    }

    [Fact]
    public void GetAllClients_WhenCalled_ReturnsAllItems()
    {
      var okResult = controller.GetAllClients().Result as OkObjectResult;
      var items = Assert.IsType<List<ClientReadDto>>(okResult.Value);
      Assert.Equal(5, items.Count);
    }

    [Fact]
    public void GetClientById_UnknowIdPassed_ReturnsNotFoundResult()
    {
      var notFoundResult = controller.GetClientById(132);
      Assert.IsType<NotFoundResult>(notFoundResult.Result);
    }

    [Fact]
    public void GetClientById_ExistingIdPassed_ReturnsOkObjectResult()
    {
      var okResult = controller.GetClientById(100);
      Assert.IsType<OkObjectResult>(okResult.Result);
    }

    [Fact]
    public void GetClientById_ExistingIdPassed_ReturnsRightItem()
    {
      var testId = 200;
      var okResult = controller.GetClientById(testId).Result as OkObjectResult;
      Assert.IsType<ClientReadDto>(okResult.Value);
      Assert.Equal(testId, (okResult.Value as ClientReadDto).Id);
    }

    [Fact]
    public void CreateClient_InvalidObjectPassed_ReturnsBadRequestResult()
    {
      var testItem = new ClientCreateDto() { Firstname = "Fernando", Lastname = "", Document = "" };
      controller.ModelState.AddModelError("Lastname", "Required");
      var badResponse = controller.CreateClient(testItem);
      Assert.IsType<BadRequestResult>(badResponse.Result);
    }

    [Fact]
    public void CreateClient_ValidObjectPassed_ReturnsCreatedAtRouteResult()
    {
      var testItem = new ClientCreateDto() { Firstname = "Fernando", Lastname = "Zabala", Document = "123854" };
      var createdResponse = controller.CreateClient(testItem);
      Assert.IsType<CreatedAtRouteResult>(createdResponse.Result);
    }

    [Fact]
    public void CreateClient_ValidObjectPassed_ReturnedResponseHasCreatedItem()
    {
      var testItem = new ClientCreateDto() { Firstname = "Fernando", Lastname = "Zabala", Document = "123854" };

      var createdResponse = controller.CreateClient(testItem).Result as CreatedAtRouteResult;
      var item = createdResponse.Value as ClientReadDto;

      Assert.IsType<ClientReadDto>(item);
      Assert.Equal(testItem.Firstname, item.Firstname);
      Assert.Equal(testItem.Lastname, item.Lastname);
      Assert.Equal(testItem.Document, item.Document);
    }

    [Fact]
    public void UpdateClient_InvalidObjectPassed_ReturnsBadRequestResult()
    {
      int clientId = 100;
      var testItem = new ClientUpdateDto() { Firstname = "Fernando", Lastname = "", Document = "" };
      controller.ModelState.AddModelError("Lastname", "Required");
      var badResponse = controller.UpdateClient(clientId, testItem);
      Assert.IsType<BadRequestResult>(badResponse);
    }

    [Fact]
    public void UpdateClient_ValidObjectPassed_ReturnsNoContentResult()
    {
      int clientId = 100;
      var testItem = new ClientUpdateDto() { Firstname = "Fernando", Lastname = "Zabala", Document = "123854" };
      var createdResponse = controller.UpdateClient(clientId, testItem);
      Assert.IsType<NoContentResult>(createdResponse);
    }

    [Fact]
    public void UpdateClient_ValidObjectPassed_ReturnedResponseHasUpdatedItem()
    {
      int clientId = 100;
      var testItem = new ClientUpdateDto() { Firstname = "Fernando", Lastname = "Zabala", Document = "123854" };
      var createdResponse = controller.UpdateClient(clientId, testItem);
      Assert.IsType<NoContentResult>(createdResponse);

      var okResult = controller.GetClientById(clientId).Result as OkObjectResult;
      Assert.IsType<ClientReadDto>(okResult.Value);
      Assert.Equal(clientId, (okResult.Value as ClientReadDto).Id);

      var item = okResult.Value as ClientReadDto;
      Assert.Equal(clientId, item.Id);
      Assert.IsType<ClientReadDto>(item);
      Assert.Equal(testItem.Firstname, item.Firstname);
      Assert.Equal(testItem.Lastname, item.Lastname);
      Assert.Equal(testItem.Document, item.Document);
    }
  }
}
