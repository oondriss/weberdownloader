using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApp.Extensions;

namespace TestApp.Tests
{
    [TestClass]
    public class CsvExtensionsTests
    {
        [TestMethod]
        public void ToCsvTestShouldReturnNull()
        {
            var dt = new DataTable();
            var res = dt.ToCsv();
            Assert.IsNull(res);
        }

        [TestMethod]
        public void ToCsvTest2()
        {

        }

        [TestMethod]
        public void ToCsvTest()
        {
            const string firstColumn = "firstColumn";
            const string secondColumn = "secondColumn";
            const string expected =
                "firstColumn;secondColumn\r\nfirstCol 0;secondValue 0\r\nfirstCol 1;secondValue 1\r\nfirstCol 2;secondValue 2\r\nfirstCol 3;secondValue 3\r\nfirstCol 4;secondValue 4\r\n";

            var dt = new DataTable();
            dt.Columns.Add(firstColumn);
            dt.Columns.Add(secondColumn);

            for (var i = 0; i < 5; i++)
            {
                var dr = dt.NewRow();
                dr[firstColumn] = $"firstCol {i}";
                dr[secondColumn] = $"secondValue {i}";
                dt.Rows.Add(dr);
            }

            var output = dt.ToCsv();
            Assert.IsNotNull(output);
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void PrepareColumnsTest()
        {
            const string col1 = "col1";
            const string col2 = "col2";
            const string col3 = "col3";
            const string col4 = "col4";
            const string col5 = "col5";
            const string val4 = "val4";
            const string val5 = "val5";
            var cols = new List<string> {col1, col2, col3};
            
            var dt = new DataTable();
            
            var addCols = new Dictionary<string, string>
            {
                {col4, val4},
                {col5, val5}
            };

            dt.PrepareColumns(cols, addCols);
            cols.Add(col4);
            cols.Add(col5);

            for (var i = 0; i < dt.Columns.Count; i++)
            {
                Assert.AreEqual(dt.Columns[i].ColumnName, cols[i]);
                if (addCols.ContainsKey(dt.Columns[i].ColumnName))
                {
                    Assert.AreEqual(addCols[dt.Columns[i].ColumnName], dt.Columns[i].DefaultValue);
                }
            }
        }
    }
}
