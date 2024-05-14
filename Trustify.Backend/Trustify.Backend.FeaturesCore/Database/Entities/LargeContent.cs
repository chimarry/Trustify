using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trustify.Backend.FeaturesCore.Database.Entities
{
    public class LargeContent
    {
        public long LargeContentId { get; set; }

        public string Path { get; set; } = null!;

        public string Name { get; set; } = null!;

        public DateTime UploadedOn { get; set; }

        public double Size { get; set; }
    }
}
