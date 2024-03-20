using LightsOn.Domain.Entities;

namespace LightsOn.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Domain.Entities.CategoryExpense> CategoryExpenses { get; }

    DbSet<Domain.Entities.Client> Clients { get; }
    DbSet<LightsOn.Domain.Entities.Customer> Customers { get; }
    DbSet<Domain.Entities.Engine> Engines { get; }

    DbSet<Domain.Entities.Estimate> Estimates { get; }
    DbSet<Domain.Entities.Material> Materials { get; }

    DbSet<Domain.Entities.PowerEquipment> PowerEquipments { get; }
    DbSet<Domain.Entities.UnitMeasurement> UnitMeasurements { get; }

    DbSet<Domain.Entities.WorkPerformanceDescription> WorkPerformanceDescriptions{ get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
