using System.Collections.Generic;

namespace Itechart.Survey.WebApi.DataContracts
{
    public sealed class ErrorDataContract
    {
        public IReadOnlyCollection<string> Errors { get; set; }


        public ErrorDataContract(IReadOnlyCollection<string> errors)
        {
            Errors = errors;
        }
    }
}
