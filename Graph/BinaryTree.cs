using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using KAPPA_OK.List;
using KAPPA_OK.Compilator;
using KAPPA_OK.Files;
using System.Drawing;

namespace KAPPA_OK.Graph
{
	class BinaryTree 
	{
		private Queue tokens;
		private ListaSimple tnode = new ListaSimple();
		private StringBuilder bodyGraphviz = new StringBuilder();
		private String executable = @".\External\dot.exe";
		private String output = @".\External\" + Guid.NewGuid().ToString();
		private bool sinError = true;
		/// <summary>
		/// Obtiene los identificadores y valores de los nodos declarados
		/// y los mete una una lista simple
		/// </summary>
		private void Depurar()
		{
			sinError = false;
			while (!tokens.EsVacio()&&(tokens.Largo()>=3))//Mientras tokens tenga algo
			{
				Token temp = (Token)tokens.VerPrimero();//Castea el nodo de la cola
				Console.WriteLine(temp);
				if (temp.lexema.Equals("Node"))//Agrega la declaración de Nodos
				{
					tokens.Desencolar();//Node
					Token id = (Token)tokens.Desencolar();//Identificador
					tokens.Desencolar();//Tipo
					Token value = (Token)tokens.Desencolar();//Etiqueta
					tnode.Agregar(new TreeNode(id.lexema, value.lexema));//Agrega el TreeNode
				}
				else//Agrega la relación entre Nodos
				{
					Token origen = (Token)tokens.Desencolar();//Obtiene el IdentificadorOrigen
					Token orden = (Token)tokens.Desencolar();//Obtiene el IZQ o DER
					Token destino = (Token)tokens.Desencolar();//Obtiene el IdentificadorDestino
															   //Separa las ramas en izq y en der
					if (orden.id.Equals("IZQ"))
					{
						TreeNode from = BuscarTNode(origen.lexema);
						TreeNode to = BuscarTNode(destino.lexema);
						from.left = to;
						to.EsRaiz(false);
						//Console.WriteLine("{0}->{1}.{2}",from,orden,destino.id);
						//if (!String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to)) lnode.Encolar((char)34 + from + (char)34 + "->" + (char)34 + to + (char)34);//"
					}
					if (orden.id.Equals("DER"))
					{
						TreeNode from = BuscarTNode(origen.lexema);
						TreeNode to = BuscarTNode(destino.lexema);
						from.right = to;
						to.EsRaiz(false);
						//Console.WriteLine("{0}->{1}.{2}", from, orden, destino.id);
						//if (!String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to)) rnode.Encolar((char)34 + from + (char)34 + "->" + (char)34 + to + (char)34);//"
					}
				}
				sinError = true;
			}
		}
		/// <summary>
		/// Construye la instrucción que utilizar DOT
		/// </summary>
		private void ConstruirInstruccion()
		{
			bodyGraphviz.Append("digraph G {"+Environment.NewLine);
			bodyGraphviz.Append("graph[ dpi = 300 ]" + Environment.NewLine);
			bodyGraphviz.Append(ConstruirArbol(Raiz()));
			bodyGraphviz.Append("}");
		}
		/// <summary>
		/// Construye el Arbol de Grafos
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		private String ConstruirArbol(TreeNode node)
		{
			StringBuilder b = new StringBuilder();
			if (node.left != null)
			{
				b.AppendFormat(" \"{0}\" -> \"{1}\" {2}", node.Value(), node.left.Value(),Environment.NewLine);
				b.Append(ConstruirArbol(node.left));
			}
			if (node.right != null)
			{
				b.AppendFormat(" \"{0}\" -> \"{1}\" {2}", node.Value(), node.right.Value(), Environment.NewLine);
				b.Append(ConstruirArbol(node.right));
			}
			return b.ToString();
		}
		/// <summary>
		/// Devuelve la Raiz del Grafo
		/// </summary>
		/// <returns></returns>
		private TreeNode Raiz()
		{
			Node aux = tnode.inicio;
			while (aux!=null)
			{
				TreeNode tree = (TreeNode)aux.objeto;
				if (tree.EsRaiz())
				{
					return tree;
				}
				aux = aux.siguiente;
			}
			return null;
		}
		/// <summary>
		/// Dado un id, busca en la lista de TreeNode's y devuelve un value si hay coincidencia
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private TreeNode BuscarTNode(String id)
		{
			Node aux = tnode.inicio;
			while (aux != null)
			{
				TreeNode tree = (TreeNode)aux.objeto;
				if (tree.Id().Equals(id))
				{
					return tree;
				}
				aux = aux.siguiente;
			}
			return null;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="tokens"></param>
		/// <returns></returns>
		public Result Graficar(Queue tokens)
		{
			this.tokens = tokens;//Copia los tokens de entrada
			Depurar();//Depura los tokens, creando los TreeNode y enlazándolos entre sí
			if (sinError)
			{
				ConstruirInstruccion();//Construye un String con las instrucciones para pasarlas al DOT de Grapvhiz
				String dot = bodyGraphviz.ToString();//Obtiene el String construido

				File.WriteAllText(output, dot);//Construye un archivo en la direccion output, con el contenido dot

				System.Diagnostics.Process process = new System.Diagnostics.Process();

				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;

				process.StartInfo.FileName = executable;
				process.StartInfo.Arguments = String.Format(@"{0} -Tjpg -O", output);

				process.Start();
				process.WaitForExit();

				File.Delete(output);//Elimina el archivo output

				return new Result("Graficar", Path.GetFileName(output) + ".jpg");
			}
			return null;
		}
	}
}

