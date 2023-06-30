using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DeveloperTest.Core.Domain.Abstraction;

namespace DeveloperTest.Core.Domain.Models
{
    public class SystemLog : BaseEntity
    {
        public int Id { get; set; }
        public string ResourceType { get; set; }
        public int ResourceId { get; set; }
        public string Event { get; set; }
        public JsonDocument Changeset { get; set; }
        public string Comment { get; set; }
    }
}
