using Application.IServices.IInspectionServices;
using Application.ViewModels.InspectionForm.MaterialInspectResult;
using Application.ViewModels.MainWarehouse;
using AutoMapper;

namespace Application.Services.InspectionServices
{
    public class MaterialInspectResultService : IMaterialInspectResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        public MaterialInspectResultService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public async Task<List<MaterialInspectResultVM>> GetAllByInspectionFormIdAsync(int inspectionFormId)
        {
            var itemList = await _unitOfWork.MaterialInspectResultRepo.GetAllByInspectionFormIdAsync(inspectionFormId);
            var result = _mapper.Map<List<MaterialInspectResultVM>>(itemList);

            return result;
        }
    }
}
