using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.BookmarkJobs.Dtos
{
    public class BookmarkedJobDto
    {
        public int JobPostingId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
    }
}
