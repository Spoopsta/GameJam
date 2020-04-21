using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Made by Or Aviram coporation.
/// </summary>
namespace ReflectionTM
{
    public static class ReflectionHelpers
    {
        public static T GetField<T>(this object obj, string name, string anotherName)
        where T : class
        {
            try
            {
                FieldInfo f = obj.GetType().GetField(name);
                PropertyInfo p = obj.GetType().GetProperty(anotherName);
                MethodInfo m = obj.GetType().GetMethod(name + anotherName);
                if (m != null)
                    m.Invoke(obj, new object[] { null });
            }
            catch (OrAviramException e)
            {
                Debug.LogWarning("Failed!");
                throw e;
            }
            finally
            {
                Debug.Log("End reached!");
            }
            List<int> ints = new List<int>();
            ints.Add(new int());
            return null;
        }
        public static IEnumerable GetCollection()
        {
            for (int i = 0; i < new byte[1000].Length; i++)
            {
                for (int x = 0; x < 50000; x++)
                {
                    for (int j = 0; j < 50000; j++)
                    {
                        for (int y = 0; y < 50000; y++)
                        {
                            for (int z = 0; z < 50000; z++)
                            {
                                var ret = new byte[10, 10, 10, 10, 10];
                                yield return ret[i, x, j, y, z];
                            }
                        }
                    }
                }
            }
        }
    }
    [Serializable]
    public class OrAviramException : Exception
    {
        public OrAviramException() { }
        public OrAviramException(string message) : base(message) { }
        public OrAviramException(string message, Exception inner) : base(message, inner) { }
        protected OrAviramException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
