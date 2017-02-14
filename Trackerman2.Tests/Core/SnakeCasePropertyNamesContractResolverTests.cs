using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using Trackerman2.Core;

namespace Trackerman2.Tests.Core
{
    public class Foo
    {
        public string LongTestVariableWithDifferentWords { get; set; }
    }

    [TestFixture]
    public class SnakeCasePropertyNamesContractResolverTests
    {
        private IContractResolver snakeCaseResolver; 

        [SetUp]
        public void Setup()
        {
            snakeCaseResolver = new SnakeCasePropertyNamesContractResolver();
        }

        [Test]
        public void ResolvePropertyName_WithSnakeCase_ReturnsCamelCase()
        {
            var camelCaseResult = JsonConvert.DeserializeObject<Foo>("{ \"long_test_variable_with_different_words\": \"datadatadata\" }"
                , new JsonSerializerSettings { ContractResolver = new SnakeCasePropertyNamesContractResolver() });

            camelCaseResult.LongTestVariableWithDifferentWords.Should().Be("datadatadata");
        }
    }
}
