namespace Avalonia.DotNetFrameworkRuntime
{
    using System;
    using System.Text.RegularExpressions;

    public class ResourceManagerUriParser : GenericUriParser
    {
        private readonly Regex regex =
            new Regex(
                @"resm:\[(?<ass>\w+(?:.\w+)*)\]/(?<folder>[\w\.]+(?:/[\w\.-]+)*)/(?<file>[\w+\.-]+)|resm:\[(?<ass>\w+(?:.\w+)*)\]/(?<file>[\w+\.-]+)|resm:\[(?<ass>\w+(?:.\w+)*)\]/(?<folder>[\w\.-]+(?:/[\w\.-]+)*/)*");

        public ResourceManagerUriParser() : base(GetOptions())
        {
        }

        private static GenericUriParserOptions GetOptions()
        {

            return GenericUriParserOptions.AllowEmptyAuthority |
                                           GenericUriParserOptions.DontCompressPath | GenericUriParserOptions.DontConvertPathBackslashes |
                                           GenericUriParserOptions.DontUnescapePathDotsAndSlashes | GenericUriParserOptions.GenericAuthority |
                                           GenericUriParserOptions.NoFragment;
        }

        protected override void InitializeAndValidate(Uri uri, out UriFormatException parsingError)
        {
            parsingError = null;
        }

        protected override bool IsBaseOf(Uri baseUri, Uri relativeUri)
        {
            return base.IsBaseOf(baseUri, relativeUri);
        }

        protected override string GetComponents(Uri uri, UriComponents components, UriFormat format)
        {
            string assembly;
            string folder;
            string file;
            string absoluteUri;

            var m = regex.Match(uri.OriginalString);
            assembly = m.Groups["ass"].Value;
            folder = m.Groups["folder"].Value;
            file = m.Groups["file"].Value;

            var fileStr = folder == string.Empty ? file : $"/{file}";
            absoluteUri = $@"resm:[{assembly}]/{folder}{fileStr}";
            var folderUri = $@"{folder}/{file}";

            switch (components)
            {
                case UriComponents.AbsoluteUri:
                    return absoluteUri;
                case UriComponents.Host:
                    return assembly;
                case UriComponents.PathAndQuery:
                    return folderUri;
                case UriComponents.UserInfo:
                    return file;
            }

            return "{N/A}";
        }

        protected override string Resolve(Uri baseUri, Uri relativeUri, out UriFormatException parsingError)
        {
            parsingError = null;

            var resolve = baseUri.AbsoluteUri + relativeUri.OriginalString;
            return resolve;
        }

        protected override bool IsWellFormedOriginalString(Uri uri)
        {
            return regex.IsMatch(uri.OriginalString);
        }
    }
}