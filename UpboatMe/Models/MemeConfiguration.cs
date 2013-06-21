using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UpboatMe.Utilities;

namespace UpboatMe.Models
{
    public class MemeConfiguration : IEnumerable<string>
    {
        private ConcurrentDictionary<string, Meme> _memes;

        public MemeConfiguration()
        {
            _memes = new ConcurrentDictionary<string, Meme>(StringComparer.OrdinalIgnoreCase);
        }

        public void Add(Meme meme, params string[] aliases)
        {
            foreach (var alias in aliases)
            {
                if (!_memes.TryAdd(alias, meme))
                {
                    throw new InvalidOperationException(string.Format("A meme with the alias '{0}' already exists", alias));
                }
            }
        }

        public Meme this[string key]
        {
            get
            {
                Meme result = null;

                _memes.TryGetValue(key.AlphaOnly(), out result);

                return result;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _memes.GroupBy(m => m.Value).Select(g => g.First().Key).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}