namespace CodeFirst.Repositories
{
    public class SoftDelete : ModelBase, ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}