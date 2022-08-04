namespace OmniKassa.Model.Enums
{
    /// <summary>
    /// Contains the supported transaction statuses
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// Transaction was accepted
        /// </summary>
        ACCEPTED,

        /// <summary>
        /// Transaction was cancelled
        /// </summary>
        CANCELLED,

        /// <summary>
        /// Transaction was expired
        /// </summary>
        EXPIRED,

        /// <summary>
        /// Transaction was a failure
        /// </summary>
        FAILURE,

        /// <summary>
        /// Transaction was succeded
        /// </summary>
        SUCCESS
    }
}
