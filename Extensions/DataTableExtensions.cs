using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TestApp.Extensions
{
    public static class DataTableExtensions
    {
		public static string ToCsv(this DataTable dataTable)
	    {
		    var sbData = new StringBuilder();
		    if (dataTable.Columns.Count == 0)
		    {
			    return null;
		    }
		    foreach (var column2 in dataTable.Columns)
		    {
			    if (column2 == null)
			    {
				    sbData.Append(CultureInfo.CurrentCulture.TextInfo.ListSeparator);
			    }
			    else
			    {
				    sbData.Append(column2 + CultureInfo.CurrentCulture.TextInfo.ListSeparator);
			    }
		    }
		    sbData.Replace(CultureInfo.CurrentCulture.TextInfo.ListSeparator, Environment.NewLine, sbData.Length - 1, 1);
		    foreach (DataRow row in dataTable.Rows)
		    {
			    var itemArray = row.ItemArray;
			    foreach (var column in itemArray)
			    {
				    if (column == null)
				    {
					    sbData.Append(CultureInfo.CurrentCulture.TextInfo.ListSeparator);
				    }
				    else
				    {
					    sbData.Append(column + CultureInfo.CurrentCulture.TextInfo.ListSeparator);
				    }
			    }
			    sbData.Replace(CultureInfo.CurrentCulture.TextInfo.ListSeparator, Environment.NewLine, sbData.Length - 1, 1);
		    }
		    return sbData.ToString();
	    }

	    public static void PrepareColumns(this DataTable dataTable, IEnumerable<string> columns, Dictionary<string, string> addColumnsWithDefaultValues)
	    {
	        columns.ToList().ForEach(i => dataTable.Columns.Add(i, typeof(string)));
		    foreach (var addColumnsWithDefaultValue in addColumnsWithDefaultValues)
		    {
			    dataTable.Columns.Add(new DataColumn(addColumnsWithDefaultValue.Key)
			    {
			        DataType = typeof(string),
			        DefaultValue = addColumnsWithDefaultValue.Value
			    });
		    }
	    }
	}
}
