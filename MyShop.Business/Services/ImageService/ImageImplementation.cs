using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Business.Services.ImageService
{
	public class ImageImplementation : IImage
	{
        public string path(IFormFile file, string rootPath, string folderName)
		{
			string fileName = Guid.NewGuid().ToString();
			var upload = Path.Combine(rootPath, @folderName);
			var ext = Path.GetExtension(file.FileName);

			using (var fileStream = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
			{
				file.CopyTo(fileStream);
			}
			return @folderName + "/" + fileName + ext;
		}
	}
}
