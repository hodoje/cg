using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ1.Models
{
    public class Image
    {
        public string Path { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }

        public Image() { }

        public Image(string path, string title, string description, string owner)
        {
            Path = path;
            Title = title;
            Description = description;
            Owner = owner;
        }
    }
}
