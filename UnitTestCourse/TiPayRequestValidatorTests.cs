using System;
using NSubstitute;
using NUnit.Framework;
using SystemUnderTest;

namespace UnitTestCourse
{
    [TestFixture]
    public class TiPayRequestValidatorTests
    {
        private TiPayRequestValidator _tiPayRequestValidator;
        private IMerchantRepository _merchantRepository;
        private TiPayRequest _tiPayRequest;
        private TestDelegate _action;

        [SetUp]
        public void SetUp()
        {
            _merchantRepository = Substitute.For<IMerchantRepository>();
            _tiPayRequestValidator = new TiPayRequestValidator(_merchantRepository);
        }
        
        // 2. write unit tests for TiPayRequestValidator
        [Test]
        public void Validate_HashedStringIsEqualToWithSignature_ThrowException()
        {
            GivenMerchantKey("1234");
            GivenTiPayRequest(
                100, 
                "Harry",
                "787fa920bf2bcf1ce5393b0513b6ada7");
            
            WhenValidate();
            Assert.DoesNotThrow(_action);
        }
        [Test]
        public void Validate_HashedStringNotEqualToWithSignature_ThrowException()
        {
            GivenMerchantKey("1234");
            GivenTiPayRequest(
                100, 
                "Harry",
                "787fa920bf2bcf1ce5");
            
            WhenValidate();
            Assert.Throws<Exception>(_action);
        }

        private void WhenValidate()
        {
            _action = () => _tiPayRequestValidator.Validate(_tiPayRequest);
        }

        private void GivenMerchantKey(string key)
        {
            _merchantRepository.GetMerchantKey(Arg.Any<String>()).Returns(key);
        }

        private void GivenTiPayRequest(int amount, string MerchantCode, string Signature)
        {
            _tiPayRequest = TiPayRequestDefult.DefaultTiPayRequest(x =>
            {
                x.Amount = amount;
                x.MerchantCode = MerchantCode;
                x.Signature = Signature;
            });
        }
    }
}