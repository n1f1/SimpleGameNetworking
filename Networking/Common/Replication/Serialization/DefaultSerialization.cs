using System;
using Networking.Common.Replication.ObjectsHashing;

namespace Networking.Common.Replication.Serialization
{
    public abstract class DefaultSerialization
    {
        protected readonly IHashedObjectsList HashedObjects;

        protected DefaultSerialization(IHashedObjectsList hashedObjects)
        {
            HashedObjects = hashedObjects ?? throw new ArgumentNullException(nameof(hashedObjects));
        }
    }
}