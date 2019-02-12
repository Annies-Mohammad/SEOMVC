using System;
using System.Collections.Generic;
using System.Text;

namespace SEO.WorkerService.Exceptions
{
    public class SEOValidationException : Exception
    {
        public SEOValidationException(string message) : base(message)
        {
        }

        public SEOValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
