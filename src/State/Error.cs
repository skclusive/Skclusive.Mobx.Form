using System;
using System.Collections.Generic;
using Skclusive.Core.Collection;
using Skclusive.Mobx.JsonSchema;

namespace Skclusive.Mobx.Form
{
    public interface IError
    {
        string[] Errors { get; }

        IMap<string, IError> Properties { get; }

        IList<IError> Items { get; }
    }

    public class Error : IError
    {
        public string[] Errors { set; get; }

        public IMap<string, IError> Properties { set; get; }

        public IList<IError> Items { set; get; }
    }
}
