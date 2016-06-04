using System;
using System.Collections.Generic;
using System.Linq;
using sb.test;
using sb.test.dal;
using Business.Entities;
namespace Test.DataStubs
{
    class UrlsStub
    {
        public static void AddToDataContext()
        {
            DataContext.AddData(GetAll());
        }
        public static IEnumerable<Url> GetAll()
        {
            var devices = DevicesStub.GetAll();
            var operators = OperatorsStub.GetAll();
            var calls = new Call[devices.Count()* operators.Count()];
            int i = 0;
            var now = DateTime.UtcNow;
            foreach (var device in devices)
            {
                foreach (var oper in operators)
                {
                    calls[i] = new Call
                    {
                        Id = i.ToGuid(),
                        DeviceId = device.Id,
                        Device = device,
                        OperatorId = oper.Id,
                        Operator = oper,
                        StatusId = CallStatus.NewCall,
                        CreatedAt = now,
                        ChangedAt = now
                    };
                    i++;
                }
            }
            return calls;
        }
    }
}
