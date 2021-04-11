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
  public class ServicesControllerTest
  {
    ServicesController controller;
    IBasicBillingRepo repository;
    IMapper mapper;

    public ServicesControllerTest()
    {
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new BillingProfile());
      });

      mapper = mappingConfig.CreateMapper();
      repository = new MockBasicBillingRepoImpl();
      controller = new ServicesController(repository, mapper);
    }

    [Fact]
    public void GetAllServices_WhenCalled_ReturnsOkObjectResult()
    {
      var okResult = controller.GetAllServices();
      Assert.IsType<OkObjectResult>(okResult.Result);
    }

    [Fact]
    public void GetAllServices_WhenCalled_ReturnsAllItems()
    {
      var okResult = controller.GetAllServices().Result as OkObjectResult;
      var items = Assert.IsType<List<ServiceReadDto>>(okResult.Value);
      Assert.Equal(3, items.Count);
    }

    [Fact]
    public void GetServiceById_UnknowIdPassed_ReturnsNotFoundResult()
    {
      var notFoundResult = controller.GetServiceById(132);
      Assert.IsType<NotFoundResult>(notFoundResult.Result);
    }

    [Fact]
    public void GetServiceById_ExistingIdPassed_ReturnsOkObjectResult()
    {
      var okResult = controller.GetServiceById(1);
      Assert.IsType<OkObjectResult>(okResult.Result);
    }

    [Fact]
    public void GetServiceById_ExistingIdPassed_ReturnsRightItem()
    {
      var testId = 2;
      var okResult = controller.GetServiceById(testId).Result as OkObjectResult;
      Assert.IsType<ServiceReadDto>(okResult.Value);
      Assert.Equal(testId, (okResult.Value as ServiceReadDto).Id);
    }

    [Fact]
    public void CreateService_InvalidObjectPassed_ReturnsBadRequestResult()
    {
      var testItem = new ServiceCreateDto() { Shortname = "", Name = "" };
      controller.ModelState.AddModelError("Shortname", "Required");
      var badResponse = controller.CreateService(testItem);
      Assert.IsType<BadRequestResult>(badResponse.Result);
    }

    [Fact]
    public void CreateService_ValidObjectPassed_ReturnsCreatedAtRouteResult()
    {
      var testItem = new ServiceCreateDto() { Shortname = "PHONE", Name = "Phone service" };
      var createdResponse = controller.CreateService(testItem);
      Assert.IsType<CreatedAtRouteResult>(createdResponse.Result);
    }

    [Fact]
    public void CreateService_ValidObjectPassed_ReturnedResponseHasCreatedItem()
    {
      var testItem = new ServiceCreateDto() { Shortname = "PHONE", Name = "Phone service" };

      var createdResponse = controller.CreateService(testItem).Result as CreatedAtRouteResult;
      var item = createdResponse.Value as ServiceReadDto;

      Assert.IsType<ServiceReadDto>(item);
      Assert.Equal(testItem.Shortname, item.Shortname);
      Assert.Equal(testItem.Name, item.Name);
    }

    [Fact]
    public void UpdateService_InvalidObjectPassed_ReturnsBadRequestResult()
    {
      int serviceId = 2;
      var testItem = new ServiceUpdateDto() { Shortname = "", Name = "" };
      controller.ModelState.AddModelError("Shortname", "Required");
      var badResponse = controller.UpdateService(serviceId, testItem);
      Assert.IsType<BadRequestResult>(badResponse);
    }

    [Fact]
    public void UpdateService_ValidObjectPassed_ReturnsNoContentResult()
    {
      int serviceId = 2;
      var testItem = new ServiceUpdateDto() { Shortname = "PHONE", Name = "Phone service" };
      var createdResponse = controller.UpdateService(serviceId, testItem);
      Assert.IsType<NoContentResult>(createdResponse);
    }

    [Fact]
    public void UpdateService_ValidObjectPassed_ReturnedResponseHasUpdatedItem()
    {
      int serviceId = 2;
      var testItem = new ServiceUpdateDto() { Shortname = "PHONE", Name = "Phone service" };
      var createdResponse = controller.UpdateService(serviceId, testItem);
      Assert.IsType<NoContentResult>(createdResponse);

      var okResult = controller.GetServiceById(serviceId).Result as OkObjectResult;
      Assert.IsType<ServiceReadDto>(okResult.Value);
      Assert.Equal(serviceId, (okResult.Value as ServiceReadDto).Id);

      var item = okResult.Value as ServiceReadDto;
      Assert.Equal(serviceId, item.Id);
      Assert.IsType<ServiceReadDto>(item);
      Assert.Equal(testItem.Shortname, item.Shortname);
      Assert.Equal(testItem.Name, item.Name);
    }
  }
}
