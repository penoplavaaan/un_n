﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.EnterpriseTask
{
    public class Enterprise
    {
        public readonly Guid guid;
        public  Guid Guid
        {
            get { return guid; } 
        } 

        private string name;
        public string Name { get; set; } 

        private string inn;
        public string Inn
        {
            get { return inn; }
            set {
                if (value.Length != 10 || !value.All(z => char.IsDigit(z)))
                    throw new ArgumentException();
                inn = value;
            }
        }

        private DateTime establishDate;
        public DateTime EstablishDate { get; set; }

         
        public TimeSpan ActiveTimeSpan
        {
            get { return DateTime.Now - establishDate; }
        }
        public TimeSpan GetActiveTimeSpan()
        {
            return DateTime.Now - establishDate;
        }

        public double GetTotalTransactionsAmount()
        {
            DataBase.OpenConnection();
            var amount = 0.0;
            foreach (Transaction t in DataBase.Transactions().Where(z => z.EnterpriseGuid == guid))
                amount += t.Amount;
            return amount;
        }
    }
}
