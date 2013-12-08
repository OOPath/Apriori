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
	public static partial class AprioriExtension
	{
		private IList<Item> firstItemSet;
		private IList<Item> items;
		private List<Item> itemSet;
		
		public static IList<Item> ToApriori(this IList<Item> items, EnumSource source)
		{		
			this.firstItemSet = items.GetFirstItemSet(source);
			this.itemSet = firstItemSet;
			this.items = items;
			this.itemSet = AprioriRecursive(this.itemSet);
			
			return this.itemSet;
		}
		
		private static IList<Item> AprioriRecursive(IList<Item> itemSet)
		{
			
					
			return this.itemSet;
		}
		
		private static int GetDepth(Item parent)
		{
			int depth = 1;
			if (parent != null) {
				var data = parent.Data.Split(' ');
				depth += data.Length;
			}
			return depth;
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
		
		
	}
}
