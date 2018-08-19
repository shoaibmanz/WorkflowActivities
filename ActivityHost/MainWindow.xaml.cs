using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Activities;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;
using System.ComponentModel;
using DataTableActivities;

namespace ActivityHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
    
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            // register metadata  
            (new DesignerMetadata()).Register();
            RegisterCustomMetadata();
            // add custom activity to toolbox  

            Toolbox.Categories.Add(new ToolboxCategory("Custom activities"));

            //Toolbox.Categories[1].Add(new ToolboxItemWrapper(typeof(AddDataColumn)));
            Toolbox.Categories[1].Add(new ToolboxItemWrapper(typeof(AddDataRow)));            
            Toolbox.Categories[1].Add(new ToolboxItemWrapper(typeof(BuildDataTable)));
            Toolbox.Categories[1].Add(new ToolboxItemWrapper(typeof(ClearDataTable)));
            Toolbox.Categories[1].Add(new ToolboxItemWrapper(typeof(GetRowItem)));
            Toolbox.Categories[1].Add(new ToolboxItemWrapper(typeof(OutputDataTable)));
            Toolbox.Categories[1].Add(new ToolboxItemWrapper(typeof(RemoveDataColumn)));
            Toolbox.Categories[1].Add(new ToolboxItemWrapper(typeof(RemoveDataRow)));
            Toolbox.Categories[1].Add(new ToolboxItemWrapper(typeof(RemoveDuplicateRows)));
            Toolbox.Categories[1].Add(new ToolboxItemWrapper(typeof(RemoveDuplicateValues)));
            
            // create the workflow designer  
            WorkflowDesigner wd = new WorkflowDesigner();
            wd.Load(new Sequence());
            DesignerBorder.Child = wd.View;
            PropertyBorder.Child = wd.PropertyInspectorView;

        }

        void RegisterCustomMetadata()
        {
            DataTableActivities.Designer.Metadata.RegisterAll();
        }

        //public MainWindow()
        //{
        //    InitializeComponent();

        //    // Register the metadata  
        //    RegisterMetadata();

        //    // Add the WFF Designer  
        //    AddDesigner();

        //    this.AddToolBox();
        //}

        //protected override void OnInitialized(EventArgs e)
        //{
        //    base.OnInitialized(e);
        //    // register metadata  
        //    (new DesignerMetadata()).Register();
        //    RegisterCustomMetadata();
        //    // add custom activity to toolbox  
        //    Toolbox.Categories.Add(new ToolboxCategory("Custom activities"));

        //    // create the workflow designer  
        //    WorkflowDesigner wd = new WorkflowDesigner();
        //    wd.Load(new Sequence());
        //    DesignerBorder.Child = wd.View;
        //    PropertyBorder.Child = wd.PropertyInspectorView;

        //}

        //void RegisterCustomMetadata()
        //{
        //    DataTableActivities.Designer.Metadata.RegisterAll();
        //}


        //private void AddDesigner()
        //{
        //    //Create an instance of WorkflowDesigner class.  
        //    this.wd = new WorkflowDesigner();

        //    //Place the designer canvas in the middle column of the grid.  
        //    Grid.SetColumn(this.wd.View, 1);

        //    //Load a new Sequence as default.  
        //    this.wd.Load(new Sequence());

        //    //Add the designer canvas to the grid.  
        //    grid1.Children.Add(this.wd.View);

        //    this.AddPropertyInspector();
        //}


        //private void RegisterMetadata()
        //{
        //    DesignerMetadata dm = new DesignerMetadata();

        //    dm.Register();
        //    DataTableActivities.Designer.Metadata.RegisterAll();
        //}

        //private ToolboxControl GetToolboxControl()
        //{
        //    // Create the ToolBoxControl.  
        //    ToolboxControl ctrl = new ToolboxControl();

        //    // Create a category.  
        //    ToolboxCategory category = new ToolboxCategory("category1");

        //    // Create Toolbox items.  
        //    ToolboxItemWrapper tool1 =
        //        new ToolboxItemWrapper("System.Activities.Statements.Assign",
        //        typeof(Assign).Assembly.FullName, null, "Assign");

        //    ToolboxItemWrapper tool2 = new ToolboxItemWrapper("System.Activities.Statements.Sequence",
        //        typeof(Sequence).Assembly.FullName, null, "Sequence");

        //    // Add the Toolbox items to the category.  
        //    category.Add(tool1);
        //    category.Add(tool2);

        //    // Add the category to the ToolBox control.  
        //    ctrl.Categories.Add(category);
        //    return ctrl;
        //}

        //private void AddToolBox()
        //{
        //    ToolboxControl tc = GetToolboxControl();
        //    Grid.SetColumn(tc, 0);
        //    grid1.Children.Add(tc);
        //}

        //private void AddPropertyInspector()
        //{
        //    Grid.SetColumn(wd.PropertyInspectorView, 2);
        //    grid1.Children.Add(wd.PropertyInspectorView);
        //}
    }
}
