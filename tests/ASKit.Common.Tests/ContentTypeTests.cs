using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mime;

namespace ASKit.Common.Tests
{
    public class ContentTypeTests
    {
        [Fact]
        public void TestContentType()
        {
            var contentType = new ContentType();
            contentType.MediaType = MediaTypeNames.Application.Pdf;
            var str = contentType.ToString();
            Assert.Equal("application/pdf", str);
        }
    }
}
