using System;
using System.Collections.Generic;
using System.Linq;
using Skclusive.Mobx.Observable;
using Skclusive.Mobx.StateTree;
using Skclusive.Mobx.JsonSchema;
using static Skclusive.Mobx.JsonSchema.AppTypes;
using Skclusive.Core.Collection;

namespace Skclusive.Mobx.Form
{
    public interface IFormActions
    {
        void Validate();

        void Reset();

        void MakeSelection(string selected);
    }

    public interface IFormObservable : IFormPrimitive, IFormActions
    {
        bool Modified { get; }

        bool Valid { get; }

        bool Validating { get; }

        IList<string> Errors { get; }

        IList<ISectionObservable> Sections { set; get; }

        ISectionObservable Selected { get; }

        IObjectObservable Schema { set; get; }

        IError Error { get; }

        IList<IAnyObservable> Fields { get; }

        IMap<string, object> Values { get; }
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

        public IError Error => Read<IError>(nameof(Error));

        public IList<IAnyObservable> Fields => Read<IList<IAnyObservable>>(nameof(Fields));

        public IList<ISectionObservable> Sections
        {
            get => Read<IList<ISectionObservable>>(nameof(Sections));
            set => Write(nameof(Sections), value);
        }

        public bool Validating => Read<bool>(nameof(Validating));

        public bool Modified => Read<bool>(nameof(Modified));

        public bool Valid => Read<bool>(nameof(Valid));

        public IList<string> Errors => Read<IList<string>>(nameof(Errors));

        public IMap<string, object> Values => Read<IMap<string, object>>(nameof(Values));

        public void Reset()
        {
            (Target as dynamic).Reset();
        }

        public void Validate()
        {
            (Target as dynamic).Validate();
        }

        public void MakeSelection(string section)
        {
            (Target as dynamic).MakeSelection(section);
        }
    }

    public partial class AppTypes
    {
        public readonly static IObjectType<IForm, IFormObservable> FormType = Types.
            Object<IForm, IFormObservable>("FormType")
            .Proxy(x => new FormProxy(x))
            .Snapshot(() => new Form())
            .Hook(Hook.AfterCreate, o =>
            {
                if (o.Sections.Count > 0 && !o.Sections.Any(section => section.Selected))
                {
                    o.Sections[0].MakeSelection(true);
                }
            })
            .Mutable(o => o.Title, Types.Maybe(Types.String))
            .Mutable(o => o.Cancel, Types.Maybe(Types.String))
            .Mutable(o => o.Submit, Types.Maybe(Types.String))
            .Mutable(o => o.Schema, Types.Maybe(ObjectType.Value))
            .Mutable(o => o.Sections, Types.Late("LateSectionType", () => Types.Maybe(Types.List(SectionType))))
            .View(o => o.Valid, Types.Boolean, (o) => o.Schema.Valid)
            .View(o => o.Modified, Types.Boolean, (o) => o.Schema.Modified)
            .View(o => o.Validating, Types.Boolean, (o) => o.Schema.Validating)
            .View(o => o.Errors, Types.List(Types.String), (o) => o.Schema.Errors)
            .View(o => o.Selected, Types.Late("LateSectionType", () => SectionType), (o) => o.Sections.Where(s => s.Selected).FirstOrDefault())
            .View(o => o.Fields, Types.Frozen, (o) => o.Schema.Fields)
            .View(o => o.Values, Types.Frozen, (o) => o.Schema.Value)
            .View(o => o.Error, Types.Frozen, (o) =>
            {
                static IError ToAnyError(IAnyObservable any)
                {
                    return any switch
                    {
                        IObjectObservable @object => new Error
                        {
                            Properties = new Map<string, IError>(@object.Properties.ToDictionary(p => p.Key, p => ToAnyError(p.Value)))
                        },

                        IArrayObservable array => new Error
                        {
                            Items = array.Items.Select(ToAnyError).ToList()
                        },

                        _ => new Error
                        {
                            Errors = any.Errors.ToArray()
                        },
                    };
                }

                return ToAnyError(o.Schema);
            })
            .Action<string>(o => o.MakeSelection(default), (o, selected) =>
            {
                foreach (var section in o.Sections)
                {
                    section.MakeSelection(section.Title == selected);
                }
            })
            .Action(o => o.Reset(), (o) => o.Schema.Reset())
            .Action(o => o.Validate(), (o) => o.Schema.Validate());
    }

    public static class FormExtensions
    {
        public static T GetField<T>(this IFormObservable form, string field) where T : IAnyObservable
        {
            static ObjectNode ResolveByNode(ObjectNode node, string path)
            {
                string prefix = "";

                if (node.Value is IObjectObservable @object)
                {
                    prefix = $"Properties/";
                }
                else if (node.Value is IArrayObservable @array)
                {
                   prefix = $"Items/";
                }

                return (ObjectNode) node.ResolveNodeByPath($"{prefix}{path}");
            }

            string[] paths = field.SplitJsonPath();

            ObjectNode current = form.Schema.Properties.GetStateTreeNode();

            foreach (var path in paths)
            {
                current = ResolveByNode(current, path);
            }

            return (T)current?.Value;

            // string path = form.Schema.Properties.GetPath();

            // return form.Schema.Properties[field];

            //return form.ResolvePath<T>($"{path}/{field}");
        }

        public static IAnyObservable GetField(this IFormObservable form, string field)
        {
            return form.GetField<IAnyObservable>(field);
        }

        public static IStringObservable GetStringField(this IFormObservable form, string field)
        {
            return form.GetField<IStringObservable>(field);
        }

        public static INumberObservable GetNumberField(this IFormObservable form, string field)
        {
            return form.GetField<INumberObservable>(field);
        }

        public static IBooleanObservable GetBooleanField(this IFormObservable form, string field)
        {
            return form.GetField<IBooleanObservable>(field);
        }

        public static IObjectObservable GetObjectField(this IFormObservable form, string field)
        {
            return form.GetField<IObjectObservable>(field);
        }

        public static IArrayObservable GetArrayField(this IFormObservable form, string field)
        {
            return form.GetField<IArrayObservable>(field);
        }
    }
}
