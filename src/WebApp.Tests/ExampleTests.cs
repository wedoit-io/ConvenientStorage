namespace WebApp.Tests
{
    using FakeItEasy;
    using Xunit;

    public class ExampleTests
    {
        public interface ISay
        {
            string SaySomething();
        }

        [Fact]
        public void PassingTest()
        {
            Assert.True(true);
        }

        [Fact]
        public void MockingTest()
        {
            // create a fake
            var someone = A.Fake<ISay>();

            // setup a call return a value
            A.CallTo(() => someone.SaySomething())
             .Returns("Hello");

            // user your fake
            var whateverTheySaid = someone.SaySomething();

            // assert outcomes
            A.CallTo(() => someone.SaySomething())
             .MustHaveHappened();
            Assert.Equal("Hello", whateverTheySaid);
        }
    }
}
