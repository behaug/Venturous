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
            var fields = control.GetType().GetFields(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(WatElement))
                    AssignElement(field, control);

                if (field.FieldType.IsSubclassOf(typeof(WatControl)))
                    AssignControl(field, control);
            }
        }

        private void AssignControl(FieldInfo field, WatControl control)
        {
            var instance = (WatControl)field.FieldType.GetConstructor(new Type[0]).Invoke(null);
            instance.InitializeControl(CreateElement(field, control));
            field.SetValue(control, instance);
        }

        private void AssignElement(FieldInfo field, WatControl control)
        {
            var auto = field.GetValue(control) as AutoWatElement;
            if (auto == null)
                return; // This field is not marked with Auto

            var element = CreateElement(field, control);
            field.SetValue(control, element);
        }

        private WatElement CreateElement(FieldInfo field, WatControl control)
        {
            var auto = field.GetValue(control) as AutoWatElement;

            By finder;
            if (auto != null && auto.Finder != By.Auto)
                finder = auto.Finder;
            else
                finder = By.Id(GetElementId(field.Name));

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
