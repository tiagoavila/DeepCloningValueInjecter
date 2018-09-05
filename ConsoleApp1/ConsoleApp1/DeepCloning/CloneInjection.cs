using Omu.ValueInjecter;
using Omu.ValueInjecter.Injections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleApp1.DeepCloning
{
    public class CloneInjection : LoopInjection
    {
        public CloneInjection()
        {

        }

        public CloneInjection(string[] ignoredProps) : base(ignoredProps)
        {

        }

        protected override void Execute(PropertyInfo sp, object source, object target)
        {
            if (ignoredProps == null || !ignoredProps.Contains(sp.Name))
            {
                var tp = target.GetType().GetProperty(sp.Name);
                if (tp == null) return;
                var val = sp.GetValue(source);
                if (val == null) return;

                tp.SetValue(target, GetClone(sp, tp, val));
            }
        }

        private static object GetClone(PropertyInfo sp, PropertyInfo tp, object val)
        {
            if (sp.PropertyType.IsValueType || sp.PropertyType == typeof(string))
            {
                return val;
            }

            if (sp.PropertyType.IsArray)
            {
                var arr = val as Array;
                var arrClone = arr.Clone() as Array;

                for (int index = 0; index < arr.Length; index++)
                {
                    var a = arr.GetValue(index);
                    if (a.GetType().IsValueType || a is string) continue;

                    arrClone.SetValue(Activator.CreateInstance(a.GetType()).InjectFrom<CloneInjection>(a), index);
                }

                return arrClone;
            }

            bool isDictionary = sp.PropertyType.IsGenericType && sp.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>);
            if (isDictionary)
            {
                return val;
            }

            if (sp.PropertyType.IsGenericType)
            {
                //handle IEnumerable<> also ICollection<> IList<> List<>
                if (sp.PropertyType.GetGenericTypeDefinition().GetInterfaces().Contains(typeof(IEnumerable)))
                {
                    var genericType = tp.PropertyType.GetGenericArguments()[0];

                    var listType = typeof(List<>).MakeGenericType(genericType);
                    var list = Activator.CreateInstance(listType);

                    var addMethod = listType.GetMethod("Add");
                    foreach (var o in val as IEnumerable)
                    {
                        var listItem = genericType.IsValueType || genericType == typeof(string) ? o : Activator.CreateInstance(genericType).InjectFrom<CloneInjection>(o);
                        addMethod.Invoke(list, new[] { listItem });
                    }

                    return list;
                }

                //unhandled generic type, you could also return null or throw
                return val;
            }

            return Activator.CreateInstance(tp.PropertyType)
                .InjectFrom<CloneInjection>(val);
        }
    }
}
