using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UpboatMe.Models
{
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