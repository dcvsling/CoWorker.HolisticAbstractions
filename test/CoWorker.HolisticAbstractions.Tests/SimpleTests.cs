using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using System.IO;

namespace CoWorker.HolisticAbstractions.Tests
{
    public class SimpleTests
    {
        private static IServiceProvider Service
            => new ServiceCollection().AddHandler<object, IWriteTo<object>, OutputWithString<object>>()
                        .AddHandler<object, IWriteTo<object>, OutputWithJson<object>>()
                        .AddHandlerDecorator<object, IDoManyTimes<object>, Twice<object>>()
                        .AddHandlerDecorator<object, IDoManyTimes<object>, ThreeTimes<object>>()

                .AddSingleton<TextWriter>(new ActualWriter())
                .BuildServiceProvider();
        public static SeedDTO Seed => new SeedDTO()
        {
            Now = OneTime,
            Word = OneTime.ToString(),
            Number = OneTime.UtcTicks
        };
        public readonly static DateTimeOffset OneTime = new DateTimeOffset(new DateTime(2017, 02, 03, 05, 06, 07));
        [Fact]
        public void test_IWriteToCommandHandler()
        {
            var service = Service;
            service.GetService<IDoManyTimes<SeedDTO>>().Invoke(Seed);
            var writer = service.GetService<TextWriter>();
            Assert.IsType<ActualWriter>(writer);
            ActualWriter actual = writer as ActualWriter;
            var str = actual.Actual;
            Assert.NotEmpty(str);
            Assert.Equal(Expect, str);
        }

        public static string Expect = string.Join(string.Empty,Enumerable.Repeat(JsonConvert.SerializeObject(Seed) + "\r\n",3));
    }
}
