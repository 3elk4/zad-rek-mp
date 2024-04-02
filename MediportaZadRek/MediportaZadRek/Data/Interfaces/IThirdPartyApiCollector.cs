namespace MediportaZadRek.Data.Interfaces
{
    public interface IThirdPartyApiCollector<T>
    {
        Task<T> CollectAsync();
    }
}
