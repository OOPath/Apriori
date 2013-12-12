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
		public static IList<Item> ToApriori(this IList<Item> items, EnumSource source, int minimumSupport)
		{		
			IList<Item> firstItemSet = items.GetFirstItemSet(source, minimumSupport);
			IList<Item> itemSet = firstItemSet;
			
			#region old
			bool hasItemSet = false;
			
			int count = 1;
			while (count < firstItemSet.Count) 
			{
				var beginItemSet = firstItemSet.Take(count);
//				foreach (var f in beginItemSet) {
//					Console.WriteLine("First: {0}", f);
//				}
				var nextItemSet = firstItemSet.Skip(count);
//				foreach (var w in nextItemSet) {
//					Console.WriteLine("Word: {0}", w.Data);
//				}
				
				string beginLambda = string.Empty; // s => 
				string nextLambda = string.Empty;
				string extendedLambda = string.Empty;
				string firstExtendedLambda = beginLambda + string.Format("Data.Contains(\"{0}\")", 
									beginItemSet.Skip(count-1).Take(1).FirstOrDefault().Data);
//				Console.WriteLine("firstExtendedLambda: {0}", firstExtendedLambda);
				
				int i = 0;
				foreach (var begin in beginItemSet)
				{
					beginLambda += i > 0 ? " && " : string.Empty;
					beginLambda += string.Format("Data.Contains(\"{0}\")", begin.Data);
					i++;
				}
//				Console.WriteLine("beginLambda: {0}", beginLambda);
				
				foreach (var next in nextItemSet.ToList())
				{
					extendedLambda = beginLambda + string.Format(" && Data.Contains(\"{0}\")", next.Data);
					nextLambda = firstExtendedLambda + string.Format(" && Data.Contains(\"{0}\")", next.Data);
//					Console.WriteLine("extendedLambda: {0}", extendedLambda);
//					Console.WriteLine("nextLambda: {0}", nextLambda);
					
					next.TransactionSupport = items.AsQueryable().Where(nextLambda).Count();
//					Console.WriteLine("\n\t transactionSupport: {0}", next.TransactionSupport);
					hasItemSet = next.TransactionSupport >= minimumSupport;
//					Console.WriteLine("\n\t hasItemSet: {0}", hasItemSet);
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
			if (!itemSet.Any(x => x.Data.Equals(str))) {
				itemSet.Add(new Item{ 
			          	Data = str, 
			          	TransactionSupport = next.TransactionSupport,
                        MinimumSupport = next.MinimumSupport
			          });
			}
			
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
