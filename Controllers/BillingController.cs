using AutoMapper;
using BasicBilling.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BasicBilling.Controllers{

  [Route("billing")]
  [ApiController]
  public class BillingController : ControllerBase{

    private readonly ILogger<BillingController> _logger = default!;
    
    private readonly IBasicBillingRepo repository = default!;
    
    private readonly IMapper mapper = default!;

    public BillingController(IBasicBillingRepo repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
  }
}