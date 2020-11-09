using Microsoft.AspNetCore.Components;
using Skclusive.Core.Component;
using Skclusive.Material.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skclusive.Mobx.Form
{
    public partial class FlexSet : MaterialComponent
    {
        public FlexSet() : base("FlexSet")
        {
        }

        [Parameter]
        public bool Fluid { set; get; }

        [Parameter]
        public Direction Direction { set; get; }

        protected override IEnumerable<string> Classes
        {
            get
            {
                foreach (var item in base.Classes)
                    yield return item;

                if (Fluid)
                    yield return nameof(Fluid);

                if (Direction == Direction.Row)
                    yield return nameof(Direction.Row);

                if (Direction == Direction.Column)
                    yield return nameof(Direction.Column);
            }
        }
    }
}
