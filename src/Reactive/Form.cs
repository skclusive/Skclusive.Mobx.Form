using System;
using System.Collections.Generic;
using System.Linq;
using Skclusive.Mobx.Observable;
using Skclusive.Mobx.StateTree;
using Skclusive.Mobx.JsonSchema;
using static Skclusive.Mobx.JsonSchema.AppTypes;

namespace Skclusive.Mobx.Form
{
    public interface IFormActions
    {
    }

    public interface IFormObservable : IFormPrimitive, IFormActions
    {
        IList<ISectionObservable> Sections { set; get; }

        ISectionObservable Selected { get; }

        IObjectObservable Schema { set; get; }
    }

    internal class FormProxy : ObservableProxy<IFormObservable, INode>, IFormObservable
    {
        public override IFormObservable Proxy => this;

        public FormProxy(IObservableObject<IFormObservable, INode> target) : base(target)
        {
        }

        public string Title
        {
            get => Read<string>(nameof(Title));
            set => Write(nameof(Title), value);
        }

        public string Cancel
        {
            get => Read<string>(nameof(Cancel));
            set => Write(nameof(Cancel), value);
        }

        public string Submit
        {
            get => Read<string>(nameof(Submit));
            set => Write(nameof(Submit), value);
        }

        public IObjectObservable Schema
        {
            get => Read<IObjectObservable>(nameof(Schema));
            set => Write(nameof(Schema), value);
        }

        public ISectionObservable Selected => Read<ISectionObservable>(nameof(Selected));

        public IList<ISectionObservable> Sections
        {
            get => Read<IList<ISectionObservable>>(nameof(Sections));
            set => Write(nameof(Sections), value);
        }
    }

    public partial class AppTypes
    {
        public readonly static IObjectType<IForm, IFormObservable> FormType = Types.
            Object<IForm, IFormObservable>("FormType")
            .Proxy(x => new FormProxy(x))
            .Snapshot(() => new Form())
            .Mutable(o => o.Title, Types.Maybe(Types.String))
            .Mutable(o => o.Cancel, Types.Maybe(Types.String))
            .Mutable(o => o.Submit, Types.Maybe(Types.String))
            .Mutable(o => o.Schema, Types.Maybe(ObjectType.Value))
            .Mutable(o => o.Sections, Types.Late("LateSectionType", () => Types.Maybe(Types.List(SectionType))))
            .View(o => o.Selected, Types.Late("LateSectionType", () => SectionType), (o) => o.Sections.Where(s => s.Selected).FirstOrDefault());
    }
}
