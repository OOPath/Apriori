/*
 * Created by SharpDevelop.
 * User: OOPath
 * Date: 12/7/2013
 * Time: 1:58 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextConsole
{
	/// <summary>
	/// Description of ItemSetGeneration.
	/// </summary>
	public class ItemSetFactory
	{
		/// <summary>
		/// Example: 
		/// 2
		/// cab
		/// bac
		/// __
		/// </summary>
		/// <param name="fileLocation"></param>
		/// <returns></returns>
		public static async Task<IList<ItemSet>> GetItemSet(string fileLocation)
		{
			List<ItemSet> list = new List<ItemSet>();
			string line = string.Empty;
			
			using(TextReader file = File.OpenText(fileLocation))
		    {        
		        while((line = await file.ReadLineAsync()) != null)
		        {
		        	list.Add(new ItemSet{Item = line});
		        }
		    }
			return list;
		}
		
		public static async Task<IList<ItemSet>> GetFirstItemSet(IList<ItemSet> itemSet)
		{
			List<ItemSet> list = new List<ItemSet>();
			
			
			
			return list;
		}
	}
}
