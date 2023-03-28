namespace Networking
{
    public interface IFactory
    {
        void Create(IInputStream inputStream);
    }
}