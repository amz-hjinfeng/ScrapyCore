using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel.Transform
{
    public class TransformDataSet
    {
        public Dictionary<String, TransformFieldWithValue> FieldValues { get; set; }

        public TransformDataSet()
        {
            FieldValues = new Dictionary<string, TransformFieldWithValue>();
        }

        public async Task<Stream> SerialzeToStream(string filetype)
        {
            // TODO: Extern the other serialzer.
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(streamWriter, FieldValues);
            await streamWriter.FlushAsync();
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
    }
}


