﻿using MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing.Common;
using System.Linq.Dynamic.Core;

namespace MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing
{
    public class OrderedListHandler : AbstractHandler
    {
        private string sortParam { get; set; }
        private string sortOrder { get; set; }

        public OrderedListHandler(string sortParam, string sortOrder)
        {
            this.sortParam = sortParam;
            this.sortOrder = sortOrder;
        }

        public override object Handle(object request)
        {
            var items = ((List<Models.Tag>)request).AsQueryable();

            items = items.OrderBy($"{sortParam} {sortOrder}");

            if (_nextHandler != null)
            {
                return _nextHandler.Handle(items.ToList());
            }
            else
            {
                return items.ToList();
            }
        }
    }
}