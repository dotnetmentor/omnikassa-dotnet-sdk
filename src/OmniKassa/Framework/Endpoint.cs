#if NET452

using OmniKassa.Exceptions;
using OmniKassa.Model;
using OmniKassa.Model.Order;
using OmniKassa.Model.Response;
using OmniKassa.Model.Response.Notification;
using System;

namespace OmniKassa
{
    public sealed partial class Endpoint
    {
        /// <summary>
        /// Announces the MerchantOrder to OmniKassa and returns the payment URL and OmniKassa order ID
        /// </summary>
        /// <param name="merchantOrder">Merchant order</param>
        /// <returns>Holder object containing payment URL and OmniKassa order ID</returns>
        public MerchantOrderResponse Announce(MerchantOrder merchantOrder)
        {
            ValidateAccessToken();

            try
            {
                return httpClient.AnnounceMerchantOrder(merchantOrder, tokenProvider.GetAccessToken());
            }
            catch (InvalidAccessTokenException)
            {
                // We might have mistakenly assumed the token was still valid
                RetrieveNewToken();

                return httpClient.AnnounceMerchantOrder(merchantOrder, tokenProvider.GetAccessToken());
            }
        }

        /// <summary>
        /// Announces the MerchantOrder to OmniKassa and returns the payment URL
        /// </summary>
        /// <param name="merchantOrder">Merchant order</param>
        /// <returns>Payment URL</returns>
        [Obsolete("AnnounceMerchantOrder is deprecated, please use Announce instead.", false)]
        public string AnnounceMerchantOrder(MerchantOrder merchantOrder)
        {
            return Announce(merchantOrder).RedirectUrl;
        }

        /// <summary>
        /// Retrieves the order status data from OmniKassa
        /// </summary>
        /// <param name="notification">Notification received from the webhook</param>
        /// <returns>Order status info</returns>
        public MerchantOrderStatusResponse RetrieveAnnouncement(ApiNotification notification)
        {
            notification.ValidateSignature(httpClient.SigningKey);

            MerchantOrderStatusResponse response = httpClient.GetOrderStatusData(notification);
            return response;
        }

        /// <summary>
        /// Initiates a refund on a transaction and returns the refund and the status of the refund
        /// </summary>
        /// <param name="transactionId">The ID of the transacion to refund</param>
        /// <param name="requestId">Unique value to enforce idempotency</param>
        /// <param name="refundRequest">The refund request with amount and currency to refund. Optional description and Vat category</param>
        /// <returns>Refund status info</returns>
        public RefundResponse InitiateRefund(String transactionId, String requestId, RefundRequest refundRequest)
        {
            ValidateAccessToken();

            try
            {
                return httpClient.InitiateRefund(transactionId, requestId, refundRequest, tokenProvider.GetAccessToken());
            }
            catch (InvalidAccessTokenException)
            {
                // We might have mistakenly assumed the token was still valid
                RetrieveNewToken();

                return httpClient.InitiateRefund(transactionId, requestId, refundRequest, tokenProvider.GetAccessToken());
            }
        }

        /// <summary>
        /// Initiates a refund on a transaction and returns the refund and the status of the refund
        /// </summary>
        /// <param name="transactionId">The ID of the transacion to refund</param>
        /// <param name="refundId">Identifier of the refund</param>
        /// <returns>Refund status info</returns>
        public RefundResponse GetRefund(String transactionId, String refundId)
        {
            ValidateAccessToken();

            try
            {
                return httpClient.GetRefund(transactionId, refundId, tokenProvider.GetAccessToken());
            }
            catch (InvalidAccessTokenException)
            {
                // We might have mistakenly assumed the token was still valid
                RetrieveNewToken();

                return httpClient.GetRefund(transactionId, refundId, tokenProvider.GetAccessToken());
            }
        }

        /// <summary>
        /// Retrieves the refundable details of given transaction Id
        /// </summary>
        /// <param name="transactionId">The ID of the transacion to fetch refundable details</param>
        /// <returns>Refund status info</returns>
        public RefundableDetailsResponse GetRefundableDetails(String transactionId)
        {
            ValidateAccessToken();

            try
            {
                return httpClient.GetRefundableDetails(transactionId, tokenProvider.GetAccessToken());
            }
            catch (InvalidAccessTokenException)
            {
                // We might have mistakenly assumed the token was still valid
                RetrieveNewToken();

                return httpClient.GetRefundableDetails(transactionId, tokenProvider.GetAccessToken());
            }
        }

        /// <summary>
        /// Retrieves the available payment brands
        /// </summary>
        /// <returns>Payment brands</returns>
        public PaymentBrandsResponse RetrievePaymentBrands()
        {
            ValidateAccessToken();

            try
            {
                return httpClient.RetrievePaymentBrands(tokenProvider.GetAccessToken());
            }
            catch (InvalidAccessTokenException)
            {
                // We might have mistakenly assumed the token was still valid
                RetrieveNewToken();

                return httpClient.RetrievePaymentBrands(tokenProvider.GetAccessToken());
            }
        }

        /// <summary>
        /// Retrieves the available iDEAL issuers
        /// </summary>
        /// <returns>iDEAL issuers</returns>
        public IdealIssuersResponse RetrieveIdealIssuers()
        {
            ValidateAccessToken();

            try
            {
                return httpClient.RetrieveIdealIssuers(tokenProvider.GetAccessToken());
            }
            catch (InvalidAccessTokenException)
            {
                // We might have mistakenly assumed the token was still valid
                RetrieveNewToken();

                return httpClient.RetrieveIdealIssuers(tokenProvider.GetAccessToken());
            }
        }

        /// <summary>
        /// Retrieves a new access token
        /// </summary>
        public void RetrieveNewToken()
        {
            AccessToken retrievedToken = httpClient.RetrieveNewToken(tokenProvider.GetRefreshToken());
            tokenProvider.SetAccessToken(retrievedToken);
        }

        private void ValidateAccessToken()
        {
            if (tokenProvider.HasNoValidAccessToken())
            {
                RetrieveNewToken();
            }
        }
    }
}

#endif