using System;
using System.Linq;
using System.Text;
using edu.miu.cs.cs489appsd.lab1.productmgmtapp.model;

namespace edu.miu.cs.cs489appsd.lab1a.productmgmtapp
{
    public class ProductMgmtApp
    {
        public static void Main(string[] args)
        {
            // Create array of products using the company's data
            Product[] products = new Product[]
            {
                new Product("31288741190182539912", "Banana", new DateTime(2025, 1, 24), 124, 0.55m),
                new Product("29274582650152771644", "Apple", new DateTime(2024, 12, 9), 18, 1.09m),
                new Product("91899274600128155167", "Carrot", new DateTime(2025, 3, 31), 89, 2.99m),
                new Product("31288741190182539913", "Banana", new DateTime(2025, 2, 13), 240, 0.65m)
            };

            // Invoke printProducts method
            printProducts(products);
        }

        public static void printProducts(Product[] products)
        {
            // Sort products: ascending by name, then descending by unit price
            var sortedProducts = products.OrderBy(p => p.Name)
                                       .ThenByDescending(p => p.UnitPrice)
                                       .ToArray();

            // Print in JSON format
            Console.WriteLine("Printed in JSON Format:");
            Console.WriteLine(GenerateJsonOutput(sortedProducts));
            Console.WriteLine();

            // Print in XML format
            Console.WriteLine("Printed in XML Format:");
            Console.WriteLine(GenerateXmlOutput(sortedProducts));
            Console.WriteLine();

            // Print in CSV format
            Console.WriteLine("Printed in Comma-Separated Values (CSV) Format:");
            Console.WriteLine(GenerateCsvOutput(sortedProducts));
        }

        private static string GenerateJsonOutput(Product[] products)
        {
            var json = new StringBuilder();
            json.AppendLine("[");

            for (int i = 0; i < products.Length; i++)
            {
                var product = products[i];
                json.AppendLine("  {");
                json.AppendLine($"    \"productId\": \"{product.ProductId}\",");
                json.AppendLine($"    \"name\": \"{product.Name}\",");
                json.AppendLine($"    \"dateSupplied\": \"{product.DateSupplied:yyyy-MM-dd}\",");
                json.AppendLine($"    \"quantityInStock\": {product.QuantityInStock},");
                json.AppendLine($"    \"unitPrice\": {product.UnitPrice:F2}");
                json.Append(i < products.Length - 1 ? "  }," : "  }");
            }

            json.AppendLine();
            json.AppendLine("]");
            return json.ToString();
        }

        private static string GenerateXmlOutput(Product[] products)
        {
            var xml = new StringBuilder();
            xml.AppendLine("<?xml version=\"1.0\"?>");
            xml.AppendLine("<products>");

            foreach (var product in products)
            {
                xml.AppendLine($"  <product productId=\"{product.ProductId}\" " +
                              $"name=\"{product.Name}\" " +
                              $"dateSupplied=\"{product.DateSupplied:yyyy-MM-dd}\" " +
                              $"quantityInStock=\"{product.QuantityInStock}\" " +
                              $"unitPrice=\"{product.UnitPrice:F2}\" />");
            }

            xml.AppendLine("</products>");
            return xml.ToString();
        }

        private static string GenerateCsvOutput(Product[] products)
        {
            var csv = new StringBuilder();

            foreach (var product in products)
            {
                csv.AppendLine($"{product.ProductId}, {product.Name}, {product.DateSupplied:yyyy-MM-dd}, {product.QuantityInStock}, {product.UnitPrice:F2}");
            }

            return csv.ToString().TrimEnd();
        }
    }
}
