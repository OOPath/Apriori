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
using ExportExtension.Model;

namespace ExportExtension
{
	/// <summary>
	/// Description of AprioriExtension.
	/// </summary>
	public static class AprioriExtension
	{
		public static IList<Item> ToApriori(IList<Item> items)
		{
			List<Item> itemSet = new List<Item>();
			
			
			
			return itemSet;
		}
		
		public static IList<Item> GetFirstItemSet(IList<Item> items)
		{
			List<Item> itemSet = new List<Item>();
			
			int minimumSupport = Convert.ToInt32(items.First().Data);
			string EOF = items.Last().Data;
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
			
			itemSet = freqItems.ToList();
			
			return itemSet;
		}
	}
}
