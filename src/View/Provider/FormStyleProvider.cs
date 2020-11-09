using Skclusive.Core.Component;

namespace Skclusive.Mobx.Form
{
    public class FormStyleProvider : StyleTypeProvider
    {
        public FormStyleProvider() : base
        (
            typeof(FormFooterStyle),

            typeof(FlexItemStyle),

            typeof(FlexSetStyle)
        )
        {
        }
    }
}
