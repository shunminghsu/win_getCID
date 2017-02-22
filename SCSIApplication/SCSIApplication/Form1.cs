using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SCSIApplication.Common;
using System.Collections;

namespace SCSIApplication
{
    public partial class Form1 : Form
    {
        private DriveWatcher _watcher;
        private DeviceInfo _deviceInfo;
        private List<string> _devices;//Win32_DiskDrive List
        public const int CID_LEN = 15;
        private byte[] buf;
        private IntPtr _handle;
        private Boolean isDetail = false;
        private byte[] mid;
        private byte[] oid;
        private byte[] pnm;
        private byte[] prv;
        private byte[] psn;
        private byte[] mdt;
        private byte[] crc;
        private int deviceIndex = 0;

        

        public Form1()
        {
            InitializeComponent();

            initializeParameters();
        }

        private void initializeParameters()
        {
            _deviceInfo = new DeviceInfo();
            _watcher = new DriveWatcher(onDriveCreate, onDriveDelete);
            buf = new byte[CID_LEN];
            mid = new byte[1];
            oid = new byte[2];
            pnm = new byte[5];
            prv = new byte[1];
            psn = new byte[4];

            mdt = new byte[2];
            crc = new byte[1];
        }

        private void onDriveCreate(Object sender, EventArrivedEventArgs e)
        {
            string logString = DateTime.Now + " - New Device Create \r\n";
            EventLog.Write(logString);
            _devices = new List<string>();
            List<string> diskName = _deviceInfo.getDeviceList(_devices);
            comboBoxRefresh(diskName);
        }

        private void onDriveDelete(Object sender, EventArrivedEventArgs e)
        {
            string logString = DateTime.Now + " - Device Delete \r\n";
            EventLog.Write(logString);
            _devices = new List<string>();
            List<string> diskName = _deviceInfo.getDeviceList(_devices);
            comboBoxRefresh(diskName);
        }

        private void comboBoxRefresh(List<string> diskName)
        {
            if (diskName.Count >= 1)
                UIHandler.setComboBoxValue(comboBox_deviceList, diskName);
            else
            {
                UIHandler.cleanComboBoxValue(comboBox_deviceList);
            }

        }

        public void reset()
        {
            Array.Clear(buf, 0, buf.Length);
            Array.Clear(mid, 0, mid.Length);
            Array.Clear(oid, 0, oid.Length);
            Array.Clear(pnm, 0, pnm.Length);
            Array.Clear(prv, 0, prv.Length);
            Array.Clear(psn, 0, psn.Length);
            Array.Clear(mdt, 0, mdt.Length);
            Array.Clear(crc, 0, crc.Length);

            UIHandler.SetTextBoxText(textBox_cid, "");
            UIHandler.SetTextBoxText(textBox_detail, "");
            button_copy.Visible = false;

        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            string logString = DateTime.Now + @" - Button Refresh Click " + "\r\n";
            EventLog.Write(logString);
            reset();
            UIHandler.cleanComboBoxValue(comboBox_deviceList);
            _devices = new List<string>();
            List<string> diskName = _deviceInfo.getDeviceList(_devices);
            comboBoxRefresh(diskName);
        }

