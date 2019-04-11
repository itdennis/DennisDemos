using System;
using System.Collections.Generic;
using System.Text;

namespace ProtoTypeDemo
{
    public class Resume : ICloneable
    {
        public string Name { get; set; }

        #region Personl info
        public string Age { get; set; }
        public string Sex { get; set; }
        #endregion

        #region Working Experience
        public string TimeArea { get; set; }
        public string Company { get; set; }
        public string JobDes { get; set; }
        #endregion

        public Resume(string name) { this.Name = name; }

        public void SetPersonalInfo(string age, string sex)
        {
            this.Age = age;
            this.Sex = sex;
        }

        public void SetWorkingExperince(string company, string jobDes)
        {
            this.JobDes = jobDes;
            this.Company = company;
        }

        public void Display()
        {
            Console.WriteLine($"{Name}");
            Console.WriteLine($"Personal info: Age: {Age}, Sex: {Sex}.");
            Console.WriteLine($"Working Experience: Company: {Company}, job des: {JobDes}.");
        }
        public object Clone()
        {
            return base.MemberwiseClone();
        }
    }
}
