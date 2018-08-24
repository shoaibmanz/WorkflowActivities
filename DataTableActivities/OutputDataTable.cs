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
        [Browsable(false)]
        public Char Delimitator
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

        public OutputDataTable()
        {
            this.Delimitator = ',';
        }


        protected override void Execute(CodeActivityContext context)
        {

            DataTable dataTable = this.DataTable.Get(context);
            
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
                    stringBuilder.Append(Delimitator);
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
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    object obj = row.ItemArray[i];
                    string text = (obj == null) ? string.Empty : obj.ToString();
                    if (!flag)
                    {
                        stringBuilder.Append(Delimitator);
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
