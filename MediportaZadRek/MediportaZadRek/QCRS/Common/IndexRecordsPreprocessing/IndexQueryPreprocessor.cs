using MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing.Common;

namespace MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing
{
    public class IndexQueryPreprocessor
    {
        private List<IHandler> handlers;
        public IndexQueryPreprocessor()
        {
            handlers = new List<IHandler>();
        }

        public IndexQueryPreprocessor AddHandler(IHandler handler)
        {
            handlers.Add(handler);
            return this;
        }

        public object Process(object obj)
        {
            SetChainOfHandlers();
            return Handle(obj);
        }

        private void SetChainOfHandlers()
        {
            for (int i = 0; i < handlers.Count() - 1; ++i)
            {
                handlers[i].SetNext(handlers[i + 1]);
            }
        }

        private object Handle(object obj)
        {
            return handlers.First().Handle(obj);
        }
    }
}
