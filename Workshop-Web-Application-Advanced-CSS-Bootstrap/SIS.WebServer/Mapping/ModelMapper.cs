using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SIS.MvcFramework.Mapping
{
    public static class ModelMapper
    {
        public static TDestination ProjectTo<TDestination>(object origin, TDestination destination)
        {
            var destinationInstance = (TDestination)Activator.CreateInstance(destination.GetType());

            foreach (var originProperty in origin.GetType().GetProperties())
            {
                string propertyName = originProperty.Name;
                destination.GetType()
                    .GetProperty(propertyName)?
                    .SetValue(destination, originProperty
                    .GetValue(origin));

                PropertyInfo destinationProperty = destination.GetType().GetProperty(propertyName);

                if (destinationProperty != null)
                {
                    if (destinationProperty.PropertyType == typeof(string))
                    {
                        destinationProperty.SetValue(destinationInstance, originProperty.GetValue(origin).ToString());
                    }
                    else
                    {
                        destinationProperty.SetValue(destinationInstance, originProperty.GetValue(origin));
                    }
                }
            }

            return destinationInstance;
        }
    }
}
