using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KAPPA_OK.Graph;

namespace KAPPA_OK.List
{
	class ListaSimple
	{
		public Node inicio;
		private int size;
		/// <summary>
		/// Devuelve el tamaño de la lista
		/// </summary>
		/// <returns></returns>
		public int Largo()
		{
			return size;
		}
		/// <summary>
		/// Determina si la lista está vacía
		/// </summary>
		/// <returns></returns>
		public bool EsVacio()
		{
			return inicio == null;
		}
		/// <summary>
		/// Constructor :v
		/// </summary>
		public ListaSimple()
		{
			this.inicio = null;
			this.size = 0;
		}
		/// <summary>
		/// Agrega un objeto al final de la Lista
		/// </summary>
		/// <param name="objeto"></param>
		public void Agregar(Object objeto)
		{
			Node nuevo = new Node(objeto);
			if (EsVacio())
			{
				inicio = nuevo;
			}
			else
			{
				Node aux = inicio;
				while (aux.siguiente != null)
				{
					aux = aux.siguiente;
				}
				aux.siguiente = nuevo;
			}
			size++;
		}
		/// <summary>
		/// Verifica la existencia de un objeto en la lista
		/// </summary>
		/// <param name="referencia"></param>
		/// <returns></returns>
		public bool Buscar(Object referencia)
		{
			Node aux = inicio;//Copia la lista
			while (aux != null)//Recorre la lista en busca de un <match>
			{
				if (referencia == aux.objeto)//Verifica que sean iguales
				{
					return true;//Existe coincidencia
				}
				else
				{
					aux = aux.siguiente;//Avanza al siguiente nodo
				}
			}
			return false;//El objeto no existe
		}
		/// <summary>
		/// Elimina un nodo de la lista con el contenido de referencia
		/// </summary>
		/// <param name="referencia"></param>
		/// <returns></returns>
		public Object Remover(Object referencia)
		{
			Object retorno = null;
			//Verifica si el nodo a eliminar es el primero
			if (inicio.objeto == referencia)
			{
				//Arregla el apuntador
				inicio = inicio.siguiente;
			}
			else
			{
				//Crea una copia de la lista
				Node aux = inicio;
				//Recorre la lista y se detiene antes de la referencia :v
				while (aux.siguiente.objeto!=referencia)
				{
					aux = aux.siguiente;//Mueve la referencia
				}
				//Guarda el nodo siguiente del nodo a eliminar
				Node siguiente = aux.siguiente.siguiente;
				//Enlaza el nodo anterior al de eliminar y lo enlaza
				//con el siguiente despues de él
				retorno = aux.siguiente.objeto;
				aux.siguiente = siguiente;
			}
			size--;
			return retorno;
		}
		/// <summary>
		/// Elimina un nodo de la lista en la poisición indicada
		/// </summary>
		/// <param name="indice"></param>
		/// <returns></returns>
		public Object Remover(int indice)
		{
			Object retorno = null;
			//Verifica si el nodo a eliminar es el primero
			if (indice == 1)
			{
				//Arregla el apuntador
				inicio = inicio.siguiente;
			}
			else
			{
				//Crea una copia de la lista
				Node aux = inicio;
				//Recorre la lista y se detiene antes de la referencia :v
				while (indice!=2)
				{
					indice--;
					aux = aux.siguiente;//Mueve la referencia
				}
				//Guarda el nodo siguiente del nodo a eliminar
				Node siguiente = aux.siguiente.siguiente;
				//Enlaza el nodo anterior al de eliminar y lo enlaza
				//con el siguiente despues de él
				retorno = aux.siguiente.objeto;
				aux.siguiente = siguiente;
			}
			size--;
			return retorno;
		}
	}
}
