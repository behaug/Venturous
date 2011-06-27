using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Venturous.Infrastructure
{
    public class AutoFieldInitializer
    {
        public void InitializeAutoFields(WatControl control)
        {
            InitializeAutoFields(control, control.GetType());
        }

        private void InitializeAutoFields(WatControl control, Type type)
        {
            var fields = type.GetFields(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(WatElement))
                    AssignElement(field, control);

                if (field.FieldType.IsSubclassOf(typeof(WatControl)))
                    AssignControl(field, control);
            }

            // Also initialize base classes
            if (type.BaseType != null && type.BaseType.IsSubclassOf(typeof(WatControl)))
                InitializeAutoFields(control, type.BaseType);
        }

        private void AssignControl(FieldInfo field, WatControl control)
        {
            var instance = field.GetValue(control) as WatControl;
            if (instance == null)
                return; // This control is not marked with Auto

            var auto = instance.Element as AutoWatElement;
            if (auto == null)
                return; // No auto element found

            var element = CreateElement(field.Name, auto, control);
            instance.InitializeControl(element);
        }

        private void AssignElement(FieldInfo field, WatControl control)
        {
            var auto = field.GetValue(control) as AutoWatElement;
            if (auto == null)
                return; // This field is not marked with Auto

            var element = CreateElement(field.Name, auto, control);
            field.SetValue(control, element);
        }

        private WatElement CreateElement(string fieldName, AutoWatElement auto, WatControl control)
        {
            By finder;
            if (auto != null && auto.Finder != By.Auto)
                finder = auto.Finder;
            else
                finder = By.Id(GetElementId(fieldName));

            return new WatElement(finder, control.Element, false);
        }

        public string GetElementId(string fieldName)
        {
            if (fieldName.StartsWith("_"))
                return fieldName.Substring(1);

            return fieldName;
        }
    }
}
