/*
 * Created by SharpDevelop.
 * User: OOPath
 * Date: 12/13/2013
 * Time: 2:28 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using NUnit.Framework;
using ExportExtension.Model;

namespace ExportExtension.Tests
{
	
	[TestFixture]
	public class AprioriExtensionTest
	{
		[Test]
		public void GetFile()
		{
			IList<Item> result = ItemSetFactory.GetCharDataSet(@"C:\Users\mzkapoo\Documents\SharpDevelop Projects\Apriori\ExportExtension.Tests\data\input1.txt");
			Assert.IsNotEmpty(result, "Result is Empty");
			foreach (var element in result) {
				Console.WriteLine(element.Data);
			}
			Console.WriteLine("-------------------------------------------");
			IList<Item> first = result.GetFirstItemSet(EnumSource.Character, 0);
			foreach (var element in first) {
				Console.WriteLine(element.Data);
			}
			Console.WriteLine("-------------------------------------------");
			IList<Item> apriori = result.ToApriori(EnumSource.Character, 0);
			
		}
	}
}
