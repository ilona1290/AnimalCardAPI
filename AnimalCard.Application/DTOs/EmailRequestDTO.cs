using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.DTOs
{
    public class EmailRequestDTO
    {
        /// <summary>
        /// Tuple (Email, Name)
        /// </summary>
        public Tuple<string, string> From { get; set; }

        /// <summary>
        /// Dictionary (Email, Name)
        /// </summary>
        public Dictionary<string, string> To { get; set; } = new Dictionary<string, string>();

        public string Subject { get; set; } = String.Empty;
        public string PlainMessage { get; set; } = String.Empty;
        public string HtmlMessage { get; set; } = String.Empty;
    }
}
