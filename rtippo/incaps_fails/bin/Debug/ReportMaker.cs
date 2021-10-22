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
        public static bool Earlier(object[] v, int day, int month, int year)
        {
            int vYear = (int)v[2];
            int vMonth = (int)v[1];
            int vDay = (int)v[0];
            if (vYear < year) return true;
            if (vYear > year) return false;
            if (vMonth < month) return true;
            if (vMonth > month) return false;
            if (vDay < day) return true;
            return false;
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

            string newDate = date.ToString(("MM/dd/yyyy"));
            Console.WriteLine(newDate);
            string[] newDateArr = newDate.Split('/');

            int day = int.Parse(newDateArr[1]);
            int month = int.Parse(newDateArr[0]);
            int year = int.Parse(newDateArr[2]);




            var problematicDevices = new HashSet<int>();


            if (failure.Length != 0)
            {
                Console.WriteLine("failure.Length = " + failure.Length);
                for (int i = 0; i < failure.Length; i++)
                {
                    Console.WriteLine("Iteration: " + i);
                    if (Failure.IsFailureSerious(failure[i]) && Common.Earlier(devices.Times[i], day, month, year))
                    {
                        problematicDevices.Add(deviceIDs[i]);
                    }
                }
            }

            Console.WriteLine("Place 5");

            var result = new List<string>();
            if (devices.Length != 0)
            {
                for (int i = 0; i < devices.Length; i++)
                {
                    var device = devices[i];
                    if (problematicDevices.Contains((int)device["DeviceId"]))
                    {
                        result.Add(device["Name"] as string);
                        Console.WriteLine(device["Name"]);
                    }

                }
            }

            Console.WriteLine();
            Console.ReadKey();

            return result;

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
            if (failureType == (int)FailureType.unexpected_shutdown || failureType == (int)FailureType.hardware_failures) return true;
            return false;
        }
        enum FailureType// Объявление перечисления
        {
            unexpected_shutdown = 0,
            short_non_responding = 1,
            hardware_failures = 2,
            connection_problems = 3,
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
