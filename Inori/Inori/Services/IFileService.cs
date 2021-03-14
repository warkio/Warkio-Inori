using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Inori.Services
{
    public interface IFileService
    {
        Task SavePictureFromurl(string name, Uri url, string location = "temp");
    }
}
