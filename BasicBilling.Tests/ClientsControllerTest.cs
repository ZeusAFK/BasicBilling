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
      var response = controller.GetAllClients();
      Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public void GetAllClients_WhenCalled_ReturnsAllItems()
    {
      var response = controller.GetAllClients().Result as OkObjectResult;
      var items = Assert.IsType<List<ClientReadDto>>(response.Value);
      Assert.Equal(5, items.Count);
    }

    [Fact]
    public void GetClientById_UnknowIdPassed_ReturnsNotFoundResult()
    {
      var response = controller.GetClientById(132);
      Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public void GetClientById_ExistingIdPassed_ReturnsOkObjectResult()
    {
      var response = controller.GetClientById(100);
      Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public void GetClientById_ExistingIdPassed_ReturnsRightItem()
    {
      var testId = 200;
      var response = controller.GetClientById(testId).Result as OkObjectResult;
      Assert.IsType<ClientReadDto>(response.Value);
      Assert.Equal(testId, (response.Value as ClientReadDto).Id);
    }

    [Fact]
    public void CreateClient_InvalidObjectPassed_ReturnsBadRequestResult()
    {
      var testItem = new ClientCreateDto() { Firstname = "Fernando", Lastname = "", Document = "" };
      controller.ModelState.AddModelError("Lastname", "Required");
      var response = controller.CreateClient(testItem);
      Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public void CreateClient_ValidObjectPassed_ReturnsCreatedAtRouteResult()
    {
      var testItem = new ClientCreateDto() { Firstname = "Fernando", Lastname = "Zabala", Document = "A1242" };
      var response = controller.CreateClient(testItem);
      Assert.IsType<CreatedAtRouteResult>(response.Result);
    }

    [Fact]
    public void CreateClient_ValidObjectPassed_ReturnsResponseHasCreatedItem()
    {
      var testItem = new ClientCreateDto() { Firstname = "Fernando", Lastname = "Zabala", Document = "A1242" };

      var response = controller.CreateClient(testItem).Result as CreatedAtRouteResult;
      var item = response.Value as ClientReadDto;

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
      var response = controller.UpdateClient(clientId, testItem);
      Assert.IsType<BadRequestResult>(response);
    }

    [Fact]
    public void UpdateClient_ValidObjectPassed_ReturnsNoContentResult()
    {
      int clientId = 100;
      var testItem = new ClientUpdateDto() { Firstname = "Fernando", Lastname = "Zabala", Document = "A1242" };
      var response = controller.UpdateClient(clientId, testItem);
      Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public void UpdateClient_ValidObjectPassed_ReturnedResponseHasUpdatedItem()
    {
      int clientId = 100;
      var testItem = new ClientUpdateDto() { Firstname = "Fernando", Lastname = "Zabala", Document = "A1242" };
      var response = controller.UpdateClient(clientId, testItem);
      Assert.IsType<NoContentResult>(response);

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
