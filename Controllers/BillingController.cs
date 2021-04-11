using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoMapper;
using BasicBilling.Data.Entities;
using BasicBilling.Data.Enums;
using BasicBilling.Data.Interfaces;
using BasicBilling.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasicBilling.Controllers
{

  [Route("billing")]
  [ApiController]
  public class BillingController : ControllerBase
  {
    private readonly IBasicBillingRepo repository = default!;
    private readonly IMapper mapper = default!;

    public BillingController(IBasicBillingRepo repository, IMapper mapper)
    {
      this.repository = repository;
      this.mapper = mapper;
    }

    [HttpGet("bills/{id}", Name = "GetBillById")]
    public ActionResult<BillReadDto> GetBillById(int id)
    {
      var billEntity = repository.GetBillById(id);

      if (billEntity == null) return NotFound();

      return Ok(mapper.Map<BillReadDto>(billEntity));
    }

    [HttpGet("pending")]
    public ActionResult<IEnumerable<BillReadDto>> GetPendingBillsByClient([FromQuery] int ClientId)
    {
      var billEntities = repository.GetPendingBillsByClient(ClientId);

      return Ok(mapper.Map<IEnumerable<BillReadDto>>(billEntities));
    }

    [HttpPost("bills")]
    public ActionResult<BillReadDto> CreateBill(BillCreateDto billCreateDto)
    {
      var serviceEntity = repository.GetServiceByShortname(billCreateDto.category);

      if (serviceEntity == null) return BadRequest();

      var clientEntity = repository.GetClientById(billCreateDto.ClientId);

      if (clientEntity == null) return BadRequest();

      int period = billCreateDto.period;

      var regex = @"^\d{4}(0[1-9]|1[0-2])$";
      var match = Regex.Match(period.ToString(), regex, RegexOptions.IgnoreCase);

      if (!match.Success) return BadRequest();

      decimal amount = billCreateDto.Amount;

      if(amount <= 0) return BadRequest();

      Bill bill = new Bill();
      bill.Client = clientEntity;
      bill.Service = serviceEntity;
      bill.Period = period;
      bill.Status = BillingStatus.Pending;
      bill.Amount = amount;

      repository.CreateBill(bill);
      repository.SaveChanges();

      var billReadDto = mapper.Map<BillReadDto>(bill);

      return CreatedAtRoute(nameof(GetBillById), new { Id = billReadDto.Id }, billReadDto);
    }
  }
}