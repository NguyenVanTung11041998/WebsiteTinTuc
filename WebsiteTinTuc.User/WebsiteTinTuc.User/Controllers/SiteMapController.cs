using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Xml.Linq;
using WebsiteTinTuc.User.Models;

namespace WebsiteTinTuc.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteMapController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public SiteMapController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public OkResult UpdateSiteMap(Post post)
        {
            string rootPath = _hostingEnvironment.WebRootPath;
            string filePathXml = $"{rootPath}/sitemap.xml";
            var doc = XElement.Load(filePathXml);
            if (post.IsCreate)
            {
                var element = new XElement(new XElement("url",
                    new XElement("loc", post.PostUrl),
                    new XElement("agency", post.AgencyName),
                    new XElement("title", post.Title),
                    new XElement("lastmod", DateTime.Now),
                    new XAttribute("post_id", post.Id)
                ));
                doc.Add(element);
            }
            else
            {
                var element = doc.Elements().FirstOrDefault(x => x.Attributes().Any(p => p.Value == $"{post.Id}"));
                element.ReplaceWith(new XElement("url",
                    new XElement("loc", post.PostUrl),
                    new XElement("agency", post.AgencyName),
                    new XElement("title", post.Title),
                    new XElement("lastmod", DateTime.Now),
                    new XAttribute("post_id", post.Id)
                ));
            }
            doc.Save(filePathXml);
            return Ok();
        }
    }
}
