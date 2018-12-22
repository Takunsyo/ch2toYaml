using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using YamlDotNet.Serialization;

namespace ch22yaml.Data
{
    
    public class MirakurunChannel
    {
        public MirakurunChannel() { }

        public MirakurunChannel(Ch2 ch)
        {
            this.Name = ch.Name;
            this.Type = ch.Type;
            this.Channel = ch.Channel.ToString();
            this.ServiceID = ch.ServiceID;
            this.Space = ch.TuningSpace;
            switch (this.Type)
            {
                case CHType.BS:
                    this.Satelite = "0";
                    break;
                case CHType.CS:
                    this.Satelite = "1";
                    break;
                default:
                    this.Satelite = null;
                    break;
            }
        }
        /// <summary>
        /// Channel name for identification only.
        /// </summary>
        [YamlMember(Alias = "name",ScalarStyle = YamlDotNet.Core.ScalarStyle.Plain)]
        public string Name { get; set; }
        /// <summary>
        /// A <see cref="CHType"/> enum specify this channel's type.
        /// </summary>
        [YamlMember(Alias = "type",ScalarStyle = YamlDotNet.Core.ScalarStyle.Plain)]
        public CHType Type { get; set; }
        /// <summary>
        /// Channel ID. I don't know why they set this to a string.
        /// </summary>
        [YamlMember(Alias = "channel", ScalarStyle = YamlDotNet.Core.ScalarStyle.SingleQuoted)]
        public string Channel { get; set; }
        
        /// <summary>
        /// Specify the Service ID (=SID) integer. if not, services will scanned automatically.
        /// <para>*Optional</para>
        /// </summary>
        [YamlMember(Alias = "serviceId")]
        public ushort? ServiceID { get; set; }

        /// <summary>
        /// string for [satelite] in tuner command. 
        /// <para>*Optinal.   e.g JCSAT4A. Or, if BS "0", if CS "1".</para>
        /// </summary>
        [YamlMember(Alias = "satelite",ScalarStyle = YamlDotNet.Core.ScalarStyle.SingleQuoted)]
        public string Satelite { get; set; }

        /// <summary>
        /// [space] as tuning space number in tuner command (default: 0)
        /// <para>*Optional</para>
        /// </summary>
        [YamlMember(Alias = "space")]
        public byte Space { get; set; }
        /// <summary>
        /// Specify if this channel is disabled.
        /// <para>*Optional</para>
        /// </summary>
        [YamlMember(Alias = "isDisabled")]
        public bool? IsDisabled { get; set; } = null;

        public static void ToYamlFile(IEnumerable<MirakurunChannel> list,string filePath) 
        {
            if (list is null) return;
            if (File.Exists(filePath))
            {
                if(MessageBox.Show($"Target file : \n{filePath}\n is already exists, do you want to overwrite it?", "Attention", 
                    MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        File.SetAttributes(filePath, FileAttributes.Normal);
                        File.Delete(filePath);
                    }
                    catch
                    {
                        MessageBox.Show($"Unable to overwrite target file : \n{filePath}","ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            var builder = new SerializerBuilder();
            var serializer = builder.Build();
            
            using (var writer = File.CreateText(filePath))
            {
                serializer.Serialize(writer, list);
            }
        }
    }
}
