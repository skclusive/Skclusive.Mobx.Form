using Microsoft.AspNetCore.Components;
using Skclusive.Core.Component;
using Skclusive.Material.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skclusive.Mobx.Form
{
    public partial class FlexItem : MaterialComponent
    {
        public FlexItem() : base("FlexItem")
        {
        }

        [Parameter]
        public bool Center { set; get; }

        [Parameter]
        public bool Fluid { set; get; }

        [Parameter]
        public string CenterStyle { set; get; }

        [Parameter]
        public string CenterClass { set; get; }

        protected override IEnumerable<string> Classes
        {
            get
            {
                foreach (var item in base.Classes)
                    yield return item;

                if (Fluid)
                    yield return nameof(Fluid);
            }
        }

        protected virtual string _CenterStyle
        {
            get => CssUtil.ToStyle(CenterStyles, CenterStyle);
        }

        protected virtual IEnumerable<Tuple<string, object>> CenterStyles
        {
            get => Enumerable.Empty<Tuple<string, object>>();
        }

        protected virtual string _CenterClass
        {
            get => CssUtil.ToClass(Selector, CenterClasses, CenterClass);
        }

        protected virtual IEnumerable<string> CenterClasses
        {
            get
            {
                yield return nameof(Center);
            }
        }
    }
}
