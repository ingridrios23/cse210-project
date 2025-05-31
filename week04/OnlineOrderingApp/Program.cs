using System;
using System.Collections.Generic;
using System.Text;

class Product
{
    private string _name;
    private string _id;
    private double _price;
    private int _quantity;

    public string Name { get => _name; }
    public string Id { get => _id; }
    public double Price { get => _price; }
    public int Quantity { get => _quantity; }

    public Product(string name, string id, double price, int quantity)
    {
        _name = name;
        _id = id;
        _price = price;
        _quantity = quantity;
    }

    public double GetTotalPrice()
    {
        return _price * _quantity;
    }
}

class Address
{
    private string _street;
    private string _city;
    private string _stateOrProvince;
    private string _country;

    public Address(string street, string city, string stateOrProvince, string country)
    {
        _street = street;
        _city = city;
        _stateOrProvince = stateOrProvince;
        _country = country;
    }

    public bool IsInUSA()
    {
        // Considerar diferentes formas de escribir USA
        return _country.Trim().ToLower() == "usa" || _country.Trim().ToLower() == "estados unidos";
    }

    public string GetFullAddress()
    {
        return $"{_street}\n{_city}, {_stateOrProvince}\n{_country}";
    }
}

class Customer
{
    private string _name;
    private Address _address;

    public string Name { get => _name; }
    public Address Address { get => _address; }

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public string GetAddressInfo()
    {
        return _address.GetFullAddress();
    }
}

class Order
{
    private List<Product> _products;
    private Customer _customer;

    public List<Product> Products { get => _products; }
    public Customer Customer { get => _customer; }

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double CalculateTotalCost()
    {
        double total = 0;
        foreach (Product product in _products)
        {
            total += product.GetTotalPrice();
        }

        double shippingCost = _customer.Address.IsInUSA() ? 5 : 35;
        return total + shippingCost;
    }

    public string GetPackingLabel()
    {
        StringBuilder label = new StringBuilder();
        foreach (Product product in _products)
        {
            label.AppendLine($"Product Name: {product.Name}, ID: {product.Id}");
        }
        return label.ToString();
    }

    public string GetShippingLabel()
    {
        return $"{_customer.Name}\n{_customer.GetAddressInfo()}";
    }
}

class Program
{
    static void Main()
    {
        // Crear direcciones
        Address addr1 = new Address("123 Elm St", "Springfield", "IL", "USA");
        Address addr2 = new Address("456 Maple Ave", "Toronto", "ON", "Canada");

        // Crear clientes
        Customer customer1 = new Customer("John Doe", addr1);
        Customer customer2 = new Customer("Jane Smith", addr2);

        // Crear pedidos
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "LP100", 1200.50, 1));
        order1.AddProduct(new Product("Mouse", "MS200", 25.75, 2));

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Desk Chair", "DC300", 350.00, 1));
        order2.AddProduct(new Product("Monitor", "MN400", 220.00, 2));
        order2.AddProduct(new Product("Keyboard", "KB500", 45.00, 1));

        // Mostrar info pedido 1
        Console.WriteLine("Order 1:");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.CalculateTotalCost():0.00}");
        Console.WriteLine(new string('-', 40));

    
        Console.WriteLine("Order 2:");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.CalculateTotalCost():0.00}");
    }
}
