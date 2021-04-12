using System.Collections.Generic;
using BasicBilling.Controllers;
using BasicBilling.Data.Interfaces;
using BasicBilling.Data.Repositories;
using BasicBilling.Profiles;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BasicBilling.Data.Models;
using BasicBilling.Data.Enums;

namespace BasicBilling.Tests
{
  public class BillingControllerTest
  {
    BillingController controller;
    IBasicBillingRepo repository;
    IMapper mapper;

    public BillingControllerTest()
    {
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new BillingProfile());
      });

      mapper = mappingConfig.CreateMapper();
      repository = new MockBasicBillingRepoImpl();
      controller = new BillingController(repository, mapper);
    }

    [Fact]
    public void GetBillById_UnknowIdPassed_ReturnsNotFoundResult()
    {
      var testId = 359;
      var response = controller.GetBillById(testId);
      Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public void GetBillById_ExistingIdPassed_ReturnsOkObjectResult()
    {
      var response = controller.GetBillById(1);
      Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public void GetBillById_ExistingIdPassed_ReturnsRightItem()
    {
      var testId = 5;
      var response = controller.GetBillById(testId).Result as OkObjectResult;
      Assert.IsType<BillReadDto>(response.Value);
      Assert.Equal(testId, (response.Value as BillReadDto).Id);
    }

    [Fact]
    public void GetPaymentById_UnknowIdPassed_ReturnsNotFoundResult()
    {
      var testId = 952;
      var response = controller.GetPaymentById(testId);
      Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public void GetPaymentById_ExistingIdPassed_ReturnsOkObjectResult()
    {
      var testId = 2;
      var response = controller.GetPaymentById(testId);
      Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public void GetPaymentById_ExistingIdPassed_ReturnsRightItem()
    {
      var testId = 4;
      var response = controller.GetPaymentById(testId).Result as OkObjectResult;
      Assert.IsType<PaymentReadDto>(response.Value);
      Assert.Equal(testId, (response.Value as PaymentReadDto).Id);
    }

    [Fact]
    public void GetPendingBillsByClient_UnknowClientIdPassed_ReturnsEmptyList()
    {
      var testId = 584;
      var response = controller.GetPendingBillsByClient(testId).Result as OkObjectResult;

      Assert.IsType<List<BillReadDto>>(response.Value);
      Assert.Empty(response.Value as List<BillReadDto>);
    }

    [Fact]
    public void GetPendingBillsByClient_ValidClientIdPassed_ReturnsPendingBills()
    {
      var testId = 100;
      var response = controller.GetPendingBillsByClient(testId).Result as OkObjectResult;

      Assert.IsType<List<BillReadDto>>(response.Value);

      List<BillReadDto> bills = response.Value as List<BillReadDto>;
      foreach (BillReadDto bill in bills)
      {
        Assert.Equal(BillingStatus.Pending, bill.Status);
      }
    }

    [Fact]
    public void CreatePyment_InvalidServiceCategoryPassed_ReturnsBadRequestResult()
    {
      PaymentCreateDto payment = new PaymentCreateDto() { ClientId = 100, category = "PHONE", period = 202002 };
      var response = controller.CreatePayment(payment);
      Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public void CreatePyment_InvalidClientIdPassed_ReturnsBadRequestResult()
    {
      PaymentCreateDto payment = new PaymentCreateDto() { ClientId = 555, category = "WATER", period = 202002 };
      var response = controller.CreatePayment(payment);
      Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public void CreatePyment_InvalidPeriodPassed_ReturnsBadRequestResult()
    {
      PaymentCreateDto payment = new PaymentCreateDto() { ClientId = 100, category = "SEWER", period = 200212 };
      var response = controller.CreatePayment(payment);
      Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public void CreatePyment_MalformedPeriodPassed_ReturnsBadRequestResult()
    {
      PaymentCreateDto payment = new PaymentCreateDto() { ClientId = 100, category = "SEWER", period = 15 };
      var response = controller.CreatePayment(payment);
      Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public void CreatePyment_PaidPeriodPassed_ReturnsBadRequestResult()
    {
      PaymentCreateDto payment = new PaymentCreateDto() { ClientId = 100, category = "WATER", period = 202101 };
      var response = controller.CreatePayment(payment);
      Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public void CreatePyment_ValidValuesPassed_ReturnsCreatedAtRouteResult()
    {
      PaymentCreateDto payment = new PaymentCreateDto() { ClientId = 100, category = "ELECTRICITY", period = 202102 };
      var response = controller.CreatePayment(payment);
      Assert.IsType<CreatedAtRouteResult>(response.Result);
    }

    [Fact]
    public void GetPaymentsByServiceShortname_UnknowServiceCategoryPassed_ReturnsBadRequestResult()
    {
      var testValue = "PHONE";
      var response = controller.GetPaymentsByServiceShortname(testValue);
      Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public void GetPaymentsByServiceShortname_ValidServiceCategoryPassed_ReturnsPaidBills()
    {
      var testValue = "WATER";
      var response = controller.GetPaymentsByServiceShortname(testValue).Result as OkObjectResult;

      Assert.IsType<List<PaymentReadDto>>(response.Value);

      List<PaymentReadDto> payments = response.Value as List<PaymentReadDto>;
      foreach (PaymentReadDto payment in payments)
      {
        Assert.Equal(BillingStatus.Paid, payment.Bill.Status);
      }
    }

    [Fact]
    public void CreateBill_InvalidServiceCategoryPassed_ReturnsBadRequestResult()
    {
      BillCreateDto bill = new BillCreateDto() { ClientId = 100, category = "PHONE", period = 202002, Amount = 200 };
      var response = controller.CreateBill(bill);
      Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public void CreateBill_InvalidClientIdPassed_ReturnsBadRequestResult()
    {
      BillCreateDto bill = new BillCreateDto() { ClientId = 555, category = "WATER", period = 202002, Amount = 200 };
      var response = controller.CreateBill(bill);
      Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public void CreateBill_MalformedPeriodPassed_ReturnsBadRequestResult()
    {
      BillCreateDto bill = new BillCreateDto() { ClientId = 100, category = "SEWER", period = 15, Amount = 200 };
      var response = controller.CreateBill(bill);
      Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public void CreateBill_ExistingValuesPassed_ReturnsBadRequestResult()
    {
      BillCreateDto bill = new BillCreateDto() { ClientId = 100, category = "ELECTRICITY", period = 202102, Amount = 200 };
      var response = controller.CreateBill(bill);
      Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public void CreateBill_ValidValuesPassed_ReturnsCreatedAtRouteResult()
    {
      BillCreateDto bill = new BillCreateDto() { ClientId = 100, category = "SEWER", period = 202111, Amount = 200 };
      var response = controller.CreateBill(bill);
      Assert.IsType<CreatedAtRouteResult>(response.Result);
    }
  }
}