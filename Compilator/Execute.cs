using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KAPPA_OK.List;
using KAPPA_OK.Files;
using KAPPA_OK.Calculator;
using KAPPA_OK.Graph;

namespace KAPPA_OK.Compilator
{
	class Execute
	{
		private KAPPA_OK.List.Queue tokens;
		/// <summary>
		/// Constructor que copia referencialmente los tokens entrada
		/// </summary>
		/// <param name="tokens"></param>
		public Execute(KAPPA_OK.List.Queue tokens)
		{
			this.tokens = tokens;
		}
		/// <summary>
		/// Determina la función a utilizar y depura la cola de entrada (tokens) que esta
		/// función utilizará
		/// </summary>
		/// <returns></returns>
		public Result Ejecutar()
		{
			//Declara una cola que auxiliará en recoger los tokens necesarios para la ejecución de las funciones
			Queue auxiliar = new Queue();
			if (!VerErrores())
			{
				//Obtiene el token
				Token referencia = (Token)tokens.Desencolar();
				if (referencia.id.Equals("Resultado"))
				{
					//Mientras la cola aún tenga objetos se seguira encolando los tokens necesarios
					//Desapila { y la siguiente y la castea
					tokens.Desencolar();
					while (!tokens.EsVacio())
					{
						//Castea la referencia
						referencia = (Token)tokens.Desencolar();
						//Encola los tokens necesarios
						if (referencia.id.Equals("Parentesis Izq") || referencia.id.Equals("Parentesis Der") || 
							referencia.id.Equals("Numero") || referencia.id.Equals("Operador"))
						{
							//Si coincide lo agrega a tokens
							auxiliar.Encolar(referencia);
						}
					}
					//Manda a ejecutar la calculadora
					return new Calculator.Calculator().Calcular(auxiliar);
				}
				else if (referencia.id.Equals("Graficar"))
				{
					//Mientras la cola aún tenga objetos se seguira encolando los tokens necesarios
					//Desapila { y la siguiente y la castea
					tokens.Desencolar();
					while (!tokens.EsVacio())
					{
						//Castea la referencia
						referencia = (Token)tokens.Desencolar();
						//Encola los tokens necesarios
						if (referencia.id.Equals("Node") || referencia.id.Equals("Identificador") ||
							referencia.id.Equals("Valor") || referencia.id.Equals("Operador") ||
							referencia.id.Equals("IZQ") || referencia.id.Equals("DER") ||
							referencia.id.Equals("IdentificadorOrigen") || referencia.id.Equals("IdentificadorDestino") ||
							referencia.id.Equals("Etiqueta"))
						{
							//Si coincide lo agrega a tokens
							auxiliar.Encolar(referencia);
							//Console.WriteLine("{0} -> {1}","Execute",referencia.lexema);
						}
					}
					//Manda a ejecutar una graficadora
					return new BinaryTree().Graficar(auxiliar);
				}
			}
			return null;
		}
		/// <summary>
		/// Devuelve la existencia de errores
		/// </summary>
		/// <returns></returns>
		private bool VerErrores()
		{
			//Crea una copia de la lista
			Node aux = tokens.primero;
			Token referencia;
			while (aux != null)
			{
				//Castea el objeto
				referencia = (Token) aux.objeto;
				//Verifica que no exista errores
				if (referencia.lexema.Equals("Desconocido"))
				{
					return true;
				}
				else
				{
					//Avanza al siguiente
					aux = aux.siguiente;
				}
			}
			return false;
		}
	}
}
