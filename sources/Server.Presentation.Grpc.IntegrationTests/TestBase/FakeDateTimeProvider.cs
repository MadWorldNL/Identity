using MadWorldNL.Common.Time;

namespace MadWorldNL.Server.Presentation.Grpc.IntegrationTests.TestBase;

public class FakeDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow() => new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
}