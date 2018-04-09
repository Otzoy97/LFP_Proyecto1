using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPPA_OK.List
{
	class Node
	{
		//public Node anterior;
		public Node siguiente;
		public Object objeto;

		public Node(Object objeto)
		{
			this.objeto = objeto;
		}
	}
}
