using System;
using System.IO;
using System.Text;
using KAPPA_OK.Compilator;
using KAPPA_OK.List;

namespace KAPPA_OK.Files
{
	class Files
	{
		private Queue errores = new Queue();
		private Queue colaEntrada;
		private Queue tokens = new Queue() ;
		private String ruta = @".\External";
		/// <summary>
		/// Escribe el HTML de Resultado o Graficar
		/// </summary>
		/// <param name="execute"></param>
		/// <returns></returns>
		public String EscribirHTML(Queue execute)
		{
			//Ayudará a crear el string a retornar
			StringBuilder htmlBuilder = new StringBuilder();
			//Crea un html
			htmlBuilder.Append("<!DOCTYPE html>");
			htmlBuilder.Append("<html>");
			//Crea un css
			htmlBuilder.Append("<style type=\"text / css\">");
			////htmlBuilder.Append(".token  {border-collapse:collapse;border-spacing:0;border-color:#aaa;margin:0px auto;}");
			htmlBuilder.Append(@".tg  {border-collapse:collapse;border-spacing:0;margin:0px auto;}");
			htmlBuilder.Append(@".tg td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;border-color:black;}");
			htmlBuilder.Append(@".tg th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;border-color:black;}");
			htmlBuilder.Append(@".tg .tg-fk2z{font-weight:bold;background-color:#34cdf9;border-color:inherit;text-align:center;vertical-align:top}");
			htmlBuilder.Append(@".tg .tg-us36{border-color:inherit;text-align:center;vertical-align:top}");
			htmlBuilder.Append(@".tg .tg-2b1a{background-color:#bbdaff;border-color:inherit;text-align:center;vertical-align:top}");
			htmlBuilder.Append(@".tg .tg-5mgg{font-weight:bold;background-color:#c0c0c0;text-align:center;vertical-align:top}");
			htmlBuilder.Append(@".tg .tg-yw4l{vertical-align:top,text-align:center;}");
			htmlBuilder.Append(@"img {max-width:auto;max-height:auto;}");
//			htmlBuilder.Append(@);
			htmlBuilder.Append("</style>");
			//Permite la utilización de acentos utf-8
			htmlBuilder.Append("<head>");
			htmlBuilder.Append("<meta charset =\"UTF-8\">");
			htmlBuilder.Append("<title>");
			htmlBuilder.Append("Salida-"+Guid.NewGuid().ToString());
			htmlBuilder.Append("</title>");
			htmlBuilder.Append("</head>");
			htmlBuilder.Append("<body >");

			while (!execute.EsVacio())
			{
				Console.WriteLine(execute.VerPrimero());
				if (execute.VerPrimero()!=null)
				{
					Result temp = (Result)execute.Desencolar();
					htmlBuilder.Append(temp.GetTable());
				}
				else
				{
					execute.Desencolar();
				}
			}
			htmlBuilder.Append("</body>");
			htmlBuilder.Append("</html>");
			//Crea un string de todo lo escrito anteriormente
			return htmlBuilder.ToString();
		}
		/// <summary>
		/// Escribe el HTML de reporte de Tokens
		/// </summary>
		/// <param name="tokens"></param>
		/// <returns></returns>
		public String ReportarTokens(Queue token)
		{
			//Realiza una referencia a la pila
			colaEntrada = token;
			//Prerpara las pilas de tokens y errores
			DepurarTokens();
			//Aquí se alojará el string final a retornar
			//string htmlString = "";
			//Ayudará a crear el string a retornar
			StringBuilder htmlBuilder = new StringBuilder();
			//Crea un html
			htmlBuilder.Append("<!DOCTYPE html>");
			htmlBuilder.Append("<html>");
			//Crea un css
			htmlBuilder.Append("<style type=\"text / css\">");
			htmlBuilder.Append(".token  {border-collapse:collapse;border-spacing:0;border-color:#aaa;margin:0px auto;}");
			htmlBuilder.Append(".token td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#aaa;color:#333;background-color:#fff;border-top-width:1px;border-bottom-width:1px;}");
			htmlBuilder.Append(".token th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#aaa;color:#fff;background-color:#f38630;border-top-width:1px;border-bottom-width:1px;}");
			htmlBuilder.Append(".err  {border-collapse:collapse;border-spacing:0;border-color:#999;margin:0px auto;}");
			htmlBuilder.Append(".err td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#999;color:#444;background-color:#F7FDFA;border-top-width:1px;border-bottom-width:1px;}");
			htmlBuilder.Append(".err th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#999;color:#fff;background-color:#26ADE4;border-top-width:1px;border-bottom-width:1px;}");
			htmlBuilder.Append(".tg-f4we{font-weight:bold;font-size:15px;font-family:Arial, Helvetica, sans-serif !important;;text-align:center;vertical-align:top}");
			htmlBuilder.Append(".tg-cjcd{font-style:italic;font-size:12px;font-family:Arial, Helvetica, sans-serif !important;;vertical-align:top}");
			htmlBuilder.Append(".tg-do2s{font-size:12px;font-family:Arial, Helvetica, sans-serif !important;;vertical-align:top}");
			htmlBuilder.Append("</style>");
			//Permite la utilización de acentos utf-8
			htmlBuilder.Append("<head>");
			htmlBuilder.Append("<meta charset =\"UTF-8\">");
			htmlBuilder.Append("<title>");
			htmlBuilder.Append("Reporte-");
			htmlBuilder.Append(Guid.NewGuid().ToString());
			htmlBuilder.Append("</title>");
			htmlBuilder.Append("</head>");
			htmlBuilder.Append("<body>");
			///
			///Inicia tabla de TOKEN
			///
			htmlBuilder.Append("<table class= \"token\">");
			htmlBuilder.Append("<tr>" +
								"<th class=\"tg-f4we\" colspan=\"5\">TOKEN</th> " +
							   "</tr>");
			htmlBuilder.Append("<tr>" +
								"<td class=\"tg-cjcd\">#<br></td>" +
								"<td class=\"tg-cjcd\">LEXEMA<br></td>" +
								"<td class=\"tg-cjcd\">FILA</td>" +
								"<td class=\"tg-cjcd\">COLUMNA</td>" +
								"<td class=\"tg-cjcd\">TOKEN</td>" +
							   "</tr>");
			int contador = 0;
			///Agrega los elementos de la lista a la tabla recién creada
			while (!tokens.EsVacio())
			{
				Token cast = (Token) tokens.Desencolar();
				htmlBuilder.Append("<tr>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + ++contador + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.lexema + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.fila + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.columna + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.id + "</td>");
				htmlBuilder.Append("</tr>");
			}
			htmlBuilder.Append("<tr>" +
								"<th class=\"tg-cjcd\" colspan=\"5\"> ---------- Fin de línea. " + contador + " tokens ---------- </th> " +
							   "</tr>");
			htmlBuilder.Append("</table><br><br>");
			///
			///Inicia tabla de ERRORES
			///
			htmlBuilder.Append("<table class= \"err\">");
			htmlBuilder.Append("<tr>" +
								"<th class=\"tg-f4we\" colspan=\"5\">ERROR</th> " +
							   "</tr>");
			htmlBuilder.Append("<tr>" +
								"<td class=\"tg-cjcd\">#<br></td>" +
								"<td class=\"tg-cjcd\">DESCRIPCIÓN<br></td>" +
								"<td class=\"tg-cjcd\">FILA</td>" +
								"<td class=\"tg-cjcd\">COLUMNA</td>" +
								"<td class=\"tg-cjcd\">ERROR</td>" +
							   "</tr>");
			contador = 0;
			///Agrega los elementos de la lista a la tabla recién creada
			while (!errores.EsVacio())
			{
				Token cast = (Token) errores.Desencolar();
				htmlBuilder.Append("<tr>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + ++contador + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.lexema + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.fila + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.columna + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.id + "</td>");
				htmlBuilder.Append("</tr>");
			}
			htmlBuilder.Append("<tr>" +
					"<th class=\"tg-cjcd\" colspan=\"5\"> ---------- Fin de línea. " + contador + " errores ---------- </th> " +
				   "</tr>");
			htmlBuilder.Append("</table><br><br>");
			htmlBuilder.Append("</body>");
			htmlBuilder.Append("</html>");
			//Crea un string de todo lo escrito anteriormente
			return htmlBuilder.ToString();
		}
		/// <summary>
		/// Mueve los tokens a tokens y los errores a errores :v
		/// </summary>
		private void DepurarTokens()
		{
			//Realiza una copia de la lista de tokens
			Node aux = colaEntrada.primero;
			//Console.WriteLine("Depurar Tokens");
			//Servirá para castear los tokens
			Token referencia;
			while (aux != null)
			{
				//Castea el objeto
				referencia = (Token)aux.objeto;
				//Si es error va a error
				if (referencia.id.Equals("Desconocido"))
				{
					errores.Encolar(referencia);
					//Console.WriteLine(referencia.id);
				}
				//Si no es error es token :v
				else
				{
					//Console.WriteLine(referencia.id);
					tokens.Encolar(referencia);
				}
				//Avanza al siguiente
				aux = aux.siguiente;
			}
		}
		/// <summary>
		/// Genera y Abre un archivo
		/// </summary>
		/// <param name="Texto"></param>
		/// <param name="extension"></param>
		/// <param name="abrir"></param>
		public void GenerarYAbrir(String nombre, String texto, String extension, bool abrir)
		{
			StreamWriter outputFile = null;
			try
			{
				using (outputFile = new StreamWriter(ruta + "\\" + nombre +"."+extension))
				{
					outputFile.Write(texto.ToString());
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				try
				{
					if (outputFile != null)
					{
						outputFile.Close();
						if (abrir)
						{
							System.Diagnostics.Process.Start(ruta + "\\" + nombre + "." + extension);
						}
					}
				}
				catch (Exception)
				{
				}
			}

		}
		/// <summary>
		/// Lee un archivo .txt en la ruta URL
		/// </summary>
		/// <param name="URL"></param>
		/// <returns></returns>
		public String Leer(String URL)
		{
			//Prepara el archivo para su lectura
			StreamReader reader = null;
			//Se almacenar temporalmente una linea
			String texto = "";
			//Aquí se almacenara TODAS las lineas leídas :v
			String retorno = "";
			//El sistema intentará escribir un string
			try
			{
				//Se instancia el archivo para su lectura
				reader = new StreamReader(URL);
				//Mientras el texto no sea nulo
				while ((texto = reader.ReadLine()) != null)
				{
					//Agrega texto a la cadena a retornar
					retorno += texto + Environment.NewLine;
				}
			}
			catch (Exception)
			{
				//Ocurre un error al leer el archivo
			}
			finally
			{
				//Intentará cerrar la lectura del archivo
				try
				{
					//Verifica que se haya instanciado el StreamReader
					if (reader != null)
					{
						//Cierra la lectura del archivo
						reader.Close();
					}
				}
				catch (Exception)
				{
					//Ocurre un error al cerrar el archivo
				}
			}
			//Retorna la cadena de texto, con o sin errores en ella
			return retorno;
		}
	}
}
