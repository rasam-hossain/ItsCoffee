using System.Collections.Generic;

namespace ItsCoffee.Core.Services
{
    public interface IValidate<T>
    {
        IEnumerable<string> GetValidationMessages(T t);

        bool IsValid(T t);
    }
}