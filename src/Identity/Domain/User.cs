namespace Identity.Domain;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Document { get; private set; }
    
    public User(string name, string email, string document)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Document = document;
    }

}