using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.External;

namespace ScrapyCore.Core.Storages
{
    public class AmazonS3Storage :Storage
    {
        private readonly IAmazonS3 amazonS3Client;
        private readonly string bucketName;

        public AmazonS3Storage(IStorageConfigure configure)
            :this(configure.Prefix)
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
            var obj = await amazonS3Client.GetObjectAsync(new GetObjectRequest()
            {
                BucketName = this.bucketName,
                Key = Prefix +path
            }) ;
            return obj.ToString();

        }
    }
}
