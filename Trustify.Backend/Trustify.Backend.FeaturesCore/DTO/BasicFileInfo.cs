using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trustify.Backend.FeaturesCore.DTO
{
    /// <summary>
    /// Abstraction for file that needs to be saved on file system.
    /// </summary>
    public class BasicFileInfo
    {
        public string FileName { get; set; } = string.Empty;

        public byte[] FileData { get; set; } = [];

        public BasicFileInfo(string fileName, byte[] fileData)
        {
            FileName = fileName;
            FileData = fileData;
        }

        public BasicFileInfo() { }
    }
}
