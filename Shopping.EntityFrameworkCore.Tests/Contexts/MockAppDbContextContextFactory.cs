using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Shopping.EntityFrameworkCore.Contexts;

namespace Shopping.EntityFrameworkCore.Tests.Contexts;

public class MockAppDbContextContextFactory
{
    readonly Mock<IHttpContextAccessor> _httpContextAccessor = new(MockBehavior.Strict);

    public AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        var httpContext = new DefaultHttpContext();
        httpContext.Items = new Dictionary<object, object>();

        _httpContextAccessor
            .Setup(accessor => accessor.HttpContext)
            .Returns(httpContext);

        return new AppDbContext(options, _httpContextAccessor.Object);
    }
}