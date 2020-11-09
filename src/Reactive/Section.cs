using System;
using System.Collections.Generic;
using System.Linq;
using Skclusive.Mobx.Observable;
using Skclusive.Mobx.StateTree;

namespace Skclusive.Mobx.Form
{
    public interface ISectionActions
    {
        void MakeSelection(bool selected);
    }

    public interface ISectionObservable : ISectionPrimitive, ISectionActions
    {
        IOutlineObservable Outline { set; get; }
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

        public IOutlineObservable Outline
        {
            get => Read<IOutlineObservable>(nameof(Outline));
            set => Write(nameof(Outline), value);
        }

        public void MakeSelection(bool selected)
        {
            (Target as dynamic).MakeSelection(selected);
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
            .Mutable(o => o.Outline, Types.Late("LateOutlineType", () => Types.Maybe(OutlineType)))
            .Action<bool>(o => o.MakeSelection(default), (o, selected) => o.Selected = selected);
    }
}
