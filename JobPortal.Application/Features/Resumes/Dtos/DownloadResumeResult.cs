using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.Resumes.Dtos
{
    public class DownloadResumeResult
    {
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
    }
}
