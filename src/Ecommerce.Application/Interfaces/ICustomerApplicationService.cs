namespace Ecommerce.Application.Interfaces;
public interface ICustomerApplicationService
{
    void SaveCustomer(CustomerDto customerDto);
    void UpdateCustomer(CustomerDto customerDto);
    void DeleteCustomer(string id);
    IEnumerable<CustomerDto> GetCustomers();
    CustomerDto GetCustomerById(string id);
}
