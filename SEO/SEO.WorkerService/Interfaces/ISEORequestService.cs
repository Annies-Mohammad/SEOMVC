using System.Net;

namespace SEO.WorkerService.Interfaces
{
   public interface ISEORequestService
   {
       HttpWebRequest CreateRequest(string searchTerm);

       string GetResponse(HttpWebRequest request, string lookUp);
   }
}
