﻿namespace MediportaZadRek.QCRS.Common.DataPreprocessing.Common
{
    public abstract class AbstractHandler : IHandler
    {
        protected IHandler _nextHandler;
        public abstract object Handle(object request);

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }
    }
}
