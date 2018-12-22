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

namespace ch22yaml
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new Data.MainViewModel();
        }

        //public void testMethod()
        //{
        //    var list = Data.Ch2.ReadChe2File(@"C:\RC\TVTest\BonDriver_PX_W3PE_S1.ch2");
        //    var chlist = list.Select(x => x.ToMirakurunChannel());
        //    Data.MirakurunChannel.ToYamlFile(chlist);
        //    Console.WriteLine(list.Count());
        //}
    }
}
