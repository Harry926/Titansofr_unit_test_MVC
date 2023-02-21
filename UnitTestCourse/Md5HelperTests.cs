using NUnit.Framework;
using SystemUnderTest;

namespace UnitTestCourse
{
    [TestFixture]
    public class Md5HelperTests
    {
        private Md5Helper _md5Helper;
        private string _input;
        private string _actual;

        [SetUp]
        public void SetUp()
        { 
            _md5Helper = new Md5Helper();
        }
        // 1. write a test for Md5Helper
        // online md5 hash generator: https://www.md5hashgenerator.com/
        [Test]
        public void Hash_InputString_ReturnsMd5HashedString()
        {
            GivenInputString("Harry");
            WhenDoMd5Hash();
            StringShouldBe("db05833c29e688b5ab54d5e8608a72ec");
        }

        private void StringShouldBe(string result)
        {
            Assert.AreEqual(result, _actual);
        }

        private void WhenDoMd5Hash()
        {
            _actual = _md5Helper.Hash(_input);
        }

        private void GivenInputString(string input)
        {
            _input = input;
        }
    }
}