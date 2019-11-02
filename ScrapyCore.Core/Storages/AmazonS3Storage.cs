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
                    Key = Prefix + path
                });
                return obj.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("Get string exception:" + path, ex);
                return null;
            }
        }

        public override Task WriteBytes(byte[] byteArray, string path)
        {
            throw new NotImplementedException();
        }

        public override async Task WriteStream(Stream stream, string path)
        {
            try
            {
                var response = await this.amazonS3Client.PutObjectAsync(new PutObjectRequest()
                {
                    InputStream = stream,
                    Key = Prefix + path
                });
            }
            catch (Exception ex)
            {
                logger.Error("Put Object exception:" + path, ex);
            }


        }
    }
}
