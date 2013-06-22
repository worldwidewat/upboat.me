using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace UpboatMe.Models
{
    public class MemeConfiguration
    {
        private ConcurrentBag<Meme> _memes;

        public MemeConfiguration()
        {
            _memes = new ConcurrentBag<Meme>();
        }

        public void Add(Meme meme)
        {
            _memes.Add(meme);
        }

        public Meme this[string key]
        {
            get
            {
                return _memes.FirstOrDefault(m => m.Aliases.Any(a => string.Equals(a, key, StringComparison.OrdinalIgnoreCase)));
            }
        }

        public List<string> GetMemeNames()
        {
            return _memes.Select(m => m.Aliases.First()).ToList();
        }

        public List<Meme> GetMemes()
        {
            return _memes.OrderBy(m => m.Description).ToList();
        }
    }
}