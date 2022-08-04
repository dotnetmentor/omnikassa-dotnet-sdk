using System;
using Newtonsoft.Json;
using OmniKassa.Model.Enums;
using OmniKassa.Utils;

namespace OmniKassa.Model
{
    public class Refund
    {
        /// <summary>
        /// The ID of the refund
        /// </summary>
        [JsonProperty(PropertyName = "refundId")]
        public string RefundId { get; set; }


        /// <summary>
        /// ID of a specific refund transaction
        /// </summary>
        [JsonProperty(PropertyName = "refundTransactionId")]
        public string RefundTransactionId { get; set; }

        /// <summary>
        /// Start date of the refund
        /// </summary>
        [JsonProperty(PropertyName = "createdAt")]
#pragma warning disable 0649
        private String createdAt;
#pragma warning restore 0649
        public DateTime CreatedAt
        {
            get
            {
                return DateTimeUtils.StringToDate(createdAt);
            }
        }

        /// <summary>
        /// Datetime of latest update to refund
        /// </summary>
        [JsonProperty(PropertyName = "updatedAt")]
#pragma warning disable 0649
        private String updatedAt;
#pragma warning restore 0649
        public DateTime UpdatedAt
        {
            get
            {
                return DateTimeUtils.StringToDate(updatedAt);
            }
        }

        /// <summary>
        /// The amount of the transaction
        /// </summary>
        [JsonProperty(PropertyName = "refundMoney")]
        public Money RefundMoney { get; private set; }

        /// <summary>
        /// The payment brand of the transaction
        /// </summary>
        [JsonProperty(PropertyName = "vatCategory")]
        [JsonConverter(typeof(EnumJsonConverter<VatCategory>))]
        public VatCategory? VatCategory { get; private set; }


        /// <summary>
        /// The payment brand of the transaction
        /// </summary>
        [JsonProperty(PropertyName = "paymentBrand")]
        [JsonConverter(typeof(EnumJsonConverter<PaymentBrand>))]
        public PaymentBrand? PaymentBrand { get; private set; }

        /// <summary>
        /// The status of the transaction
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(EnumJsonConverter<TransactionStatus>))]
        public TransactionStatus TransactionStatus { get; private set; }

        /// <summary>
        /// Description of the refund
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// The ID of the transaction
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string TransactionId { get; private set; }

    }
}
