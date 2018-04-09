using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using KAPPA_OK.List;
using KAPPA_OK.Files;

namespace KAPPA_OK.Compilator
{
	class Scanner
	{
		public RichTextBox _RichTextBox;
		private int columna = 0, fila = 0;
		private String cadenaEn = "";
		public Queue token = new Queue();
		/// <summary>
		/// Agrega texto al richtextbox
		/// </summary>
		/// <param name="objeto"></param>
		/// <param name="color"></param>
		private void AgregarTexto(Object objeto, Color color)
		{
			//Convierte el objeto a un string
			String prro = objeto.ToString();
			//Agrega el string al richtxtbox
			_RichTextBox.AppendText(prro);
			//Selecciona desde un punto TextLenght con un tamano de prro.Lenght
			_RichTextBox.SelectionStart = _RichTextBox.TextLength - prro.Length;
			_RichTextBox.SelectionLength = prro.Length;
			//Pone color al texto
			switch (prro)
			{
				case "+":
					_RichTextBox.SelectionColor = Color.Purple;
					break;
				case "-":
					_RichTextBox.SelectionColor = Color.Gray;
					break;
				case "*":
					_RichTextBox.SelectionColor = Color.Orange;
					break;
				case "/":
					_RichTextBox.SelectionColor = Color.LightBlue;
					break;
				default:
					_RichTextBox.SelectionColor = color;
					break;
			}
		}
		/// <summary>
		/// En algunas ocasiones es necesario conocer
		/// si la palabra es reservada, debido a una limitacion
		/// de la propia función.
		/// </summary>
		/// <returns></returns>
		private String DetReservada()
		{
			switch (cadenaEn)
			{
				case "Resultado":
				case "Graficar":
				case "Node":
				case "Valor":
				case "Operador":
				case "IZQ":
				case "DER":
					AgregarTexto(cadenaEn,Color.Green);
					return cadenaEn;
				default:
					AgregarTexto(cadenaEn,Color.Black);
					return "Desconocido";
			}
		}
		/// <summary>
		/// Igual que DetReservada pero esta no agrega nada a RichTextBox
		/// </summary>
		/// <returns></returns>
		private String DeReservada()
		{
			switch (cadenaEn)
			{
				case "Resultado":
				case "Graficar":
				case "Node":
				case "Valor":
				case "Operador":
				case "IZQ":
				case "DER":
					return cadenaEn;
				default:
					return "Desconocido";
			}
		}
		/// <summary>
		/// A través de AFD analiza léxicamente la cadena
		/// de entrada
		/// </summary>
		/// <param name="cadena"></param>
		/// <returns></returns>
		public Queue Escanear(String cadena,RichTextBox rtbox)
		{
			_RichTextBox = rtbox;
			_RichTextBox.Text = "";
			char caracter;
			//Mantiene el control de estados
			int estadoAceptacion = 0;
			//Alojara los comandos que mandará a ejecutar
			Queue ejecutar = new Queue();
			Queue execute = new Queue();
			//*.*
			for (int i = 0; i < cadena.Length; i++)
			{
				caracter = cadena[i];
				//P(estadoAceptacion + " -> " + caracter);
				switch (estadoAceptacion)
				{
					case 0:
						if ( (caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122) )
						{//B
							cadenaEn = caracter + "";//Limpia la cadena y agrega el caracter
							estadoAceptacion = 1;//Mueve el estado de aceptación
							columna++;//Aumenta la columna
						}
						else if (caracter == 10 || caracter == 11)
						{ //Salto de línea
							fila++;
							columna = 0;
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{ //Espacio y tab
							columna++;
							AgregarTexto(caracter, Color.Black);
						}
						else
						{ //Error prro
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
							AgregarTexto(caracter,Color.Black);
						}
						break;
					case 1:
						if ( (caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn += caracter;//Agrega caracter a cadena
							estadoAceptacion = 1;//Mantiene el estado de aceptación
							columna++;//Aumenta la columna
						}
						else if (caracter == 123)//{
						{
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DetReservada()));//Guarda el contenido de cadena
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DeReservada()));//Guarda el contenido de cadena
							token.Encolar(new Token(caracter + "", fila, columna++, "Llave Izq"));//Guarda la llave como token
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Llave Izq"));//Guarda la llave como token
							AgregarTexto(caracter,Color.Black);
							estadoAceptacion = 2;//Se mueve el estado
							cadenaEn = "";//Se limpia la cadena
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
							estadoAceptacion = 31;
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DetReservada()));//Guarda el contenido de cadena
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DeReservada()));//Guarda el contenido de cadena
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
							estadoAceptacion = 31;
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DetReservada()));//Guarda el contenido de cadena
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DeReservada()));//Guarda el contenido de cadena
							AgregarTexto(caracter, Color.Black);
						}
						else
						{ //Error prro
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));//Agrega el error_
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error_
							AgregarTexto(caracter,Color.Black);
						}
						break;
					case 2:
						if ((caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn = caracter + "";//Limpia la cadena
							estadoAceptacion = 7;//Mueve el estado de aceptacion
							columna++;//Aumenta la columna
						}
						else if (caracter == 40)//(
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Parentesis Izq"));//Encola el parentesis
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Parentesis Izq"));//Encola el parentesis
							cadenaEn ="";//Limpia la cadena
							estadoAceptacion = 3;//Mueve el estado de aceptacion
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter >= 48 && caracter <= 57)//N
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Numero"));//Encola el numero
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Numero"));//Encola el numero
							cadenaEn = "";//Limpia la cadena
							estadoAceptacion = 4;//Mueve el estado de aceptacion
							AgregarTexto(caracter, Color.Blue);
						}
						else if (caracter == 10 || caracter == 11)
						{ //Salto de línea
							fila++;
							columna = 0;
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{ //Espacio y tab
							columna++;
							AgregarTexto(caracter, Color.Black);
						}
						else
						{ //Error prro
							AgregarTexto(caracter, Color.Black);
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
						}
						break;
					case 3:
						if (caracter >= 48 && caracter <= 57)//N
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Numero"));//Encola el numero
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Numero"));//Encola el numero
							cadenaEn = caracter + "";//Limpia la cadena
							estadoAceptacion = 4;//Mueve el estado de aceptacion
							AgregarTexto(caracter, Color.Blue);
						}
						else if (caracter == 40)//(
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Parentesis Izq"));//Encola el parentesis
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Parentesis Izq"));//Encola el parentesis
							cadenaEn = caracter + "";//Limpia la cadena
							estadoAceptacion = 3;//Mantiene el estado de aceptacion
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 10 || caracter == 11)
						{ //Error prro
							fila++;
							columna = 0;
							token.Encolar(new Token(caracter+"",fila,columna,"Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna, "Desconocido"));//Agrega el error
							AgregarTexto(caracter, Color.Black);
						}
						else
						{ //Error prro
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
							_RichTextBox.AppendText(caracter + "");
						}
						break;
					case 4:
						if (caracter >= 48 && caracter <= 57)//N
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Numero"));//Encola el numero
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Numero"));//Encola el numero
							cadenaEn = caracter + "";//Limpia la cadena
							estadoAceptacion = 4;//Mueve el estado de aceptacion
							AgregarTexto(caracter, Color.Blue);
						}
						else if (caracter == 42 || caracter == 43 || caracter == 45 || caracter == 47)//O
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Operador"));//Encola el operador
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Operador"));//Encola el operador
							cadenaEn = caracter + "";//Limpia la cadena
							estadoAceptacion = 5;//Mueve el estado de aceptacion
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 41)//)
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Parentesis Der"));//Encola el par izq
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Parentesis Der"));//Encola el par izq
							cadenaEn = caracter + ""; //Limpia la cadena
							estadoAceptacion = 6;//Mueve el estado de aceptación
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 125)//}
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Llave Der"));//Encola el par izq
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Llave Der"));//Encola el par izq
							cadenaEn = caracter + ""; //Limpia la cadena
							estadoAceptacion = 29;//Mueve el estado de aceptación
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 10 || caracter == 11)
						{ //Salto de línea
							fila++;
							columna = 0;
							estadoAceptacion = 31;
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{ //Espacio y tab
							columna++;
							AgregarTexto(caracter, Color.Black);
							estadoAceptacion = 31;
						}
						else
						{ //Error prro
							AgregarTexto(caracter, Color.Black);
							token.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
						}
						break;
					case 5:
						if (caracter >= 48 && caracter <= 57)//N
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Numero"));//Encola el numero
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Numero"));//Encola el numero
							cadenaEn = caracter + "";//Limpia la cadena
							estadoAceptacion = 4;//Mueve el estado de aceptacion
							AgregarTexto(caracter, Color.Blue);
						}
						else if (caracter == 40)//(
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Parentesis Izq"));//Encola el parentesis
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Parentesis Izq"));//Encola el parentesis
							cadenaEn = caracter + "";//Limpia la cadena
							estadoAceptacion = 3;//Mueve el estado de aceptacion
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 10 || caracter == 11)
						{ //Error prro
							fila++;
							columna = 0;
							token.Encolar(new Token(caracter+"",fila,columna,"Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna, "Desconocido"));//Agrega el error
							AgregarTexto(caracter, Color.Black);
						}
						else
						{ //Error prro
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 6:
						if (caracter == 41)//)
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Parentesis Der"));//Encola el par izq
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Parentesis Der"));//Encola el par izq
							cadenaEn = caracter + ""; //Limpia la cadena
							estadoAceptacion = 6;//Mueve el estado de aceptación
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 42 || caracter == 43 || caracter == 45 || caracter == 47)//O
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Operador"));//Encola el operador
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Operador"));//Encola el operador
							cadenaEn = caracter + "";//Limpia la cadena
							estadoAceptacion = 5;//Mueve el estado de aceptacion
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 125)//}
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Llave Der"));//Encola el par izq
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Llave Der"));//Encola el par izq
							cadenaEn = caracter + ""; //Limpia la cadena
							estadoAceptacion = 29;//Mueve el estado de aceptación
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 10 || caracter == 11)
						{ //Salto de línea
							fila++;
							columna = 0;
							estadoAceptacion = 31;
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{ //Espacio y tab
							columna++;
							AgregarTexto(caracter, Color.Black);
							estadoAceptacion = 31;
						}
						else
						{ //Error prro
							AgregarTexto(caracter, Color.Black);
							token.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
						}
						break;
					case 7:
						if ((caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn += caracter;//Agrega a la cadena
							columna++;//Aumenta la columna
							estadoAceptacion = 7;//Cambia estado de aceptación
						}
						else if (caracter >= 48 && caracter <= 57)//N
						{
							cadenaEn += caracter;//Agrega a la cadena
							columna++;//Aumenta la columna
							estadoAceptacion = 8;//Cambia estado de aceptación
						}
						else if (caracter == 95)//_
						{
							cadenaEn += caracter;//Agrega a la cadena
							columna++;//Aumenta la columna
							estadoAceptacion = 9;//Cambia estado de aceptación
						}
						else if (caracter == 46)//.
						{
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "IdentificadorOrigen"));//Agrega el token -cadena-
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "IdentificadorOrigen"));//Agrega el token -cadena-
							AgregarTexto(cadenaEn, Color.Red);
							token.Encolar(new Token(caracter + "", fila, columna++, "Punto"));//Agrega el token -46-
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Punto"));//Agrega el token -46-
							AgregarTexto(caracter, Color.Black);
							cadenaEn = "";//Limpia la cadena
							estadoAceptacion = 10;//Cambia estado de aceptación
						}
						else if (caracter == 60)//<
						{
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DetReservada()));//Agrega el token -cadena-
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DeReservada()));//Agrega el token -cadena-
							token.Encolar(new Token(caracter + "", fila, columna++, "Menor que"));//Agrega el token -60-
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Menor que"));//Agrega el token -60-
							AgregarTexto(caracter, Color.Black);
							cadenaEn = "";//Limpia la cadena
							estadoAceptacion = 17;//Cambia estado de aceptación
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
							estadoAceptacion = 31;
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DetReservada()));//Agrega el token -cadena-
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DeReservada()));//Agrega el token -cadena-
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
							estadoAceptacion = 31;
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DetReservada()));//Agrega el token -cadena-
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DeReservada()));//Agrega el token -cadena-
							AgregarTexto(caracter, Color.Black);
						}
						else
						{// Error prro >:v
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 8:
					case 9:
						if ((caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn += caracter;//Agrega a la cadena
							columna++;//Aumenta la columna
							estadoAceptacion = 7;//Cambia estado de aceptación
						}
						else if (caracter >= 48 && caracter <= 57)//N
						{
							cadenaEn += caracter;//Agrega a la cadena
							columna++;//Aumenta la columna
							estadoAceptacion = 8;//Cambia estado de aceptación
						}
						else if (caracter == 95)//_
						{
							cadenaEn += caracter;//Agrega a la cadena
							columna++;//Aumenta la columna
							estadoAceptacion = 9;//Cambia estado de aceptación
						}
						else if (caracter == 46)//.
						{
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "IdentificadorOrigen"));//Agrega el token -cadena-
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "IdentificadorOrigen"));//Agrega el token -cadena-
							AgregarTexto(cadenaEn, Color.Red);
							token.Encolar(new Token(caracter + "", fila, columna++, "Punto"));//Agrega el token -46-
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Punto"));//Agrega el token -46-
							AgregarTexto(caracter, Color.Black);
							cadenaEn = "";//Limpia la cadena
							estadoAceptacion = 10;//Cambia estado de aceptación
						}
						else if (caracter == 10 || caracter == 11)//salto de linea
						{ //Error prro
							fila++;
							columna = 0;
							token.Encolar(new Token(caracter + "", fila, columna, "Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna, "Desconocido"));//Agrega el error
							AgregarTexto(caracter, Color.Black);
						}
						else
						{ //Error prro
							token.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 10:
						if ((caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn = "" + caracter;//Limpia la cadena
							columna++;
							estadoAceptacion = 11;
						}
						else if (caracter == 10 || caracter == 11)//salto de linea
						{ //Error prro
							fila++;
							columna = 0;
							token.Encolar(new Token(caracter + "", fila, columna, "Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna, "Desconocido"));//Agrega el error
							AgregarTexto(caracter, Color.Black);
						}
						else
						{ //Error prro
							token.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 11:
						if ((caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn += caracter;//Añade el caracter
							columna++;
							estadoAceptacion = 11;
						}
						else if (caracter == 45)//-
						{
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DetReservada()));//Guarda la cadena token
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DeReservada()));//Guarda la cadena token
							token.Encolar(new Token(caracter + "", fila, columna++, "Guion"));//Guarda 45
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Guion"));//Guarda 45
							AgregarTexto(caracter, Color.Black);
							cadenaEn = "";//Limpia cadea
							estadoAceptacion = 12;//Mueve estado de aceptacion
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
							estadoAceptacion = 31;
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DetReservada()));//Guarda la cadena token
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DeReservada()));//Guarda la cadena token
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
							estadoAceptacion = 31;
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DetReservada()));//Guarda la cadena token
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DeReservada()));//Guarda la cadena token
							AgregarTexto(caracter, Color.Black);
						}
						else
						{// Error prro >:v
							token.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 12:
						if (caracter == 62)//>
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Mayor que"));//Guarda 62
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Mayor que"));//Guarda 62
							cadenaEn = "";//Limpia cadea
							estadoAceptacion = 13;//Mueve estado de aceptacion
						}
						else if (caracter == 10 || caracter == 11)//salto de linea
						{ //Error prro
							fila++;
							columna = 0;
							token.Encolar(new Token(caracter + "", fila, columna, "Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna, "Desconocido"));//Agrega el error
						}
						else
						{ //Error prro
							token.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
						}
						AgregarTexto(caracter, Color.Black);
						break;
					case 13:
						if ((caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn = "" + caracter;//Limpia la cadena
							columna++;//Aumenta la columna
							estadoAceptacion = 14;//Mueve el estado de aceptación
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
							AgregarTexto(caracter, Color.Black);
						}
						else
						{// Error prro >:v
							token.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 14:
					case 15:
					case 16:
						if ((caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn += caracter;//Agrega a la cadena
							columna++;//Aumenta la columna
							estadoAceptacion = 14;//Cambia estado de aceptación
						}
						else if (caracter >= 48 && caracter <= 57)//N
						{
							cadenaEn += caracter;//Agrega a la cadena
							columna++;//Aumenta la columna
							estadoAceptacion = 15;//Cambia estado de aceptación
						}
						else if (caracter == 95)//_
						{
							cadenaEn += caracter;//Agrega a la cadena
							columna++;//Aumenta la columna
							estadoAceptacion = 16;//Cambia estado de aceptación
						}
						else if (caracter == 59)//;
						{
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "IdentificadorDestino"));//Agrega el token -cadena-
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "IdentificadorDestino"));//Agrega el token -cadena-
							AgregarTexto(cadenaEn, Color.Red);
							token.Encolar(new Token(caracter + "", fila, columna++, "Punto y Coma"));//Agrega el token -59-
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Punto y Coma"));//Agrega el token -59-
							AgregarTexto(caracter, Color.Black);
							cadenaEn = "";//Limpia la cadena
							estadoAceptacion = 28;//Cambia estado de aceptación
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
							estadoAceptacion = 31;
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "IdentificadorDestino"));//Agrega el token -cadena-
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "IdentificadorDestino"));//Agrega el token -cadena-
							AgregarTexto(cadenaEn, Color.Red);
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
							estadoAceptacion = 31;
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "IdentificadorDestino"));//Agrega el token -cadena-
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "IdentificadorDestino"));//Agrega el token -cadena-
							AgregarTexto(cadenaEn, Color.Red);
							AgregarTexto(caracter, Color.Black);
						}
						else
						{// Error prro >:v
							token.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 17:
						if ((caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn = "" + caracter;//Limpia la cadena
							columna++;
							estadoAceptacion = 18;
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
							AgregarTexto(caracter, Color.Black);
						}
						else
						{// Error prro >:v
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 18:
					case 19:
					case 20:
						if ((caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn += caracter;//Agrega a la cadena
							columna++;//Aumenta la columna
							estadoAceptacion = 18;//Cambia estado de aceptación
						}
						else if (caracter >= 48 && caracter <= 57)//N
						{
							cadenaEn += caracter;//Agrega a la cadena
							columna++;//Aumenta la columna
							estadoAceptacion = 20;//Cambia estado de aceptación
						}
						else if (caracter == 95)//_
						{
							cadenaEn += caracter;//Agrega a la cadena
							columna++;//Aumenta la columna
							estadoAceptacion = 19;//Cambia estado de aceptación
						}
						else if (caracter == 44) //,
						{
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "Identificador"));//Agrega el token -cadena-
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "Identificador"));//Agrega el token -cadena-
							AgregarTexto(cadenaEn, Color.Red);
							token.Encolar(new Token(caracter + "", fila, columna++, "Coma"));//Agrega el token -59-
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Coma"));//Agrega el token -59-
							AgregarTexto(caracter, Color.Black);
							cadenaEn = "";//Limpia la cadena
							estadoAceptacion = 21;//Cambia estado de aceptación
						}
						else if (caracter == 10 || caracter == 11)//salto de linea
						{ //Error prro
							fila++;
							columna = 0;
							token.Encolar(new Token(caracter + "", fila, columna, "Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna, "Desconocido"));//Agrega el error
							AgregarTexto(caracter, Color.Black);
						}
						else
						{ //Error prro
							token.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));//Agrega el error
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 21:
						if ((caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn = "" + caracter;//Limpia la cadena
							columna++;
							estadoAceptacion = 22;
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
							AgregarTexto(caracter, Color.Black);
						}
						else
						{// Error prro >:v
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 22:
						if ((caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn += caracter;//Añade el caracter
							columna++;
							estadoAceptacion = 22;
						}
						else if (caracter == 44)//,
						{
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DetReservada()));//Guarda la cadena token
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DeReservada()));//Guarda la cadena token
							token.Encolar(new Token(caracter + "", fila, columna++, "Coma"));//Guarda 44
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Coma"));//Guarda 44
							AgregarTexto(caracter, Color.Black);
							cadenaEn = "";//Limpia cadea
							estadoAceptacion = 23;//Mueve estado de aceptacion
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DetReservada()));//Guarda la cadena token
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DeReservada()));//Guarda la cadena token
							AgregarTexto(caracter, Color.Black);
							estadoAceptacion = 31;
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DetReservada()));//Guarda la cadena token
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, DeReservada()));//Guarda la cadena token
							AgregarTexto(caracter, Color.Black);
							estadoAceptacion = 31;
						}
						else
						{// Error prro >:v
							token.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 23:
						if (caracter == 34)//
						{//"
							token.Encolar(new Token(caracter + "", fila, columna++, "Comillas"));//Guarda 34
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Comillas"));//Guarda 34
							AgregarTexto(caracter, Color.Black);
							cadenaEn = "";//Limpia la cadena
							estadoAceptacion = 24;//Mueve el estado de aceptación
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
							AgregarTexto(caracter, Color.Black);
						}
						else
						{// Error prro >:v
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 24:
						cadenaEn = "" + caracter;//Limpiar cadena y agregar caracter
						if (caracter == 10 || caracter == 11)
						{ //Salto de línea
							fila++;
							columna = 0;
						}
						else
						{
							columna++;
						}
						estadoAceptacion = 25;//mover estado de aceptacion
						break;
					case 25:
						if (caracter == 34)
						{//"
							token.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "Etiqueta"));//Agrega el token E
							ejecutar.Encolar(new Token(cadenaEn, fila, columna - cadenaEn.Length, "Etiqueta"));//Agrega el token E
							AgregarTexto(cadenaEn, Color.Black);
							token.Encolar(new Token(caracter + "", fila, columna++, "Comillas"));//Agrega el token comilla
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Comillas"));//Agrega el token comilla
							AgregarTexto(caracter, Color.Black);
							cadenaEn = "";//Limpia a cadena
							estadoAceptacion = 26;//Mueve el estado de aceptación
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
							cadenaEn += caracter;//Limpiar cadena y agregar caracter
						}
						else
						{//Cualquier cosa
							cadenaEn += caracter;//Limpiar cadena y agregar caracter
							estadoAceptacion = 25;//mover estado de aceptacion
							columna++; //Aumenta la columna
						}
						break;
					case 26:
						if (caracter == 62)//>
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Mayor que"));//Encolar simbolo
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Mayor que"));//Encolar simbolo
							cadenaEn = "";//limpiar cadena
							estadoAceptacion = 27;//mover estado de aceptacion
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
						}
						else
						{// Error prro >:v
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
						}
						AgregarTexto(caracter, Color.Black);
						break;
					case 27:
						if (caracter == 59)//;
						{
							token.Encolar(new Token(caracter + "", fila, columna++, "Punto y Coma"));//Agrega el token -59-
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Punto y Coma"));//Agrega el token -59-
							cadenaEn = "";//Limpia la cadena
							estadoAceptacion = 28;//Cambia estado de aceptación
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
						}
						else
						{// Error prro >:v
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
						}
						AgregarTexto(caracter, Color.Black);
						break;
					case 28:
						if (caracter == 125)//}
						{
							token.Encolar(new Token(caracter+"",fila,columna++,"Llave Der"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Llave Der"));
							AgregarTexto(caracter, Color.Black);
							cadenaEn = ""; //Limpia la cadena
							estadoAceptacion = 29;//Mueve el estado de aceptación
						}
						else if ((caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn = caracter + "";//Limpia la cadena
							columna++;//aumenta columna
							estadoAceptacion = 7;//Mueve estado de aceptación
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
							AgregarTexto(caracter, Color.Black);
						}
						else
						{// Error prro >:v
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 29:
						if (caracter == 59)//;
						{
							token.Encolar(new Token(caracter+"",fila,columna++,"Punto y Coma"));//Encolar token
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Punto y Coma"));//Encolar token
							execute.Encolar(new Execute(ejecutar).Ejecutar());//Manda a ejecutar la cola <ejecutar> 
							cadenaEn = "";//Limpiar cadena
							estadoAceptacion = 30;//Mover estado de aceptación
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
						}
						else
						{// Error prro >:v
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
						}
						AgregarTexto(caracter, Color.Black);
						break;
					case 30://Reinicia ejecutar
						if ((caracter >= 65 && caracter <= 90) ||
							(caracter >= 97 && caracter <= 122))
						{//B
							cadenaEn = caracter + "";//Limpia la cadena
							columna++;//aumenta columna
							estadoAceptacion = 1;//Mueve estado de aceptación
							ejecutar = new Queue();
						}
						else if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
							AgregarTexto(caracter, Color.Black);
						}
						else
						{// Error prro >:v
							token.Encolar(new Token(caracter+"",fila,columna++,"Desconocido"));
							//ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							AgregarTexto(caracter, Color.Black);
						}
						break;
					case 31:
						if (caracter == 10 || caracter == 11)
						{//Salto de línea
							fila++;
							columna = 0;
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 32 || caracter == 9)
						{//Espacio y tab
							columna++;
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 59)//;
						{
							estadoAceptacion = 28;
							token.Encolar(new Token(caracter + "", fila, columna++, "Punto y Coma"));//Encolar token
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Punto y Coma"));//Encolar token
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 123)//{
						{
							estadoAceptacion = 2;
							token.Encolar(new Token(caracter + "", fila, columna++, "Llave Izq"));//Guarda la llave como token
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Llave Izq"));//Guarda la llave como token
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 60)//<
						{
							estadoAceptacion = 17;
							token.Encolar(new Token(caracter + "", fila, columna++, "Menor que"));//Agrega el token -60-
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Menor que"));//Agrega el token -60-
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 45)//-
						{
							estadoAceptacion = 12;
							token.Encolar(new Token(caracter + "", fila, columna++, "Guion"));//Guarda 45
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Guion"));//Guarda 45
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 44)//,
						{
							estadoAceptacion = 23;
							token.Encolar(new Token(caracter + "", fila, columna++, "Coma"));//Guarda 45
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Coma"));//Guarda 45
							AgregarTexto(caracter, Color.Black);
						}
						else if (caracter == 125)//}
						{
							estadoAceptacion = 29;
							token.Encolar(new Token(caracter + "", fila, columna++, "Llave Der"));//Guarda 45
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Llave Der"));//Guarda 45
							AgregarTexto(caracter, Color.Black);
						}
						else
						{//Eror prro >:v
							token.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							ejecutar.Encolar(new Token(caracter + "", fila, columna++, "Desconocido"));
							AgregarTexto(caracter, Color.Black);
						}
						break;
				}
			}
			if (!String.IsNullOrEmpty(cadenaEn))
			{
				token.Encolar(new Token(cadenaEn, fila, columna++, "Desconocido"));
				AgregarTexto(cadenaEn, Color.Black);
			}
			return execute;
		}
		/*
		 * public void P(Object objeto)
		{
			Console.WriteLine(objeto);
		}
		 */
	}
}
