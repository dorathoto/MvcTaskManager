using System.Text.Json.Serialization;

namespace MvcTaskManager.Entities
{
    public class Tarefa
    {
        public Guid TarefaId { get; set; }

        public string? Descricao { get; set; }

        public DateTime? DataRealizacaoTarefa { get; set; }

        public Guid UsuarioId { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
    }
}
