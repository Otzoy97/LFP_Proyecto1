using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPPA_OK.Files
{
	class Result
	{
		private String id;
		public String expresion, resultado;
		public Object imagen;
		/// <summary>
		/// Constructor para Resultado
		/// </summary>
		/// <param name="id"></param>
		/// <param name="expresion"></param>
		/// <param name="resultado"></param>
		public Result(String id, String expresion, String resultado)
		{
			this.id = id;
			this.expresion = expresion;
			this.resultado = resultado;
		}
		/// <summary>
		/// Constructor para Graficar
		/// </summary>
		/// <param name="id"></param>
		/// <param name="imagen"></param>
		public Result(String id, Object imagen)
		{
			this.id = id;
			this.imagen = imagen;
		}
		/// <summary>
		/// Devuelve una tabla HTML con los datos de Resultado o Graficar
		/// </summary>
		/// <returns></returns>
		public String GetTable()
		{
			StringBuilder stream = new StringBuilder();
			stream.Append("<table class = \"tg\" >");
			if (id.Equals("Resultado"))
			{
				stream.Append("<tr>"+
							 "<th class= \"tg-fk2z\" colspan=\"2\">Función Resultado<br></th>"+
							 "</tr>");
				stream.Append("<tr>"+
							  "<td class=\"tg-2b1a\">Expresión</td>"+
							  "<td class=\"tg-us36\">"+this.expresion+"</td>" +
							  "</tr>");
				stream.Append("<tr>" +
							  "<td class=\"tg-2b1a\">Resultado</td>" +
							  "<td class=\"tg-us36\">" + this.resultado + "</td>" +
							  "</tr>");
			}
			else if (id.Equals("Graficar"))
			{
				stream.Append("<tr>" +
							  "<th class= \"tg-5mgg\" colspan=\"2\">Función Graficar<br></th>" +
							  "</tr>");
				stream.Append("<tr>" +
							  "<td class=\"tg-yw41\">" + "<img src=\""+this.imagen+"\" alt =  \" " + Guid.NewGuid().ToString() + "\" >" + "</td>" +
							  "</tr>");
			}
			stream.Append("</table>");
			stream.Append("<br><br>");
			return stream.ToString();
		}
	}
}
