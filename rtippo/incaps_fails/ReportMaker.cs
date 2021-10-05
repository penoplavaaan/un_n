using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Failures
{

    public class Common
    {
        


        public static int Earlier(object[] v, int day, int month, int year)
        {
            int vYear = (int)v[2];
            int vMonth = (int)v[1];
            int vDay = (int)v[0];
            if (vYear < year) return 1;
            if (vYear > year) return 0;
            if (vMonth < month) return 1;
            if (vMonth > month) return 0;
            if (vDay < day) return 1;
            return 0;
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
            int day,//
            int month,//
            int year,//
            int[] failureTypes,//
            int[] deviceId, 
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            Date date = new Date(day,month,year);

            Failure failureType = new Failure(failureTypes);

            DeviceID deviceIDs = new DeviceID(deviceId);

            
            return FindDevicesFailedBeforeDate(date, failureType, deviceIDs);
        }

        static List<string> FindDevicesFailedBeforeDate(Date date, Failure failure, DeviceID deviceIDs)
        {
            var problematicDevices = new HashSet<int>();
            for (int i = 0; i < failure.Length; i++)
                if (Failure.IsFailureSerious(failure[i]) && Common.Earlier(times[i], date.Day, date.Month, date.Year) == 1)
                    problematicDevices.Add(deviceIDs[i]);

            var result = new List<string>();
            foreach (var device in devices)
                if (problematicDevices.Contains((int)device["DeviceId"]))
                    result.Add(device["Name"] as string);

            return result;
        }
    }

    public struct Date
    {
        public Date(int day, int month, int year)
        {
            this.day = this.month = this.year = 0;
            this.Day = day;
            this.Month = month;
            this.Year = year;
        }

        int day;
        public int Day
        {
            get { return day; }
            set { day = value; }
        }

        int month;
        public int Month
        {
            get { return month; }
            set { month = value; }
        }

        int year;
        public int Year
        {
            get { return year; }
            set { year = value; }
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
            if (failureType % 2 == 0) return true;
            return false;
        }
    }

    public struct DeviceID
    {
        public DeviceID(int[] devices)
        {
            this.deviceIDs = devices;
        }


        int[] deviceIDs;
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


}
