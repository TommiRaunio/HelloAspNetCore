using System.Security.Cryptography.X509Certificates;

namespace HelloAspNetCore.Models.Pages
{
    public interface IPage
    {
        Layout Layout { get; set; }
    }
}
