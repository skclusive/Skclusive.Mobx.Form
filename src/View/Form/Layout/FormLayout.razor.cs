using Microsoft.AspNetCore.Components;
using Skclusive.Core.Component;
using Skclusive.Material.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skclusive.Mobx.Form
{
    public partial class FormLayout : MaterialComponentBase
    {
        public FormLayout() : base("FormLayout")
        {
        }

        [Parameter]
        public bool Fluid { set; get; }

        [Parameter]
        public bool Center { set; get; } = true;

        [Parameter]
        public Direction Direction { set; get; } = Direction.Column;

        [Parameter]
        public IList<IOutlineObservable> Outlines { set; get; }

        [Parameter]
        public RenderFragment<string> ChildContent { set; get; }

        private Direction InverseDirection => Direction == Direction.Row ? Direction.Column : Direction.Row;
    }
}
