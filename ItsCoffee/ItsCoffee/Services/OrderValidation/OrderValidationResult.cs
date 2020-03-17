using System.Collections.Generic;

namespace ItsCoffee.Core.Services.OrderValidation
{
    public abstract class OrderValidationResult
    {
        public class SuccessfulResult : OrderValidationResult
        {
        }

        public class FailedResult : OrderValidationResult
        {
            public IEnumerable<string> ValidationMessages;

            public FailedResult(string validationMessage)
            {
                ValidationMessages = new []
                {
                    validationMessage
                };
            }

            public FailedResult(IEnumerable<string> validationMessages)
            {
                ValidationMessages = validationMessages;
            }
        }
    }
}