using Application.IServices;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductInPlanService : IProductInPlanService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IClaimsService _claimsService;

        public ProductInPlanService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _claimsService = claimsService;
        }
    }
}
