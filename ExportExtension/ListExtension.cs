/*
 * Created by SharpDevelop.
 * User: OOPath
 * Date: 12/7/2013
 * Time: 9:23 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExportExtension.Model;
using Newtonsoft.Json;

namespace ExportExtension
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public static class ListExtension
	{
		public static string ToJson(IList<Item> itemSet)
		{
			return JsonConvert.ToString(itemSet);
		}
	}
}