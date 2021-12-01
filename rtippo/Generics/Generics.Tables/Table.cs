using System;

using System.Collections.Generic; 

namespace Generics.Tables
{
    public class Table<TRows, TColumns, TItems>
    {
        private readonly TItems[,] items = new TItems[1000, 1000];
        private readonly Dictionary<TRows, int> rows = new Dictionary<TRows, int>();
        private int rowsCounter;
        private readonly Dictionary<TColumns, int> columns = new Dictionary<TColumns, int>();
        private int columnsCounter;

        public struct Counts
        {
            public int Param_Count;
            public int Count()
            {
                return Param_Count;
            }
        }

        private Counts rowsCount;
        public Counts Rows => rowsCount;
        private Counts columnsCount;
        public Counts Columns => columnsCount;

        private bool RowContains(TRows rowItem)
        {
            if (rows.ContainsKey(rowItem)) return false;
            rows[rowItem] = rowsCounter;
            ++rowsCounter;
            return true;
        }

        public void AddRow(TRows rowItem)
        {
            if (RowContains(rowItem)) ++rowsCount.Param_Count;
        }

        private bool ColumnContains(TColumns columnItem)
        {
            if (columns.ContainsKey(columnItem)) return false;
            columns[columnItem] = columnsCounter;
            ++columnsCounter;
            return true;
        }

        public void AddColumn(TColumns columnItem)
        {
            if (ColumnContains(columnItem)) ++columnsCount.Param_Count;
        }

        public Accessor<TRows, TColumns, TItems> Open => new Accessor<TRows, TColumns, TItems>(this, true);

        public Accessor<TRows, TColumns, TItems> Existed => new Accessor<TRows, TColumns, TItems>(this, false);

        public class Accessor<TRows, TColumns, TItems>
        {
            private bool open;
            private Table<TRows, TColumns, TItems> owner;

            public Accessor(Table<TRows, TColumns, TItems> owner, bool open)
            {
                this.open = open;
                this.owner = owner;
            }

            private bool AddRowsColumnsIfNeeded(TRows row, TColumns column)
            {
                var rowAdded = owner.RowContains(row);
                var columnAdded = owner.ColumnContains(column);
                return rowAdded || columnAdded;
            }

            public TItems this[TRows row, TColumns column]
            {
                get
                {
                    if (open && AddRowsColumnsIfNeeded(row, column)) 
                        owner.items[owner.rows[row], owner.columns[column]] = default;

                    try
                    {
                        return owner.items[owner.rows[row], owner.columns[column]];
                    }

                    catch (Exception)
                    {
                        throw new ArgumentException();
                    }
                }

                set
                {
                    if (open)
                    {
                        owner.AddRow(row);
                        owner.AddColumn(column);
                    }

                    try
                    {
                        owner.items[owner.rows[row], owner.columns[column]] = value;
                    }

                    catch (Exception)
                    {
                        throw new ArgumentException();
                    }
                }
            }
        }
    }
}