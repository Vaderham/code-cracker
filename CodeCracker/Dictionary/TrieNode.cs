using Gma.DataStructures.StringSearch;

namespace CodeCracker.Dictionary;

public class TrieNode
{
    public IList<TrieNode> _children = new List<TrieNode>();
    public TrieNode? Parent { get; set; }
    public int Depth { get; set; }
    public char? Letter { get; set; }

    public bool HasBeenTraversed = false;

    public TrieNode(char? letter, int depth, TrieNode? parent)
    {
        Parent = parent;
        Depth = depth;
        Letter = letter;
    }

    public TrieNode? GetChild(char letter)
    {
        return _children.FirstOrDefault(c => c.Letter == letter);
    }
    
    public bool IsLeaf()
    {
        return _children.Count == 0;
    }
}
