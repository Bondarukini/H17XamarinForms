using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace H17XamarinForms.Services
{
    public class DeepCopy
    {
        /// <summary>
        /// Create new objet in memory with same parametrs as parent
        /// </summary>
        /// <typeparam name="T">Object type work with</typeparam>
        /// <param name="obj">Deep copy from this object</param>
        static public T CopyFrom<T>(T obj)
        {
            var properites = obj.GetType().GetProperties();
            object newObject = Activator.CreateInstance(obj.GetType());

            foreach (var prop in properites)
                newObject.GetType().GetProperty(prop.Name).SetValue(newObject, prop.GetValue(obj));

            return (T)newObject;
        }
    }
}
