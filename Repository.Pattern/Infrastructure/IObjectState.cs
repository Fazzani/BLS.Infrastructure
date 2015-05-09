
using System.ComponentModel.DataAnnotations.Schema;

namespace BLS.Infrastructure.Infrastructure
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}