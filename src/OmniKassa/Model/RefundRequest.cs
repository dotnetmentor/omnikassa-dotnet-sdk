using System;
using Newtonsoft.Json;
using OmniKassa.Model.Enums;

namespace OmniKassa.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RefundRequest
    {
        /// <summary>
        /// Amount to be refunded to the consumer
        /// </summary>
        [JsonProperty(PropertyName = "money")]
        public Money Amount { get; private set; }

        /// <summary>
        /// Reason for refund request. This field is required for partial refunds of payment transactions with payment brand Afterpay.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public String Description { get; private set; }

        /// <summary>
        /// The VAT category of the product. The values refer to the different rates used in the Netherlands. This field is required for partial refunds of payment transactions with payment brand Afterpay.
        /// </summary>
        [JsonProperty(PropertyName = "vatCategory")]
        [JsonConverter(typeof(EnumJsonConverter<VatCategory>))]
        public VatCategory VatCategory { get; private set; }

        /// <summary>
        /// Initializes an empty RefundRequest
        /// </summary>
        public RefundRequest()
        {

        }

        /// <summary>
        /// Initializes a RefundRequest using the Builder
        /// </summary>
        /// <param name="builder">Builder</param>
        private RefundRequest(Builder builder)
        {
            this.Amount = builder.Amount;
            this.Description = builder.Description;
            this.VatCategory = builder.VatCategory;
        }

        /// <summary>
        /// RefundRequest builder
        /// </summary>
        public class Builder
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            public Money Amount { get; private set; }
            public String Description { get; private set; }
            public VatCategory VatCategory { get; private set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

            /// <summary>
            /// Amount to be refunded to the consumer
            /// </summary>
            /// <param name="amount">Amount</param>
            /// <returns>Builder</returns>
            public Builder WithAmount(Money amount)
            {
                this.Amount = amount;
                return this;
            }

            /// <summary>
            /// Reason for refund request. This field is required for partial refunds of payment transactions with payment brand Afterpay.
            /// </summary>
            /// <param name="description">Description</param>
            /// <returns>Builder</returns>
            public Builder WithDescription(String description)
            {
                this.Description = description;
                return this;
            }

            /// <summary>
            /// The VAT category of the product. The values refer to the different rates used in the Netherlands. This field is required for partial refunds of payment transactions with payment brand Afterpay.
            /// Available options are: "HIGH", "LOW", "ZERO"
            /// </summary>
            /// <param name="vatCategory">Vat category</param>
            /// <returns>Builder</returns>
            public Builder WithVatCategory(VatCategory vatCategory)
            {
                this.VatCategory = vatCategory;
                return this;
            }

            /// <summary>
            /// Initializes and returns a RefundRequest
            /// </summary>
            /// <returns>MerchantOrder</returns>
            public RefundRequest Build()
            {
                return new RefundRequest(this);
            }
        }
    }
}




