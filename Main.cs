using System.Windows.Forms;
using System.IO;
using KAPPA_OK.Compilator;
using KAPPA_OK.Files;
using System;

namespace KAPPA_OK
{
	public partial class main : Form
	{
		private int reporteTokens = 0;
		private int funcion = 0;
		private string fileDirectoryFocus;
		private bool textChange = false;
		private SaveFileDialog saveDialog;
		private OpenFileDialog openDialog;
		/// <summary>
		/// Constructor :v
		/// </summary>
		public main()
		{
			InitializeComponent();
			//this.
		}
		/// <summary>
		/// Especifica la función a realizar al 'Analizar'
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnStart_Click(object sender, System.EventArgs e)
		{
			this.txtbox.Enabled = false;
			//Instancia un Scanner
			Scanner scanner = new Scanner();
			//Aloja una referencia a la cola retornada
			KAPPA_OK.List.Queue cola = scanner.Escanear(txtbox.Text, txtbox);
			//Salida de funciones
			if (cola.primero!=null) new Files.Files().GenerarYAbrir("Salida"+ ++funcion, new Files.Files().EscribirHTML(cola), "html", true );
			//Reporte de tokens
			if (scanner.token.primero!=null) new Files.Files().GenerarYAbrir("ReporteToken" + ++reporteTokens, new Files.Files().ReportarTokens(scanner.token), "html", true);
			this.txtbox.Enabled = true;
		}
		/// <summary>
		/// Abre un archivo de texto (*.txt), lo lea y pega en contenido en el txtbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AbrirMenuItem_Click(object sender, System.EventArgs e)
		{
			openDialog = new OpenFileDialog
			{
				//Establece el directorio inicial
				InitialDirectory = "C:\\Users\\otzoy\\Desktop",
				//Se crea y agrega un filtro para los archivos
				Filter = "Documento de Texto (*.txt) |*.txt",
				FilterIndex = 0,
				//Deja el directorio en el directorio en el que se cerro la 
				//última vez que se abrio el cuadro de dialogo durante 
				//el tiempo de ejecuacion
				RestoreDirectory = true,
				//Evita que se seleccione mas de un archivo
				Multiselect = false
			};
			//Hace visible el dialogo
			//Si se presiona OK se selecciona un archivo se ejecuta lo del if
			//
			if (openDialog.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(openDialog.FileName))
			{
				//Llena el cuadro de texto con lo que se encuentra en el archivo *.if
				txtbox.Text = new Files.Files().Leer(openDialog.FileName);
				//Hace focus al file :v
				fileDirectoryFocus = openDialog.FileName;
				//Coloca el nombre del form
				this.Text = "Kappa-ΩK - " + Path.GetFileNameWithoutExtension(fileDirectoryFocus);
			}
		}
		/// <summary>
		/// Sale de la aplicación
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SalirToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			Exit();
		}
		/// <summary>
		/// Devuelve información de la Aplicación
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AboutMenuItem_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("201602782 - Sergio Fernando Otzoy Gonzalez", "Lenguajes Formales y de Programación - B+");
		}
		/// <summary>
		/// Guarda un archivo sin lugar en el Disco
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GuardarCMenuItem_Click(object sender, System.EventArgs e)
		{
			this.SaveFileLike();
			
		}
		/// <summary>
		/// Guarda un archivo con lugar en el Disco
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GuardarMenuItem_Click(object sender, System.EventArgs e)
		{
			if (!String.IsNullOrEmpty(fileDirectoryFocus))
			{
				this.SaveFile();
			}
			else
			{
				this.SaveFileLike();
			}
		}
		/// <summary>
		/// Determina si el texto ha cambiado
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtbox_TextChanged(object sender, System.EventArgs e)
		{
			if(!this.Text.Substring(this.Text.Length-1,1).Equals("*"))
			{
				this.Text += "*";
				textChange = true;
			}
		}
		/// <summary>
		/// Evita que la opción 'cerrar' de la barra de título
		/// cierre el formulario y ejecuta las acciones de Exit()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void main_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Exit();
		}
		/// <summary>
		/// Guarda un nuevo archivo en el Disco
		/// </summary>
		private void SaveFileLike()
		{
			//Despliega el dialogo para guardar un archivo
			this.saveDialog = new SaveFileDialog()
			{
				//Establece el directorio inicial
				InitialDirectory = "C:\\Users\\otzoy\\Desktop",
				//Se crea y agrega un filtro para los archivos
				Filter = "Documento de Texto (*.txt) |*.txt",
				FilterIndex = 0,
				//Deja el directorio en el directorio en el que se cerro la 
				//última vez que se abrio el cuadro de dialogo durante 
				//el tiempo de ejecuacion
				RestoreDirectory = true,
				//Muestra el dialogo
			};
			if (saveDialog.ShowDialog() == DialogResult.OK)
			{
				this.fileDirectoryFocus = saveDialog.FileName;
				if (saveDialog.FileName != "")
				{
					StreamWriter outputFile = null;
					try
					{
						using (outputFile = new StreamWriter(saveDialog.FileName))
						{
							outputFile.Write(txtbox.Text);
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
								this.Text = "Kappa-ΩK - " + Path.GetFileNameWithoutExtension(fileDirectoryFocus);
								this.textChange = false;
							}
						}
						catch (Exception)
						{
						}
					}
				}

			}
		}
		/// <summary>
		/// Guarda el archivo que se aloja en el Disco
		/// </summary>
		private void SaveFile()
		{
			if (!String.IsNullOrEmpty(this.fileDirectoryFocus))
			{
				StreamWriter outputFile = null;
				try
				{
					using (outputFile = new StreamWriter(fileDirectoryFocus))
					{
						outputFile.Write(txtbox.Text);
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
							this.Text = "Kappa-ΩK - " + Path.GetFileNameWithoutExtension(fileDirectoryFocus);
							this.textChange = false;
						}
					}
					catch (Exception)
					{
					}
				}
			}
		}
		/// <summary>
		/// Especifica las acciones cuando se intenta cerrar el formulario
		/// </summary>
		private void Exit()
		{
			//Determina si el texto ha cambiado de alguna forma
			if (this.textChange)
			{
				String title = (!String.IsNullOrEmpty(fileDirectoryFocus) ? Path.GetFileNameWithoutExtension(this.fileDirectoryFocus) : "Untitled");
				DialogResult result1 = MessageBox.Show(null, "¿Desea guardar los cambios en " + title + "?", "KAPPA-OK", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
				//Determina la acción a realizar según el botón que haya presionado el usuario
				switch (result1)
				{
					case DialogResult.Yes:
						if (!String.IsNullOrEmpty(fileDirectoryFocus))
						{
							SaveFile();//Si hay algo previamente guardado ira a SaveFile
						}
						else
						{
							SaveFileLike();//No hay nada previamente guardadso ira a SaveFileLike
						}
						//Cerrará el Form
						Dispose();
						Close();
						break;
					case DialogResult.Cancel:
						//No hará nada
						break;
					case DialogResult.No:
						//Cerrará el Form
						Dispose();
						Close();
						break;
				}
			}
			else
			{
				//Cerrará el Form
				Dispose();
				Close();
			}
		}
	}
}
