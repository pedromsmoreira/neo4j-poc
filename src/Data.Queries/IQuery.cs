namespace Data.Queries
{
    public interface IQuery
    {
        int OffSet { get; set; }

        string Query { get; set; }
    }
}