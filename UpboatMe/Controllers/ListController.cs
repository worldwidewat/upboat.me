using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Http;
using UpboatMe.Models;

namespace UpboatMe.Controllers
{
    public class ListController : ApiController
    {
        public IEnumerable<ApiMemeResult> Get()
        {
            return GlobalMemeConfiguration
                .Memes
                .GetMemes()
                .Select(m => new ApiMemeResult
                {
                    Name = m.Aliases.Last(),
                    Description = m.Description,
                    Aliases = m.Aliases
                });
        }

        public ApiMemeResult Get(string id)
        {
            var meme = GlobalMemeConfiguration.Memes[id];
            return new ApiMemeResult
                       {
                           Name = meme.Aliases.Last(),
                           Description = meme.Description,
                           Aliases = meme.Aliases
                       };
        }
    }

    [DataContract(Name = "Meme")]
    public class ApiMemeResult
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public IEnumerable<string> Aliases { get; set; }
    }
}
