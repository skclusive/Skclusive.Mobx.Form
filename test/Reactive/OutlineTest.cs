using Xunit;

namespace Skclusive.Mobx.Form.Tests
{
    public class OutlineTest
    {
        [Fact]
        public void TestCreate()
        {
            var outline = AppTypes.OutlineType.Create(new Outline
            {
               Title = "one",

               Items = new Outline [] { "two", "three" }
            });

            Assert.NotNull(outline);

            Assert.Equal("one", outline.Title);

            Assert.Equal(2, outline.Items.Count);

            Assert.Equal("two", outline.Items[0].Title);

            Assert.Equal("three", outline.Items[1].Title);
        }
    }
}
