using System;
using Newtonsoft.Json;

namespace OmniKassa.Model.Response
{
    public class RefundableDetailsResponse
    {
        /// <summary>
        /// The ID of the transaction
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string TransactionId { get; private set; }

        /// <summary>
        /// The amount of the transaction
        /// </summary>
        [JsonProperty(PropertyName = "refundableMoney")]
        public Money RefundableMoney { get; private set; }
    }
}
