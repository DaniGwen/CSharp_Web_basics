using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIS.MvcFramework.Mapping
{
    public static class ModelMapper
    {
        private static void MapProperty<T>(object originInstance, object destinationInstance, PropertyInfo originProperty, PropertyInfo destinationProperty, T destinationPropertyType)
        {
            if (destinationProperty != null)
            {
                if (destinationProperty.PropertyType == typeof(string))
                {
                    destinationProperty.SetValue(destinationInstance, originProperty.GetValue(originInstance).ToString());
                }
                else if (typeof(IEnumerable).IsAssignableFrom(destinationProperty.PropertyType))
                {
                    var originCollection = (IEnumerable)originProperty
                        .GetValue(originInstance);

                    var destinationElementType = destinationProperty.GetValue(destinationInstance)
                        .GetType()
                        .GetGenericArguments()[0];

                    var destinationCollection = (IList)Activator.CreateInstance(destinationProperty.PropertyType);

                    foreach (var originElement in originCollection)
                    {
                        destinationCollection.Add(MapObject(originElement, destinationElementType));
                    }

                    destinationProperty.SetValue(destinationInstance, destinationCollection);
                }
                else
                {
                    destinationProperty.SetValue(destinationInstance, originProperty.GetValue(originInstance));
                }
            }
        }

        private static object MapObject(object origin, Type destinationType)
        {
            var destinationInstance = Activator.CreateInstance(destinationType);

            foreach (var originProperty in origin.GetType().GetProperties())
            {
                string propertyName = originProperty.Name;
                PropertyInfo destinationProperty = destinationInstance.GetType().GetProperty(propertyName);

                MapProperty(origin, destinationInstance, originProperty, destinationProperty, destinationProperty?.PropertyType);
            }
            return destinationInstance;
        }
        public static TDestination ProjectTo<TDestination>(object origin)
        {
            return (TDestination)MapObject(origin, typeof(TDestination));
        }
    }
}
