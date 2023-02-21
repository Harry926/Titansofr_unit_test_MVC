using System;
using SystemUnderTest;

namespace UnitTestCourse
{
    public static  class TiPayRequestDefult
    {
        public static TiPayRequest DefaultTiPayRequest(Action<TiPayRequest> overrides = null)
        {
            var payRequest = new TiPayRequest()
            {
                CreatedOn = DateTime.Now,
                MerchantCode = "Harry",
                Signature = "",
                Amount = 1000
            };
            overrides?.Invoke(payRequest);
            return payRequest;
        }
    }
}