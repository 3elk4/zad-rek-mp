using MediportaZadRek.QCRS.Tag;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.QCRS.Tag
{
    [TestClass]
    public class TagsWithPaginatinDetailsTest
    {
        [TestMethod]
        public void IsInvalid_CurrentPageIsNegative()
        {
            var tagDetails = new TagsWithPaginationDetails() { CurrentPage = -1 };

            IList<ValidationResult> result = ModelValidator.Validate(tagDetails);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(v => v.MemberNames.Contains("CurrentPage") && v.ErrorMessage.Contains("must be between 0 and 2147483647")));
        }

        [TestMethod]
        public void IsInvalid_PageSizeIsNegative()
        {
            var tagDetails = new TagsWithPaginationDetails() { PageSize = -1 };

            IList<ValidationResult> result = ModelValidator.Validate(tagDetails);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(v => v.MemberNames.Contains("PageSize") && v.ErrorMessage.Contains("must be between 0 and 2147483647")));
        }

        [TestMethod]
        public void IsInvalid_TotalIsNegative()
        {
            var tagDetails = new TagsWithPaginationDetails() { Total = -1 };

            IList<ValidationResult> result = ModelValidator.Validate(tagDetails);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(v => v.MemberNames.Contains("Total") && v.ErrorMessage.Contains("must be between 0 and 2147483647")));
        }
    }
}
