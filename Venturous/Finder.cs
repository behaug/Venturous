using System;
using WatiN.Core;
using WatiN.Core.Constraints;

namespace Venturous
{
    internal class Finder
    {
        public Constraint Constraint { get; private set; }
        public string Description { get; private set; }

        public static readonly Finder Body = new Finder { Constraint = null, Description = "body" };

        public static Finder ById(string id)
        {
            return new Finder
            {
                Constraint = Find.ByElement(e => (e.Id ?? "").EndsWith(id)),
                Description = "#" + id
            };
        }

        public static Finder ByClass(string cssClass)
        {
            return new Finder
            {
                Constraint = Find.ByElement(e => e.ClassName == cssClass),
                Description = "." + cssClass
            };
        }

        public static Finder ByAttribute(string attributeName, string value)
        {
            return new Finder
            {
                Constraint = Find.By(attributeName, value),
                Description = "[" + attributeName + "=" + value + "]"
            };
        }

        public static Finder ByTagName(string tagName)
        {
            return new Finder
            {
                Constraint = Find.ByElement(e => e.TagName.ToLower() == tagName.ToLower()),
                Description = tagName
            };
        }

        public static Finder ByTagName(string tagName, int index)
        {
            return new Finder
            {
                Constraint = Find.ByElement(e => e.TagName.ToLower() == tagName.ToLower()).And(Find.ByIndex(index)),
                Description = tagName + ":" + index
            };
        }
    }
}