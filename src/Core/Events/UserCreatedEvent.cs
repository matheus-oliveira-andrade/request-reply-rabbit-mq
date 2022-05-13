namespace Core.Events;

public class UserCreatedEvent : BaseEvent
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Document { get; private set; }
    
    public UserCreatedEvent(string name, string email, string document)
    {
        Name = name;
        Email = email;
        Document = document;
    }
}