/*
 * Created by SharpDevelop.
 * User: OOPath
 * Date: 12/7/2013
 * Time: 9:30 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqHierarchy;
using ExportExtension.Model;

namespace ExportExtension
{
	/// <summary>
	/// Description of AprioriExtension.
	/// </summary>
	public static class AprioriExtension
	{
		public static IList<Item> ToApriori(this IList<Item> items, EnumSource source)
		{
			List<Item> itemSet = new List<Item>();
			
			var firstItemSet = items.GetFirstItemSet(source);
			
			return itemSet;
		}
		
		public static IList<Item> GetFirstItemSet(this IList<Item> items, EnumSource source)
		{
			switch (source) {
				case EnumSource.Character:
					return GetCharFirstItemSet(items);
					
				case EnumSource.IBM:
					return GetIBMFirstItemSet(items);
					
				default:
					throw new Exception("Invalid value for EnumSource");
			}
		}
		
		private static IList<Item> GetIBMFirstItemSet(IList<Item> items)
		{
			List<Item> itemSet = new List<Item>();
			
			int minimumSupport = items.First().MinimumSupport;
			
			
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
