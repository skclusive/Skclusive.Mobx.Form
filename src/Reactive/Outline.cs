using System;
using System.Collections.Generic;
using Skclusive.Mobx.Observable;
using Skclusive.Mobx.StateTree;

namespace Skclusive.Mobx.Form
{
    public interface IOutlineActions
    {
    }

    public interface IOutlineObservable : IOutlinePrimitive, IOutlineActions
    {
        IList<IOutlineObservable> Items { set; get; }
    }

    internal class OutlineProxy : ObservableProxy<IOutlineObservable, INode>, IOutlineObservable
    {
        public override IOutlineObservable Proxy => this;

        public OutlineProxy(IObservableObject<IOutlineObservable, INode> target) : base(target)
        {
        }

        public string Title
        {
            get => Read<string>(nameof(Title));
            set => Write(nameof(Title), value);
        }

        public IList<IOutlineObservable> Items
        {
            get => Read<IList<IOutlineObservable>>(nameof(Items));
            set => Write(nameof(Items), value);
        }
    }

    public partial class AppTypes
    {
        public readonly static IObjectType<IOutline, IOutlineObservable> OutlineType = Types.
            Object<IOutline, IOutlineObservable>("OutlineType")
            .Proxy(x => new OutlineProxy(x))
            .Snapshot(() => new Outline())
            .Mutable(o => o.Title, Types.Maybe(Types.String))
            .Mutable(o => o.Items, Types.Late("LateOutlineType", () => Types.Maybe(Types.List(OutlineType))));
    }
}
