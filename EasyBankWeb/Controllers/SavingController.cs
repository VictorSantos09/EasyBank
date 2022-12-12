using EasyBankWeb.Dto;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SavingController : ControllerBase
    {
        private readonly Saving saving;

        public SavingController(Saving _saving)
        {
            saving = _saving;
        }

        [Route("GetAll")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(saving.GetAll());
        }

        [Route("NewSaving")]
        [HttpPost]
        public IActionResult Post([FromBody] SavingsDto savingsDto)
        {
            var (message, statusCode) = saving.NewSavingProcess(savingsDto.OwnerID, savingsDto);

            return StatusCode(statusCode, new { Message = message });
        }

        [Route("InsertIntoSaving")]
        [HttpPost]
        public IActionResult InsertMoney([FromBody] InsertSavingDto insertSavingDto)
        {
            if (insertSavingDto.Confirmed)
            {
                var (data, statusCode) = saving.InsertMoneyProcess(insertSavingDto.OwnerID, insertSavingDto.Value);

                return StatusCode(statusCode, data);
            }

            return Ok("Solicitação cancelada");
        }

        [Route("RescueMoney")]
        [HttpPost]
        public IActionResult RescueMoney([FromBody] RescueDto rescueDto)
        {
            if (rescueDto.Confirmed)
            {
                var result = saving.RescueMoneyProcess(rescueDto.OwnerID, rescueDto.AllMoney, rescueDto.Value);

                return StatusCode(result._StatusCode, result._Data == null ? new { result._Message } : result._Data);
            }

            return Ok("");
        }

        [Route("DeleteSaving")]
        [HttpDelete]
        public IActionResult CancelSaving([FromBody] DeleteSavingDto deleteSavingDto)
        {
            var (data, statusCode) = ("", 0);

            if (deleteSavingDto.Confirmed)
                (data, statusCode) = saving.CancelSavingProcess(deleteSavingDto.OwnerID, deleteSavingDto.UserSafetyKey);

            return StatusCode(statusCode, data);
        }

        [Route("ViewBenefits")]
        [HttpPost]
        public IActionResult ViewBenefits([FromBody] ViewBenefitsDto benefitsDto)
        {
            var result = saving.PrintBenefits(benefitsDto.UserID);

            return StatusCode(result._StatusCode, result._Data == null ? new { Message = result._Message } : result._Data);
        }
    }
}