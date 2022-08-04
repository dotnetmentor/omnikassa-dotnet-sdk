#if NETSTANDARD1_3 || NETSTANDARD2_0 || NETSTANDARD2_1 || NET5_0_OR_GREATER

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using OmniKassa.Model;
using OmniKassa.Model.Order;
using OmniKassa.Model.Response;
using OmniKassa.Model.Response.Notification;

namespace OmniKassa.Http
{
    /// <summary>
    /// OmniKassa API client functions
    /// </summary>
    public sealed partial class OmniKassaHttpClient
    {
        private void InitCertificate()
        {

        }

        /// <summary>
        /// Announces the MerchantOrder to OmniKassa and returns the payment URL
        /// </summary>
        /// <param name="order">Merchant order</param>
        /// <param name="token">Access token</param>
        /// <returns>Response with payment URL</returns>
        public Task<MerchantOrderResponse> AnnounceMerchantOrder(MerchantOrder order, String token)
        {
            DateTime now = DateTime.Now;
            order.Timestamp = now.ToString("s") + now.ToString("zzz");
            return PostAsync<MerchantOrderResponse>(mClient, PATH_ANNOUNCE_ORDER, token, order);
        }

        /// <summary>
        /// Retrieves the order status data from OmniKassa
        /// </summary>
        /// <param name="notification">Notification received from the webhook</param>
        /// <returns>Order status info</returns>
        public Task<MerchantOrderStatusResponse> GetOrderStatusData(ApiNotification notification)
        {
            return GetAsync<MerchantOrderStatusResponse>(mClient,
                                       PATH_GET_ORDER_STATUS + notification.EventName,
                                       notification.Authentication);
        }

        /// <summary>
        /// Initiates a refund on a transaction and returns the refund and the status of the refund
        /// </summary>
        /// <param name="transactionId">The ID of the transacion to refund</param>
        /// <param name="requestId">Unique value to enforce idempotency</param>
        /// <param name="refundRequest">The refund request with amount and currency to refund. Optional description and Vat category</param>
        /// <param name="token">Access token</param>
        /// <returns>Refund status info</returns>
        public Task<RefundResponse> InitiateRefund(String transactionId, String requestId, RefundRequest refundRequest, String token)
        {
            var additionalHeaders = new Dictionary<string, string> { { "Request-ID", requestId } };
            return PostAsync<RefundResponse>(mClient, PATH_INITIATE_REFUND.Replace("{transaction_id}", transactionId), token, refundRequest, additionalHeaders);
        }

        /// <summary>
        /// Initiates a refund on a transaction and returns the refund and the status of the refund
        /// </summary>
        /// <param name="transactionId">The ID of the transacion to refund</param>
        /// <param name="refundId">Identifier of the refund</param>
        /// <param name="token">Access token</param>
        /// <returns>Refund status info</returns>
        public Task<RefundResponse> GetRefund(String transactionId, String refundId, String token)
        {
            return GetAsync<RefundResponse>(mClient, PATH_GET_REFUND.Replace("{transaction_id}", transactionId).Replace("{refund_id}", refundId), token);
        }

        /// <summary>
        /// Retrieves the refunable details of given transaction Id
        /// </summary>
        /// <param name="transactionId">The ID of the transacion to fetch refundable details</param>
        /// <param name="token">Access token</param>
        /// <returns>Refund status info</returns>
        public Task<RefundableDetailsResponse> GetRefundableDetails(String transactionId, String token)
        {
            return GetAsync<RefundableDetailsResponse>(mClient, PATH_GET_REFUNDABLE_DETAILS.Replace("{transaction_id}", transactionId), token);
        }

        /// <summary>
        /// Retrieves the available payment brands
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns>Payment brands</returns>
        public Task<PaymentBrandsResponse> RetrievePaymentBrands(String token)
        {
            return GetAsync<PaymentBrandsResponse>(mClient, PATH_GET_PAYMENT_BRANDS, token);
        }

        /// <summary>
        /// Retrieves the available iDEAL issuers
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns>iDEAL issuers</returns>
        public Task<IdealIssuersResponse> RetrieveIdealIssuers(String token)
        {
            return GetAsync<IdealIssuersResponse>(mClient, PATH_GET_IDEAL_ISSUERS, token);
        }

        /// <summary>
        /// Retrieves a new token.
        /// </summary>
        /// <returns>New access token</returns>
        /// <param name="refreshToken">Refresh token</param>
        public Task<AccessToken> RetrieveNewToken(String refreshToken)
        {
            return GetAsync<AccessToken>(mClient, PATH_GET_ACCESS_TOKEN, refreshToken);
        }

        private async Task<T> PostAsync<T>(HttpClient client, string path, string token, object input, Dictionary<string, string> additionalHeaders = null) where T : class
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Headers.ExpectContinue = false;
            request.Content = GetHttpContentForPost(input);

            UpdateHttpClientAuth(client, token);

            if (additionalHeaders != null)
            {
                foreach (KeyValuePair<string, string> header in additionalHeaders)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            HttpResponseMessage response = await client.SendAsync(request);
            return await ProcessResponse<T>(response);
        }

        private async Task<T> GetAsync<T>(HttpClient client, string path, string token) where T : class
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.ExpectContinue = false;

            UpdateHttpClientAuth(client, token);

            HttpResponseMessage response = await client.SendAsync(request);
            return await ProcessResponse<T>(response);
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response) where T : class
        {
            String result = await response.Content.ReadAsStringAsync();
            return ProcessResult<T>(result);
        }
    }
}

#endif
