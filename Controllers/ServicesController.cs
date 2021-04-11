using System.Collections.Generic;
using AutoMapper;
using BasicBilling.Data.Entities;
using BasicBilling.Data.Interfaces;
using BasicBilling.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasicBilling.Controllers
{

  [Route("services")]
  [ApiController]
  public class ServicesController : ControllerBase
  {
    private readonly IBasicBillingRepo repository = default!;
    private readonly IMapper mapper = default!;

    public ServicesController(IBasicBillingRepo repository, IMapper mapper)
    {
      this.repository = repository;
      this.mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ClientReadDto>> GetAllServices()
    {
      var clientEntities = repository.GetAllClients();

      return Ok(mapper.Map<IEnumerable<ClientReadDto>>(clientEntities));
    }

    [HttpGet("{id}", Name = "GetServiceById")]
    public ActionResult<ServiceReadDto> GetServiceById(int id)
    {
      var serviceEntity = repository.GetServiceById(id);

      if (serviceEntity == null) return NotFound();

      return Ok(mapper.Map<ServiceReadDto>(serviceEntity));
    }

    [HttpPost()]
    public ActionResult<ServiceReadDto> CreateService(ServiceCreateDto serviceCreateDto)
    {
      var serviceEntity = mapper.Map<Service>(serviceCreateDto);
      repository.CreateService(serviceEntity);
      repository.SaveChanges();

      var serviceReadDto = mapper.Map<ServiceReadDto>(serviceEntity);

      return CreatedAtRoute(nameof(GetServiceById), new { Id = serviceReadDto.Id }, serviceReadDto);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateService(int id, ServiceUpdateDto serviceUpdateDto)
    {
      var serviceEntity = repository.GetServiceById(id);

      if (serviceEntity == null) return NotFound();

      mapper.Map(serviceUpdateDto, serviceEntity);

      repository.UpdateService(serviceEntity);

      repository.SaveChanges();

      return NoContent();
    }
  }
}