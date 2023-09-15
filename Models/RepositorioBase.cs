using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
	public abstract class RepositorioBase
	{
		protected readonly IConfiguration configuration;
		protected readonly string connectionString;

		protected RepositorioBase(IConfiguration configuration)
		{
			this.configuration = configuration;
			/*connectionString = configuration["ConnectionStrings:DefaultConnection"];*/
			//connectionString = configuration["ConnectionStrings:MySql"];
            connectionString= "Server=localhost;User=root; Password=; Database=inmobiliaria; sslMode=none";
		}
	}
}
