using Mando.Web.Models;

namespace Mando.Web.Services.IServices;

public interface IBaseService : IDisposable
{
    ResponseDto responseModel { get; set; }

    Task<T> SendAsync<T>(ApiRequest apiRequest);
}
