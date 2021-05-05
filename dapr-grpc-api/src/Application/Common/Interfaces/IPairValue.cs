namespace SC.API.CleanArchitecture.Application.Common.Interfaces
{
    public interface IPairValue<T>
    {
        T GetFirst();
        T GetSecond();
        bool Contains(T value);
    }
}
