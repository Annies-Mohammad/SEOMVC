using Microsoft.AspNetCore.Mvc;

namespace SEO.API.Controllers
{
    public enum JsonResponseTypes
    {
        /// <summary>
        /// The result
        /// </summary>
        RESULT,

        /// <summary>
        /// The error
        /// </summary>
        ERROR,

        /// <summary>
        /// 204 No Content
        /// </summary>
        NO_CONTENT,
    }

    public class SEOController : Controller
    {

    }
}