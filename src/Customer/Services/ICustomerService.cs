namespace Customer.Services;

public interface ICustomerService
{
    bool Create(Domain.Customer customer);
}