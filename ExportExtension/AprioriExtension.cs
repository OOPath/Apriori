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
using System.Linq.Dynamic;
using ExportExtension.Model;

namespace ExportExtension
{
	/// <summary>
	/// Description of AprioriExtension.
	/// </summary>
	public static partial class AprioriExtension
	{
		private static IList<Item> firstItemSet;
		private static IList<Item> itemSet;
		
		public static IList<Item> ToApriori(this IList<Item> items, EnumSource source, int minimumSupport)
		{		
			firstItemSet = items.GetFirstItemSet(source, minimumSupport);
			itemSet = firstItemSet;
			
			#region old
			bool hasItemSet;
			
			int count = 1;
			while (count < firstItemSet.Count) 
			{
				var beginItemSet = firstItemSet.Take(count);
//				foreach (var f in beginItemSet) {
//					Console.WriteLine("First: {0}", f);
//				}
				var nextItemSet = firstItemSet.Skip(count);
//				foreach (var w in nextItemSet) {
//					Console.WriteLine("Word: {0}", w);
//				}
				
				string beginLambda = "s => ";
				string extendedLambda = string.Empty;
				string firstExtendedLambda = beginLambda + string.Format("s.Data.Contains(\"{0}\")", 
									beginItemSet.Skip(count-1).Take(1).FirstOrDefault());
				
				int i = 0;
				foreach (var begin in beginItemSet)
				{
					beginLambda += i > 0 ? " && " : string.Empty;
					beginLambda += string.Format("s.Data.Contains(\"{0}\")", begin.Data);
					i++;
				}
				
				foreach (var next in nextItemSet)
				{
					extendedLambda = beginLambda + string.Format(" && s.Data.Contains(\"{0}\")", next.Data);
					var nextLambda = firstExtendedLambda + string.Format(" && s.Data.Contains(\"{0}\")", next.Data);
		
					next.TransactionSupport = items.AsQueryable().Where(nextLambda).Count();
					hasItemSet = next.TransactionSupport >= minimumSupport;
					if (hasItemSet)
					{
						AddFreq(beginItemSet.Skip(count-1).Take(1), next, itemSet);
					}
					
					next.TransactionSupport = items.AsQueryable().Where(extendedLambda).Count();
					hasItemSet = next.TransactionSupport >= minimumSupport;
					if (hasItemSet)
					{
						AddFreq(beginItemSet, next, itemSet);
					}
					
				}
				count++;
			}
			#endregion
			
			return itemSet;
		}
		
		#region added old
		private static void AddFreq(IEnumerable<Item> begin, Item next, IList<Item> itemSet)
		{
			string str = string.Empty;
			foreach (var item in begin)
			{
				str += item.Data + " ";
			}
			str += next.Data;
			itemSet.Add(new Item{ 
			          	Data = str, 
			          	TransactionSupport = next.TransactionSupport,
                        MinimumSupport = next.MinimumSupport
			          });
		}
		#endregion
		
		#region Recursive
//		private static IList<Item> AprioriRecursive(IList<Item> itemSet)
//		{
//			
//					
//			return this.itemSet;
//		}
//		
//		private static int GetDepth(Item parent)
//		{
//			int depth = 1;
//			if (parent != null) {
//				var data = parent.Data.Split(' ');
//				depth += data.Length;
//			}
//			return depth;
//		}
		#endregion
		
		public static IList<Item> GetFirstItemSet(this IList<Item> items, EnumSource source, int minimumSupport)
		{
			switch (source) {
				case EnumSource.Character:
					return GetCharFirstItemSet(items);
					
				case EnumSource.IBM:
					return GetIBMFirstItemSet(items, minimumSupport);
					
				default:
					throw new Exception("Invalid value for EnumSource");
			}
		}
		
		
	}
}
