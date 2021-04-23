using System.Collections.Generic;
using BasicBilling.Controllers;
using BasicBilling.Data.Interfaces;
using BasicBilling.Data.Repositories;
using BasicBilling.Profiles;
using Moq;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BasicBilling.Data.Models;
using BasicBilling.Data.Enums;
using BasicBilling.Data.Entities;

namespace BasicBilling.Tests
{
  public class BillingControllerMoqTest
  {
    BillingController controller;
    IMapper mapper;

    private Mock<IBasicBillingRepo> basicBillingRepoMock;

    public BillingControllerMoqTest()
    {
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new BillingProfile());
      });

      mapper = mappingConfig.CreateMapper();
      basicBillingRepoMock = new Mock<IBasicBillingRepo>();
      controller = new BillingController(basicBillingRepoMock.Object, mapper);
    }

    [Fact]
    public void GetBillById_UnknowIdPassed_ReturnsNotFoundResult()
    {
      basicBillingRepoMock.Setup(p => p.GetBillById(It.IsAny<int>())).Returns<Bill>(null);

      var testId = 359;
      var response = controller.GetBillById(testId);

      basicBillingRepoMock.Verify(s => s.GetBillById(testId), Times.Once);
      Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public void GetBillById_ExistingIdPassed_ReturnsOkObjectResult()
    {
      var testId = 1;
      basicBillingRepoMock.Setup(p => p.GetBillById(testId)).Returns(new Bill() { Id = testId });

      var response = controller.GetBillById(testId);

      Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public void GetBillById_ExistingIdPassed_ReturnsRightItem()
    {
      var testId = 5;
      basicBillingRepoMock.Setup(p => p.GetBillById(testId)).Returns(new Bill() { Id = testId });

      var response = controller.GetBillById(testId).Result as OkObjectResult;

      Assert.IsType<BillReadDto>(response.Value);
      Assert.Equal(testId, (response.Value as BillReadDto).Id);
    }

    [Fact]
    public void GetPaymentById_UnknowIdPassed_ReturnsNotFoundResult()
    {
      var testId = 952;
      basicBillingRepoMock.Setup(p => p.GetPaymentById(It.IsAny<int>())).Returns<Payment>(null);

      var response = controller.GetPaymentById(testId);

      Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public void GetPaymentById_ExistingIdPassed_ReturnsOkObjectResult()
    {
      var testId = 2;
      basicBillingRepoMock.Setup(p => p.GetPaymentById(It.IsAny<int>())).Returns(new Payment() { Id = testId });

      var response = controller.GetPaymentById(testId);
      Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public void GetPaymentById_ExistingIdPassed_ReturnsRightItem()
    {
      var testId = 4;
      basicBillingRepoMock.Setup(p => p.GetPaymentById(It.IsAny<int>())).Returns(new Payment() { Id = testId });

      var response = controller.GetPaymentById(testId).Result as OkObjectResult;

      Assert.IsType<PaymentReadDto>(response.Value);
      Assert.Equal(testId, (response.Value as PaymentReadDto).Id);
    }

    [Fact]
    public void GetPendingBillsByClient_UnknowClientIdPassed_ReturnsEmptyList()
    {
      var testId = 584;
      basicBillingRepoMock.Setup(p => p.GetPendingBillsByClient(It.IsAny<int>())).Returns(new List<Bill>());

      var response = controller.GetPendingBillsByClient(testId).Result as OkObjectResult;

      Assert.IsType<List<BillReadDto>>(response.Value);
      Assert.Empty(response.Value as List<BillReadDto>);
    }

    [Fact]
    public void GetPendingBillsByClient_ValidClientIdPassed_ReturnsPendingBills()
    {
      var testId = 100;
      basicBillingRepoMock.Setup(p => p.GetPendingBillsByClient(testId)).Returns(new List<Bill>() { new Bill() { Status = BillingStatus.Pending } });

      var response = controller.GetPendingBillsByClient(testId).Result as OkObjectResult;

      Assert.IsType<List<BillReadDto>>(response.Value);

      List<BillReadDto> bills = response.Value as List<BillReadDto>;
      foreach (BillReadDto bill in bills)
      {
        Assert.Equal(BillingStatus.Pending, bill.Status);
      }
    }
  }
}