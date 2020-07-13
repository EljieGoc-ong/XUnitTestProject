using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Xunit;

namespace ProductsApp.Tests
{
   
    public class ProductsAppShould
    {
        [Fact]
        public void Products_Null_Or_Not_Specified_AddNew()
        {
            //arrange
            var products = new Products();

            //act and assert
            Assert.Throws<ArgumentNullException>( () => products.AddNew(null) );
        }

        [Fact]
        public void Products_AddnewProducts()
        {
            // arrange
            var products = new Products();
            var product = new Product() {
                Name = "Test Product",
                IsSold = false
            };

            var productTotalCount = 0;

            //act
            products.AddNew(product);
            productTotalCount = products.Items.Count();

            //assert
            Assert.Equal(1, productTotalCount);
        }

        [Fact]
        public void Products_Null_Or_Not_Specified_AddNewProductName()
        {
            // arrange
            var products = new Products();

            var product = new Product()
            {
                Name = null,
                IsSold = false
            };

            //act and assert
            Assert.Throws<NameRequiredException>(() => products.AddNew(product));
        }


    }

    internal class Products
    {
        private readonly List<Product> _products = new List<Product>();

        public IEnumerable<Product> Items => _products.Where(t => !t.IsSold);

        public void AddNew(Product product)
        {
            product = product ??
                throw new ArgumentNullException();
            product.Validate();
            _products.Add(product);
        }

        public void Sold(Product product)
        {
            product.IsSold = true;
        }

    }

    internal class Product
    {
        public bool IsSold { get; set; }
        public string Name { get; set; }

        internal void Validate()
        {
            Name = Name ??
                throw new NameRequiredException();
        }

    }

    [Serializable]
    internal class NameRequiredException : Exception
    {
        public NameRequiredException() { /* ... */ }

        public NameRequiredException(string message) : base(message) { /* ... */ }

        public NameRequiredException(string message, Exception innerException) : base(message, innerException) { /* ... */ }

        protected NameRequiredException(SerializationInfo info, StreamingContext context) : base(info, context) { /* ... */ }
    }
}