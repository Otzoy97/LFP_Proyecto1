using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPPA_OK.Compilator
{
	class Token
	{
		public String lexema;
		public int fila;
		public int columna;
		public String id;
		/// <summary>
		/// Constructor prro :v
		/// </summary>
		/// <param name="no"></param>
		/// <param name="lexema"></param>
		/// <param name="fila"></param>
		/// <param name="columna"></param>
		/// <param name="token"></param>
		public Token(String lexema, int fila, int columna, String token)
		{
			this.lexema = lexema;
			this.fila = fila;
			this.columna = columna;
			this.id = token;
		}
	}
}
