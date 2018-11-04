using CodeFirst.Models;

namespace CodeFirst.Repositories
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(CodeFirstContext context) : base(context)
        {
            
        }
    }
}
