using Microsoft.Win32;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace ch22yaml.Data
{
    public class MainViewModel:ViewModelBase
    {
        public ObservableCollection<ChannelViewModel> ChannelList { get; set; }

        private List<ChannelViewModel> GRList { get; set; }
        private List<ChannelViewModel> BSList { get; set; }
        private List<ChannelViewModel> CSList { get; set; }
        public MainViewModel()
        {
            ChannelList = new ObservableCollection<ChannelViewModel>();
            GRList = new List<ChannelViewModel>();
            BSList = new List<ChannelViewModel>();
            CSList = new List<ChannelViewModel>();
        }
        public ICommand OpenCH2 { get => 
                new CustonCommand(new Action(() => {
                    var mdlg = new OpenFileDialog();
                    mdlg.CheckFileExists = true;
                    mdlg.Multiselect = false;
                    mdlg.Filter = "Tvtest channel file(*.ch2)|*.ch2|All file(*.*)|*.*";
                    if (mdlg.ShowDialog() == true)
                    {
                        foreach (var item in Ch2.ReadChe2File(mdlg.FileName))
                        {
                            switch (item.Type)
                            {
                                case CHType.GR:
                                    if (!GRList.Any(x => x.ServiceID == item.ServiceID))
                                    {
                                        GRList.Add(new ChannelViewModel(item));
                                    }
                                    break;
                                case CHType.BS:
                                    if (!BSList.Any(x => x.ServiceID == item.ServiceID))
                                    {
                                        BSList.Add(new ChannelViewModel(item));
                                    }
                                    break;
                                case CHType.CS:
                                    if (!CSList.Any(x => x.ServiceID == item.ServiceID))
                                    {
                                        CSList.Add(new ChannelViewModel(item));
                                    }
                                    break;
                                default: break;
                            }
                        }
                        GRList.Sort((x, y) => x.Channel == y.Channel ? 0 : x.Channel.CompareTo(y.Channel));
                        BSList.Sort((x, y) => x.Channel == y.Channel ? 0 : x.Channel.CompareTo(y.Channel));
                        CSList.Sort((x, y) => x.Channel == y.Channel ? 0 : x.Channel.CompareTo(y.Channel));
                        ChannelList = new ObservableCollection<ChannelViewModel>( GRList.Concat(BSList).Concat(CSList));
                    }
                    else return;
                }));
        }

        public ICommand SaveYaml { get =>
                new CustonCommand(new Action(() => {
                    
                    if (GRList.Count <= 0 || BSList.Count<=0 || CSList.Count <= 0)
                    {
                        var tmp = (GRList.Count <= 0 ? "[GR]" : "") + (BSList.Count <= 0 ? "[BS]" : "") + (CSList.Count <= 0 ? "[CS]" : "");
                        MessageBox.Show($"You have not fully import all broadcast types!\n please add following type:\n {tmp}","Internal check",
                            MessageBoxButton.OK,MessageBoxImage.Exclamation);
                        return;
                    } 
                    var mdlg = new SaveFileDialog();
                    mdlg.Filter = "Mirakurun Yaml(*.yml)|*.yml|All file(*.*)|*.*";
                    var defPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), @".Mirakurun");
                    //var defPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    mdlg.InitialDirectory = defPath;
                    mdlg.FileName = "channels.yml";
                    if(mdlg.ShowDialog() == true)
                    {
                        MirakurunChannel.ToYamlFile(this.ChannelList?.Select(x => x.GetMirakurunChannel()),mdlg.FileName);
                    }
                }));

        }
    }
}
