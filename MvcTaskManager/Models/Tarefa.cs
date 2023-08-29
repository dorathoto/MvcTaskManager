using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MvcTaskManager.Models
{
    public class Tarefa
    {
        public Guid TarefaId { get; set; }

        [MaxLength(2000)]//faltou isso! otimizando
        public string Descricao { get; set; }

        public DateTime? DataRealizacaoTarefa { get; set; }

        public Guid UsuarioId { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
    }
}
