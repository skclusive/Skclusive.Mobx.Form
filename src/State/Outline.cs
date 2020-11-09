using System.Linq;

namespace Skclusive.Mobx.Form
{
    public interface IOutlinePrimitive
    {
        string Title { set; get; }
    }

    public interface IOutline : IOutlinePrimitive
    {
        IOutline[] Items { set; get; }
    }

    public class Outline : IOutline
    {
        public string Title { set; get; }

        public IOutline[] Items { set; get; }

        public Outline()
        {
        }

        public Outline(string title)
        {
            Title = title;
        }

        public Outline(params Outline[] items)
        {
            Items = items;
        }

        public static implicit operator Outline(string title) => new Outline(title);

        public static implicit operator Outline(string[] titles) => new Outline(titles);

        public static implicit operator Outline(Outline[] items) => new Outline(items);
    }
}
