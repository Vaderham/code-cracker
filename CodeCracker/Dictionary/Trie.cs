using System.Collections;

namespace CodeCracker.Dictionary;

public class Trie
{
    private readonly TrieNode _rootNode;

    public Trie()
    {
        _rootNode = new TrieNode(null, 0, null);
    }

    private TrieNode GetDeepestCurrentNode(string word)
    {
        var currentNode = _rootNode;
        var result = currentNode;

        foreach (var letter in word)
        {
            if (letter == '_') return currentNode;

            currentNode = currentNode.GetChild(letter);
            if (currentNode == null)
                break;
            result = currentNode;
        }

        return result;
    }

    // ___
    // a__
    // a_b

    public string GetWordForPattern(string wordPattern)
    {
        var nodeQueue = new Stack<TrieNode>();
        var currentNode = _rootNode;

        while (nodeQueue.Count < wordPattern.Length)
        {
            var currentLetter = wordPattern[nodeQueue.Count];

            // Handle the last letter.
            if (nodeQueue.Count == wordPattern.Length - 1)
            {
                // If this is the last letter, find the children that are word ends.
                var finalNodeOptions = currentNode._children.Where(n => n.IsLeaf()).ToList();
                
                // If one or more exist, choose a random one and bust on outta here.
                if (finalNodeOptions.Count != 0)
                {
                    var randomNode = currentNode._children[GetRandomNumber(0, finalNodeOptions.Count - 1)];
                    nodeQueue.Push(randomNode);
                    break;
                }

                //Otherwise, we need to go back up and try the previous letter again.
                currentNode = currentNode.Parent;
                nodeQueue.Pop();
                continue;
            }
            
            if (currentLetter == '_')
            {
                if (currentNode._children.Count == 0)
                {
                    currentNode = currentNode.Parent;
                    nodeQueue.Pop();
                    continue;
                }
                
                var randomChild =
                    currentNode!._children[GetRandomNumber(0, currentNode._children.Count - 1)];

                if (randomChild.HasBeenTraversed)
                {
                    currentNode = currentNode.Parent;
                    if (nodeQueue.Count > 0)
                    {
                        nodeQueue.Pop();   
                    }
                    continue;
                }
                
                nodeQueue.Push(randomChild);
                randomChild.HasBeenTraversed = true;
                currentNode = randomChild;
                continue;
            }

            var childMatched = false;
            
            foreach (var child in currentNode!._children)
            {
                if (child.Letter != currentLetter) continue;
                
                nodeQueue.Push(child);
                child.HasBeenTraversed = true;
                childMatched = true;
                currentNode = child;
            }

            if (childMatched)
            {
                continue;
            }
            
            currentNode = currentNode.Parent;
            nodeQueue.Pop();
        }
        
        return string.Join("", nodeQueue.Reverse().Select(node => node.Letter));
    }

    public static int GetRandomNumber(int minValue, int maxValue)
    {
        var random = new Random();
        return random.Next(minValue, maxValue);
    }

    public void Insert(string word)
    {
        var deepestNode = GetDeepestCurrentNode(word);
        var current = deepestNode;

        for (var i = current.Depth; i < word.Length; i++)
        {
            var newNode = new TrieNode(word[i], current.Depth + 1, current);
            current._children.Add(newNode);
            current = newNode;
        }

        // current._children.Add(new TrieNode(null, current.Depth + 1, current));
    }
}