using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Incapsulation.Failures
{
    public class Common
    {
        public static bool FailureWasEarlier(object[] v, int day, int month, int year)
        {
            int vYear = (int)v[2];
            int vMonth = (int)v[1];
            int vDay = (int)v[0]; 
            if (vYear < year) return true;
            if (vYear > year) return false;
            if (vMonth < month) return true;
            if (vMonth > month) return false;
            return vDay < day;
        }
    }

    public class ReportMaker
    {
        /// <summary>
        /// </summary>
        /// <param name="day"></param>
        /// <param name="failureTypes">
        /// 0 for unexpected shutdown, 
        /// 1 for short non-responding, 
        /// 2 for hardware failures, 
        /// 3 for connection problems
        /// </param>
        /// <param name="deviceId"></param>
        /// <param name="times"></param>
        /// <param name="devices"></param>
        /// <returns></returns>


        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,                                 // Date
            int month,                               // Date
            int year,                                // Date
            int[] failureTypes,                      // Failure
            int[] deviceId,                          // DeviceID
            object[][] times,
            List<Dictionary<string, object>> devices)// Device
        {
            //Date date = new Date(day,month,year);

            DateTime date = new DateTime(year, month, day);

            Failure failureType = new Failure(failureTypes);

            DeviceID deviceIDs = new DeviceID(deviceId);

            Device new_devices = new Device(devices, times);

            return FindDevicesFailedBeforeDate(date, failureType, deviceIDs, new_devices);
        }

        static List<string> FindDevicesFailedBeforeDate(
            DateTime date,
            Failure failure,
            DeviceID deviceIDs,
            Device devices)
        { 
            var problematicDevices = findProblematicDevices(failure, deviceIDs, devices, date);



            var result = new List<string>();
            if (devices.Length != 0)
            {
                for (int i = 0; i < devices.Length; i++)
                {
                    var device = devices[i];
                    if (problematicDevices.Contains((int)device["DeviceId"]))
                    {
                        result.Add(device["Name"] as string); 
                    }
                }
            }
            return result;
        }

        static HashSet<int> findProblematicDevices(Failure failure, DeviceID deviceIDs, Device devices, DateTime date)
        {
            string[] dateArr = date.ToString(("MM/dd/yyyy")).Split('/');

            int day = int.Parse(dateArr[1]);
            int month = int.Parse(dateArr[0]);
            int year = int.Parse(dateArr[2]);

            var problematicDevices = new HashSet<int>();

            if (failure.Length != 0)
            {
                for (int i = 0; i < failure.Length; i++)
                {
                    if (Failure.IsFailureSerious(failure[i]) 
                        && Common.FailureWasEarlier(devices.Times[i], day, month, year))
                    {
                        problematicDevices.Add(deviceIDs[i]);
                    }
                }
            }

            return problematicDevices;
        }
    }



    public struct Failure
    {
        public Failure(int[] failureTypes)
        {
            this.failureTypes = failureTypes;
        }

        int[] failureTypes;
        public int[] FailureTypes
        {
            get { return failureTypes; }
            set { failureTypes = value; }
        }

        public int Length
        {
            get { return failureTypes.Length; }
        }

        public int this[int ind]
        {
            get { return failureTypes[ind]; }
        }

        public static bool IsFailureSerious(int failureType)
        {
            return 
                failureType == (int)FailureType.Unexpected_shutdown || 
                failureType == (int)FailureType.Hardware_failures;
        }
        enum FailureType 
        {
            Unexpected_shutdown = 0,
            Short_non_responding = 1,
            Hardware_failures = 2,
            Connection_problems = 3,
        }

    }

    public struct DeviceID
    {
        public DeviceID(int[] devices)
        {
            this.deviceIDs = devices;
        }


        private int[] deviceIDs;
        public int[] DeviceIDs
        {
            get { return deviceIDs; }
            set { deviceIDs = value; }
        }

        public int this[int ind]
        {
            get { return deviceIDs[ind]; }
        }
    }

    public struct Device
    {
        public Device(List<Dictionary<string, object>> devices, object[][] times)
        {
            this.devices = devices;
            this.times = times;
        }

        private List<Dictionary<string, object>> devices;
        public List<Dictionary<string, object>> Devices
        {
            get { return devices; }
            set { devices = value; }
        }

        private object[][] times;
        public object[][] Times
        {
            get { return times; }
        }

        public int Length
        {
            get { return this.devices.Count; }
        }

        public Dictionary<string, object> this[int ind]
        {
            get { return devices[ind]; }
        }
    }
}
