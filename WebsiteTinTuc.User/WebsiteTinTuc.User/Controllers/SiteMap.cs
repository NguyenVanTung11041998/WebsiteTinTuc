using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Xml.Linq;
using WebsiteTinTuc.User.Models;

namespace WebsiteTinTuc.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteMap : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public SiteMap(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public OkResult UpdateSiteMap(Post post)
        {
            string rootPath = _hostingEnvironment.WebRootPath;
            string filePathXml = $"{rootPath}/sitemap.xml";
            var doc = XElement.Load(filePathXml);
            var element = new XElement(new XElement("url", 
                new XElement("loc", post.PostUrl),
                new XElement("agency", post.AgencyName),
                new XElement("title", post.Title),
                new XElement("lastmod", DateTime.Now)
            ));
            doc.Add(element);
            doc.Save(filePathXml);
            return Ok();
        }
    }
}
