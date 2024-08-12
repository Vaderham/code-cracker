using CodeCracker.Dictionary;

namespace CodeCracker.GridTemplates;

public class SmallGrid : IGrid
{
    public int SideLength => 5;

    public char[,] Grid { get; } = new char[5, 5]
    {
        { char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MinValue },
        { char.MinValue, char.MaxValue, char.MinValue, char.MaxValue, char.MinValue },
        { char.MinValue, char.MaxValue, char.MinValue, char.MaxValue, char.MinValue },
        { char.MaxValue, char.MinValue, char.MaxValue, char.MinValue, char.MinValue },
        { char.MinValue, char.MinValue, char.MinValue, char.MinValue, char.MaxValue }
    };

    public EnglishDictionary Dictionary;

    public SmallGrid()
    {
        Dictionary = new EnglishDictionary(SideLength);
    }

    public string FindHorizontalWordPattern(int startX, int startY)
    {
        if (Grid[startX, startY] == GridConstants.Filled)
        {
            return string.Empty;
        }

        var letters = new List<char>();
        var currentCoordinate = Grid[startX, startY];

        while (currentCoordinate != GridConstants.Filled)
        {
            letters.Add(currentCoordinate == GridConstants.Empty ? '_' : Grid[startX, startY] );

            if (startY + letters.Count == SideLength)
            {
                return string.Join("", letters);
            }

            currentCoordinate = Grid[startX, startY + letters.Count];
        }

        return string.Join("", letters);
    }

    public string FindVerticalWordPattern(int startX, int startY)
    {
        if (Grid[startX, startY] == GridConstants.Filled)
        {
            return string.Empty;
        }

        var letters = new List<char>();
        var currentCoordinate = Grid[startX, startY];

        while (currentCoordinate != GridConstants.Filled)
        {
            letters.Add(currentCoordinate == GridConstants.Empty ? '_' : currentCoordinate );

            if (startX + letters.Count == SideLength)
            {
                return string.Join("", letters);
            }

            currentCoordinate = Grid[startX + letters.Count, startY];
        }

        return string.Join("", letters);
    }

    public void InsertHorizontalWord(int startX, int startY, string word)
    {
        for (var i = 0; i < word.Length; i++)
        {
            Grid[startX, startY + i] = word[i];
        }
    }
    
    public void InsertVerticalWord(int startX, int startY, string word)
    {
        for (var i = 0; i < word.Length; i++)
        {
            Grid[startX + i, startY] = word[i];
        }
    }

    public void PopulatedGrid()
    {
        for (var x = 0; x < SideLength; x++)
        {
            for (var y = 0; y < SideLength; y++)
            {
                PrintGrid(x, y);
                
                var horizontalWordPattern = FindHorizontalWordPattern(x, y);
                if (horizontalWordPattern.Any(c => c == '_') && horizontalWordPattern.Length > 1)
                {
                    var word = Dictionary.GetRandomWordMatchingPattern(horizontalWordPattern);
                    InsertHorizontalWord(x, y, word);
                };

                var verticalWordPattern = FindVerticalWordPattern(x, y);
                if (verticalWordPattern.Any(c => c == '_') && verticalWordPattern.Length > 1)
                {
                    var word = Dictionary.GetRandomWordMatchingPattern(verticalWordPattern);
                    InsertVerticalWord(x, y, word);
                }
                Console.WriteLine();
            }
        }
    }

    private void PrintGrid(int x, int y)
    {
        // Print the grid in a nice readable format to the console.
        for (var i = 0; i < SideLength; i++)
        {
            for (var j = 0; j < SideLength; j++)
            {
                if (Grid[i,j] == GridConstants.Empty)
                {
                    if (i == x && j == y)
                    {
                        Console.Write("[0]");
                    }
                    else
                    {
                        Console.Write(" " + 0 + " ");   
                    }
                }
                else
                {
                    if (i == x && j == y)
                    {
                        Console.Write("[" + Grid[i, j] + "]");
                    }
                    else
                    {
                        Console.Write(" " + Grid[i, j] + " ");   
                    }
                }
            }
            Console.WriteLine();
        }
    }
}