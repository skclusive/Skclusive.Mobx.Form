using Microsoft.AspNetCore.Components;
using Skclusive.Core.Component;
using Skclusive.Material.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Skclusive.Core.Collection;

namespace Skclusive.Mobx.Form
{
    public partial class FormView : MaterialComponentBase
    {
        public FormView() : base("FormView")
        {
        }

        [Parameter]
        public IFormObservable Form { set; get; }

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
    }
}
