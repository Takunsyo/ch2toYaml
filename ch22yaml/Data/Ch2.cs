using System;
using System.Collections.Generic;
using System.Text;

namespace ch22yaml.Data
{
    /// <summary>
    /// TVRock Ch2 channel file definitions.
    /// </summary>
    public class Ch2
    {
        public string Name { get; set; }

        public byte TuningSpace { get; set; }

        public byte Channel { get; set; }

        public UInt16? RemoteNumber { get; set; }

        public int? ServiceType { get; set; }

        public UInt16 ServiceID { get; set; }

        public byte NetworkID { get; set; }

        public Int16 TSID { get; set; }

        public bool Status { get; set; }

        public CHType Type { get; set; }

        internal bool Enabled { get; set; } = true;

        /// <summary>
        /// Read a *.Ch2 file.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static IEnumerable<Ch2>ReadChe2File(string fullPath)
        {
            if (!System.IO.File.Exists(fullPath)) yield break;
            using (var reader = new System.IO.StreamReader(fullPath, Encoding.GetEncoding("Shift-JIS")))
            {
                string line;
                CHType mType = CHType.BS;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith(";"))
                    {
                        if (line.Contains(";#SPACE"))
                        {
                            switch (line)
                            {
                                case string x when x.Contains("BS"):
                                    mType = CHType.BS;
                                    break;
                                case string x when x.Contains("CS"):
                                    mType = CHType.CS;
                                    break;
                                case string x when x.Contains("UHF"):
                                    mType = CHType.GR;
                                    break;
                                default:
                                    mType = CHType.SKY;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        var tmp = line.Split(',');
                        var item = new Ch2();
                        item.Name= tmp[0];
                        item.Type = mType;
                        if (byte.TryParse(tmp[1],out var valTuningSpace))
                        {
                            item.TuningSpace = valTuningSpace;
                        }
                        if (byte.TryParse(tmp[2], out var valChannel))
                        {
                            item.Channel = valChannel;
                        }
                        if (ushort.TryParse(tmp[3], out var valRemoteNumber))
                        {
                            item.RemoteNumber = valRemoteNumber;
                        }
                        if (int.TryParse(tmp[4], out var valServiceType))
                        {
                            item.ServiceType = valServiceType;
                        }
                        if (ushort.TryParse(tmp[5], out var valServiceID))
                        {
                            item.ServiceID = valServiceID;
                        }
                        if (byte.TryParse(tmp[6], out var valNetworkID))
                        {
                            item.NetworkID = valNetworkID;
                        }
                        if (short.TryParse(tmp[7], out var valTSID))
                        {
                            item.TSID = valTSID;
                        }
                        if (int.TryParse(tmp[8], out var valStatus))
                        {
                            item.Status = valStatus != 0;
                        }
                        yield return item;
                    }
                }
            }
        }

        public MirakurunChannel ToMirakurunChannel()=> new MirakurunChannel(this);        
    }
}
