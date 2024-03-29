namespace MediportaZadRek.QCRS.Common.DataPreprocessing.Common
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(object request);
    }
}
