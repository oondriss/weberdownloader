﻿using System;
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
		    StringBuilder sbData = new StringBuilder();
		    if (dataTable.Columns.Count == 0)
		    {
			    return null;
		    }
		    foreach (object column2 in dataTable.Columns)
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
			    object[] itemArray = row.ItemArray;
			    foreach (object column in itemArray)
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
		    columns.ToList().ForEach(delegate (string i)
		    {
			    dataTable.Columns.Add(i, typeof(string));
		    });
		    foreach (KeyValuePair<string, string> addColumnsWithDefaultValue in addColumnsWithDefaultValues)
		    {
			    DataColumn dataColumn2 = new DataColumn(addColumnsWithDefaultValue.Key)
			    {
				    DataType = typeof(string),
				    DefaultValue = addColumnsWithDefaultValue.Value
			    };
			    DataColumn dataColumn = dataColumn2;
			    dataTable.Columns.Add(dataColumn);
		    }
	    }
	}
}
