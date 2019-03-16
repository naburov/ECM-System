using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace СЭД_ЭК
{
    /// <summary>
    /// Логика взаимодействия для extDoc.xaml
    /// </summary>
    public partial class extDoc : UserControl
    {
        public event DocEvent openDoc;
        public event DocEvent deleteDoc;
        public event DocEvent chooseDoc;
        public EventHandler addedDoc;

        public TabControl tbControl { get; set; }
        public TabItem item { get; set; }
        public extDoc()
        {
            InitializeComponent();
            addedDoc += OnDocAdd;
        }

        public int docId { get; set; }
        public string physicalAddress;

        public string PhysicalAddress
        {
            get => physicalAddress;
            set { physicalAddress = value; addedDoc?.Invoke(this, new EventArgs()); }
        }

        public void OnDocAdd(object sender, EventArgs e)
        {
            if (PhysicalAddress == "")
            {
                lblFileStatus.Foreground=new SolidColorBrush(Colors.DarkRed);
                lblFileStatus.Content = "Файл не прикреплен";
            }
            else { lblFileStatus.Content = "Файл прикрпелен"; lblFileStatus.Foreground=new SolidColorBrush(Color.FromRgb(4,60,93));}
        }

        public string docName { get => lblDocName.Content.ToString(); set => lblDocName.Content = value; }
        public string status { get => lblStatus.Content.ToString(); set => lblStatus.Content = value; }
        public string planDate { get => lblPlanDate.Content.ToString(); set => lblPlanDate.Content = value.ToString(); }
        public string factDate { get => lblFactDate.Content.ToString(); set => lblFactDate.Content = value.ToString(); }
        public string description { get => new TextRange(rtxtDescription.Document.ContentStart,
            rtxtDescription.Document.ContentEnd).Text;
            set { rtxtDescription.Document.Blocks.Clear();
                rtxtDescription.AppendText(value); } }
        public string empName { get => lblEmployeeName.Content.ToString(); set => lblEmployeeName.Content = value; }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnChooseDoc_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            chooseDoc?.Invoke(this, new DocEventArgs()
            {
                physicalAddress = this.PhysicalAddress,
                Id = this.docId,
            });
        }

        private void BtnOpen_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            openDoc?.Invoke(this, new DocEventArgs()
            {
                physicalAddress = this.PhysicalAddress,
                Id = this.docId,
            });
        }

        private void BtnDeleteDoc_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            deleteDoc?.Invoke(this, new DocEventArgs()
            {
                physicalAddress = this.PhysicalAddress,
                Id = this.docId,
            });
        }
    }
}
