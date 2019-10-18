using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace DocumentationBuilder {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }

        private void MenuCloseButton_Click(object sender, RoutedEventArgs e) {
            System.Windows.Application.Current.Shutdown();
        }

        private void ComputationButton_Click(object sender, RoutedEventArgs e) {
            string computedText = FormatOutputText();
            this.InputBox.Text = String.Empty;
            if (PrintToScreenRadio.IsChecked == true) {
                //this.InputBox.Text = String.Empty;
                InputBox.AppendText(computedText);
            } else {
                if (PrintToFileRadio.IsChecked == true && FilePath.Text == "Filepath" || String.IsNullOrEmpty(FilePath.Text)) {
                    System.Windows.MessageBox.Show("Print to file chosen, but no file path was given", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                } else {
                    File.WriteAllText(FilePath.Text, computedText);
                }
            }
        }

        private String FormatOutputText() {
            if (LanguageDropdown.Text.Equals("C#")) {
                return ParseCSharp();
            }
            if (LanguageDropdown.Text.Equals("Java")) {
                return ParseJava();
            }
            if (LanguageDropdown.Text.Equals("Ruby")) {
                return ParseRuby();
            }
            if (LanguageDropdown.Text.Equals("Python")) {
                return ParsePython();
            }
            return "Error: Language dropdown error.";
        }

        private String ParseCSharp() {
            FormatData fd = new FormatData();
            TextFramework tf = new TextFramework(VerticalIconInput.Text, HorizontalIconInput.Text, CrossIconInput.Text, Int32.Parse(TypeWidthInput.Text), Int32.Parse(MethodWidthInput.Text));
            DocumentStripper ds = new DocumentStripper(InputBox.Text, fd, "C#");
            StringBuilder displayText = new StringBuilder();
            if (!fd.IsClassContainerDescriptionSet()) {
                displayText.Append(fd.ReturnClassContainerName());
            } else {
                displayText.Append(fd.GetClassContainerName() + " -- " + fd.GetClassContainerDescription());
                displayText.Append(Environment.NewLine + Environment.NewLine);
            }
            displayText.Append(tf.GetConstructorSummaryHeader() + Environment.NewLine);
            for (int p = 0; p < fd.ConstructorCount(); p++) {
                if (!fd.IsCommentSet(p)) {
                    displayText.Append(tf.AssembleConstructorRow(fd.GetConstructorTitle(p)));
                } else {
                    displayText.Append(tf.AssembleConstructorRow(fd.GetConstructorTitle(p) + " -- " + fd.GetConstructorComment(p)));
                }
            }
            displayText.Append(Environment.NewLine + Environment.NewLine + tf.GetMethodSummaryHeader());
            for (int r = 0; r < fd.GetFunctionCount(); r++) {
                if (String.IsNullOrEmpty(fd.GetFunctionComment(r))) {
                    displayText.Append(tf.AssembleFunctionRow(fd.GetFunctionType(r), fd.GetFunctionTitle(r)));
                } else {
                    displayText.Append(tf.AssembleFunctionRow(fd.GetFunctionType(r), fd.GetFunctionTitle(r), fd.GetFunctionComment(r)));
                }
            }
            displayText.Append(DisplayCurrentDate());
            return displayText.ToString();
        }

        private String ParseJava() {
            FormatData fd = new FormatData();
            TextFramework tf = new TextFramework(VerticalIconInput.Text, HorizontalIconInput.Text, CrossIconInput.Text, Int32.Parse(TypeWidthInput.Text), Int32.Parse(MethodWidthInput.Text));
            DocumentStripper ds = new DocumentStripper(InputBox.Text, fd, "Java");
            StringBuilder displayText = new StringBuilder();
            if (!fd.IsClassContainerDescriptionSet()) {
                displayText.Append(fd.ReturnClassContainerName());
            } else {
                displayText.Append(fd.GetClassContainerName() + " -- " + fd.GetClassContainerDescription());
                displayText.Append(Environment.NewLine + Environment.NewLine);
            }
            displayText.Append(tf.GetConstructorSummaryHeader() + Environment.NewLine);
            for (int p = 0; p < fd.ConstructorCount(); p++) {
                if (!fd.IsCommentSet(p)) {
                    displayText.Append(tf.AssembleConstructorRow(fd.GetConstructorTitle(p)));
                } else {
                    displayText.Append(tf.AssembleConstructorRow(fd.GetConstructorTitle(p) + " -- " + fd.GetConstructorComment(p)));
                }
            }
            displayText.Append(Environment.NewLine + Environment.NewLine + tf.GetMethodSummaryHeader());
            for (int r = 0; r < fd.GetFunctionCount(); r++) {
                if (String.IsNullOrEmpty(fd.GetFunctionComment(r))) {
                    displayText.Append(tf.AssembleFunctionRow(fd.GetFunctionType(r), fd.GetFunctionTitle(r)));
                } else {
                    displayText.Append(tf.AssembleFunctionRow(fd.GetFunctionType(r), fd.GetFunctionTitle(r), fd.GetFunctionComment(r)));
                }
            }
            displayText.Append(DisplayCurrentDate());
            return displayText.ToString();
        }

        private String ParseRuby() {
            FormatData fd = new FormatData();
            TextFramework tf = new TextFramework(VerticalIconInput.Text, HorizontalIconInput.Text, CrossIconInput.Text, Int32.Parse(TypeWidthInput.Text), Int32.Parse(MethodWidthInput.Text));
            DocumentStripper ds = new DocumentStripper(InputBox.Text, fd, "Ruby");
            StringBuilder displayText = new StringBuilder();


            return "Ruby";
        }

        private String ParsePython() {
            FormatData fd = new FormatData();
            TextFramework tf = new TextFramework(VerticalIconInput.Text, HorizontalIconInput.Text, CrossIconInput.Text, Int32.Parse(TypeWidthInput.Text), Int32.Parse(MethodWidthInput.Text));
            DocumentStripper ds = new DocumentStripper(InputBox.Text, fd, "Python");
            StringBuilder displayText = new StringBuilder();



            return "Python";
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private String DisplayCurrentDate() {
            return Environment.NewLine + "Created Date: " + DisplayHeaders.GetCurrentDate();
        }


        private void FilePathBrowseButton_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog();
        }

        private void FilePath_GotFocus(object sender, RoutedEventArgs e) {
            OpenFileDialog();
        }

        private void OpenFileDialog() {
            OpenFileDialog choofdlog = new OpenFileDialog();
            FilePath.Text = String.Empty;
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (!choofdlog.ShowDialog() == DialogResult.HasValue) {
                FilePath.AppendText(choofdlog.FileName);
            }
        }

        private void WidthResetButton_Click(object sender, RoutedEventArgs e) {
            VerticalIconInput.Text = String.Empty;
            VerticalIconInput.Text = "" + DisplayHeaders.GetOriginalVertIcon();
            HorizontalIconInput.Text = String.Empty;
            HorizontalIconInput.Text = "" + DisplayHeaders.GetOriginalHoriIcon();
            CrossIconInput.Text = String.Empty;
            CrossIconInput.Text = "" + DisplayHeaders.GetOriginalCrosIcon();
        }

        private void IconsResetButton_Click(object sender, RoutedEventArgs e) {
            TypeWidthInput.Text = String.Empty;
            TypeWidthInput.Text = "" + DisplayHeaders.GetOriginalTypeWidth();
            MethodWidthInput.Text = String.Empty;
            MethodWidthInput.Text = "" + DisplayHeaders.GetOriginalMethodWidth();
        }

        private void VerticalIconInput_Initialized(object sender, EventArgs e) {
            VerticalIconInput.Text = "" + DisplayHeaders.GetOriginalVertIcon();
        }

        private void HorizontalIconInput_Initialized(object sender, EventArgs e) {
            HorizontalIconInput.Text = "" + DisplayHeaders.GetOriginalHoriIcon();
        }

        private void CrossIconInput_Initialized(object sender, EventArgs e) {
            CrossIconInput.Text = "" + DisplayHeaders.GetOriginalCrosIcon();
        }

        private void TypeWidthInput_Initialized(object sender, EventArgs e) {
            TypeWidthInput.Text = "" + DisplayHeaders.GetOriginalTypeWidth();
        }

        private void MethodWidthInput_Initialized(object sender, EventArgs e) {
            MethodWidthInput.Text = "" + DisplayHeaders.GetOriginalMethodWidth();
        }

        private void VerticalIconInput_LostFocus(object sender, RoutedEventArgs e) {
            if (VerticalIconInput.Text == "") {
                VerticalIconInput.Text = "" + DisplayHeaders.GetOriginalVertIcon();
            }
        }

        private void VerticalIconInput_GotFocus(object sender, RoutedEventArgs e) {
            VerticalIconInput.Text = String.Empty;
        }

        private void HorizontalIconInput_GotFocus(object sender, RoutedEventArgs e) {
            HorizontalIconInput.Text = String.Empty;
        }

        private void CrossIconInput_GotFocus(object sender, RoutedEventArgs e) {
            CrossIconInput.Text = String.Empty;
        }

        private void HorizontalIconInput_LostFocus(object sender, RoutedEventArgs e) {
            if (HorizontalIconInput.Text == "") {
                HorizontalIconInput.Text = "" + DisplayHeaders.GetOriginalHoriIcon();
            }
        }

        private void CrossIconInput_LostFocus(object sender, RoutedEventArgs e) {
            if (CrossIconInput.Text == "") {
                CrossIconInput.Text = "" + DisplayHeaders.GetOriginalCrosIcon();
            }
        }

        private void TypeWidthInput_GotFocus(object sender, RoutedEventArgs e) {
            TypeWidthInput.Text = String.Empty;
        }

        private void MethodWidthInput_GotFocus(object sender, RoutedEventArgs e) {
            MethodWidthInput.Text = String.Empty;
        }

        private void TypeWidthInput_LostFocus(object sender, RoutedEventArgs e) {
            if (TypeWidthInput.Text == "") {
                TypeWidthInput.Text = "" + DisplayHeaders.GetOriginalTypeWidth();
            }
        }

        private void MethodWidthInput_LostFocus(object sender, RoutedEventArgs e) {
            if (MethodWidthInput.Text == "") {
                MethodWidthInput.Text = "" + DisplayHeaders.GetOriginalMethodWidth();
            }
        }

        private void InputBox_LostFocus(object sender, RoutedEventArgs e) {
            if (InputBox.Text == "") {
                InputBox.Text = "Paste in Functions you would like formatted";
            }
        }

        private void InputBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            InputBox.Text = String.Empty;
        }

        private void InputBox_GotFocus(object sender, RoutedEventArgs e) {
            if(InputBox.Text == "Paste in Functions you would like formatted") {
                InputBox.Text = String.Empty;
            }
        }

        private void MenuAboutButton_Click(object sender, RoutedEventArgs e) {
            Window1 win1 = new Window1();
            win1.ShowDialog();
        }
    }
}
