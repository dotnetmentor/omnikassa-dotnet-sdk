using System;
using Newtonsoft.Json;
using OmniKassa.Model.Enums;
using OmniKassa.Utils;

namespace OmniKassa.Model
{
    //  "transactionId": "22b36073-57a3-4c3d-9585-87f2e55275a5",
    //           "paymentBrand": "IDEAL",
    //           "transactionType": "AUTHORIZE",
    //           "transactionStatus": "SUCCESS",
    //           "amount": {
    //             "amount": 10997,
    //             "currency": "EUR"
    //           },
    //           "confirmedAmount": {
    //             "amount": 10997,
    //             "currency": "EUR"
    //           },
    //           "startDateTime": "2018-03-20T09:12:28Z",
    //           "lastUpdateTime": "2018-03-20T09:12:28Z"

    public class Transaction
    {
        /// <summary>
        /// The ID of the transaction
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string TransactionId { get; private set; }

        /// <summary>
        /// The payment brand of the transaction
        /// </summary>
        [JsonProperty(PropertyName = "paymentBrand")]
        [JsonConverter(typeof(EnumJsonConverter<PaymentBrand>))]
        public PaymentBrand? PaymentBrand { get; private set; }

        /// <summary>
        /// The type of the transaction, Payment or Authorize
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(EnumJsonConverter<TransactionType>))]
        public TransactionType TransactionType { get; private set; }

        /// <summary>
        /// The status of the transaction
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(EnumJsonConverter<TransactionStatus>))]
        public TransactionStatus TransactionStatus { get; private set; }

        /// <summary>
        /// The amount of the transaction
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public Money Amount { get; private set; }

        /// <summary>
        /// The confirmed amount of the transaction
        /// </summary>
        [JsonProperty(PropertyName = "confirmedAmount")]
        public Money ConfirmedAmount { get; private set; }

        [JsonProperty(PropertyName = "startTime")]
#pragma warning disable 0649
        private String startTime;
#pragma warning restore 0649

        /// <summary>
        /// Start date of the transaction
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return DateTimeUtils.StringToDate(startTime);
            }
        }

        [JsonProperty(PropertyName = "lastUpdateTime")]
#pragma warning disable 0649
        private String lastUpdateTime;
#pragma warning restore 0649

        /// <summary>
        /// When the transaction was last updated
        /// </summary>
        public DateTime LastUpdateTime
        {
            get
            {
                return DateTimeUtils.StringToDate(lastUpdateTime);
            }
        }
    }
}
