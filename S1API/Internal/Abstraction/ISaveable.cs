namespace S1API.Internal.Abstraction
{
    /// <summary>
    /// INTERNAL: Represents a class that should serialize by GUID instead of values directly.
    /// This is important to utilize on instanced objects such as dead drops.
    /// </summary>
    internal interface ISaveable
    {
        /// <summary>
        /// The GUID assocated with the object.
        /// </summary>
        public string GUID { get; }
    }
}