using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuletapruebaAPI.Context;
using RuletapruebaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RuletapruebaAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class RuletaWebAPI2Controller : ControllerBase
    {
        private readonly AppDbContext context;
        public RuletaWebAPI2Controller(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet("newRoulette")]
        public int  Get()
        {
            try
            {
                int id = context.Ruleta.Max(p => p.IdApuesta);
                if (id == 0) { id = 1; }
                else
                {
                    id = id + 1;
                    Ruleta ruleta = new Ruleta();
                    ruleta.Color = null;
                    ruleta.MontoApuesta = 0;
                    ruleta.NumeroApostado = -1;
                    context.Ruleta.Add(ruleta);
                    context.SaveChanges();
                }
                return id;
            }
            catch(NullReferenceException )
            {
                return 0;
            }
        }
        [HttpGet("OpenRoulette/{id}")]
        public string Open(int id)
        {
            try
            {
                var status = context.Ruleta.FirstOrDefault(b => b.IdApuesta == id);
                if (status != null)
                {
                    status.Estado= true;
                    context.SaveChanges(); 
                }
                return "Operación Exitosa";
            }
            catch (NullReferenceException)
            {
                return "Denegado";
            }
        }
        [HttpGet("OpenbetRoulette/{id}/{color}/{betnumber}/{cash}/{userid}")]
        public string bet(int id,string color,int betnumber,int cash,int userid)
        {
            string result= "";
            try
            { 
                var status = context.Ruleta.FirstOrDefault(b => b.IdApuesta == id);
                Historial historial = new Historial();//var historial = context.Historial;
                if (status.Estado == true && status.IdUsuario == userid && cash <= 10000 && betnumber >= 0 && betnumber<=36) 
                {
                    historial.Color = color;
                    historial.NumeroApostado = betnumber;
                    historial.MontoApuesta = cash;
                    historial.IdApuesta = id;
                    status.Color = color;
                    status.NumeroApostado = betnumber;
                    status.MontoApuesta = cash;
                    context.Add(historial);
                    context.SaveChanges();
                    result = "Apuesta Realizada";
                }
                else {
                    result = "Apuesta denegada";
                }
                return result;
            }
            catch (NullReferenceException)
            {
                return "Denegado";
            }
        }

        [HttpGet("CloseRoulette/{id}")]
        public IEnumerable<Historial> Close(int id)
        {
            try
            {
                var Historial = context.Historial.Where(b => b.IdApuesta == id);
                Ruleta ruleta = new Ruleta();
                if (Historial != null)
                {
                    var status = context.Ruleta.FirstOrDefault(a => a.IdApuesta == id);
                    status.Estado = false;
                    Random generate = new Random();
                    int winner = 0;
                    int colorwinner = 0;
                    colorwinner = generate.Next(0, 1); 
                    string meancolor="";
                    if (colorwinner== 1)
                    {
                        meancolor = "Rojo";
                    }
                    else { meancolor = "Negro"; }
                    winner = generate.Next(0, 36);
                    var winnerlist = context.Historial.Where(c => c.NumeroApostado == winner);
                    var colorW = context.Historial.Where(d => d.Color == meancolor);
                    if (winnerlist != null)
                    {
                        double finalmount = 0;
                        int countcolor = 0;
                        foreach (var i in winnerlist)
                        {
                            finalmount += i.MontoApuesta;
                            if (i.Color == meancolor)
                            {
                                countcolor++;
                            }
                        }
                        finalmount = finalmount * 5;
                        if (colorW != null)
                        {
                            finalmount = finalmount * 1.8 * countcolor;
                        }
                        status.Resultado = Convert.ToInt32(finalmount);
                        status.Estado = false;
                    }
                    else
                    {
                        status.Resultado = 0;
                        status.Estado = false;
                    }
                    context.SaveChanges();
                }

                return Historial.ToList() ;

            }
            catch (NullReferenceException )
            {
                return null;
            }
        }
        [HttpGet("FinalList/")]
       public IEnumerable<Ruleta>FinalList()
        {
            try
            {
                var RuletaList= context.Ruleta.ToList();
                return RuletaList;

            }
            catch (NullReferenceException )
            {
                return null;
            }
        }
    }
}
