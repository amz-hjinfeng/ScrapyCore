using System;
using System.Collections.Generic;
using System.Text;
using ScrapyCore.Core.External.Conventor;

namespace ScrapyCore.Core.External
{
    public static class DictionaryOperator
    {
        public static T DefaultValue<K, T>(this IDictionary<K, T> referenceDictionary, K key)
        {
            if (referenceDictionary == null)
                throw new ArgumentNullException(nameof(referenceDictionary), "This argument could not be nullable ");

            if (referenceDictionary.ContainsKey(key))
            {
                return referenceDictionary[key];
            }
            return default(T);
        }

        public static F GetKeyAndConvertTo<K,V,F>(this IDictionary<K,V> referenceDictionary, K key, IObjectConvertor<F,V> convertor)
        {
            V v = referenceDictionary.DefaultValue(key);
            return convertor.Parse(v);
        }
       
    }
}
