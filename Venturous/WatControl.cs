using System;
using System.Reflection;
using Venturous.Infrastructure;

namespace Venturous
{
    /// <summary>
    /// Represents a user control in a web page
    /// </summary>
    public abstract class WatControl
    {
        internal protected WatElement Element;
        private readonly AutoFieldInitializer _initializer = new AutoFieldInitializer();

        internal void InitializeControl(WatElement element)
        {
            Element = element;
            _initializer.InitializeAutoFields(this);
        }

        /// <summary>Indicates that the element will be automatically assigned</summary>
        protected static WatElement Auto()
        {
            return new AutoWatElement();
        }

        /// <summary>Indicates that the element will be automatically assigned</summary>
        protected static WatElement Auto(By finder)
        {
            return new AutoWatElement(finder);
        }

        /// <summary>Indicates that the element will be automatically assigned</summary>
        protected static WatElement Auto(string id)
        {
            return Auto(By.Id(id));
        }

        /// <summary>Indicates that the control will be automatically assigned</summary>
        protected static TControl Auto<TControl>() 
            where TControl : WatControl
        {
            return null; // TODO: Use Proxy to enable custom finder
        }
    }
}