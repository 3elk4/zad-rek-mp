using MediportaZadRek.Models;
using MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing;
using MediportaZadRek.QCRS.Common.IndexRecordsPreprocessing.CollectionHandlers;

namespace UnitTests.QCRS.Common
{
    [TestClass]
    public class CollectionPreprocessorTest
    {
        private List<MediportaZadRek.Models.Tag> GenerateTags()
        {
            List<MediportaZadRek.Models.Tag> tags = new List<MediportaZadRek.Models.Tag>();

            for (int i = 0; i < 5; ++i)
            {
                tags.Add(new MediportaZadRek.Models.Tag() { Id = Guid.NewGuid(), Name = $"TestTag{i + 1}", Count = 123, PercentagePopulation = Convert.ToDecimal(0.2) });
            }

            return tags;
        }

        [TestMethod]
        public void IsOneHandler_ReturnsPreprocessedCollection()
        {
            var tags = GenerateTags();
            var collectionPreprocessor = new CollectionPreprocessor();

            collectionPreprocessor = collectionPreprocessor.AddHandler(new OrderedCollectionHandler("Name", SortOrder.desc));

            var result = (List<MediportaZadRek.Models.Tag>)collectionPreprocessor.Process(tags);

            Assert.IsTrue(result.Count() == tags.Count());
            Assert.AreEqual(tags.First().Name, result.Last().Name);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Object reference not set to an instance of an object.")]
        public void HandlerIsNull()
        {
            var tags = GenerateTags();
            var collectionPreprocessor = new CollectionPreprocessor();

            collectionPreprocessor = collectionPreprocessor.AddHandler(null);

            collectionPreprocessor.Process(tags);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Sequence contains no elements")]
        public void NoHandlersSet_ThrowsInvalidOperationException()
        {
            var tags = GenerateTags();
            var collectionPreprocessor = new CollectionPreprocessor();

            collectionPreprocessor.Process(tags);
        }
    }
}
