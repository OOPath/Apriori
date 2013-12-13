/*
 * Created by SharpDevelop.
 * User: OOPath
 * Date: 12/8/2013
 * Time: 1:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExportExtension.Model;

namespace ExportExtension
{
	/// <summary>
	/// Description of AprioriMethod.
	/// </summary>
	public static partial class AprioriExtension
	{
		/// <summary>
		/// TransID     ItemID : Data
		/// 1           7 127
		/// 2           0 52
		/// </summary>
		/// <param name="items"></param>
		/// <param name="minimumSupport"></param>
		/// <returns></returns>
		private static IList<Item> GetIBMFirstItemSet(IList<Item> items, int minimumSupport)
		{
			List<Item> itemSet;
			List<string> itemIDs = new List<string>();
			
			items.AsParallel().ForAll(x => { 
			                          	string[] str = x.Data.Split(' ');
			                          	itemIDs.AddRange(str);
			                          });
			try {
				var freqItems = 
				from c in itemIDs.AsParallel()
				group c by c into g
				let transSupport = g.Count()
				let data = g.Key
				where transSupport >= minimumSupport
				orderby data.Length, data
				select new Item {
					Data = data,
					MinimumSupport = minimumSupport, 
					TransactionSupport = transSupport};
			
				itemSet = freqItems.ToList();
			} catch (Exception) {
				itemSet = new List<Item>();
			}
			
			return itemSet;
		}
		
		private static IList<Item> GetCharFirstItemSet(IList<Item> items)
		{
			List<Item> itemSet = new List<Item>();
			
			int minimumSupport = Convert.ToInt32(items.First().Data);
			string EOF = items.Last().Data;
//			Console.WriteLine("EOF = {0}", EOF);
			List<char> chList = new List<char>();
			foreach (var item in items.Skip(1)) {
				if (!item.Data.Equals(EOF)) {
					chList.AddRange(item.Data.ToCharArray());
				}
			}
			var freqItems = 
				from c in chList
				group c by c into g
				let transSupport = g.Count()
				let data = g.Key.ToString()
				where transSupport >= minimumSupport
				orderby data
				select new Item {
					Data = data,
					MinimumSupport = minimumSupport, 
					TransactionSupport = transSupport};
			
//			Console.WriteLine("Freq count = {0}", freqItems.Count());
			
			itemSet = freqItems.ToList();
			
			if (itemSet.Count == 0) {
				itemSet.Add(new Item{ Data = EOF });
			}
			
			return itemSet;
		}
	}
}
