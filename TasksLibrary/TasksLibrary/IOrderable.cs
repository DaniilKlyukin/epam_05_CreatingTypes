namespace TasksLibrary
{
    public enum Direction
    {
        Ascending = 1,
        Descending = -1
    }

    public interface IOrderable
    {
        void Order(int[,] arr, Direction d);
    }
}
