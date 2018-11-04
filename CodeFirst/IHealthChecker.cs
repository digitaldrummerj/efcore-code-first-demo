namespace CodeFirst
{
    using System.Threading.Tasks;

    public interface IHealthChecker
    {
        Task CheckHealth();
    }
}
