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
using System.Threading.Tasks;
using ExportExtension.Model;

namespace ExportExtension
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
		/// <returns>dataSet</returns>
		public static IList<Item> GetCharDataSet(string fileLocation)
		{
			IList<Item> dataSet = new List<Item>();
			string line = string.Empty;
			
			Task.Run(async() => {
						using(TextReader file = File.OpenText(fileLocation))
					    {        
					        while((line = await file.ReadLineAsync()) != null)
					        {
					        	dataSet.Add(new Item{Data = line});
					        }
					    }
			         });
			return dataSet;
		}
		
		/// <summary>
		/// TranID      ItemID
		/// 1     1      7
		/// 1     1      127
		/// 2     2      0
		/// 2     2      52
		/// </summary>
		/// <param name="fileLocation"></param>
		/// <returns></returns>
		public static IList<Item> GetIBMDataSet(string fileLocation)
		{
			IList<Item> dataSet = new List<Item>();
			string line = string.Empty;
			
			Task.Run(async() => {
						using(TextReader file = File.OpenText(fileLocation))
					    {        
					        while((line = await file.ReadLineAsync()) != null)
					        {
					        	string[] str = line.Trim().Split(' ');
					        	dataSet.Add(new Item{
					        	            	TransactionId = str[0],
					        	            	Data = str[str.Length - 1]
					        	            });
					        }
					    }
			         });
			
			/*
			 * Transpose to 
			 * TranID      ItemID
			 * 1           7 127
			 * 2           0 52
			 * 
			 * */
			var transpose = from item in dataSet
				group item by item.TransactionId into g
				select new {TransID = g.Key, Items = g};
	
			IList<Item> itemList = new List<Item>();
			var list = transpose.ToList();
			list.AsParallel().ForAll(x => {
				Item item = new Item();
				item.TransactionId = x.TransID;
				foreach (var e in x.Items)
				{
					item.Data += e.Data + " ";
				}
				itemList.Add(item);
			});
							
			return itemList;
		}
	}
}
