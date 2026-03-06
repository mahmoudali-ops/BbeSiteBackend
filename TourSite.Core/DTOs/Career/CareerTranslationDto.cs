using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.Career
{
    public class CareerTranslationDto
    {
        public int Id { get; set; }

        public string Language { get; set; }

        public string JobTitle { get; set; }           // Account Manager
        public string EmploymentType { get; set; }     // Full time

        public decimal SalaryFrom { get; set; }        // 1000
        public decimal SalaryTo { get; set; }          // 1200
        public string SalaryPeriod { get; set; }       // Per Month

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}
