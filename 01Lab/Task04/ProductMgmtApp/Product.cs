using System;

namespace edu.miu.cs.cs489appsd.lab1.productmgmtapp.model
{
    public class Product
    {
        // Private fields
        private string _productId;
        private string _name;
        private DateTime _dateSupplied;
        private int _quantityInStock;
        private decimal _unitPrice;

        // Default constructor
        public Product()
        {
            _productId = string.Empty;
            _name = string.Empty;
            _dateSupplied = DateTime.MinValue;
            _quantityInStock = 0;
            _unitPrice = 0.00m;
        }

        // Parameterized constructor
        public Product(string productId, string name, DateTime dateSupplied, int quantityInStock, decimal unitPrice)
        {
            _productId = productId;
            _name = name;
            _dateSupplied = dateSupplied;
            _quantityInStock = quantityInStock;
            _unitPrice = unitPrice;
        }

        // Copy constructor
        public Product(Product other)
        {
            if (other != null)
            {
                _productId = other._productId;
                _name = other._name;
                _dateSupplied = other._dateSupplied;
                _quantityInStock = other._quantityInStock;
                _unitPrice = other._unitPrice;
            }
        }

        // Getter and Setter for ProductId
        public string ProductId
        {
            get { return _productId; }
            set { _productId = value ?? string.Empty; }
        }

        // Getter and Setter for Name
        public string Name
        {
            get { return _name; }
            set { _name = value ?? string.Empty; }
        }

        // Getter and Setter for DateSupplied
        public DateTime DateSupplied
        {
            get { return _dateSupplied; }
            set { _dateSupplied = value; }
        }

        // Getter and Setter for QuantityInStock
        public int QuantityInStock
        {
            get { return _quantityInStock; }
            set { _quantityInStock = value; }
        }

        // Getter and Setter for UnitPrice
        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        // Override ToString for debugging
        public override string ToString()
        {
            return $"Product{{ProductId='{_productId}', Name='{_name}', DateSupplied={_dateSupplied:yyyy-MM-dd}, QuantityInStock={_quantityInStock}, UnitPrice={_unitPrice:C}}}";
        }
    }
}
