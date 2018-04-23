// Developer Express Code Central Example:
// How to Create a Custom Column Chooser
// 
// This example shows how to create a custom standalone Column Chooser, and display
// it within LayoutControl.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3991

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core;

namespace DXGrid_CustomColumnChooser.SL {
    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();
            grid.ItemsSource = IssueList.GetData();
            InitCustomColumnChooser(tableView);

        }
        void InitCustomColumnChooser(GridViewBase view) {
            columnChooser.View = view;
            view.ColumnChooserFactory = new CustomColumnChooser(columnChooser);
            view.ShowColumnChooser();
        }
    }
    public class CustomColumnChooser : IColumnChooser, IColumnChooserFactory {
        readonly CustomColumnChooserControl columnChooserControl;
        public CustomColumnChooser(CustomColumnChooserControl columnChooserControl) {
            this.columnChooserControl = columnChooserControl;
        }
        #region IColumnChooser Members
        void IColumnChooser.Show() { }
        void IColumnChooser.Hide() { }
        void IColumnChooser.ApplyState(IColumnChooserState state) { }
        void IColumnChooser.SaveState(IColumnChooserState state) { }
        void IColumnChooser.Destroy() { }
        UIElement IColumnChooser.TopContainer {
            get {
                return columnChooserControl.ColunmChooserControl;
            }
        }
        #endregion

        #region IColumnChooserFactory Members

        IColumnChooser IColumnChooserFactory.Create(DevExpress.Xpf.Core.WPFCompatibility.SLControl owner) {
            return this;
        }

        #endregion
    }
    public class CustomColumnChooserControl : Control {
        public static readonly DependencyProperty ViewProperty;
        static CustomColumnChooserControl() {
            ViewProperty = DependencyProperty.Register("View", typeof(GridViewBase),
                typeof(CustomColumnChooserControl), new PropertyMetadata(null));
        }
        public CustomColumnChooserControl() {
            DefaultStyleKey = typeof(CustomColumnChooserControl);
        }
        public GridViewBase View {
            get { return (GridViewBase)GetValue(ViewProperty); }
            set { SetValue(ViewProperty, value); }
        }
        internal ColumnChooserControl ColunmChooserControl { get; private set; }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            ColunmChooserControl = (ColumnChooserControl)GetTemplateChild(
                "PART_ColumnChooserControl");
        }
    }
    public class IssueList {
        static public List<IssueDataObject> GetData() {
            List<IssueDataObject> data = new List<IssueDataObject>();
            data.Add(new IssueDataObject() {
                IssueName = "Transaction History",
                IssueType = "Bug", IsPrivate = true
            });
            data.Add(new IssueDataObject() {
                IssueName = "Ledger: Inconsistency",
                IssueType = "Bug", IsPrivate = false
            });
            data.Add(new IssueDataObject() {
                IssueName = "Data Import",
                IssueType = "Request", IsPrivate = false
            });
            data.Add(new IssueDataObject() {
                IssueName = "Data Archiving",
                IssueType = "Request", IsPrivate = true
            });
            return data;
        }
    }

    public class IssueDataObject {
        public string IssueName { get; set; }
        public string IssueType { get; set; }
        public bool IsPrivate { get; set; }
    }
}
