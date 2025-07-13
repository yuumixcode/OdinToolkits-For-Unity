using System;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Shared
{
    [MultiLanguageComment("URL工具类", "URL utility class")]
    public static class UrlUtil
    {
        [MultiLanguageComment("验证并规范化URL", "Validate and normalize a URL")]
        public static string ValidateAndNormalizeUrl(string inputUrl, string fallbackUrl)
        {
            inputUrl = inputUrl?.Trim() ?? "";

            if (string.IsNullOrEmpty(inputUrl))
            {
                return fallbackUrl;
            }

            if (Uri.TryCreate(inputUrl, UriKind.Absolute, out Uri uriResult) &&
                IsValidWebProtocol(uriResult.Scheme))
            {
                return uriResult.ToString();
            }

            return fallbackUrl;
        }

        [MultiLanguageComment("判断协议是否为有效的Web协议", "Determine if a protocol is a valid web protocol")]
        static bool IsValidWebProtocol(string scheme) =>
            string.Equals(scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);
    }
}
