using Microsoft.AspNetCore.Components;
using Skclusive.Core.Component;
using Skclusive.Material.Core;
using Skclusive.Material.Button;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skclusive.Core.Collection;

namespace Skclusive.Mobx.Form
{
    public partial class FormSubmit : MaterialComponentBase
    {
        public FormSubmit() : base("FormSubmit")
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
        public EventCallback<IMap<string, object>> OnSubmit { set; get; }

        [Parameter]
        public EventCallback<IError> OnError { set; get; }

        protected bool InValid => Form != null && !Form.Valid;

        protected string Text => Label ?? Form?.Submit ?? "Submit";

        protected async Task HandleSubmit(EventArgs args)
        {
            Form.Validate();

            if (Form.Valid)
            {
                await OnSubmit.InvokeAsync(Form.Values);
            }
            else
            {
                await OnError.InvokeAsync(Form.Error);
            }
        }
    }
}
