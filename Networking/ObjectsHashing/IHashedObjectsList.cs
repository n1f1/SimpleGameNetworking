namespace Networking.ObjectsHashing
{
    public interface IHashedObjectsList
    {
        bool HasInstance<TType>(short instanceId);
        TType GetInstance<TType>(short instanceId);
        void RegisterNew<TType>(TType tObject, short instanceID);
        short GetID<TType>(TType tObject);
        short Register<TType>(TType tObject);
    }
}