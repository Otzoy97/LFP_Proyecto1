using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPPA_OK.List
{
	class Stack
	{
		public Node primero;
		private int size;
		/// <summary>
		/// Devuelve el largo de la Pila
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
		/// Observa la cima de la Pila
		/// </summary>
		/// <returns></returns>
		public Object Ver()
		{
			return primero.objeto;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		public Stack()
		{
			this.primero = null;
			this.size = 0;
		}
		/// <summary>
		/// Apila un nuevo nodo a la pila
		/// </summary>
		/// <param name="objeto"></param>
		public void Apilar(Object objeto)
		{
			//Crea el nodo a agregar a la pila
			Node nuevo = new Node(objeto);
			//Agrega el nodo a la pila
			nuevo.siguiente = primero;
			//Compone el apuntador de 'primero'
			primero = nuevo;
			//Aumenta el tamaño de la pila
			size++;
		}
		/// <summary>
		/// Desapila el nodo de la pila
		/// </summary>
		/// <returns></returns>
		public Object Desapilar()
		{
			//Aloja temporalmente el objeto de la pila
			Object objeto = primero.objeto;
			//Compone al apuntador de 'primero'
			primero = primero.siguiente;
			//Disminuye el tamaño de la pila
			size--;
			//>:v
			return objeto;
		}
	}
}
