namespace MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing.Common
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(object request);
    }
}
