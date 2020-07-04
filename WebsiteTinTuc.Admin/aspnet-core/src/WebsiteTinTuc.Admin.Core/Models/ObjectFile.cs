using System;
using WebsiteTinTuc.Admin.Entities;

namespace WebsiteTinTuc.Admin.Models
{
    public class ObjectFile
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public FileType FileType { get; set; }
    }
}
