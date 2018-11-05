namespace CodeFirst.Repositories
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}