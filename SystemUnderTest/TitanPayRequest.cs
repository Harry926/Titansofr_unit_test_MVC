using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SystemUnderTest
{
    /* TitanPay 是鈦坦科技的支付, Pchome和鈦坦簽約後，鈦坦提供Pchome一組MerchantCode與MerchantKey
     *
     * PChome在對TitanPay的Server API發出請求時，要將Signature填入
     *
     * Md5("{MerchantCode}{Amount}{MerchantKey}")
     *
     * 其中Amount每三個零要包含一個逗點, e.g. 1000 => 1,000
     *
     * md5: https://www.md5hashgenerator.com/
     */

    public interface ITxtMerchKeyReder
    {
        string Get();
    }

    public class TxtMerchKeyReder : ITxtMerchKeyReder
    {
        public string Get()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                @"Data\key.txt");
            var merchantKey = File.ReadLines(path).First();
            return merchantKey;
        }
    }


    public interface IDateTimeNow
    {
        DateTime Get();
    }

    public class DateTimeNow : IDateTimeNow
    {
        public DateTime Get()
        {
            return DateTime.Now;
        }
    }

    public class TitanPayRequest
    {
        public TitanPayRequest(ITxtMerchKeyReder reder, IDateTimeNow date)
        {
            _txtMerchKeyReder = reder;
            _dateTimeNow = date;
        }
        private readonly ITxtMerchKeyReder _txtMerchKeyReder;
        private readonly IDateTimeNow _dateTimeNow;
        private string MerchantCode => "pchome";
        public int Amount { get; set; }
        public string Signature { get; private set; }
        public DateTime CreatedOn { get; private set; }
        
        public void Sign()
        {
            const string merchantKey = "asdf1234";
            var beforeHash = $"{MerchantCode}{Amount:n0}{merchantKey}";

            Signature = new Md5Helper().Hash(beforeHash);
        }

        public void Sign2()
        {
            var merchantKey = _txtMerchKeyReder.Get();
            var beforeHash = $"{MerchantCode}{Amount:n0}{merchantKey}";

            Signature = new Md5Helper().Hash(beforeHash);
        }


        public void Sign3()
        {
            CreatedOn = _dateTimeNow.Get();

            const string merchantKey = "asdf1234";
            var beforeHash = $"{MerchantCode}{Amount:n0}{merchantKey}{CreatedOn:yyyy-MM-ddTHH:mm:ss}";

            Signature = new Md5Helper().Hash(beforeHash);
        }
    }
}