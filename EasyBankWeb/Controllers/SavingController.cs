using EasyBankWeb.Crosscutting;
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
            if (saving.HasExistentSaving(savingsDto.OwnerID))
                return BadRequest("Poupança já existente");

            if (saving.CreateNewSaving(savingsDto))
                return Ok("Poupança criada com sucesso");
            
            return BadRequest("Saldo insuficiente");
        }

        [Route("InserIntoSaving")]
        [HttpPost]
        public IActionResult InsertMoney([FromBody] InsertSavingDto insertSavingDto)
        {
            if (insertSavingDto.Confirmed)
            {
                if (!saving.SucessInsertMoney(insertSavingDto.OwnerID, insertSavingDto.Value))
                    return BadRequest("Dinheiro insuficiente");
            }

            return Ok("Dinheiro aplicado com sucesso");
        }

        [Route("RescueMoney")]
        [HttpPost]
        public IActionResult RescueMoney([FromBody] RescueDto rescueDto)
        {
            if (rescueDto.Confirmed)
                saving.RescueMoney(rescueDto.OwnerID, rescueDto.AllMoney, rescueDto.Value);

            return Ok("Concluido");
        }

        [Route("DeleteSaving")]
        [HttpDelete]
        public IActionResult CancelSaving([FromBody] bool confirmed, int ownerID, string userSafetyKey)
        {
            if (confirmed)
                saving.SucessCancelSaving(ownerID, userSafetyKey);

            return Ok("Poupança cancelada");
        }

        [Route("ViewBenefits")]
        [HttpPost]
        public IActionResult ViewBenefits([FromBody] int ownerID)
        {
            var result = saving.PrintBenefits(ownerID);

            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}