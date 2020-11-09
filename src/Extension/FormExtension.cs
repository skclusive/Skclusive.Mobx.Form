using Microsoft.Extensions.DependencyInjection;
using Skclusive.Core.Component;
using Skclusive.Material.Core;
using Skclusive.Material.Theme;
using Skclusive.Mobx.Component;

namespace Skclusive.Mobx.Form
{
    public static class FormExtension
    {
        public static void TryAddMobxFormServices(this IServiceCollection services, IMaterialConfig config)
        {
            services.TryAddMobxServices(config);

            services.TryAddThemeServices(config);

            services.TryAddStyleTypeProvider<FormStyleProvider>();
        }
    }
}
