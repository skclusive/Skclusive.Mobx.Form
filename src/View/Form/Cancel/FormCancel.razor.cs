using Microsoft.AspNetCore.Components;
using Skclusive.Core.Component;
using Skclusive.Material.Core;
using Skclusive.Material.Button;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skclusive.Mobx.Form
{
    public partial class FormCancel : MaterialComponentBase
    {
        public FormCancel() : base("FormCancel")
        {
        }

        [Parameter]
        public IFormObservable Form { set; get; }

        [Parameter]
        public ButtonVariant Variant { set; get; } = ButtonVariant.Contained;

        [Parameter]
        public Color Color { set; get; } = Color.Primary;

        [Parameter]
        public string Label { set; get; }

        [Parameter]
        public bool Reset { set; get; }

        [Parameter]
        public EventCallback<IFormObservable> OnCancel { set; get; }

        protected string Text => Label ?? Form?.Cancel ?? "Cancel";

        protected async Task HandleCancelAsync(EventArgs args)
        {
            if (Reset)
            {
                Form.Reset();
            }
            await OnCancel.InvokeAsync(Form);
        }
    }
}
