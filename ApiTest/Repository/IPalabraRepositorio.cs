using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Repository
{
    public interface IPalabraRepositorio
    {
        Task<bool> Any(string palabra);
        Task<string> Get(string palabra);
        Task<Dictionary<string, string>> Get(Func<KeyValuePair<string,string>, bool> selector,
            Func<KeyValuePair<string, string>, KeyValuePair<string, string>> select);
        Task<string> Add(string palabra);

    }
}
