using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace RegexPower
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PatternRepository _pattern;
        bool check = true;
        public MainWindow()
        {
            _pattern = new PatternRepository();

            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            rtb.SelectAll();
            rtb.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
            rtb.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);


            Regex reg = new Regex(this.matchBox.Text, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            TextPointer position = rtb.Document.ContentStart;
            List<TextRange> ranges = new List<TextRange>();
            while (position != null)
            {
                if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string text = position.GetTextInRun(LogicalDirection.Forward);
                    var matchs = reg.Matches(text);

                    foreach (Match match in matchs)
                    {

                        TextPointer start = position.GetPositionAtOffset(match.Index);
                        TextPointer end = start.GetPositionAtOffset(this.matchBox.Text.Length);

                        TextRange textrange = new TextRange(start, end);
                        ranges.Add(textrange);
                    }
                }
                position = position.GetNextContextPosition(LogicalDirection.Forward);
            }


            foreach (TextRange range in ranges)
            {
                range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));
                range.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile1 = new OpenFileDialog();
            if (openFile1.ShowDialog() == true && openFile1.FileName.Length > 0)
            {
                this.rtb.Document.Blocks.Clear();
                this.rtb.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(openFile1.FileName)))); ;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (IsValidEmail(GetString(this.rtb)) == true)
            {
                MessageBox.Show(string.Format("Email is valid:{0}", GetString(this.rtb)));
            }
            else
            {
                MessageBox.Show(string.Format("Email is  not valid:{0}", GetString(this.rtb)));
            }
        }

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"\A[a-z0-9]+([-._][a-z0-9]+)*@([a-z0-9]+(-[a-z0-9]+)*\.)+[a-z]{2,4}\z")
                && Regex.IsMatch(email, @"^(?=.{1,64}@.{4,64}$)(?=.{6,100}$).*");
        }

        string GetString(RichTextBox rtb)
        {
            return new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text.Trim();
        }

        private static bool IsValidRegex(string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return false;

            try
            {
                Regex.Match("", pattern);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string pattern = this.ownPatter.Text;
            if (IsValidRegex(pattern))
            {
                rtb.SelectAll();
                rtb.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
                rtb.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);


                Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                TextPointer position = rtb.Document.ContentStart;
                List<TextRange> ranges = new List<TextRange>();
                while (position != null)
                {
                    if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                    {
                        string text = position.GetTextInRun(LogicalDirection.Forward);
                        var matchs = reg.Matches(text);

                        foreach (Match match in matchs)
                        {

                            TextPointer start = position.GetPositionAtOffset(match.Index);
                            TextPointer end = start.GetPositionAtOffset(match.Length);

                            TextRange textrange = new TextRange(start, end);
                            ranges.Add(textrange);
                        }
                    }
                    position = position.GetNextContextPosition(LogicalDirection.Forward);
                }


                foreach (TextRange range in ranges)
                {
                    range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));
                    range.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                }
            }
            else
            {
                this.rtb.SelectAll();
                this.rtb.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
                this.rtb.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
        }
    }
}
