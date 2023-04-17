namespace Networking.Common.Replication.ObjectsHashing
{
    public interface IHashedObjectsList
    {
        bool HasInstance<TType>(short instanceId);
        TType GetInstance<TType>(short instanceId);
        void RegisterWithID<TType>(TType tObject, short instanceID);
        short GetID<TType>(TType tObject);
        short RegisterOrGetRegistered<TType>(TType tObject);
    }
}