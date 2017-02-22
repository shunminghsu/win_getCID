using System;
using System.Collections;
using System.Security.Cryptography;
using System.Threading;

namespace SCSIApplication
{
    class CmdSet
    {

        #region SCSIApplication
        public static byte[] send_CMD_1(IntPtr handle)
        {
            //cmd7
            byte[] resp = new byte[3];
            byte[] cdb = new byte[16];
            cdb[0] = 0xD0;

            
            SCSI.Cmd(handle, 16, cdb, resp.Length, ref resp, SCSI.SCSI_IOCTL_DATA_IN);
            return resp;
        }

        public static void send_CMD_2(IntPtr handle)
        {
            //讓reader回到stand-by state
            byte[] resp = new byte[5];
            byte[] cdb = new byte[16];
            cdb[0] = 0xD1;
            cdb[2] = 0x07;
            cdb[9] = 0x10;
            cdb[10] = 0x1A;
            SCSI.Cmd(handle, 16, cdb, resp.Length, ref resp, SCSI.SCSI_IOCTL_DATA_IN);
        }

        public static byte[] send_CMD_3(IntPtr handle, byte[] RCA)
        {
            //get CID
            byte[] resp = new byte[16];
            byte[] cdb = new byte[16];
            cdb[0] = 0xD1;
            cdb[2] = 0x0A;
            cdb[3] = RCA[1];
            cdb[4] = RCA[2];
            cdb[9] = 0x10;
            cdb[10] = 0x2A;
            SCSI.Cmd(handle, 16, cdb, resp.Length, ref resp, SCSI.SCSI_IOCTL_DATA_IN);

            byte[] cid = new byte[15];
            Array.Copy(resp, 1, cid, 0, cid.Length);
            return cid;
        }

        public static void send_CMD_4(IntPtr handle)
        {
            //讓reader回到transfer state
            byte[] resp = new byte[5];
            byte[] cdb = new byte[16];
            cdb[0] = 0xD1;
            cdb[2] = 0x07;
            cdb[3] = 0x59;
            cdb[4] = 0xB4;
            cdb[9] = 0x10;
            cdb[10] = 0x1A;
            SCSI.Cmd(handle, 16, cdb, resp.Length, ref resp, SCSI.SCSI_IOCTL_DATA_IN);
        }
        #endregion 

        
    }
}
