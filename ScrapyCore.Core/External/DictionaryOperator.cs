using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.External
{
    public static class DictionaryOperator
    {
        public static T DefaultValue<K, T>(this Dictionary<K, T> referenceDictionary, K key)
        {
            if (referenceDictionary == null)
                throw new ArgumentNullException(nameof(referenceDictionary), "This argument could not be nullable ");

            if (referenceDictionary.ContainsKey(key))
            {
                return referenceDictionary[key];
            }
            return default(T);
        }

    }
}
