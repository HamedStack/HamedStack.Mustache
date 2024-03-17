﻿using System.Dynamic;
using System.Reflection;
using System.Text.RegularExpressions;
using HamedStack.Mustache.Core;
using HamedStack.Mustache.Tags;
using Microsoft.Extensions.FileProviders;

namespace HamedStack.Mustache
{
    public static class MustacheSharpenExtensions
    {
        private const string TemplateRegex = @"{{#template\s+.+\s*}}";
        private const string OpenBraceReplacement = @"$@$@$***___@$%$";
        private const string TemplateReplacement = @"$@$@$***___@$%$#template";
        private const string TemplateTag = @"{{#template";
        public static Generator CompileInMemoryNestedTemplates(this FormatCompiler compiler, string format, Dictionary<string, string> templates)
        {
            var text = compiler.ResolveInMemoryNestedTemplates(format, templates);
            return compiler.Compile(text);
        }
        public static Generator CompileInMemoryNestedTemplates(this HtmlFormatCompiler compiler, string format, Dictionary<string, string> templates)
        {
            var text = compiler.ResolveInMemoryNestedTemplates(format, templates);
            return compiler.Compile(text);
        }
        public static Generator CompileNestedTemplates(this FormatCompiler compiler, string format)
        {
            var text = compiler.ResolveNestedTemplates(format, true);
            return compiler.Compile(text);
        }
        public static Generator CompileNestedTemplates(this HtmlFormatCompiler compiler, string format)
        {
            var text = compiler.ResolveNestedTemplates(format, true);
            return compiler.Compile(text);
        }
        private static IEnumerable<IFileInfo> GetResources(this Assembly assembly, Regex regex = null)
        {
            List<IFileInfo> files = new List<IFileInfo>();
            var embedded = new EmbeddedFileProvider(assembly);
            var dirContents = embedded.GetDirectoryContents(Path.DirectorySeparatorChar.ToString());
            foreach (var resource in regex != null ? dirContents.Where(x => regex.IsMatch(x.Name)) : dirContents)
                files.Add(resource);
            return files;
        }
        public static void RegistersCustomTags(this HtmlFormatCompiler compiler)
        {
            compiler.RegisterTag(new TemplateDefinition(), true);
            compiler.RegisterTag(new IsNullOrEmptyTagDefinition(), true);
            compiler.RegisterTag(new AnyTagDefinition(), true);
            compiler.RegisterTag(new CamelizeTagDefinition(), true);
            compiler.RegisterTag(new LowerTagDefinition(), true);
            compiler.RegisterTag(new UpperTagDefinition(), true);
            compiler.RegisterTag(new TabTagDefinition(), true);
            compiler.RegisterTag(new CommentTagDefinition(), true);
        }
        public static void RegistersCustomTags(this FormatCompiler compiler)
        {
            compiler.RegisterTag(new TemplateDefinition(), true);
            compiler.RegisterTag(new IsNullOrEmptyTagDefinition(), true);
            compiler.RegisterTag(new AnyTagDefinition(), true);
            compiler.RegisterTag(new CamelizeTagDefinition(), true);
            compiler.RegisterTag(new LowerTagDefinition(), true);
            compiler.RegisterTag(new UpperTagDefinition(), true);
            compiler.RegisterTag(new TabTagDefinition(), true);
            compiler.RegisterTag(new CommentTagDefinition(), true);

        }
        private static string ResolveInMemoryNestedTemplates(this FormatCompiler compiler, string format, Dictionary<string, string> templates)
        {
            if (templates != null && templates.Count > 0)
            {
                Dictionary<string, object> obj = new Dictionary<string, object>();
                foreach (var tuple in templates)
                    obj.Add(tuple.Key, tuple.Value);
                var anonymous = obj.ToDynamicObject();
                while (true)
                {

                    if (Regex.IsMatch(format, TemplateRegex))
                    {
                        format = format.Replace("{{", OpenBraceReplacement);
                        format = format.Replace(TemplateReplacement, TemplateTag);
                        Generator generator = compiler.Compile(format);
                        format = generator.Render(anonymous);
                    }
                    else
                    {
                        format = format.Replace(OpenBraceReplacement, "{{");
                        return format;
                    }
                }
            }
            return format;

        }
        private static string ResolveInMemoryNestedTemplates(this HtmlFormatCompiler compiler, string format, Dictionary<string, string> templates)
        {
            if (templates != null && templates.Count > 0)
            {
                Dictionary<string, object> obj = new Dictionary<string, object>();
                foreach (var tuple in templates)
                    obj.Add(tuple.Key, tuple.Value);
                var anonymous = obj.ToDynamicObject();
                while (true)
                {

                    if (Regex.IsMatch(format, TemplateRegex))
                    {
                        format = format.Replace("{{", OpenBraceReplacement);
                        format = format.Replace(TemplateReplacement, TemplateTag);
                        Generator generator = compiler.Compile(format);
                        format = generator.Render(anonymous);
                    }
                    else
                    {
                        format = format.Replace(OpenBraceReplacement, "{{");
                        return format;
                    }
                }
            }
            return format;
        }
        private static string ResolveNestedTemplates(this FormatCompiler compiler, string format, bool searchDirectory = false)
        {
            var assembly = Assembly.GetEntryAssembly();
            Dictionary<string, object> resources = new Dictionary<string, object>();
            foreach (var file in assembly.GetResources(new Regex(@".*\.mustache", RegexOptions.IgnoreCase)))
            {
                var fName = Path.GetFileNameWithoutExtension(file.Name).ToLowerInvariant();
                if (fName.Contains("/"))
                {
                    var lastIndex = fName.LastIndexOf('/');
                    fName = fName.Substring(lastIndex);
                }
                if (!resources.ContainsKey(fName))
                    resources.Add(fName, file.CreateReadStream().ToString());
            }

            if (searchDirectory && !(assembly is null))
            {
                foreach (var file in Directory.EnumerateFiles(assembly.Location, "*.mustache", SearchOption.AllDirectories))
                {
                    var name = Path.GetFileNameWithoutExtension(file).ToLowerInvariant();
                    if (!resources.ContainsKey(name))
                        resources.Add(name, File.ReadAllText(file));
                }
            }
            if (resources.Count > 0)
            {
                var anonymous = resources.ToDynamicObject();
                while (true)
                {

                    if (Regex.IsMatch(format, TemplateRegex))
                    {
                        format = format.Replace("{{", OpenBraceReplacement);
                        format = format.Replace(TemplateReplacement, TemplateTag);
                        Generator generator = compiler.Compile(format);
                        format = generator.Render(anonymous);
                    }
                    else
                    {
                        format = format.Replace(OpenBraceReplacement, "{{");
                        return format;
                    }
                }
            }
            return format;

        }
        private static string ResolveNestedTemplates(this HtmlFormatCompiler compiler, string format, bool searchDirectory = false)
        {
            var assembly = Assembly.GetEntryAssembly();
            Dictionary<string, object> resources = new Dictionary<string, object>();
            foreach (var file in assembly.GetResources(new Regex(@".*\.mustache", RegexOptions.IgnoreCase)))
            {
                var fName = Path.GetFileNameWithoutExtension(file.Name).ToLowerInvariant();
                if (fName.Contains("/"))
                {
                    var lastIndex = fName.LastIndexOf('/');
                    fName = fName.Substring(lastIndex);
                }
                if (!resources.ContainsKey(fName))
                    resources.Add(fName, file.CreateReadStream().ToString());
            }

            if (searchDirectory && !(assembly is null))
            {
                foreach (var file in Directory.EnumerateFiles(assembly.Location, "*.mustache", SearchOption.AllDirectories))
                {
                    var name = Path.GetFileNameWithoutExtension(file).ToLowerInvariant();
                    if (!resources.ContainsKey(name))
                        resources.Add(name, File.ReadAllText(file));
                }
            }
            if (resources.Count > 0)
            {
                var anonymous = resources.ToDynamicObject();
                while (true)
                {

                    if (Regex.IsMatch(format, TemplateRegex))
                    {
                        format = format.Replace("{{", OpenBraceReplacement);
                        format = format.Replace(TemplateReplacement, TemplateTag);
                        Generator generator = compiler.Compile(format);
                        format = generator.Render(anonymous);
                    }
                    else
                    {
                        format = format.Replace(OpenBraceReplacement, "{{");
                        return format;
                    }
                }
            }
            return format;
        }
        private static dynamic ToDynamicObject(this IDictionary<string, object> source)
        {
            ICollection<KeyValuePair<string, object>> someObject = new ExpandoObject();
            foreach (var item in source)
            {
                someObject.Add(item);
            }
            return someObject;
        }
    }
}
