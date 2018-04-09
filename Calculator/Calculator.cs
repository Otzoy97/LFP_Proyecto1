using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KAPPA_OK.List;
using KAPPA_OK.Files;
using KAPPA_OK.Compilator;

namespace KAPPA_OK.Calculator
{
	class Calculator
	{
		private String expresion;
		private Queue infijo;
		private String numero;
		private Stack postfijo = new Stack();
		private bool sinError = true;

		public Result Calcular(Queue tokenDepurado)
		{
			//Instancia un result
			Result resultado;// = new Result("Resultado",expresion,Operar());
			//Realiza una referencia a infijo
			infijo = tokenDepurado;
			//Realiza un Postfix
			Postfix();
			if (sinError)
			{
				//Opera el Postfix
				return resultado = new Result("Resultado", expresion, Operar());
			}
			return null;
		}
		/// <summary>
		/// Arregla la operación en un postfijo
		/// </summary>
		private void Postfix()
		{
			Stack simbolo = new Stack();
			if (infijo.EsVacio())
			{
				sinError = false;
			}
			while (!infijo.EsVacio())
			{
				KAPPA_OK.Compilator.Token referencia = (KAPPA_OK.Compilator.Token)infijo.Desencolar();
				expresion += referencia.lexema;
				switch (Precedencia(referencia.lexema))
				{
					case 1://Apila un parentesis de apertura
						simbolo.Apilar(referencia.lexema);
						break;
					case 3://Apila + -
					case 4://Apila * /
						if (!String.IsNullOrEmpty(numero)) postfijo.Apilar(numero);//Apila los numeros que hubieran
						numero = "";//Limpia la cadena
						while (!simbolo.EsVacio() && Precedencia((String)simbolo.Ver()) >= Precedencia(referencia.lexema))
						{//Mete todos los simbolos según su precedencia
							postfijo.Apilar(simbolo.Desapilar());
						}
						simbolo.Apilar(referencia.lexema);//Mete el signo correspondiente
						break;
					case 2://Por agrupamiento saca todos los simbolos hasta encontrar (
						if (!String.IsNullOrEmpty(numero)) postfijo.Apilar(numero);//Apila los numeros que hubieran
						numero = "";//
						try
						{
							while (!"(".Equals((String)simbolo.Ver()))
							{
								postfijo.Apilar(simbolo.Desapilar());
							}
							simbolo.Desapilar();
						}
						catch (Exception)
						{
							sinError = false;
						}
						break;
					default:
						numero += referencia.lexema;//44
						break;
				}
			}
			if (!String.IsNullOrEmpty(numero)) postfijo.Apilar(numero);//Apila los numeros que hubieran
			numero = "";
			while (!simbolo.EsVacio())
			{
				if (Precedencia((String)simbolo.Ver()) != 1)
				{
					postfijo.Apilar(simbolo.Desapilar());
				}
				else
				{
					sinError = false;
					simbolo.Desapilar();
				}
			}
		}
		/// <summary>
		/// Determina la precedencia del operador, según jerarquía
		/// </summary>
		/// <param name="operador"></param>
		/// <returns></returns>
		private int Precedencia(String operador)
		{
			String op = operador;
			if (op.Equals("*") || op.Equals("/")) return 4;
			if (op.Equals("+") || op.Equals("-")) return 3;
			if (op.Equals(")")) return 2;
			if (op.Equals("(")) return 1;
			return 99;
		}
		/// <summary>
		/// Evalua los numeros dados con el operando
		/// </summary>
		/// <param name="op"></param>
		/// <param name="n2"></param>
		/// <param name="n1"></param>
		/// <returns></returns>
		private int Evaluar(String op, String n2, String n1)
		{
			int num1 = int.Parse(n1);
			int num2 = int.Parse(n2);
			if (op.Equals("+")) return (num1 + num2);
			if (op.Equals("-")) return (num1 - num2);
			if (op.Equals("*")) return (num1 * num2);
			if (op.Equals("/")) return (num1 / num2);
			if (op.Equals("%")) return (num1 % num2);
			return 0;
		}
		/// <summary>
		/// Opera la pila de postfijo
		/// </summary>
		private String Operar()
		{
			//Se almacenará el resultado
			Stack operandos = new Stack();
			//Volcando pila
			Stack fix = new Stack();
			while (!postfijo.EsVacio())
			{
				fix.Apilar(postfijo.Desapilar());
			}
			while (!fix.EsVacio())
			{
				if (fix.Ver().Equals("+") || fix.Ver().Equals("-") || fix.Ver().Equals("*") || fix.Ver().Equals("/"))
				{
					//	Console.WriteLine(fix.Ver()+ " " + operandos.Ver());
					operandos.Apilar(Evaluar((String)fix.Desapilar(), (String)operandos.Desapilar(), (String)operandos.Desapilar()) + "");
				}
				else
				{
					operandos.Apilar(fix.Desapilar());
				}
			}
			Console.WriteLine((String)operandos.Ver());
			return (String)operandos.Ver();
		}
	}
}
