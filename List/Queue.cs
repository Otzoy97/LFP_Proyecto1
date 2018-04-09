using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPPA_OK.List
{
	class Queue
	{
		public Node primero;
		public Node ultimo;
		private int size;
		/// <summary>
		/// Devuelve el tamaño de la cola
		/// </summary>
		/// <returns></returns>
		public int Largo()
		{
			return size;
		}
		/// <summary>
		/// Consulta si la cola está vacía
		/// </summary>
		/// <returns></returns>
		public bool EsVacio()
		{
			return primero == null;
		}
		/// <summary>
		/// Observa la cabeza de la cola
		/// </summary>
		/// <returns></returns>
		public Object VerPrimero()
		{
			return primero.objeto;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		public Queue()
		{
			this.primero = null;
			this.size = 0;
			ultimo = primero;
		}
		/// <summary>
		/// Agrega a la cola un objeto
		/// </summary>
		/// <param name="objeto"></param>
		public void Encolar(Object objeto)
		{
			//Crea un nuevo nodo
			Node nuevo = new Node(objeto);
			//Si la cola está vacía el nuevo nodo
			//apuntará a la cabeza
			if (EsVacio())
			{
				primero = nuevo;
				ultimo = primero;
			}
			else
			//Agrega el nuevo nodo al final de la cola
			{
				ultimo.siguiente = nuevo;
				ultimo = nuevo;
			}
			//Aumenta el tamaño de la cola
			size++;
		}
		/// <summary>
		/// Retira y devuelve la cabeza de la cola
		/// </summary>
		/// <returns></returns>
		public Object Desencolar()
		{
			//Aloja temporalmente el objeto del nodo
			Object obj = primero.objeto;
			//Console.WriteLine(obj);
			//Quinta el apuntador de la cola
			primero = primero.siguiente;
			//Disminuye el tamaño de la cola
			size--;
			//¿Tengo que decirlo? :v
			return obj;
		}
	}
}
