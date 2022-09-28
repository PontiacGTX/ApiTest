using ApiTest.Models;
using ApiTest.Repository;
using ApiTest.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Controllers
{
    [Route("[controller]")]
    public class PruebaController : Controller
    {
        IPalabraRepositorio _palabraRepositorio { get; }
        PalabraServicios _palabraServicios { get; }
        public PruebaController(IPalabraRepositorio repositorio, PalabraServicios palabraServicios)
        {
            _palabraRepositorio = repositorio;
            _palabraServicios = palabraServicios;
        }
        // GET: PruebaController
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]string palabra)
        {
            if (string.IsNullOrWhiteSpace(palabra))
                return BadRequest();

            try
            {
                var map =await _palabraRepositorio.Get(x => x.Key.Length == palabra.Length,select:
                    x=>
                    {
                        return  new KeyValuePair<string, string>(x.Key, x.Value);
                    });

                var word = String.Concat(palabra.OrderBy(y => y));

                var result = map.Where(x =>_palabraServicios.EsAnagrama(x.Key, word).Result)
                    .Select(x=> new Palabra { Valor = x.Key }).ToList();

                if (!result.Any())
                    return StatusCode(StatusCodes.Status404NotFound, new { Data = result, Success = true, Message = "Not Found", StatusCode = 404 });
                    
                return Ok(new { Data = result, Success = true, Message = "Ok", StatusCode = 200 });
            }
            catch (Exception)
            {
                object o = null;//normalmente crearia un metodo de fabrica con un objecto de respuesta y no anonimo
                return StatusCode(StatusCodes.Status500InternalServerError, new { Data = o, StatusCode = 500, Success = false, Message = "Server ERROR" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] Palabra body)
        {
            if (string.IsNullOrWhiteSpace(body.Valor))
                return BadRequest();

            try
            {
               var existe=await _palabraRepositorio.Any(body.Valor);
                string result = null;
                if (!existe)
                    result = await _palabraRepositorio.Add(body.Valor);

                return Ok(new { Data = result, Success = true, Message = "Ok", StatusCode = 200 });
            }
            catch (Exception ex)
            {
                object o = null;//normalmente crearia un metodo de fabrica con un objecto de respuesta y no anonimo
                return StatusCode(StatusCodes.Status500InternalServerError, new { Data = o, StatusCode = 500, Success = false, Message = "Server ERROR" });
            }
        }

            
    }
}
