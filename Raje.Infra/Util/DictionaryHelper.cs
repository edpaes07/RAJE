using System.Collections.Generic;

namespace Raje.Infra.Util
{
    public static class DictionaryHelper
    {
        public static Dictionary<TKey, HashSet<TValue>> MergeValueForKeys<TKey, TValue>(this Dictionary<TKey, HashSet<TValue>> dic,
            IEnumerable<TKey> keys, TValue value)
        {
            foreach (TKey key in keys)
            {
                dic.GetOrCreateAndGet(key)
                    .Add(value);
            }
            return dic;
        }

        public static IEnumerable<TKey> GetKeysWithoutValue<TKey, TValue>(this Dictionary<TKey, HashSet<TValue>> dic, TValue value)
        {
            var retorno = new List<TKey>();
            foreach (KeyValuePair<TKey, HashSet<TValue>> item in dic)
            {
                if (!item.Value.Contains(value))
                    retorno.Add(item.Key);
            }

            return retorno;
        }

        public static TValue GetOrCreateAndGet<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key)
            where TValue : new()
        {
            if (!dic.TryGetValue(key, out TValue value))
            {
                value = new TValue();
                dic.Add(key, value);
            }
            return value;
        }

        public static void AddSafe<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            if (!dic.ContainsKey(key))
                dic.Add(key, value);
        }

        public static bool ContainsAnyKey<TKey, TValue>(this Dictionary<TKey, TValue> dic, HashSet<TKey> keys)
        {
            foreach (TKey key in keys)
            {
                if (dic.ContainsKey(key))
                    return true;
            }
            return false;
        }

        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> values)
        {
            foreach (KeyValuePair<TKey, TValue> value in values)
            {
                dic.Add(value.Key, value.Value);
            }
        }
    }
}