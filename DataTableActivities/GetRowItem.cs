using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.Composition;

namespace DataTableActivities
{
    public class GetRowItem : CodeActivity
    {
        [RequiredArgument, Category("Input")]
        public InArgument<DataRow> Row
        {
            get;
            set;
        }

        [OverloadGroup("ColumnIndex"), RequiredArgument, Category("Input")]
        public InArgument<int> ColumnIndex
        {
            get;
            set;
        }

        [OverloadGroup("ColumnName"), RequiredArgument, Category("Input")]
        public InArgument<string> ColumnName
        {
            get;
            set;
        }

        [OverloadGroup("Column"), RequiredArgument, Category("Input")]
        public InArgument<DataColumn> ColumnObject
        {
            get;
            set;
        }

        [Category("Output")]
        public OutArgument OutputValue
        {
            get;
            set;
        }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            Type argumentType = typeof(object);
            if (this.OutputValue != null)
            {
                argumentType = this.OutputValue.ArgumentType;
            }
            RuntimeArgument argument = new RuntimeArgument("Value", argumentType, ArgumentDirection.Out);
            metadata.Bind(this.OutputValue, argument);
            metadata.AddArgument(argument);
        }

        protected override void Execute(CodeActivityContext context)
        {
            if (this.OutputValue == null || this.OutputValue.Expression == null)
            {
                return;
            }
            object obj = null;
            DataRow dataRow = this.Row.Get(context);
            DataColumn dataColumn = this.ColumnObject.Get(context);
            string text = this.ColumnName.Get(context);
            int columnIndex = this.ColumnIndex.Get(context);
            if (dataColumn != null)
            {
                obj = dataRow[dataColumn];
            }
            else if (text != null)
            {
                obj = dataRow[text];
            }
            else
            {
                obj = dataRow[columnIndex];
            }
            if (obj == DBNull.Value)
            {
                this.OutputValue.Set(context, null);
                return;
            }
            try
            {
                this.OutputValue.Set(context, obj);
            }
            catch (InvalidOperationException)
            {
                if (this.OutputValue != null)
                {
                    this.OutputValue.Set(context, TypeDescriptor.GetConverter(this.OutputValue.ArgumentType).ConvertFrom(obj));
                }
            }
        }
    }
}
