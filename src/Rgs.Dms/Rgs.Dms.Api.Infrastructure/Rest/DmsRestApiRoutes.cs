using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rgs.Dms.Api.Infrastructure.Rest
{
    /// <summary>
    /// Роуты ЛК корпоративного ДМС.
    /// </summary>
    public class DmsRestApiRoutes
    {
        /// <summary>
        /// Префикс всех url для ЛК ДМС.
        /// </summary>
        /// <remarks> RGSSUP-13584 </remarks>
        public const string DmsRoutePrefix = RestApiHelper.DefaultRoutePrefix;
    }
}
