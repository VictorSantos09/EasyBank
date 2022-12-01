using EasyBankWeb.Services;
using static Xunit.Assert;

namespace EasyBank.Test.Services
{
    public class TransferTest
    {
        private readonly Transfer _transfer;

        public TransferTest()
        {
            _transfer = new Transfer(null);
        }

        [Fact]
        public void CheckPix_VerifyTheKeyType_ShouldBeTrue()
        {
            var actual = _transfer.CheckPix("victor@GMAIL.COM");

            (string?, bool) expected = ("EMAIL", true);

            Equal(expected, actual);
        }
        [Fact]
        public void CheckPix_VerifyTheKeyType_ShouldBeFalse()
        {
            var actual = _transfer.CheckPix("victor@gmailcom");

            (string?, bool) expected = (null, false);

            Equal(expected, actual);
        }
    }
}
