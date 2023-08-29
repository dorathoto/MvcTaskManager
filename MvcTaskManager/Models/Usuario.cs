using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace MvcTaskManager.Models
{
    public class Usuario : IdentityUser<Guid>
    {
        [JsonIgnore]
        public virtual ICollection<Tarefa> Tarefas { get; set; }

    }
}
