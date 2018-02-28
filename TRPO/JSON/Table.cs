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
        public string[] columnNames;
        public string[] columnTypes;
        public object[] columnUnits;
        public object[][] rows;
    }
}

