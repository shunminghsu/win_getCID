using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCSIApplication
{
    class DeviceInfo
    {
        public List<string> getDeviceList(List<string> _devices)
        {
            try
            {
                List<string> diskName = new List<string>();//DiskName

                ManagementObjectSearcher DiskDrive_Searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
                ManagementObjectCollection DiskDrive_Collection = DiskDrive_Searcher.Get();

                foreach (ManagementObject wmi_DiskDrive in DiskDrive_Collection)
                {
                    if (wmi_DiskDrive["Size"] != null)
                        if (Convert.ToInt64(wmi_DiskDrive["Size"].ToString()) > 10)
                            if (!(wmi_DiskDrive["MediaType"].ToString().ToLower().Contains("fixed hard disk media")))
                            {
                                _devices.Add(wmi_DiskDrive["DeviceID"].ToString());
                                diskName.Add("Disk" + wmi_DiskDrive["DeviceID"].ToString().Substring(wmi_DiskDrive["DeviceID"].ToString().Length - 1) + "(" + FindDiskName(wmi_DiskDrive["DeviceID"].ToString()) + ")");//ex: Disk2(I:)
                                string log = DateTime.Now + " - DeviceID = " + wmi_DiskDrive["DeviceID"].ToString() + " \r\n";
                                EventLog.Write(log);
                            }
                                
                }
                return diskName;
            }
            catch (ManagementException f)
            {
                List<string> diskName = new List<string>();//DiskName
                string log = DateTime.Now + " -Fail getDeviceList " + f.Message + "\r\n";
                EventLog.Write(log);
                MessageBox.Show("Error while querying for WMI data: " + f.Message);
                return diskName;
            }
        }

        public static string FindDiskName(string DeviceID)   // insert  "\\\\.\\PHYSICALDRIVE0" return C:
        {
            try
            {
                ManagementObjectSearcher DiskDriveToDiskPartition_Searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDriveToDiskPartition");
                ManagementObjectSearcher LogicalDiskToPartition_Searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_LogicalDiskToPartition");
                ManagementObjectCollection DiskDrive_Collection = DiskDriveToDiskPartition_Searcher.Get();
                ManagementObjectCollection LogicalDisk_Collection = LogicalDiskToPartition_Searcher.Get();
                foreach (ManagementObject wmi_DriveToVolume in DiskDrive_Collection)
                {
                    string targetPhysicalDrive = wmi_DriveToVolume["Antecedent"].ToString();
                    string targetVolume = wmi_DriveToVolume["Dependent"] == null ? "null" : wmi_DriveToVolume["Dependent"].ToString();
                    int subStringIndex = targetPhysicalDrive.IndexOf("PHYSICALDRIVE");
                    if (DeviceID.Substring(4) == targetPhysicalDrive.Substring(subStringIndex, targetPhysicalDrive.Length - subStringIndex - 1))
                    {
                        foreach (ManagementObject wmi_Partition in LogicalDisk_Collection)
                        {
                            string targetPartitionAntecedent = wmi_Partition["Antecedent"] == null ? "No Detect" : wmi_Partition["Antecedent"].ToString();
                            if (targetVolume == targetPartitionAntecedent)
                            {
                                subStringIndex = wmi_Partition["Dependent"].ToString().IndexOf("DeviceID=");
                                if (subStringIndex > 0)
                                {
                                    string PartitionNumberID = wmi_Partition["Dependent"].ToString().Substring(subStringIndex + 10, wmi_Partition["Dependent"].ToString().Length - subStringIndex - 11);
                                    return PartitionNumberID;
                                }
                                else
                                    return "";
                            }
                            else if (targetPartitionAntecedent == "No Detect")
                                MessageBox.Show("Please reinsert " + DeviceID);
                        }
                    }
                }
            }
            catch
            { MessageBox.Show(Marshal.GetLastWin32Error().ToString()); }

            return "";
        }

        public IntPtr getDeviceHandle(string id)
        {
            return Kernel32.CreateFile(
                id,
                Kernel32.GENERIC_READ | Kernel32.GENERIC_WRITE,
                Kernel32.FILE_SHARE_READ | Kernel32.FILE_SHARE_WRITE,
                IntPtr.Zero,
                Kernel32.OPEN_EXISTING,
                Kernel32.FILE_FLAG_OVERLAPPED,
                IntPtr.Zero);
        }
       
    }
}
