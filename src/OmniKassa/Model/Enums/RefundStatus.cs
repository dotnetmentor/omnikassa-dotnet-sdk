namespace OmniKassa.Model.Enums
{
    /// <summary>
    /// Contains the supported transaction statuses
    /// </summary>
    public enum RefundStatus
    {
        /// <summary>
        /// Refund succeeded
        /// </summary>
        SUCCEEDED,

        /// <summary>
        /// Refund is pending
        /// </summary>
        PENDING,

        /// <summary>
        /// Refund failed
        /// </summary>
        FAILED,

        /// <summary>
        /// Refund status is unknown
        /// </summary>
        UNKNOWN
    }
}
