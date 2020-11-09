namespace Skclusive.Mobx.Form
{
    public interface ISectionPrimitive
    {
        string Title { set; get; }

        bool Selected { set; get; }
    }

    public interface ISection : ISectionPrimitive
    {
        IOutline Outline { set; get; }
    }

    public class Section : ISection
    {
        public string Title { set; get; }

        public bool Selected { set; get; }

        public IOutline Outline { set; get; }
    }
}
