using Microsoft.AspNetCore.Components;
using Skclusive.Core.Component;
using Skclusive.Material.Core;
using Skclusive.Material.Button;
using System;
using System.Collections.Generic;
using System.Linq;
using Skclusive.Core.Collection;

namespace Skclusive.Mobx.Form
{
    public partial class FormFooter : MaterialComponentBase
    {
        public FormFooter() : base("FormFooter")
        {
        }

        [Parameter]
        public IFormObservable Form { set; get; }

        [Parameter]
        public ButtonVariant Variant { set; get; } = ButtonVariant.Contained;

        [Parameter]
        public Color Color { set; get; } = Color.Primary;

        [Parameter]
        public string Submit { set; get; }

        [Parameter]
        public string Cancel { set; get; }

        [Parameter]
        public bool Reset { set; get; }

        [Parameter]
        public EventCallback<IFormObservable> OnCancel { set; get; }

        [Parameter]
        public EventCallback<IMap<string, object>> OnSubmit { set; get; }

        [Parameter]
        public EventCallback<IError> OnError { set; get; }

        protected bool HasCancel => OnCancel.HasDelegate || !string.IsNullOrWhiteSpace(Cancel);

        [Parameter]
        public string ItemStyle { set; get; }

        [Parameter]
        public string ItemClass { set; get; }

        protected virtual string _ItemStyle
        {
            get => CssUtil.ToStyle(ItemStyles, ItemStyle);
        }

        protected virtual IEnumerable<Tuple<string, object>> ItemStyles
        {
            get => Enumerable.Empty<Tuple<string, object>>();
        }

        protected virtual string _ItemClass
        {
            get => CssUtil.ToClass(Selector, ItemClasses, ItemClass);
        }

        protected virtual IEnumerable<string> ItemClasses
        {
            get
            {
                yield return "Item";
            }
        }
    }
}
