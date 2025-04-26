using UnityEngine;

namespace S1API.Entities.Interfaces
{
    /// <summary>
    /// Represents an entity within the game world.
    /// </summary>
    public interface IEntity
    {
        GameObject gameObject { get; }
        
        /// <summary>
        /// The world position of the entity.
        /// </summary>
        public Vector3 Position => gameObject.transform.position;

        /// <summary>
        /// The scale of the entity.
        /// </summary>
        public float Scale
        {
            get => gameObject.transform.localScale.magnitude;
            set => gameObject.transform.localScale = new Vector3(value, value, value);
        }
    }
}