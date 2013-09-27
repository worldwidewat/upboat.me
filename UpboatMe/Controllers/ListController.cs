using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using UpboatMe.Models;

namespace UpboatMe.Controllers
{
    public class ListController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var result = GlobalMemeConfiguration
                .Memes
                .GetMemes()
                .Select(m => new ApiMemeResult
                                 {
                                     Name = m.Aliases.Last(),
                                     Description = m.Description,
                                     Aliases = m.Aliases
                                 });

            return HttpCachedResponseMessage(result);
        }

        public HttpResponseMessage Get(string id)
        {
            var meme = GlobalMemeConfiguration.Memes[id];
            var result = new ApiMemeResult
                             {
                                 Name = meme.Aliases.Last(),
                                 Description = meme.Description,
                                 Aliases = meme.Aliases
                             };

            return HttpCachedResponseMessage(result);
        }

        private HttpResponseMessage HttpCachedResponseMessage<T>(T result)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            response.Headers.CacheControl = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromHours(1),
                Public = true
            };
            return response;
        }
    }
}
