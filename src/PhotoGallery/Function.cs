using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PhotoGallery
{
    public class PhotoFunction
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> PhotoFunctionHandler(string input, ILambdaContext context)
        {
            var photoProvider = new PhotoProvider(new AmazonDynamoDBClient());
            var images = await photoProvider.GetImages();

            return JsonConvert.SerializeObject(images);
        }
    }
}
