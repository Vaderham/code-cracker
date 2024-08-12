using System.Text.Json;

namespace CodeCracker.Dictionary;

public class EnglishDictionary
{
    private static string Words => File.ReadAllText("./Dictionary/dictionary_words_only.json");
    private Dictionary<int, List<string>> WordsByLength;
    private Trie root;

    public EnglishDictionary(int gridSize)
    {
        var validWords = JsonSerializer.Deserialize<IList<string>>(Words)!.Where(w => w.Length <= gridSize && w.Length > 1).ToList();
        WordsByLength = validWords.GroupBy(w => w.Length).ToDictionary(g => g.Key, g => g.ToList());
        root = new Trie();
        foreach (var word in validWords)
        {
            root.Insert(word);
        }
    }

    public string GetRandomWordMatchingPattern(string pattern)
    {
        return root.GetWordForPattern(pattern);
    }
}