using Microsoft.AspNetCore.Components;
using Skclusive.Core.Component;
using Skclusive.Material.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skclusive.Mobx.Form
{
    public partial class FormContent : MaterialComponentBase
    {
        public FormContent() : base("FormContent")
        {
        }

        [Parameter]
        public IFormObservable Form { set; get; }
    }
}
