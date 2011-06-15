using System;
using WatiN.Core;
using WatiN.Core.Constraints;

namespace Venturous
{
    public class By
    {
        internal Constraint Constraint { get; private set; }
        internal string Description { get; private set; }

        internal static readonly By Body = new By { Constraint = null, Description = "body" };

        /// <summary>Finds by id</summary>
        public static By Id(string id)
        {
            return new By
            {
                Constraint = Find.ByElement(e => (e.Id ?? "").EndsWith(id)),
                Description = "#" + id
            };
        }

        /// <summary>Finds by CSS class</summary>
        public static By Class(string cssClass)
        {
            return new By
            {
                Constraint = Find.ByElement(e => e.ClassName == cssClass),
                Description = "." + cssClass
            };
        }

        /// <summary>Finds by the given attribute</summary>
        public static By Attribute(string attributeName, string value)
        {
            return new By
            {
                Constraint = Find.By(attributeName, value),
                Description = "[" + attributeName + "=" + value + "]"
            };
        }

        /// <summary>Finds by the given tag name, taking the first one</summary>
        public static By Tag(string tagName)
        {
            return new By
            {
                Constraint = Find.ByElement(e => e.TagName.ToLower() == tagName.ToLower()),
                Description = tagName
            };
        }

        /// <summary>Finds by the given tag name, taking the one at the given index</summary>
        public static By Tag(string tagName, int index)
        {
            return new By
            {
                Constraint = Find.ByElement(e => e.TagName.ToLower() == tagName.ToLower()).And(Find.ByIndex(index)),
                Description = tagName + ":" + index
            };
        }
    }
}