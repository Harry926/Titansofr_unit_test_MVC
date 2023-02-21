using System;
using NSubstitute;
using NUnit.Framework;
using SystemUnderTest;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace UnitTestCourse
{
    [TestFixture]
    public class TitanPayRequestTests
    {
        private TitanPayRequest _titanPayRequest;
        private ITxtMerchKeyReder _txtMerchKeyReder;
        private IDateTimeNow _dateTimeNow;

        [SetUp]
        public void SetUp()
        {
            _txtMerchKeyReder = Substitute.For<ITxtMerchKeyReder>();
            _dateTimeNow = Substitute.For<IDateTimeNow>();
            _titanPayRequest = new TitanPayRequest(_txtMerchKeyReder, _dateTimeNow);
        }
        
        // 3. write tests for TitanPayRequest.Sign()
        [Test]
        public void calculate_signature_with_fixed_key()
        {
            _titanPayRequest.Sign();
            SignatureShouldBe("15b8227044a11d6edd31f0684375f313");
        }

        private void SignatureShouldBe(string result)
        {
            Assert.AreEqual(result, _titanPayRequest.Signature);
        }

        // 4. write unit test for Sign2
        [Test]
        public void calculate_signature_with_key_from_file()
        {
            _txtMerchKeyReder.Get().Returns("abcd1234");
            _titanPayRequest.Sign2();
            SignatureShouldBe("bd5bc5b8be2da176d08ba8322015e437");
        }

        // 5. write unit test for Sign3
        [Test]
        public void calculate_signature_with_created_on()
        {
            _dateTimeNow.Get().Returns(Convert.ToDateTime("2/22/2023 10:12:36 AM"));
            _titanPayRequest.Sign3();
            SignatureShouldBe("7b6ddd1982082dc03f73bbd8b332c4f3");
        }
    }
}