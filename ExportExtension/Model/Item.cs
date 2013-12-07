/*
 * Created by SharpDevelop.
 * User: OOPath
 * Date: 12/7/2013
 * Time: 9:54 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ExportExtension.Model
{
	/// <summary>
	/// Description of Item.
	/// </summary>
	public class Item
	{
		[SQLite.PrimaryKey, SQLite.AutoIncrement]
		public int Id { get; set; }
		public MongoDB.Bson.ObjectId ObjectId { get; set; }
		public int MinimumSupport { get; set; }
		public int TransactionSupport { get; set; }
		public string TransactionId { get; set; }
		public string Data { get; set; }
		public Item Parent { get; set; }
	}
}
