using System;
using System.Management;


namespace SCSIApplication.Common
{
    class DriveWatcher
    {
        private ManagementEventWatcher _createWatcher, _deleteWatcher;

        public DriveWatcher(EventArrivedEventHandler createHandler, EventArrivedEventHandler deleteHandler)
        {
            try
            {
                _createWatcher = new ManagementEventWatcher(newCreateQuery());
                _createWatcher.EventArrived += createHandler;
                _createWatcher.Start();
            }
            catch (Exception ex)
            {
                string errlog = DateTime.Now + @" -Fail _createWatcher - " + ex.ToString() + "\r\n";
                EventLog.Write(errlog);
            }

            try
            {
                _deleteWatcher = new ManagementEventWatcher(newdeleteQuery());
                _deleteWatcher.EventArrived += deleteHandler;
                _deleteWatcher.Start();
            }
            catch (Exception ex)
            {
                string errlog = DateTime.Now + @" -Fail _deleteWatcher - " + ex.ToString() + "\r\n";
                EventLog.Write(errlog);
            }
            
        }

        public void stop()
        {
            if (_createWatcher != null) _createWatcher.Stop();
            if (_deleteWatcher != null) _deleteWatcher.Stop();
        }

        private WqlEventQuery newCreateQuery()
        {
            WqlEventQuery query = new WqlEventQuery();
            query.EventClassName = "__InstanceCreationEvent";
            query.WithinInterval = TimeSpan.FromSeconds(1);
            query.Condition = "TargetInstance ISA 'Win32_DiskDrive'";
            return query;
        }

        private WqlEventQuery newdeleteQuery()
        {
            WqlEventQuery query = new WqlEventQuery();
            query.EventClassName = "__InstanceDeletionEvent";
            query.WithinInterval = TimeSpan.FromSeconds(1);
            query.Condition = "TargetInstance ISA 'Win32_DiskDrive'";
            return query;
        }
    }
}
