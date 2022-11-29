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

        [HttpGet(Name = "GetSaving")]
        public IActionResult Get()
        {
            return Ok(saving.GetSavings());
        }

        [HttpPost(Name = "PostSaving")]
        public IActionResult Post([FromBody] SavingsDto savingsDto)
        {
            var (data, statusCode) = saving.NewSavingProcess(savingsDto.OwnerID, savingsDto);

            return StatusCode(statusCode, data);
        }

        [Route("InserIntoSaving")]
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

                return StatusCode(result._StatusCode, result._Data == null ? result._Message : result._Data);
            }

            return Ok("");
        }

        [Route("DeleteSaving")]
        [HttpDelete]
        public IActionResult CancelSaving([FromBody] bool confirmed, int ownerID, string userSafetyKey)
        {
            var (data, statusCode) = ("", 0);

            if (confirmed)
                (data, statusCode) = saving.CancelSavingProcess(ownerID, userSafetyKey);

            return StatusCode(statusCode, data);
        }

        [Route("ViewBenefits")]
        [HttpPost]
        public IActionResult ViewBenefits([FromBody] int ownerID)
        {
            var result = saving.PrintBenefits(ownerID);

            return StatusCode(result._StatusCode, result._Data == null ? result._Message : result._Data);
        }
    }
}