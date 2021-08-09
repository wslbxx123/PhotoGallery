using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery
{
    public class PhotoProvider
    {
        private readonly IAmazonDynamoDB _dynamoDB;

        public PhotoProvider(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDB = dynamoDB;
        }

        public async Task<List<Image>> GetImages()
        {
            var result = await _dynamoDB.ScanAsync(new ScanRequest 
            {
                TableName = "Images"
            });

            if(result != null && result.Items != null)
            {
                var images = new List<Image>();
                foreach (var item in result.Items)
                {
                    item.TryGetValue("ImageId", out var imageId);
                    item.TryGetValue("FullImage", out var fullImage);
                    item.TryGetValue("Thumbnail", out var thumbnail);
                    item.TryGetValue("UpoadTime", out var uploadTime);

                    images.Add(new Image
                    {
                        ImageId = imageId?.S,
                        FullImage = fullImage?.S,
                        Thumbnail = thumbnail?.S,
                        UploadTime = uploadTime?.S
                    });
                }

                return images;
            }

            return new List<Image>();
        }

        public async Task DeleteImage(string imageId)
        {
            var key = new Dictionary<string, AttributeValue>()
            {
                {
                    "ImageId", new AttributeValue { S = imageId }
                }
            };
            var result = await _dynamoDB.DeleteItemAsync(new DeleteItemRequest
            {
                TableName = "Images",
                Key = key
            });
        }
    }
}
