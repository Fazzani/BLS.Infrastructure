using System.ComponentModel.DataAnnotations.Schema;
using BLS.Infrastructure.Infrastructure;

namespace BLS.Infrastructure.Ef6
{
    public abstract class Entity : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

    public static class ExtentionsMethods
    {
        /// <summary>
        /// Set ObjectState Entity to Delete
        /// </summary>
        /// <param name="entity"></param>
        public static void Delete(this Entity entity)
        {
            entity.ObjectState = ObjectState.Deleted;
        }

        /// <summary>
        /// Set ObjectState Entity to Added
        /// </summary>
        /// <param name="entity"></param>
        public static void Create(this Entity entity)
        {
            entity.ObjectState = ObjectState.Added;
        }

        /// <summary>
        /// Set ObjectState Entity to Modified
        /// </summary>
        /// <param name="entity"></param>
        public static void Update(this Entity entity)
        {
            entity.ObjectState = ObjectState.Modified;
        }
    }
}