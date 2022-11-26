using EasyBankWeb.Dto;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SavingController : ControllerBase
    {
        private readonly Saving saving;
        private readonly MonthTimer monthTimer;
        private readonly SavingRepository _savingRepository;

        public SavingController(Saving _saving, SavingRepository savingRepository)
        {
            saving = _saving;
            _savingRepository = savingRepository;
        }

        [HttpGet(Name = "GetSaving")]
        public IActionResult Get()
        {
            return Ok(_savingRepository.GetSavings());
        }

        [HttpPost(Name = "PostSaving")]
        public IActionResult Post([FromBody] SavingsDto savingsDto)
        {
            if (!saving.HasExistentSaving(savingsDto.OwnerID))
            {
                var newSaving = new SavingEntity(savingsDto.OwnerID, savingsDto.Value, 1, saving.CalculateTaxes(savingsDto.Value, 1));
                _savingRepository.AddSavings(newSaving);
                return Ok();
            }
            return BadRequest("Emprestimo em aberto existente");
        }
    }
}