﻿#if NETSTANDARD1_3 || NETSTANDARD2_0 || NETSTANDARD2_1 || NET5_0_OR_GREATER

using OmniKassa.Exceptions;
using OmniKassa.Model;
using OmniKassa.Model.Order;
using OmniKassa.Model.Response;
using OmniKassa.Model.Response.Notification;
using System;
using System.Threading.Tasks;

namespace OmniKassa
{
    public sealed partial class Endpoint
    {
        /// <summary>
        /// Announces the MerchantOrder to OmniKassa and returns the payment URL and OmniKassa order ID
        /// </summary>
        /// <param name="merchantOrder">Merchant order</param>
        /// <returns>Payment URL and OmniKassa order ID</returns>
        public Task<MerchantOrderResponse> Announce(MerchantOrder merchantOrder)
        {
            return AnnounceMerchantOrder(merchantOrder);
        }

        /// <summary>
        /// Announces the MerchantOrder to OmniKassa and returns the payment URL and OmniKassa order ID
        /// </summary>
        /// <param name="merchantOrder">Merchant order</param>
        /// <returns>Payment URL and OmniKassa order ID</returns>
        public async Task<MerchantOrderResponse> AnnounceMerchantOrder(MerchantOrder merchantOrder)
        {
            await ValidateAccessToken();

            try
            {
                return await httpClient.AnnounceMerchantOrder(merchantOrder, tokenProvider.GetAccessToken());
            }
            catch (InvalidAccessTokenException)
            {
                // We might have mistakenly assumed the token was still valid
                await RetrieveNewToken();

                return await httpClient.AnnounceMerchantOrder(merchantOrder, tokenProvider.GetAccessToken());
            }
        }

        /// <summary>
        /// Retrieves the order status data from OmniKassa
        /// </summary>
        /// <param name="notification">Notification received from the webhook</param>
        /// <returns>Order status info</returns>
        public async Task<MerchantOrderStatusResponse> RetrieveAnnouncement(ApiNotification notification)
        {
            notification.ValidateSignature(httpClient.SigningKey);

            MerchantOrderStatusResponse response = await httpClient.GetOrderStatusData(notification);
            return response;
        }

        /// <summary>
        /// Initiates a refund on a transaction and returns the refund and the status of the refund
        /// </summary>
        /// <param name="transactionId">The ID of the transacion to refund</param>
        /// <param name="requestId">Unique value to enforce idempotency</param>
        /// <param name="refundRequest">The refund request with amount and currency to refund. Optional description and Vat category</param>
        /// <returns>Refund status info</returns>
        public async Task<RefundResponse> InitiateRefund(String transactionId, String requestId, RefundRequest refundRequest)
        {
            await ValidateAccessToken();

            try
            {
                return await httpClient.InitiateRefund(transactionId, requestId, refundRequest, tokenProvider.GetAccessToken());
            }
            catch (InvalidAccessTokenException)
            {
                // We might have mistakenly assumed the token was still valid
                await RetrieveNewToken();

                return await httpClient.InitiateRefund(transactionId, requestId, refundRequest, tokenProvider.GetAccessToken());
            }
        }

        /// <summary>
        /// Initiates a refund on a transaction and returns the refund and the status of the refund
        /// </summary>
        /// <param name="transactionId">The ID of the transacion to refund</param>
        /// <param name="refundId">Identifier of the refund</param>
        /// <returns>Refund status info</returns>
        public async Task<RefundResponse> GetRefund(String transactionId, String refundId)
        {
            await ValidateAccessToken();

            try
            {
                return await httpClient.GetRefund(transactionId, refundId, tokenProvider.GetAccessToken());
            }
            catch (InvalidAccessTokenException)
            {
                // We might have mistakenly assumed the token was still valid
                await RetrieveNewToken();

                return await httpClient.GetRefund(transactionId, refundId, tokenProvider.GetAccessToken());
            }
        }

        /// <summary>
        /// Initiates a refund on a transaction and returns the refund and the status of the refund
        /// </summary>
        /// <param name="transactionId">The ID of the transacion to refund</param>
        /// <param name="refundId">Identifier of the refund</param>
        /// <returns>Refund status info</returns>
        public async Task<RefundableDetailsResponse> GetRefundableDetails(String transactionId)
        {
            await ValidateAccessToken();

            try
            {
                return await httpClient.GetRefundableDetails(transactionId, tokenProvider.GetAccessToken());
            }
            catch (InvalidAccessTokenException)
            {
                // We might have mistakenly assumed the token was still valid
                await RetrieveNewToken();

                return await httpClient.GetRefundableDetails(transactionId, tokenProvider.GetAccessToken());
            }
        }

        /// <summary>
        /// Retrieves the available payment brands
        /// </summary>
        /// <returns>Payment brands</returns>
        public async Task<PaymentBrandsResponse> RetrievePaymentBrands()
        {
            await ValidateAccessToken();

            try
            {
                return await httpClient.RetrievePaymentBrands(tokenProvider.GetAccessToken());
            }
            catch (InvalidAccessTokenException)
            {
                // We might have mistakenly assumed the token was still valid
                await RetrieveNewToken();

                return await httpClient.RetrievePaymentBrands(tokenProvider.GetAccessToken());
            }
        }

        /// <summary>
        /// Retrieves the available iDEAL issuers
        /// </summary>
        /// <returns>iDEAL issuers</returns>
        public async Task<IdealIssuersResponse> RetrieveIdealIssuers()
        {
            await ValidateAccessToken();

            try
            {
                return await httpClient.RetrieveIdealIssuers(tokenProvider.GetAccessToken());
            }
            catch (InvalidAccessTokenException)
            {
                // We might have mistakenly assumed the token was still valid
                await RetrieveNewToken();

                return await httpClient.RetrieveIdealIssuers(tokenProvider.GetAccessToken());
            }
        }

        /// <summary>
        /// Retrieves a new access token
        /// </summary>
        public async Task RetrieveNewToken()
        {
            AccessToken retrievedToken = await httpClient.RetrieveNewToken(tokenProvider.GetRefreshToken());
            tokenProvider.SetAccessToken(retrievedToken);
        }

        private async Task ValidateAccessToken()
        {
            if (tokenProvider.HasNoValidAccessToken())
            {
                await RetrieveNewToken();
            }
        }

        /// <summary>
        /// Retrieves the order status data from OmniKassa
        /// </summary>
        /// <param name="notification">Notification received from the webhook</param>
        /// <returns>Order status info</returns>
        public async Task<MerchantOrderStatusResponse> InitiateRefund(ApiNotification notification)
        {
            notification.ValidateSignature(httpClient.SigningKey);

            MerchantOrderStatusResponse response = await httpClient.GetOrderStatusData(notification);
            return response;
        }
    }
}

#endif