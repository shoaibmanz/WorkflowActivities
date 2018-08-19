using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace DataTableActivities
{
    public enum ArgumentType
    {
        ColumnName,
        ColumnIndex
    }

    public class Pair<T1, T2>
    {
        public T1 First { get; set; }
        public T2 Second { get; set; }

        public Pair()
        {
        }

        public Pair(T1 first, T2 second)
        {
            this.First = first;
            this.Second = second;
        }
    }

    public sealed class RemoveDuplicateValues : CodeActivity
    {



        [Category("Input")]
        [RequiredArgument]
        public InOutArgument<DataTable> DataTable
        {
            get;
            set;
        }

        [Browsable(false)]
        public List<Pair<ArgumentType, string>> Columns
        {
            get;
            set;
        }

        public RemoveDuplicateValues()
        {
            Columns = new List<Pair<ArgumentType, string>>();
            this.DisplayName = "Remove Duplicate Values";
        }

        protected override void Execute(CodeActivityContext context)
        {
            DataTable dataTable = DataTable.Get(context);

            foreach (Pair<ArgumentType, string> tuple in Columns)
            {
                if (tuple.First == ArgumentType.ColumnName)
                {
                    dataTable = RemoveDuplicateRows(dataTable, tuple.Second);
                }
                else
                {
                    int index = 0;
                    if (Int32.TryParse(tuple.Second, out index))
                    {
                        dataTable = RemoveDuplicateRows(dataTable, index);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("Column {0}: Given input was not a number", tuple.Second));
                    }
                    
                }
            }

            DataTable.Set(context, dataTable);
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, int colIndex)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colIndex]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colIndex], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }
    }
}
