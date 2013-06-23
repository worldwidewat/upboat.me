using System.Collections.Generic;

namespace UpboatMe.Models
{
    public class IndexViewModel
    {
        public List<Meme> Memes { get; set; }
        public List<RecentMeme> RecentMemes { get; set; }
    }
}