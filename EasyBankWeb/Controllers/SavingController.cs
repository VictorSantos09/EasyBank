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
        private readonly MonthTimer monthTimer;
        private readonly SavingRepository _savingRepository;
        private readonly UserValidator userValidator;

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
                var newSaving = new SavingEntity(savingsDto.OwnerID, savingsDto.Value, saving.IncrementID(),
                    saving.CalculateTaxes(savingsDto.Value, 1));

                _savingRepository.AddSavings(newSaving);

                saving.DiscountMoneyFromUser(savingsDto.OwnerID, savingsDto.Value);
                
                return Ok();
            }

            return BadRequest("Usuario com empréstimo aberto existente");
        }

        [Route("InserIntoSaving")]
        [HttpPost]
        public IActionResult PostInsertMoney([FromBody] InsertSavingDto insertSavingDto)
        {
            if (insertSavingDto.Confirmed)
                saving.InsertMoney(insertSavingDto.OwnerID, insertSavingDto.Value);

            return Ok();
        }

        [HttpPost(Name = "RescueMoney")]
        private IActionResult RescueMoney([FromBody] RescueDto rescueDto)
        {
            if (rescueDto.Confirmed == true)
                saving.RescueMoney(rescueDto.OwnerID, rescueDto.AllMoney, rescueDto.Value);

            return Ok();
        }

        [HttpDelete(Name = "DeleteSaving")]
        private IActionResult DeleteSaving([FromBody] bool confirmed, int ownerID, string userSafetyKey)
        {
            if (confirmed)
                saving.CancelSaving(ownerID, userSafetyKey);

            return Ok();
        }

        [HttpPost(Name = "ViewBenefits")]
        private IActionResult ViewBenefits([FromBody] int ownerID)
        {
            var result = saving.PrintBenefits(ownerID);

            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}