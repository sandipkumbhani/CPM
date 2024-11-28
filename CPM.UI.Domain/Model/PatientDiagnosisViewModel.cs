using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Model
{
    public class PatientDiagnosisViewModel
    {
        public PatientDiagnosisViewModel()
        {
            PatientDiagnosisDto = new List<PatientDiagnosisDto>();
        }
        public List<PatientDiagnosisDto> PatientDiagnosisDto { get; set; }
    }
    public class PatientDiagnosisDto
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        [ForeignKey("PatientId")]
        public PatientDto? Patient { get; set; }
        public DateTime? VisitedDateTime { get; set; }
        public string? Comments { get; set; } = null;
        public string? Reports { get; set; } = null;
        public string? prescription { get; set; } = null;
        public string? FoodSuggestions { get; set; } = null;
        public bool IsQueue { get; set; } 
    }
}
