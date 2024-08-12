namespace CodeCracker.GridTemplates;

public interface IGrid
{
    int SideLength  { get; }
    char[,] Grid { get; }
}