using System.Threading.Tasks;

namespace CodeFirst
{
    public interface IHealthChecker
    {
        Task CheckHealth();
    }
}
