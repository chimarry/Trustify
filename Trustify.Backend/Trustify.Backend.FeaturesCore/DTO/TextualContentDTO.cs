using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trustify.Backend.FeaturesCore.DTO
{
    public class TextualContentDTO
    {
        public long TextualContentId { get; set; }

        public string Title { get; set; } = null!;

        public string Text { get; set; } = null!;

        public string? Author { get; set; } = null;

        public int Lenght { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
