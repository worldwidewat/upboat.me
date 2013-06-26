using System.Collections.Generic;
using UpboatMe.Models;

namespace UpboatMe.App_Start
{
    public static class MemeConfig
    {
        public static void RegisterMemes(MemeConfiguration memes)
        {
            memes.Add(new Meme("10 Guy", "10-guy", new List<string> { "10guy", "tenguy" }));
            memes.Add(new Meme("Actual Advice Mallard", "actual-advice-mallard", new List<string> { "aam", "actualadvicemallard" }));
            memes.Add(new Meme("All the Things", "all-the-things", new List<string> { "att", "allthethings" }));
            memes.Add(new Meme("Angry Walter", "angry-walter", new List<string> { "angrywalter", "amitheonlyone", "aitoo" }));
            memes.Add(new Meme("Annoyed Picard", "annoyed-picard", new List<string> { "ap", "picard", "annoyed-picard", "why-the-fuck" }));
            memes.Add(new Meme("Bad Joke Eel", "bad-joke-eel", new List<string> { "bje", "badjokeeel" }));
            memes.Add(new Meme("Bad Luck Brian", "bad-luck-brian", new List<string> { "blb", "badluckbrian" }));
            memes.Add(new Meme("Brace Yourselves", "brace-yourselves", new List<string> { "by", "braceyourselves", }));
            memes.Add(new Meme("Business Cat", "business-cat", new List<string>{"bc", "business-cat", "suit-cat"}));
            memes.Add(new Meme("College Liberal", "college-liberal", new List<string> {"collegeliberal", "scumbagcollegeliberal"}));
            memes.Add(new Meme("Condescending Wonka", "condescending-wonka", new List<string> { "cw", "condescendingwonka", "wonka" }));
            memes.Add(new Meme("Drunk Baby", "drunk-baby", new List<string> {"db", "drunk-baby"}));
            memes.Add(new Meme("Ermahgerd", "ermahgerd", new List<string>{ "emg", "ermahgerd", "ermagerd", "ermagod", "ermahgod" }));
            memes.Add(new Meme("Everyone Loses Their Minds", "everyone-loses-their-minds", new List<string> { "eltm", "everyonelosestheirminds" }));
            memes.Add(new Meme("First World Problems", "first-world-problems", new List<string> { "fwp", "firstworldproblems" }));
            memes.Add(new Meme("Futurama Fry", "futurama-fry", new List<string> { "ff", "fry", "futuramafry", "notsureiffry" }));
            memes.Add(new Meme("Good Guy Greg", "good-guy-greg", new List<string> { "ggg", "goodguygreg" }));
            memes.Add(new Meme("Grumpy Cat", "grumpy-cat", new List<string>{"gc", "grumpycat", "sadcat"}));
            memes.Add(new Meme("I don't want to live on this planet anymore", "i-don't-want-to-live-on-this-planet-anymore", new List<string> { "farnsworth", "idwtlotpa", "idontwanttoliveonthisplanetanymore", "idontwanttoliveonthisplanet" }));
            memes.Add(new Meme("I Should Buy a Boat Cat", "i-should-buy-a-boat-cat", new List<string> {"isbabc", "babc", "boatcat", "ishouldbuyaboatcat", "buyaboatcat"}));
            memes.Add(new Meme("I'll Have You Know", "ill-have-you-know", new List<string> { "ihyk", "illhaveyouknow" }));
            memes.Add(new Meme("Internet Grandma Surprise", "internet-grandma-surprise", new List<string> { "grandma", "igs", "internetgrandma", "internetgrandmasurprise", "grandmasurprise" }));
            memes.Add(new Meme("Irrationally Hostile Mark", "irrationally-hostile-mark", new List<string> { "irrationallyhostilemark", "hostilemark", "ihm" }));
            memes.Add(new Meme("Malicious Advice Mallard", "malicious-advice-mallard", new List<string> { "mam", "maliciousadvicemallard" }));
            memes.Add(new Meme("Office Space Lumbergh", "office-space-lumbergh", new List<string> { "osl", "lumbergh", "officespacelumbergh", "thatdbegreat", "that'dbegreat", "yeah" }));
            memes.Add(new Meme("One Does Not Simply", "one-does-not-simply", new List<string> { "odns", "boromir", "mordor", "onedoesnotsimply" }));
            memes.Add(new Meme("Overly Attached Girlfriend", "overly-attached-girlfriend", new List<string> { "oag", "overlyattachedgirlfriend" }));
            memes.Add(new Meme("Overly Manly Man", "overly-manly-man", new List<string> { "omm", "overlymanlyman" }));
            memes.Add(new Meme("Philosoraptor", "philosoraptor", new List<string> { "raptor", "philosoraptor", "pr" }));
            memes.Add(new Meme("Scumbag Steve", "scumbag-steve", new List<string> { "ss", "scumbagsteve" }));
            memes.Add(new Meme("Shrute Facts", "shrute-facts", new List<string> { "shrute", "shrutefacts", "dwight", "correction", "false" }));
            memes.Add(new Meme("Slowpoke", "slowpoke", new List<string> { "slowpoke" }));
            memes.Add(new Meme("Socially Awesome Awkward Penguin", "socially-awesome-awkward-penguin", new List<string> { "sociallyawesomeawkwardpenguin", "awesomeawkwardpenguin" }));
            memes.Add(new Meme("Socially Awkward Awesome Penguin", "socially-awkward-awesome-penguin", new List<string> { "sociallyawkwardawesomepenguin", "awkwardawesomepenguin" }));
            memes.Add(new Meme("Socially Awkward Penguin", "socially-awkward-penguin", new List<string> { "sap", "sociallyawkwardpenguin" }));
            memes.Add(new Meme("Success Kid", "success-kid", new List<string> { "sk", "successkid" }));
            memes.Add(new Meme("Sudden Clarity Clarence", "sudden-clarity-clarence", new List<string> { "scc", "suddenclarityclarence" }));
            memes.Add(new Meme("Technologically Impaired Duck", "technologically-impaired-duck", new List<string> { "tid", "technologyduck", "technologicallyimpairedduck", "technologyimpairedduck" }));
            memes.Add(new Meme("That Escalated Quickly (Ron Burgundy)", "boy-that-escalated-quickly", new List<string> { "teq", "bteq", "wteq", "thatescalatedquickly", "boythatescalatedquickly", "wellthatescalatedquickly", "ronburgundy" }));
            memes.Add(new Meme("The Most Interesting Man in the World", "the-most-interesting-man-in-the-world", new List<string> { "mim", "tmim", "tmimitw", "themostinterestingmanintheworld", "mostinterestingman" }));
            memes.Add(new Meme("Toy Story: Everywhere", "toy-story-everywhere", new List<string> { "toystory", "toystoryeverywhere", "buzzwoody" }));
            memes.Add(new Meme("Unhelpful High School Teacher", "unhelpful-high-school-teacher", new List<string>{"uhst", "unhelpfulhighschoolteacher", "scumbagteacher", "st"}));
            memes.Add(new Meme("Why Not Zoidberg?", "why-not-zoidberg", new List<string>{"wnz", "ynz", "whynotzoidberg", "zoidberg"}));
            memes.Add(new Meme("Y U NO", "y-u-no", new List<string> { "yuno" } ));
        }
    }
}
