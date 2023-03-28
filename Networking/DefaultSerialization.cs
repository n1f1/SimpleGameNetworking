using System;
using Networking.ObjectsHashing;

namespace Networking
{
    public abstract class DefaultSerialization
    {
        protected readonly IHashedObjectsList HashedObjects;
        protected readonly ITypeIdConversion TypeIdConversion;

        protected DefaultSerialization(IHashedObjectsList hashedObjects, ITypeIdConversion typeId)
        {
            HashedObjects = hashedObjects ?? throw new ArgumentNullException(nameof(hashedObjects));
            TypeIdConversion = typeId ?? throw new ArgumentNullException(nameof(typeId));
        }
    }
}