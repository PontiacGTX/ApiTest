using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Servicios
{
    public class PalabraServicios
    {
        public PalabraServicios()
        {

        }

        public async Task<bool> EsAnagrama(string palabra,string palabraContenida)
        {
            var p1 = palabra.ToLower().OrderBy(x => x);
            var p2 = palabraContenida.ToLower().OrderBy(x => x);
            return p1.Equals(p2);
        }
    }
}
