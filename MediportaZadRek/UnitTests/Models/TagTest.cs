using MediportaZadRek.Models;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.Models
{
    [TestClass]
    public class TagTest
    {
        [TestMethod]
        public void IsInvalid_PercentagePopulationHasMoreDecimals()
        {
            var tag = new Tag() { Id = Guid.NewGuid(), Name = "TestTag", Count = 123, PercentagePopulation = Convert.ToDecimal(0.5555) };

            IList<ValidationResult> result = ModelValidator.Validate(tag);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(v => v.MemberNames.Contains("PercentagePopulation") && v.ErrorMessage.Contains("must match the regular expression")));
        }

        [TestMethod]
        public void IsInvalid_PercentagePopulationIsNegative()
        {
            var tag = new Tag() { Id = Guid.NewGuid(), Name = "TestTag", Count = 123, PercentagePopulation = -1 };

            IList<ValidationResult> result = ModelValidator.Validate(tag);

            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.Any(v => v.MemberNames.Contains("PercentagePopulation") && v.ErrorMessage.Contains("must be between 0 and 10000000000000000")));
        }


        [TestMethod]
        public void IsInvalid_CountIsNegative()
        {
            var tag = new Tag() { Id = Guid.NewGuid(), Name = "TestTag", Count = -123, PercentagePopulation = Convert.ToDecimal(0.95) };

            IList<ValidationResult> result = ModelValidator.Validate(tag);

            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.Any(v => v.MemberNames.Contains("Count") && v.ErrorMessage.Contains("must be between 0 and 10000000000000000")));
        }

        [TestMethod]
        public void IsInvalid_CountHasDecimalPart()
        {
            var tag = new Tag() { Id = Guid.NewGuid(), Name = "TestTag", Count = (decimal)12.3, PercentagePopulation = Convert.ToDecimal(0.95) };

            IList<ValidationResult> result = ModelValidator.Validate(tag);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(v => v.MemberNames.Contains("Count") && v.ErrorMessage.Contains("must match the regular expression")));
        }

        [TestMethod]
        public void IsInvalid_NameIsNull()
        {
            var tag = new Tag() { Id = Guid.NewGuid(), Name = null, Count = (decimal)12, PercentagePopulation = Convert.ToDecimal(0.95) };

            IList<ValidationResult> result = ModelValidator.Validate(tag);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("required")));
        }

        [TestMethod]
        public void IsInvalid_NameIsEmpty()
        {
            var tag = new Tag() { Id = Guid.NewGuid(), Name = "", Count = (decimal)12, PercentagePopulation = Convert.ToDecimal(0.9) };

            IList<ValidationResult> result = ModelValidator.Validate(tag);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("required")));
        }
    }
}
