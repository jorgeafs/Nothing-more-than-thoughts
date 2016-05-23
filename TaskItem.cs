using System;
using SQLite;

namespace wherever
{
	public class TaskItem
	{
		public TaskItem ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Nombre { get; set; }

		public string Descripcion { get; set; }
		
		public string Situacion { get; set;}
		
		public bool Hecho { get; set; }
	}
}
