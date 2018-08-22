using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ConvertExt
    {
        public static Type[] GenericTypes;
        /// <summary>
        /// 将object转为Data数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="SourceT">被赋值类</param>
        /// <param name="TragetT">源类</param>
        /// <param name="IsConvertNull">是否将Null转化给Data</param>
        /// <param name="ConvertRule">特殊转化规则key为源属性，value为目标属性</param>
        /// <returns></returns>
        public static T O2D<T, T1>(this T SourceT, T1 TragetT, bool IsConvertNull = false, List<KeyValuePair<string, string>> ConvertRule = null, string noValue = null)
        {
            if (SourceT == null)
            {
                SourceT = System.Activator.CreateInstance<T>();
            }
            if (TragetT != null)
            {
                Type type = typeof(T);
                Type type1 = typeof(T1);
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    KeyValuePair<string, string> rule = ConvertRule != null ? ConvertRule.Where(p => p.Key == prop.Name).FirstOrDefault() : new KeyValuePair<string, string>();
                    if (ConvertRule != null && !default(KeyValuePair<string, string>).Equals(rule)) //判断rule和ConvertRule是否都为空
                    {
                        object tragetValue = type1.GetProperty(rule.Value).GetValue(TragetT);
                        if (!IsConvertNull)
                        {
                            if (tragetValue != null)
                            {
                                prop.SetValue(SourceT, tragetValue);
                            }
                        }
                        else
                        {
                            prop.SetValue(SourceT, tragetValue);
                        }
                    }
                    else
                    {
                        PropertyInfo tragetProp = type1.GetProperty(prop.Name);
                        if (tragetProp != null && (noValue != null && !noValue.Contains(prop.Name)))
                        {
                            Type convertType = null;
                            if (prop.PropertyType.Name != "Nullable`1")
                            {
                                convertType = prop.PropertyType;
                            }
                            else
                            {
                                convertType = prop.PropertyType.GetGenericArguments()[0];
                            }
                            object targetVal = tragetProp.GetValue(TragetT);
                            if (targetVal != default(object))
                            {
                                prop.SetValue(SourceT, Convert.ChangeType(tragetProp.GetValue(TragetT), convertType));
                            }
                        }
                    }
                }
            }
            return SourceT;
        }
        /// <summary>
        /// 将object转为Data数据(List形式)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="SourceList">被赋值类</param>
        /// <param name="TragetList">源类</param>
        /// <param name="IsConvertNull">是否将Null转化给Data</param>
        /// <param name="ConvertRule">特殊转化规则key为源属性，value为目标属性</param>
        /// <param name="noValue">不进行赋值的属性名称数组</param>
        /// <returns></returns>
        public static List<T> O2D<T, T1>(this List<T> SourceList, List<T1> TragetList, bool IsConvertNull = false, List<KeyValuePair<string, string>> ConvertRule = null,string noValue=null)
        {
            if (TragetList != null)
            {
                if (SourceList == null)
                {
                    SourceList = new List<T>();
                }
                for (int i = 0; i < TragetList.Count(); i++)
                {
                    if (SourceList.Count() == i)
                    {
                        SourceList.Add(System.Activator.CreateInstance<T>());
                    }
                    SourceList[i] = SourceList[i].O2D(TragetList[i], IsConvertNull, ConvertRule,noValue);
                }
            }
            return SourceList;
        }
        /// <summary>
        /// 将TragetT类中带有key value的存储形式的属性，赋值到SourceT类的属性中（针对于硬件平台的partstate）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="SourceT">被赋值类</param>
        /// <param name="TragetT">源类</param>
        /// <param name="KeyValueFields">KeyValue形式的属性名称</param>
        /// <param name="IsConvertNull">是否将Null转化给Data<</param>
        /// <param name="ConvertRule">特殊转化规则key为源属性，value为目标属性</param>
        /// <returns></returns>
        public static T O2D<T, T1>(this T SourceT, T1 TragetT, string[] KeyValueFields, bool IsConvertNull = false, List<KeyValuePair<string, string>> ConvertRule = null)
        {
            if (TragetT == null)
            {
                return default(T);
            }
            SourceT.O2D(TragetT, IsConvertNull, ConvertRule);
            if (KeyValueFields == null)
            {
                return SourceT;
            }
            foreach (var keyValueField in KeyValueFields)
            {

                Type typeTarget = typeof(T1);
                Type typeSource = typeof(T);
                PropertyInfo propertyTarget = typeTarget.GetProperty(keyValueField, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly | BindingFlags.GetProperty);
                foreach (var property in typeSource.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly | BindingFlags.GetProperty))
                {
                    if (propertyTarget.PropertyType.IsArray)   //判断是否为数组
                    {
                        SetArrayValue(SourceT, TragetT, propertyTarget, property);
                    }
                    if (propertyTarget.PropertyType.IsGenericType)   //判断是否为泛型（只识别dicctionary和keyvaluepair）
                    {
                        if (propertyTarget.PropertyType.Name.Contains("Dictionary"))
                        {
                            try
                            {
                                property.ConvertAndSetValue(SourceT, propertyTarget.PropertyType.GetProperty("Item").GetValue(propertyTarget.GetValue(TragetT), new[] { property.Name }));
                            }
                            catch (Exception e)
                            { }
                        }
                        if (propertyTarget.PropertyType.GetGenericArguments()[0].Name.Contains("KeyValuePair"))
                        {
                            SetKeyValuePairValue(SourceT, TragetT, propertyTarget, property);
                        }
                    }
                }
            }
            return SourceT;
        }
        /// <summary>
        /// 将TragetT的具有key的类赋给SourceT实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="SourceT"></param>
        /// <param name="TragetT"></param>
        /// <param name="propertyTarget"></param>
        /// <param name="propertySource"></param>
        private static void SetArrayValue<T, T1>(T SourceT, T1 TragetT, PropertyInfo propertyTarget, PropertyInfo propertySource)
        {
            Array keyValue = (Array)propertyTarget.GetValue(TragetT);
            if (keyValue != null)
            {
                foreach (var key in keyValue)
                {
                    PropertyInfo keyValueProperty = key.GetType().GetProperty("key", BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly | BindingFlags.GetProperty);

                    if (keyValueProperty.GetValue(key).Equals(propertySource.Name))
                    {
                        propertySource.ConvertAndSetValue(SourceT, key.GetType().GetProperty("value", BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly | BindingFlags.GetProperty).GetValue(key));
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 反射赋值并将源值转换成目标对象的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="propertyTraget"></param>
        /// <param name="TragetT">目标实体</param>
        /// <param name="SourceValue">源值</param>
        public static void ConvertAndSetValue<T, T1>(this PropertyInfo propertyTraget, T TragetT, T1 SourceValue)
        {
            Type convertType = null;
            if (propertyTraget.PropertyType.Name != "Nullable`1")
            {
                convertType = propertyTraget.PropertyType;
            }
            else
            {
                convertType = propertyTraget.PropertyType.GetGenericArguments()[0];
            }
            propertyTraget.SetValue(TragetT, Convert.ChangeType(SourceValue, convertType));
        }
        /// <summary>
        /// 将TragetT的KeyValuePair的类的Value赋给SourceT实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="SourceT"></param>
        /// <param name="TragetT"></param>
        /// <param name="propertyTarget"></param>
        /// <param name="propertySource"></param>
        private static void SetKeyValuePairValue<T, T1>(T SourceT, T1 TragetT, PropertyInfo propertyTarget, PropertyInfo propertySource)
        {
            int count = (int)propertyTarget.PropertyType.GetProperty("Count").GetValue(propertyTarget.GetValue(TragetT));
            for (int i = 0; i < count; i++)
            {
                object keyValuePair = propertyTarget.PropertyType.GetProperty("Item").GetValue(propertyTarget.GetValue(TragetT), new object[] { i });
                Type keyValuePairType = keyValuePair.GetType();
                PropertyInfo keyValuePairProp = keyValuePairType.GetProperty("Key");
                if (keyValuePairProp.GetValue(keyValuePair).Equals(propertySource.Name))
                {
                    propertySource.ConvertAndSetValue(SourceT, keyValuePairType.GetProperty("Value").GetValue(keyValuePair));
                    break;
                }
            }
        }
        /// <summary>
        /// 将实体赋空值的扩展方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operationT"></param>
        /// <param name="nullFields">将为空的属性名集合</param>
        /// <returns>未成功转换的属性名称，如果全部成功转换，则返回null</returns>
        public static string[] SetNull<T>(this T operationT, string[] nullFields)
        {
            string[] activeFields = SetNullOp(operationT, nullFields);
            int num = nullFields.Count(p => p != null);
            if (nullFields.Length != num + 1)
            {
                return ArrayDifferent(nullFields, activeFields);
            }
            return null;
        }
        /// <summary>
        /// 将实体赋空值的扩展方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operationT"></param>
        /// <param name="notNullFields">将要保留值的属性名集合</param>
        /// <returns>未成功排除的属性，如果全部成功，则返回null</returns>
        public static string[] SetNullNotField<T>(this T operationT, string[] notNullFields)
        {
            string[] failFields = null;
            List<string> nullFields = ReverseSelectPropName(typeof(T), notNullFields, out failFields);
            operationT.SetNull(nullFields.ToArray());
            return failFields;
        }
        /// <summary>
        /// 获取type的反向选择属性名称
        /// </summary>
        /// <param name="type">需要提取属性的类型</param>
        /// <param name="propNames">反向选取的属性名称集合</param>
        /// <param name="failPropery">没有找到的反向选取的属性名称集合</param>
        /// <returns></returns>
        public static List<string> ReverseSelectPropName(Type type, string[] propNames, out string[] failPropery)
        {
            List<string> reversePropNames = new List<string>();
            List<string> successProp = new List<string>();
            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly | BindingFlags.GetProperty))
            {
                bool success = false;
                foreach (string propName in propNames)
                {
                    if (propName == prop.Name)
                    {
                        success = true;
                    }
                }
                if (success)
                {
                    successProp.Add(prop.Name);
                }
                else
                {
                    reversePropNames.Add(prop.Name);
                }
            }
            failPropery = ArrayDifferent(propNames, successProp.ToArray());
            return reversePropNames;
        }
        private static string[] SetNullOp<T>(T operationT, string[] nullFields)
        {
            int num = 0;
            Type type = typeof(T);
            string[] activeFields = new string[nullFields.Length];
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly | BindingFlags.GetProperty))
            {
                foreach (string nullField in nullFields)
                {
                    if (nullField == property.Name)
                    {
                        if (property.PropertyType.IsValueType)
                            if (Nullable.GetUnderlyingType(property.PropertyType) == null)
                                continue;
                        property.SetValue(operationT, null);
                        activeFields[num] = nullField;
                        num++;
                    }
                }
            }
            return activeFields;
        }
        private static string[] ArrayDifferent(string[] Array1, string[] Array2)
        {
            int arrayLength = 0;
            if (Array1.Length >= Array2.Length)
                arrayLength = Array1.Length;
            else
                arrayLength = Array2.Length;
            string[] DiffFields = new string[Array1.Length];
            int failNum = 0;
            foreach (string array in Array1)
            {
                if (!Array2.Contains(array))
                {
                    DiffFields[failNum] = array;
                    failNum++;
                }
            }
            return DiffFields;
        }
    }
}
