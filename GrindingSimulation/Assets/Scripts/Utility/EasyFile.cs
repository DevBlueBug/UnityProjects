using UnityEngine;
namespace Utility
{
	public class EasyFile
	{
		public static string Load (string path)
		{
			try{
				string content = "";
				using(var file =new System.IO.StreamReader(path)){
					content = file.ReadToEnd();
				}
				return content;
			}
			catch{
				return null;
			}
		}
		
		public static bool Save (string path, string content)
		{
			try{
				using(var file =new System.IO.StreamWriter(path)){
					file.Write(content);
					//content = file.ReadToEnd();
				}
				return true;
			}
			catch{
				return false;
			}
		}
	}
}

