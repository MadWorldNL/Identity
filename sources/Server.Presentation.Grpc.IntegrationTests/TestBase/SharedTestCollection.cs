namespace MadWorldNL.Server.Presentation.Grpc.IntegrationTests.TestBase;

[CollectionDefinition(Collections.Applcation)]
public class SharedTestCollection : ICollectionFixture<GrpcFactory>
{
    public SharedTestCollection(GrpcFactory factory)
    {
        factory.CreateDefaultClient();
    }
}