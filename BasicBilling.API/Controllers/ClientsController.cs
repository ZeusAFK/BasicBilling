using System.Collections.Generic;
using AutoMapper;
using BasicBilling.Data.Entities;
using BasicBilling.Data.Interfaces;
using BasicBilling.Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BasicBilling.Controllers
{
  [Route("clients")]
  [ApiController]
  public class ClientsController : ControllerBase
  {
    private readonly IBasicBillingRepo repository = default!;
    private readonly IMapper mapper = default!;

    public ClientsController(IBasicBillingRepo repository, IMapper mapper)
    {
      this.repository = repository;
      this.mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ClientReadDto>> GetAllClients()
    {
      var clientEntities = repository.GetAllClients();

      return Ok(mapper.Map<IEnumerable<ClientReadDto>>(clientEntities));
    }

    [HttpGet("{id}", Name = "GetClientById")]
    public ActionResult<ClientReadDto> GetClientById(int id)
    {
      var clientEntity = repository.GetClientById(id);

      if (clientEntity == null) return NotFound();

      return Ok(mapper.Map<ClientReadDto>(clientEntity));
    }

    [HttpPost()]
    public ActionResult<ClientReadDto> CreateClient(ClientCreateDto clientCreateDto)
    {
      if (clientCreateDto.Firstname.Equals("") || clientCreateDto.Lastname.Equals("") || clientCreateDto.Document.Equals(""))
        return BadRequest();

      var clientEntity = mapper.Map<Client>(clientCreateDto);
      repository.CreateClient(clientEntity);
      repository.SaveChanges();

      var clientReadDto = mapper.Map<ClientReadDto>(clientEntity);

      return CreatedAtRoute(nameof(GetClientById), new { Id = clientReadDto.Id }, clientReadDto);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateClient(int id, ClientUpdateDto clientUpdateDto)
    {
      if (clientUpdateDto.Firstname.Equals("") || clientUpdateDto.Lastname.Equals("") || clientUpdateDto.Document.Equals(""))
        return BadRequest();

      var clientEntity = repository.GetClientById(id);

      if (clientEntity == null) return NotFound();

      mapper.Map(clientUpdateDto, clientEntity);

      repository.UpdateClient(clientEntity);

      repository.SaveChanges();

      return NoContent();
    }
  }
}