using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Repository
{
    public class PalabraRepositorio:IPalabraRepositorio
    {
        static  Dictionary<string,string> _Map { get; set; }
        public PalabraRepositorio()
        {
            if(_Map is null)
            _Map = new Dictionary<string, string>();
        }

        public async Task<bool> Any(string palabra)
        {
            try
            {
                return _Map.Values.Any(x => x.ToLower() == palabra.ToLower());
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> Get(string palabra)
        {
            try
            {
                return _Map[palabra];
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> Add(string palabra)
        {
            try
            {
                _Map.Add(palabra.ToLower(), palabra);
                return palabra;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Dictionary<string, string>> Get(Func<KeyValuePair<string, string>, bool> selector,
            Func<KeyValuePair<string, string>, KeyValuePair<string, string>> select)
        {
            return _Map.Where(selector).Select(select).ToDictionary(x=>x.Key,x=>x.Value);
        }
    }
}
