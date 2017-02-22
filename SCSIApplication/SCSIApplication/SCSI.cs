using System;
using System.IO;
using System.Runtime.InteropServices;


namespace SCSIApplication
{
    class SCSI
    {
        #region SCSI_PASS_THROUGH_DIRECT
        public struct SCSI_PASS_THROUGH_DIRECT
        {
            public ushort Length;
            public byte ScsiStatus;
            public byte PathId;
            public byte TargetId;
            public byte Lun;
            public byte Cdblength;
            public byte SenseInfoLength;
            public byte DataIn;
            public uint DataTransferLength;
            public uint TimeOutValue;
            public IntPtr DataBufferOffset;
            public uint SenseInfoOffset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] cdb;
        };
        #endregion

        #region SCSI_PASS_THROUGH_DIRECT_EX
        public struct SCSI_PASS_THROUGH_DIRECT_EX
        {
            public ushort Version;
            public ushort Length;
            public uint CdbLength;
            public byte StorAddressLength;
            public byte ScsiStatus;
            public byte SenseInfoLength;
            public byte DataDirection;
            public byte Reserved;
            public uint TimeOutValue;
            public uint StorAddressOffset;
            public uint SenseInfoOffset;
            public uint DataOutTransferLength;
            public uint DataInTransferLength;
            public IntPtr DataOutBuffer;
            public IntPtr DataInBuffer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] cdb;
        };
        #endregion

        #region SCSI_PASS_THROUGH_WITH_BUFFERS
        [StructLayout(LayoutKind.Sequential)]
        public class SCSI_PASS_THROUGH_WITH_BUFFERS
        {
            internal SCSI_PASS_THROUGH_DIRECT sptd = new SCSI_PASS_THROUGH_DIRECT();
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            internal byte[] sense;
        };
        #endregion

        #region SCSI_FLAGS
        public const uint FILE_READ_ACCESS = (0x0001);
        public const uint FILE_WRITE_ACCESS = (0x0002);
        public const uint METHOD_BUFFERED = 0;
        public const uint IOCTL_SCSI_BASE = 0x00000004;

        public static uint IOCTL_SCSI_PASS_THROUGH_DIRECT = CTL_CODE(IOCTL_SCSI_BASE, 0x0405, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS);
        public static byte SCSI_IOCTL_DATA_OUT = 0;
        public static byte SCSI_IOCTL_DATA_IN = 1;

        static uint CTL_CODE(uint DeviceType, uint Function, uint Method, uint Access)
        {
            return ((DeviceType) << 16) | ((Access) << 14) | ((Function) << 2) | (Method);
        }
        #endregion

        #region DeviceIoControl
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Boolean DeviceIoControl(
            IntPtr hFile,
            Int32 dwIoControlCode,
            IntPtr lpInBuffer,
            Int32 nInBufferSize,
            IntPtr lpOutBuffer,
            Int32 nOutBufferSize,
            out Int32 nBytesReturned,
            IntPtr lpOverlapped);
        #endregion

        public static bool Cmd(IntPtr handle, byte cdbLength, byte[] cdb, int dataLength, ref byte[] dataBuffer, byte dataInOut)
        {
            bool status;
            Int32 nBytesReturned;
            IntPtr result = IntPtr.Zero;
            int errorMessage;

            SCSI_PASS_THROUGH_WITH_BUFFERS info = new SCSI_PASS_THROUGH_WITH_BUFFERS();

            info.sptd.cdb = new byte[cdbLength];
            info.sptd.cdb = cdb;
            info.sptd.Cdblength = cdbLength;
            info.sense = new byte[32];
            info.sptd.Length = (ushort)Marshal.SizeOf(typeof(SCSI_PASS_THROUGH_DIRECT));
            info.sptd.SenseInfoOffset = (uint)Marshal.OffsetOf(typeof(SCSI_PASS_THROUGH_WITH_BUFFERS), "sense");
            info.sptd.SenseInfoLength = 32;
            info.sptd.DataTransferLength = (uint)dataLength;
            info.sptd.DataBufferOffset = Marshal.AllocHGlobal(dataLength);
            info.sptd.TimeOutValue = 3;
            info.sptd.DataIn = dataInOut;

            if (dataInOut == SCSI_IOCTL_DATA_OUT)
                Marshal.Copy(dataBuffer, 0, info.sptd.DataBufferOffset, dataLength);

            result = Marshal.AllocHGlobal(Marshal.SizeOf(info));
            Marshal.StructureToPtr(info, result, false);

            status = DeviceIoControl(handle,
                (int)IOCTL_SCSI_PASS_THROUGH_DIRECT,
                result,
                Marshal.SizeOf(info),
                result,
                Marshal.SizeOf(info),
                out nBytesReturned,
                IntPtr.Zero);

            if (status == false)
            {
                errorMessage = Marshal.GetLastWin32Error();
                StreamWriter log = new StreamWriter("log.txt", true);
                DateTime time = DateTime.Now;
                log.WriteLine(time.Hour.ToString() + ":" + time.Minute.ToString() + ":" + time.Second.ToString() + " Write IO Control Fail, Error Message is:" + errorMessage.ToString());
                log.Close();
            }

            info = (SCSI_PASS_THROUGH_WITH_BUFFERS)Marshal.PtrToStructure(result, typeof(SCSI_PASS_THROUGH_WITH_BUFFERS));

            //if(dataInOut == SCSI_IOCTL_DATA_IN)
            Marshal.Copy(info.sptd.DataBufferOffset, dataBuffer, 0, dataLength);

            Marshal.FreeHGlobal(result);
            Marshal.FreeHGlobal(info.sptd.DataBufferOffset);

            if (status == false)
                return false;
            else
                return true;
        }

        #region Standard Commands
        public static byte[] Inquiry(IntPtr handle)
        {
            byte[] data = new byte[64];
            byte[] cdb = new byte[16];
            cdb[0] = 0x12;
            cdb[4] = 0x40;
            SCSI.Cmd(handle, 16, cdb, data.Length, ref data, SCSI.SCSI_IOCTL_DATA_IN);
            return data;
        }

        public static byte[] ReadCapacity(IntPtr handle)
        {
            byte[] data = new byte[8];
            byte[] cdb = new byte[16];
            cdb[0] = 0x25;
            SCSI.Cmd(handle, 16, cdb, data.Length, ref data, SCSI.SCSI_IOCTL_DATA_IN);
            return data;
        }

        public static byte[] StartStopUint(IntPtr handle)
        {
            byte[] data = new byte[8];
            byte[] cdb = new byte[16];
            cdb[0] = 0x1B;
            cdb[4] = 0x02;
            SCSI.Cmd(handle, 16, cdb, data.Length, ref data, SCSI.SCSI_IOCTL_DATA_IN);
            return data;
        }
        #endregion
    }
}
