using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParam = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder = "dating-app-profile"
                };
                uploadResult = await cloudinary.UploadAsync(uploadParam);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            return await cloudinary.DestroyAsync(deleteParams);
        }
    }
}