using HRSC.Core.Enums;
using HRSC.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HRSC.Core.Data
{
    public class DataContext<T> where T : BaseModel
    {
        private string DataPath;

        private string ReadRaw()
        {
            DataPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\DataStore\\{typeof(T).Name}.json";
            if (!File.Exists(DataPath))
            {
                string dir = Path.GetDirectoryName(DataPath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                File.WriteAllText(DataPath, Seed());
            }

            var fileStr = File.ReadAllText(DataPath);
            return fileStr;
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                string raw = ReadRaw();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(raw);
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        public T GetById(Guid id)
        {
            try
            {
                string raw = ReadRaw();
                var data = JsonConvert.DeserializeObject<IEnumerable<T>>(raw);
                if (data == null) return null;

                return data.FirstOrDefault(a => a.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Guid Add(T item)
        {
            try
            {
                string raw = ReadRaw();
                var data = JsonConvert.DeserializeObject<List<T>>(raw);
                item.Id = Guid.NewGuid();
                item.CreatedOn = DateTime.Now;
                data.Add(item);

                raw = JsonConvert.SerializeObject(data);
                File.WriteAllText(DataPath, raw);

                return item.Id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public bool Update(T item)
        {
            try
            {
                string raw = ReadRaw();
                var data = JsonConvert.DeserializeObject<List<T>>(raw);
                data.RemoveAll(a => a.Id == item.Id);

                item.ModifiedOn = DateTime.Now;
                data.Add(item);

                raw = JsonConvert.SerializeObject(data);
                File.WriteAllText(DataPath, raw);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string Seed()
        {
            string content = "[]";

            if (typeof(T).Name == nameof(User))
            {
                var data = new List<User> {
                        new User
                        {
                            Id = Guid.NewGuid(),
                            Name = "Administrator",
                            EmailAddress = "admin@gmail.com",
                            MobileNumber = "09088826621",
                            Password = "123456",
                            IsAdmin = true
                        }
                    };
                content = JsonConvert.SerializeObject(data);
            }
            else if (typeof(T).Name == nameof(EmployeeType))
            {
                var data = new List<EmployeeType>
                {
                    new EmployeeType
                    {
                        Id = Guid.NewGuid(),
                        Name = SalaryType.RegularStaff.ToString(),
                        DisplayName = "Regular Employee",
                        PaymentType = PaymentType.Monthly,
                        HasTax = true,
                        Amount = 20000.00m,
                        SalaryType = SalaryType.RegularStaff
                    },
                    new EmployeeType
                    {
                        Id = Guid.NewGuid(),
                        Name = SalaryType.ContractualStaff.ToString(),
                        DisplayName = "Contractual Employee",
                        PaymentType = PaymentType.Daily,
                        HasTax = false,
                        Amount = 500.00m,
                        SalaryType = SalaryType.ContractualStaff
                    }
                };
                content = JsonConvert.SerializeObject(data);
            }

            return content;
        }
    }
}
