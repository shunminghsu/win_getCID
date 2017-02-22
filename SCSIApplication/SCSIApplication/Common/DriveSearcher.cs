using System;
using System.Management;


namespace SCSIApplication.Common
{
    class DriveSearcher
    {
        public static string[] GetIDs()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            ManagementObjectCollection drives = searcher.Get();
            int size = drives.Count;
            string[] ids = new string[32];
            int count = 0;
            foreach (ManagementObject drive in drives)
            {
                string id = drive["DeviceID"].ToString();
                if (CheckIfSystemVolume(id)) continue;
                ids[count++] = id;
            }
            return ids;
        }

        // FIXME
        private static bool CheckIfSystemVolume(string DeviceID)
        {
            ManagementObjectSearcher DiskDriveToDiskPartition_Searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDriveToDiskPartition");
            ManagementObjectSearcher LogicalDiskToPartition_Searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_LogicalDiskToPartition");
            ManagementObjectCollection DiskDrive_Collection = DiskDriveToDiskPartition_Searcher.Get();
            ManagementObjectCollection LogicalDisk_Collection = LogicalDiskToPartition_Searcher.Get();
            foreach (ManagementObject wmi_DriveToVolume in DiskDrive_Collection)
            {
                string targetPhysicalDrive = wmi_DriveToVolume["Antecedent"].ToString();
                string targetVolume = wmi_DriveToVolume["Dependent"].ToString();
                int subStringIndex = targetPhysicalDrive.IndexOf("PHYSICALDRIVE");
                if (DeviceID.Substring(4) == targetPhysicalDrive.Substring(subStringIndex, targetPhysicalDrive.Length - subStringIndex - 1))
                {
                    foreach (ManagementObject wmi_Partition in LogicalDisk_Collection)
                    {
                        if (targetVolume == wmi_Partition["Antecedent"].ToString())
                        {
                            subStringIndex = wmi_Partition["Dependent"].ToString().IndexOf("DeviceID=");
                            string PartitionNumberID = wmi_Partition["Dependent"].ToString().Substring(subStringIndex + 10, wmi_Partition["Dependent"].ToString().Length - subStringIndex - 11);
                            if (PartitionNumberID == "C:")
                                return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
