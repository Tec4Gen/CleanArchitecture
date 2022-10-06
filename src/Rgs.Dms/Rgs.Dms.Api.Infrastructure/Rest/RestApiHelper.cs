using Rgs.Dms.Api.Infrastructure.Rest;

namespace Rgs.Dms.Api.Infrastructure;

/// <summary>
/// Содержит в себе вспомогательные данные для формирования маршрутов к api-контроллерам и их методам.
/// </summary>
public class RestApiHelper
{
    /// <summary>
    /// Список запрещенных версий api.
    /// </summary>
    public const string ApiDeprecatedVersions = "1.4";

    /// <summary>
    /// Текущая версия api.
    /// </summary>
    public const string ApiCurrentVersion = RestApiVersion.ApiCurrentVersion;

    /// <summary>
    /// Стандартный префикс маршрутов api.
    /// </summary>
    public const string DefaultRoutePrefix = "api/rest/v{version:apiVersion}/";
}

