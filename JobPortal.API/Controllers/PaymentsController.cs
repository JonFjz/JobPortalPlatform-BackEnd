using JobPortal.API.Controllers.Base;
using JobPortal.Application.Contracts.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace JobPortal.API.Controllers
{

    public class PaymentsController : BaseApiController
    {
        private readonly string _whSecret;
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentService paymentService, IConfiguration config, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _whSecret = config.GetSection("Stripe:WebhookSecret")?.Value;
            _logger = logger;
        }


        [Authorize]
        [HttpPost("payment-intent/{jobPostingId}")]
        public async Task<IActionResult> CreatePaymentIntent(int jobPostingId)
        {
            var jobPosting = await _paymentService.UpgradeToPremium(jobPostingId);
            if (jobPosting == null)
            {
                return NotFound("Job posting not found.");
            }
            return Ok(new { clientSecret = jobPosting.ClientSecret });
        }


        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"], _whSecret);

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    await _paymentService.UpdateJobPostingPaymentSucceeded(paymentIntent.Id);
                    _logger.LogInformation($"Payment for job posting succeeded: {paymentIntent.Id}");
                }
                else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    await _paymentService.UpdateJobPostingPaymentFailed(paymentIntent.Id);
                    _logger.LogInformation($"Payment for job posting failed: {paymentIntent.Id}");
                }
                return Ok();
            }
            catch (StripeException e)
            {
                _logger.LogError(e, "Stripe webhook failed.");
                return BadRequest();
            }
        }

    }
}