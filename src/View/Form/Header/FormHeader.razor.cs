using Microsoft.AspNetCore.Components;
using Skclusive.Core.Component;
using Skclusive.Material.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skclusive.Mobx.Form
{
    public partial class FormHeader : MaterialComponentBase
    {
        public FormHeader() : base("FormHeader")
        {
        }

        [Parameter]
        public IFormObservable Form { set; get; }

        protected string Selected => Form?.Selected?.Title;

        private void HandleChange(object value)
        {
            if (!string.IsNullOrWhiteSpace(value?.ToString()))
            {
                Form.MakeSelection(value?.ToString());
            }
        }

        protected string TabStyle(ISectionObservable section)
        {
            return HasSectionError(section, Form) ? $"color: var(--theme-palette-secondary-main);" : ""; // "color: var(--theme-palette-primary-main);";
        }

        protected Color IndicatorColor => HasSelectedError ? Color.Secondary : Color.Primary;

        protected Color TextColor => HasSelectedError ? Color.Secondary : Color.Primary;

        protected bool HasSelectedError => Form != null && HasSectionError(Form.Selected, Form);

        protected string HasAnySectionError => Form != null ? string.Join(", ", Form.Sections.Where(section => HasSectionError(section, Form)).Select(section => section.Title)) : "";

        protected bool HasSectionError(ISectionObservable section, IFormObservable form)
        {
            return HasOutlineError(section.Outline, form);
        }

        protected bool HasOutlineError(IOutlineObservable outline, IFormObservable form)
        {
            var fields = outline.FlatOutline();

            return fields.Any(field => !form.GetField(field).Valid);//.Any(hasError => hasError);
        }
    }
}