        private void btn_get_info_Click(object sender, EventArgs e)
        {
            btn_get_info.Enabled = false;
            if (comboBox_deviceList.Text != null && comboBox_deviceList.Text != "")
            {
                try
                {
                    string compareString = comboBox_deviceList.Text.Substring(4, 1);
                    string selectDevice = getSelectDevice(_devices, compareString);
                    string logString = DateTime.Now + @" - Button Get Smart Info Click, Select Device =  " + selectDevice + "\r\n";
                    EventLog.Write(logString);
                    _handle = new IntPtr();
                    _handle = _deviceInfo.getDeviceHandle(selectDevice);
                    if (-1 == _handle.ToInt64())
                    {
                        btn_get_info.Enabled = true;
                        string errString = DateTime.Now + @" -Fail Kernel32.CreateFile = -1" + "\r\n";
                        EventLog.Write(errString);
                    }
                    else
                    {
                        buf = getCID(_handle);
                        btn_get_info.Enabled = true;
                        if (buf.Length > 0)
                        {
                            string string_CID = ByteArrayToString(buf);
                            string _temp = "";
                            for (int i = 0; i < string_CID.Length; i++)
                            {
                                _temp += string_CID[i];
                                if (i % 2 == 1 && (i != string_CID.Length - 1))
                                    _temp += " ";
                                if (i % 8 == 7 && (i != string_CID.Length - 1))
                                    _temp += " ";
                            }
                            textBox_cid.Text = _temp;

                            Array.Copy(buf, 0, mid, 0, mid.Length);
                            Array.Copy(buf, 1, oid, 0, oid.Length);
                            Array.Copy(buf, 3, pnm, 0, pnm.Length);
                            Array.Copy(buf, 8, prv, 0, prv.Length);
                            Array.Copy(buf, 9, psn, 0, psn.Length);
                            Array.Copy(buf, 13, mdt, 0, mdt.Length);

                            showDetail();
                            button_copy.Visible = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    exceptionResponse(DateTime.Now + @" -Fail btn_get_info_Click " + ex.ToString() + "\r\n");
                }
            }
            else
            {
                btn_get_info.Enabled = true;
                string logString = DateTime.Now + @" - comboBox_deviceList.Text == null || comboBox_deviceList.Text " + "\r\n";
                EventLog.Write(logString);
            }
        }

        private string getSelectDevice(List<string> _devices, string compareString)
        {
            string selectDevice = "";
            foreach (string deviceName in _devices)
            {
                if (deviceName.Substring(deviceName.Length - 1) == compareString)
                {
                    selectDevice = deviceName;
                    break;
                }
                else
                    selectDevice = "";
            }
            return selectDevice;
        }

        private void exceptionResponse(string log)
        {
            EventLog.Write(log);
            btn_get_info.Enabled = true;
        }

        public byte[] getCID(IntPtr handle)
        {
            byte[] value_RCA = new byte[3];
            byte[] value_CID = new byte[CID_LEN];

            try
            {
                value_RCA = CmdSet.send_CMD_1(handle);
                CmdSet.send_CMD_2(handle);
                value_CID = CmdSet.send_CMD_3(handle, value_RCA);
                CmdSet.send_CMD_4(handle);

            }
            catch (Exception ex)
            {
                exceptionResponse(DateTime.Now + @" -Fail getCID " + ex.ToString() + "\r\n");
            }

            return value_CID;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_cid.Text.Length > 0)
            {
                string s = textBox_cid.Text.Replace(" ", String.Empty);
                System.Windows.Forms.Clipboard.SetText(s);
            }
        }

        private void showDetail()
        {
            textBox_detail.Text = "Manufacture ID: 0x" + ByteArrayToString(mid) + Environment.NewLine +
                                "OEM/Application ID: 0x" + ByteArrayToString(oid) + Environment.NewLine +
                                "Product name: 0x" + ByteArrayToString(pnm) + Environment.NewLine +
                                "Product revision: 0x" + ByteArrayToString(prv) + Environment.NewLine +
                                "Product serial number: 0x" + ByteArrayToString(psn) + Environment.NewLine +
                                "Manufacturing date: " + getMTDString(mdt);
        }

        private string getMTDString(byte[] _mdt)
        {
            string d = ByteArrayToString(_mdt);
            if (d.Length != 4)
                return "error";
            string hex_year = d.Substring(1,2);
            int year = 2000 + Convert.ToInt32(hex_year, 16);

            string hex_mon = d.Substring(3, 1);
            int mon = Convert.ToInt32(hex_mon, 16);
            string month;
            switch (mon)
            {
                case 1:
                    month = "January";
                    break;
                case 2:
                    month = "February";
                    break;
                case 3:
                    month = "March";
                    break;
                case 4:
                    month = "April";
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "June";
                    break;
                case 7:
                    month = "July";
                    break;
                case 8:
                    month = "August";
                    break;
                case 9:
                    month = "September";
                    break;
                case 10:
                    month = "October";
                    break;
                case 11:
                    month = "November";
                    break;
                case 12:
                    month = "December";
                    break;
                default:
                    month = "unknown";
                    break;
            }
            return month + " " + year;
        }

        private void comboBox_deviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deviceIndex != comboBox_deviceList.SelectedIndex)
            {
                reset();
            }
            deviceIndex = comboBox_deviceList.SelectedIndex;
        }
    }
}
