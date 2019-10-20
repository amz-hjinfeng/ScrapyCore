using ScrapyCore.Fundamental.Kernel.Convertors.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Enums
{

    public abstract class EnumeratorConvertor : Convertor, IEnumerator<string>
    {
        public abstract string Current { get; }

        object IEnumerator.Current => this.Current;

        public virtual void Dispose()
        {
        }

        public abstract bool MoveNext();

        public abstract void Reset();
    }
}
