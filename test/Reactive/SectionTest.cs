using Xunit;

namespace Skclusive.Mobx.Form.Tests
{
    public class SectionTest
    {
        [Fact]
        public void TestCreate()
        {
            var section = AppTypes.SectionType.Create(new Section
            {
                Title = "Basic",

                Selected = true,

                Outline =  new Outline
                (
                    "name",

                    "birthdate",

                    new Outline []
                    {
                        "size",

                        new Outline []
                        {
                           "three-left",

                           "three-right"
                       },

                        "color"
                    }
                )
            });

            Assert.NotNull(section);

            Assert.NotNull(section.Outline);

            Assert.Equal(3, section.Outline.Items.Count);

            Assert.Equal("name", section.Outline.Items[0].Title);

            Assert.Equal("birthdate", section.Outline.Items[1].Title);

            Assert.Equal(3, section.Outline.Items[2].Items.Count);

            Assert.Equal("size", section.Outline.Items[2].Items[0].Title);

            Assert.Equal("three-left", section.Outline.Items[2].Items[1].Items[0].Title);

            Assert.Equal("three-right", section.Outline.Items[2].Items[1].Items[1].Title);

            Assert.Equal("color", section.Outline.Items[2].Items[2].Title);
        }
    }
}
