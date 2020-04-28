using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBlog.Entities.ComplexTypes.Dtos.Helpers
{
    public class MailContent
    {
        public string To { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
    }
}
