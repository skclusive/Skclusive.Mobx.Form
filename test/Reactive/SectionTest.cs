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

                Outlines =  new Outline[]
                {
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
                }
            });

            Assert.NotNull(section);

            Assert.NotNull(section.Outlines);

            Assert.Equal(3, section.Outlines.Count);

            Assert.Equal("name", section.Outlines[0].Title);

            Assert.Equal("birthdate", section.Outlines[1].Title);

            Assert.Equal(3, section.Outlines[2].Items.Count);

            Assert.Equal("size", section.Outlines[2].Items[0].Title);

            Assert.Equal("three-left", section.Outlines[2].Items[1].Items[0].Title);

            Assert.Equal("three-right", section.Outlines[2].Items[1].Items[1].Title);

            Assert.Equal("color", section.Outlines[2].Items[2].Title);
        }
    }
}
