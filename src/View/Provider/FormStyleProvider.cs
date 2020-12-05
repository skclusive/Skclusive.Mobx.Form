using Skclusive.Core.Component;

namespace Skclusive.Mobx.Form
{
    public class FormStyleProvider : StyleTypeProvider
    {
        public FormStyleProvider() : base
        (
            priority: 1200,

            typeof(FormFooterStyle),

            typeof(FlexItemStyle),

            typeof(FlexSetStyle)
        )
        {
        }
    }
}
