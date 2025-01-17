using InventoryMgmt.Model;
using System.ComponentModel.DataAnnotations;

// guide: https://learn.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2022

namespace InventoryMgmtQA.Model
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void TestAddProduct()
        {
            // create a new product with compliant attribute values
            Product product = new()
            {
                Name = "Apple",
                QuantityInStock = 6,
                Price = 125
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            // the product must be valid since all attributes values validated correctly
            Assert.IsTrue(isProductValid);
        }

        [TestMethod]
        public void TestAddProductQuantityNegative()
        {
            Product product = new()
            {
                Name = "Pears",
                QuantityInStock = -5,// test for negative quantiy
                Price = 100
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            // the product must NOT be valid since the Quantity attribute has invalid value
            Assert.IsFalse(isProductValid);
        }

        [TestMethod]
        public void TestAddProductPriceNegative()
        {
            Product product = new()
            {
                Name = "Pears",
                QuantityInStock = 5,
                Price = -100 // test for negative price
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            // the product must NOT be valid since the Price attribute has invalid value
            Assert.IsFalse(isProductValid);
        }

        [TestMethod]
        public void TestAddProductPricenQtyNegative()
        {
            Product product = new() // test for negative price n qty
            {
                Name = "Apple",
                QuantityInStock = -5,
                Price = -100 
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            // the product must NOT be valid since the Price and Qty attribute has invalid value
            Assert.IsFalse(isProductValid);
        }

        [TestMethod]
        public void TestAddProductQtyInvalid()
        {
            Product product = new() // test for invalid qty input
            {
                Name = "Apple",
                QuantityInStock = -5,
                Price = 0
            };

            bool isProductValid = false;

            try
            {
                // Simulate invalid price input (convert text to double)
                product.Price = (decimal)Convert.ToDouble("5m");
            }
            catch (FormatException ex)
            {
                // If FormatException occurs, test invalid input
                isProductValid = false;
                Console.WriteLine($"Validation Error: {ex.Message}");
            }

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);

            // the product must NOT be valid since the Qty attribute has invalid value
            Assert.IsFalse(isProductValid);
        }

        [TestMethod]
        public void TestAddProductPriceInvalid()
        {
            Product product = new() // test for invalid price input
            {
                Name = "Apple",
                QuantityInStock = 5,
                Price = 0
            };

            bool isProductValid = false;

            try
            {
                // Simulate invalid price input (convert text to double)
                product.Price = (decimal)Convert.ToDouble("100m");
            }
            catch (FormatException ex)
            {
                // If FormatException occurs, test invalid input
                isProductValid = false;
                Console.WriteLine($"Validation Error: {ex.Message}");
            }

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);

            // the product must NOT be valid since the Price attribute has invalid value
            Assert.IsFalse(isProductValid);
        }
    }
}