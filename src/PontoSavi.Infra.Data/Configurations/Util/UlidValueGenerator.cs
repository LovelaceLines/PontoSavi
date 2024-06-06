using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace PontoSavi.Infra.Data.Configurations.Util;

public class UlidValueGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry) =>
        Ulid.NewUlid().ToString();
}
