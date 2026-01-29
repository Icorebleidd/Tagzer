using System.Windows;
using System.Windows.Controls;
using Tagzer.Core;

namespace Tagzer
{
    public partial class CheckinCommentToolWindowControl : UserControl
    {
        public CheckinCommentToolWindowControl()
        {
            InitializeComponent();
            LoadTags();

            btnCopy.Click += BtnCopy_Click;

            cmbIsMerge.SelectionChanged += (_, __) => UpdatePreview();
            cmbProduct.SelectionChanged += (_, __) => ProductChanged();
            cmbType.SelectionChanged += (_, __) => UpdatePreview();

            cmbCustomer.SelectionChanged += (_, __) => UpdatePreview();
            cmbCustomer.KeyUp += (_, __) => UpdatePreview();
            cmbCustomer.LostFocus += (_, __) => UpdatePreview();

            txtTopic.TextChanged += (_, __) => UpdatePreview();
            txtDescription.TextChanged += (_, __) => UpdatePreview();

            chkIsRollback.Checked += (_, __) => UpdatePreview();
            chkIsRollback.Unchecked += (_, __) => UpdatePreview();
        }

        private void LoadTags()
        {
            cmbIsMerge.ItemsSource = TagDefinitions.IsMergeOptions;
            cmbProduct.ItemsSource = TagDefinitions.ProductOptions;
            cmbCustomer.ItemsSource = TagDefinitions.CustomerOptions;
            cmbType.ItemsSource = TagDefinitions.TypeOptions;
        }

        private void ProductChanged()
        {
            var selectedProduct = cmbProduct.SelectedItem?.ToString() ?? cmbProduct.Text;
            if (selectedProduct == "CORE")
            {
                cmbCustomer.Text = "STD";
                cmbCustomer.IsEnabled = false;
            }
            else
            {
                cmbCustomer.IsEnabled = true;
            }
            UpdatePreview();
        }

        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            var comment = new CheckinComment
            {
                IsRollback = chkIsRollback.IsChecked == true ? "[ROLLBACK]" : "",
                IsMerge = GetComboValue(cmbIsMerge),
                Product = GetComboValue(cmbProduct),
                Customer = GetComboValue(cmbCustomer),
                Type = GetComboValue(cmbType),
                Topic = txtTopic.Text,
                Description = txtDescription.Text
            };

            if (!comment.IsValid(out string error))
            {
                MessageBox.Show(error, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Clipboard.SetText(txtOutput.Text);
        }

        private void BtnClearAll_Click(object sender, RoutedEventArgs e)
        {
            cmbIsMerge.SelectedIndex = -1;
            cmbProduct.SelectedIndex = -1;
            cmbCustomer.SelectedIndex = -1;
            cmbCustomer.Text = string.Empty;
            cmbType.SelectedIndex = -1;
            txtTopic.Clear();
            txtDescription.Clear();
            chkIsRollback.IsChecked = false;
            txtOutput.Clear();

            cmbCustomer.IsEnabled = true;
        }

        private void UpdatePreview()
        {
            var comment = new CheckinComment
            {
                IsRollback = chkIsRollback.IsChecked == true ? "[ROLLBACK]" : "",
                IsMerge = GetComboValue(cmbIsMerge),
                Product = GetComboValue(cmbProduct),
                Customer = GetComboValue(cmbCustomer),
                Type = GetComboValue(cmbType),
                Topic = txtTopic.Text,
                Description = txtDescription.Text
            };

            txtOutput.Text = comment.ToString();
        }

        private string GetComboValue(ComboBox combo)
        {
            var value = combo.SelectedItem?.ToString() ?? combo.Text;
            return string.IsNullOrWhiteSpace(value) ? "" : $"[{value}]";
        }
    }
}
