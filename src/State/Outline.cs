using System;
using System.Collections.Generic;

namespace Skclusive.Mobx.Form
{
    public interface IOutlinePrimitive
    {
        string Title { set; get; }
    }

    public interface IOutline : IOutlinePrimitive
    {
        IOutline[] Items { set; get; }
    }

    public class Outline : IOutline
    {
        public string Title { set; get; }

        public IOutline[] Items { set; get; }

        public static implicit operator Outline(string title) => new Outline { Title = title };

        public static implicit operator Outline(Outline[] items) => new Outline { Items = items };
    }
}
