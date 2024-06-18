using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Business.Services.ImageService
{
	public interface IImage
	{
		public string path(IFormFile file, string rootPath,string folderName);
	}
}
