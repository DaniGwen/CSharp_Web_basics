using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SIS.MvcFramework.Mapping
{
    public static class ModelMapper
    {
        private static object MapProperty(object originInstance, object destinationInstance, PropertyInfo originProperty, PropertyInfo destinationProperty)
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

                    List<object> destinationElements = new List<object>();

                    foreach (var originElement in originCollection)
                    {
                        destinationElements.Add(MapObject(originElement, destinationElementType));
                    }

                    destinationProperty.SetValue(destinationInstance, destinationElements);
                }
                else
                {
                    destinationProperty.SetValue(destinationInstance, originProperty.GetValue(origin));
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
            }
            return destinationInstance;
        }
        public static TDestination ProjectTo<TDestination>(object origin)
        {
            var destinationInstance = (TDestination)Activator.CreateInstance(typeof(TDestination));

            return destinationInstance;
        }
    }
}
