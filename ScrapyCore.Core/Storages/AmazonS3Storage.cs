using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.External;

namespace ScrapyCore.Core.Storages
{
    public class AmazonS3Storage : Storage
    {
        private readonly IAmazonS3 amazonS3Client;
        private readonly string bucketName;

        public AmazonS3Storage(IStorageConfigure configure)
            : this(configure.Prefix)
        {
            AmazonS3Config amazonS3Config = new AmazonS3Config()
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(configure.ConfigureDetail.DefaultValue("region"))
            };
            bucketName = configure.ConfigureDetail.DefaultValue("bucket");
            amazonS3Client = new AmazonS3Client(amazonS3Config);
        }

        public AmazonS3Storage(string prefix)
            : base(prefix)
        {
        }

        public override string StorageName => "Amazon S3";

        public override string GetString(string path)
        {
            return GetStringAsync(path).Result;
        }

        public override async Task<string> GetStringAsync(string path)
        {
            try
            {
                var obj = await amazonS3Client.GetObjectAsync(new GetObjectRequest()
                {
                    BucketName = this.bucketName,
                    Key = Path.Combine(Prefix,path)
                });
                StreamReader streamReader = new StreamReader(obj.ResponseStream);
                return await streamReader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                logger.Error("Get string exception:" + path, ex);
                return null;
            }
        }

        public override async Task ReadAsStream(string path, Func<Stream, Task> streamUsage)
        {
            try
            {
                var response = await this.amazonS3Client.GetObjectAsync(new GetObjectRequest()
                {
                    BucketName = bucketName,
                    Key = Prefix + path,
                });

                using (var stream = response.ResponseStream)
                {
                    await streamUsage(stream);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Get Object exception:" + path, ex);
            }
        }

        public override Task WriteBytes(byte[] byteArray, string path)
        {
            MemoryStream ms = new MemoryStream(byteArray);
            ms.Seek(0, SeekOrigin.Begin);
            return WriteStream(ms,path);
            
        }

        public override async Task WriteStream(Stream stream, string path)
        {
            try
            {
                var response = await this.amazonS3Client.PutObjectAsync(new PutObjectRequest()
                {
                    BucketName = bucketName,
                    InputStream = stream,
                    Key = Path.Combine(Prefix , path)
                });
            }
            catch (Exception ex)
            {
                logger.Error("Put Object exception:" + path, ex);
            }


        }
    }
}
