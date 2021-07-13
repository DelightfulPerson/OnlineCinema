using OnlineCinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Object> Get()
        {
            using (var context = new cinemaContext())
            {
                // all film
                // return context.Films.ToList();


                // get film by id
                // return context.Films.Where(zxc => zxc.FilmId == 2).ToList();

                // SelectActualFilms
                //return
                var selectActualFilms = context.Films.Join(
                    context.Sessions,
                    a => a.FilmId,
                    b => b.FilmId,
                    (a, b) => new
                    {
                        Name = a.Name
                    });
                return selectActualFilms.ToList().Distinct();
            }
        }
    }
}