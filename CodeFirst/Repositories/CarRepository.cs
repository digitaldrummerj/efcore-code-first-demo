namespace CodeFirst.Repositories
{
    using Models;

    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(CodeFirstContext context) : base(context)
        {
            
        }
    }
}
