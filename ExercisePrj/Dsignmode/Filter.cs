using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//过滤器模式
namespace ExercisePrj.Dsignmode
{
    //实体
    public class Person
    {
        public string Name { get; }
        public string Gender { get; }
        public string MaritalStatus { get; }
        public Person(string name,string gender,string maritalstatus)
        {
            Name = name;
            Gender = gender;
            MaritalStatus = maritalstatus;
        }
    }
    //过滤标准接口
    public interface ICriteria
    {
        List<Person> MeetCriteria(List<Person> persons);
    }
    //不同标准实现
    public class CriteriaMale:ICriteria
    {
        public List<Person> MeetCriteria(List<Person> persons)
        {
            List<Person> maleCriterial = new List<Person>();
            foreach(var p in persons)
            {
                if(p.Gender=="male")
                {
                    maleCriterial.Add(p);
                }
            }
            return maleCriterial;
        }
    }
    public class CriteriaFemale : ICriteria
    {
        public List<Person> MeetCriteria(List<Person> persons)
        {
            List<Person> femaleCriterial = new List<Person>();
            foreach (var p in persons)
            {
                if (p.Gender == "female")
                {
                    femaleCriterial.Add(p);
                }
            }
            return femaleCriterial;
        }
    }
    public class CriteriaSingle : ICriteria
    {
        public List<Person> MeetCriteria(List<Person> persons)
        {
            List<Person> Criterialsingle = new List<Person>();
            foreach (var p in persons)
            {
                if (p.MaritalStatus ==  "Single")
                {
                    Criterialsingle.Add(p);
                }
            }
            return Criterialsingle;
        }
    }
    public class AndCriteria : ICriteria
    {
        private ICriteria criteria;
        private ICriteria otherCriteria;

        public AndCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            this.criteria = criteria;
            this.otherCriteria = otherCriteria;
        }
        public List<Person> MeetCriteria(List<Person> persons)
        {
            List<Person> firstCriteriaPersons = criteria.MeetCriteria(persons);
            return otherCriteria.MeetCriteria(firstCriteriaPersons);
        }
    }

    public class OrCriteria :ICriteria
    {
       private ICriteria criteria;
        private ICriteria otherCriteria;

        public OrCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            this.criteria = criteria;
            this.otherCriteria = otherCriteria;
        }

       public List<Person> MeetCriteria(List<Person> persons)
        {
            List<Person> firstCriteriaItems = criteria.MeetCriteria(persons);
            List<Person> otherCriteriaItems = otherCriteria.MeetCriteria(persons);
            foreach (Person p  in  otherCriteriaItems)
            {
                if (!firstCriteriaItems.Contains(p))
                {
                    firstCriteriaItems.Add(p);
                }
            }
            return firstCriteriaItems;
        }
    }
}
