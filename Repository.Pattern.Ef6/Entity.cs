using System.ComponentModel.DataAnnotations.Schema;
using BLS.Infrastructure.Infrastructure;

namespace BLS.Infrastructure.Ef6
{
    public abstract class Entity : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}