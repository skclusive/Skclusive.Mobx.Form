using System;
using System.Collections.Generic;
using Skclusive.Mobx.Observable;
using Skclusive.Mobx.StateTree;

namespace Skclusive.Mobx.Form
{
    public interface ISectionActions
    {
    }

    public interface ISectionObservable : ISectionPrimitive, ISectionActions
    {
        IList<IOutlineObservable> Outlines { set; get; }
    }

    internal class SectionProxy : ObservableProxy<ISectionObservable, INode>, ISectionObservable
    {
        public override ISectionObservable Proxy => this;

        public SectionProxy(IObservableObject<ISectionObservable, INode> target) : base(target)
        {
        }

        public string Title
        {
            get => Read<string>(nameof(Title));
            set => Write(nameof(Title), value);
        }

        public bool Selected
        {
            get => Read<bool>(nameof(Selected));
            set => Write(nameof(Selected), value);
        }

        public IList<IOutlineObservable> Outlines
        {
            get => Read<IList<IOutlineObservable>>(nameof(Outlines));
            set => Write(nameof(Outlines), value);
        }
    }

    public partial class AppTypes
    {
        public readonly static IObjectType<ISection, ISectionObservable> SectionType = Types.
            Object<ISection, ISectionObservable>("SectionType")
            .Proxy(x => new SectionProxy(x))
            .Snapshot(() => new Section())
            .Mutable(o => o.Title, Types.Maybe(Types.String))
            .Mutable(o => o.Selected, Types.Maybe(Types.Boolean))
            .Mutable(o => o.Outlines, Types.Late("LateOutlineType", () => Types.Maybe(Types.List(OutlineType))));
    }
}
