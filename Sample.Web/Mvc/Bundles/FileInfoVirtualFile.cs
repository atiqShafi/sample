using System.IO;
using System.Web.Hosting;

namespace Sample.Web.Mvc.Bundles
{
    internal class FileInfoVirtualFile : VirtualFile
    {
        public FileInfo File { get; set; }

        public FileInfoVirtualFile(string virtualPath, FileInfo file) : base(virtualPath)
        {
            File = file;
        }

        public override Stream Open()
        {
            return File.OpenRead();
        }
    }
}