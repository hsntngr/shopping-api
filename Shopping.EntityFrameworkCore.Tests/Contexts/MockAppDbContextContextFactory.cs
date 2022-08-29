using System.Globalization;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Contexts;

namespace Shopping.EntityFrameworkCore.Tests.Contexts;

public class MockAppDbContextContextFactory
{
    readonly Mock<IHttpContextAccessor> _httpContextAccessor = new(MockBehavior.Strict);

    public AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var httpContext = new DefaultHttpContext();
        httpContext.Items = new Dictionary<object, object>();

        _httpContextAccessor
            .Setup(accessor => accessor.HttpContext)
            .Returns(httpContext);

        return new AppDbContext(options, _httpContextAccessor.Object);
    }
}