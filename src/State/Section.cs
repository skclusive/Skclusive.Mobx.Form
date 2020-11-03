using System;
using System.Collections.Generic;

namespace Skclusive.Mobx.Form
{
    public interface ISectionPrimitive
    {
        string Title { set; get; }

        bool Selected { set; get; }
    }

    public interface ISection : ISectionPrimitive
    {
        IOutline[] Outlines { set; get; }
    }

    public class Section : ISection
    {
        public string Title { set; get; }

        public bool Selected { set; get; }

        public IOutline[] Outlines { set; get; }
    }
}
