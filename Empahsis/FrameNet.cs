using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Empahsis
{
	class FrameNet
	{
		public FrameNet()
		{
			SQLiteConnection sqlite_conn;
			sqlite_conn = new SQLiteConnection("Data Source='dicts/framenet.db'; Version = 3;");
			try
			{
				sqlite_conn.Open();
				SQLiteDataReader sqlite_datareader;
				SQLiteCommand sqlite_cmd;
				sqlite_cmd = sqlite_conn.CreateCommand();
				//sqlite_cmd.CommandText = "SELECT * FROM FE WHERE CoreType = 'Core'";
				sqlite_cmd.CommandText = "SELECT * FROM FE WHERE Name='event'";

				sqlite_datareader = sqlite_cmd.ExecuteReader();
				while (sqlite_datareader.Read())
				{
					string myreader = sqlite_datareader.ToString();
					//string myreader = sqlite_datareader.GetString(0);
					Console.WriteLine(myreader);
				}
				sqlite_conn.Close();
			}
			catch (Exception)
			{
			}
		}
	}
}
