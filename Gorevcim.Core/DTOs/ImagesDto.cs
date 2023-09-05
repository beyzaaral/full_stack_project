using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorevcim.Core.DTOs
{
    public class ImagesDto:BaseDto
    {
        public int ImageName { get; set; }
        public string FilePath { get; set; }
        public string LogoBase64  { get; set; }

    }
}
