using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TestApp.Extensions
{
    public static class DataTableExtensions
    {
	    public static string ToCsv(this DataTable dataTable)
	    {
		    var sbData = new StringBuilder();

		    // Only return Null if there is no structure.
		    if (dataTable.Columns.Count == 0)
			    return null;

		    foreach (var col in dataTable.Columns)
		    {
			    if (col == null)
				    sbData.Append(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator); 
			    else
				    sbData.Append(col + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
		    }

		    sbData.Replace(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator, Environment.NewLine, sbData.Length - 1, 1);

		    foreach (DataRow dr in dataTable.Rows)
		    {
			    foreach (var column in dr.ItemArray)
			    {
				    if (column == null)
					    sbData.Append(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
				    else
					    sbData.Append(column + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
			    }
			    sbData.Replace(",", Environment.NewLine, sbData.Length - 1, 1);
		    }

		    return sbData.ToString();
	    }

	    public static void AddColumns(this DataTable dataTable, IEnumerable<string> columns)
	    {
		    dataTable.Columns.AddRange(columns.Select(i => new DataColumn(i)).ToArray());
	    }
    }
}
