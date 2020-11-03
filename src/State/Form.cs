using System;
using System.Collections.Generic;
using Skclusive.Mobx.JsonSchema;

namespace Skclusive.Mobx.Form
{
    public interface IFormPrimitive
    {
        string Title { set; get; }

        string Cancel { set; get; }

        string Submit { set; get; }
    }

    public interface IForm : IFormPrimitive
    {
        ISection[] Sections { set; get; }

        IObject Schema { set; get; } 
    }

    public class Form : IForm
    {
        public string Title { set; get; }

        public string Cancel { set; get; }

        public string Submit { set; get; }

        public IObject Schema { set; get; }

        public ISection[] Sections { set; get; }
    }
}
