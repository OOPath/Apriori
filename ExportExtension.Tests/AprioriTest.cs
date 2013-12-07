/*
 * Created by SharpDevelop.
 * User: OOPath
 * Date: 12/7/2013
 * Time: 10:51 AM
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
using NUnit.Framework;
using ExportExtension.Model;

namespace ExportExtension.Tests
{
	
	[TestFixture]
	public class AprioriTest
	{
		private IList<Item> items;
		
		[Test]
		public void GetFile()
		{
			List<Item> list = new List<Item>();
			string fileLocation = @"C:\Users\mzkapoo\Documents\SharpDevelop Projects\" +
									  @"Apriori\ExportExtension.Tests\data\input1.txt";
		
			// Read data from input file
			string line = string.Empty;
			Task.Run(async() => {
				using(TextReader file = File.OpenText(fileLocation))
			    {        
			        while((line = await file.ReadLineAsync()) != null)
			        {
			        	list.Add(new Item{ Data = line });
			        }
			    }
			});
			items = list;
//			list.ForEach(x => Console.WriteLine(x.Data));
		}
		
		[Test]
		public void GetFirstItemSet()
		{
			GetFile();
			var list = items.GetFirstItemSet(EnumSource.Character);
			((List<Item>)list).ForEach(x => Console.WriteLine(x.Data));
		}
	}
}
