using System;

namespace CodeFirst.Repositories
{
    public interface IModelBase
    {
        int Id { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Modified { get; set; }
    }
}