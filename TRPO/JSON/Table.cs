using System;
using System.Collections.Generic;
using System.Text;

namespace JSON
{
    [Serializable]
    internal class TableWrapper
    {
        public Table table;
    } 

    [Serializable]
    internal class Table
    {
        public List<string> columnNames;
        public List<string> columnTypes;
        public object[] columnUnits;
        public object[][] rows;
    }
}

