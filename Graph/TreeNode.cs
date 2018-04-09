using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPPA_OK.Graph
{
	class TreeNode
	{
		private String id;
		private String value;
		public TreeNode left, right;
		private bool raiz = true;
		/// <summary>
		/// Constructor prro
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		public TreeNode(String id, String value)
		{
			this.id = id;
			this.value = value;
			this.left = null;
			this.right = null;
		}
		public String Id()
		{
			return this.id;
		}
		public String Value()
		{
			return this.value;
		}
		public void EsRaiz(bool raiz)
		{
			this.raiz = raiz;
		}
		public bool EsRaiz()
		{
			return this.raiz;
		}
	}
}
