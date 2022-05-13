namespace Customer.Domain;

public class Customer
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Document { get; private set; }

    public Customer(string name, string email, string document)
    {
        Name = name;
        Email = email;
        Document = document;
    }
}