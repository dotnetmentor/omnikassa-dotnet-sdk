using System;

namespace OmniKassa.Model.Enums
{
    /// <summary>
    /// Contains the supported transaction types
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Transaction of type payment
        /// </summary>
        PAYMENT,
        /// <summary>
        /// Transaction of type authorize
        /// </summary>
        AUTHORIZE
    }
}
