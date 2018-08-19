using System;
using System.Activities;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace DataTableActivities
{

    public sealed class OutputDataTable : CodeActivity
    {
        [Category("Delimitator")]
        public InArgument<Nullable<char>> Delimitator
        {
            get;
            set;
        }

        [RequiredArgument]
        [Category("Input")]
        public InArgument<DataTable> DataTable
        {
            get;
            set;
        }

        [Category("Output")]
        public OutArgument<string> Result
        {
            get;
            set;
        }


        protected override void Execute(CodeActivityContext context)
        {

            DataTable dataTable = this.DataTable.Get(context);
            Nullable<char> Delim = Delimitator.Get(context);

            

            char deliminator = new char();

            if (Delim.HasValue)
            {
                deliminator = Delim.Value;
            } else
            {
                deliminator = ',';
            }

            if (dataTable == null)
            {
                return;
            }

            StringBuilder stringBuilder = new StringBuilder();

            bool flag = true;
            
            foreach (DataColumn Column in dataTable.Columns)
            {
                string text = Column.ColumnName.ToString();
                if (!flag)
                {
                    stringBuilder.Append(deliminator);
                }
                if (text.IndexOfAny(new char[]
                {
                        '"',
                        ','
                }) != -1)
                {
                    text = string.Format("\"{0}\"", text.Replace("\"", "\"\""));
                }
                stringBuilder.Append(text);
                flag = false;
            }
            stringBuilder.Append(Environment.NewLine);                    

            foreach (DataRow row in dataTable.Rows)
            {
                flag = true;
                object[] itemArray = row.ItemArray;
                for (int i = 0; i < itemArray.Length; i++)
                {
                    object obj = itemArray[i];
                    string text = (obj == null) ? string.Empty : obj.ToString();
                    if (!flag)
                    {
                        stringBuilder.Append(deliminator);
                    }
                    if (text.IndexOfAny(new char[]
                    {
                    '"',
                    ','
                    }) != -1)
                    {
                        text = string.Format("\"{0}\"", text.Replace("\"", "\"\""));
                    }
                    stringBuilder.Append(text);
                    flag = false;
                }

                stringBuilder.Append(Environment.NewLine);
            }

            Result.Set(context, stringBuilder.ToString());
        }
    }
}
