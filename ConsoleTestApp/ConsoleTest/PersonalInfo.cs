using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class PersonalInfo
    {
        public string VorName { get; set; }
        public string NachName { get; set; }
        public ContactInformation contact { get; set; }
        public Level level;
        public PersonalInfo()
        {
           
        }

        public enum Level
        {
            beginner = 1,
            junior = 2,
            intermediate = 3,
            advanced = 4
        }
        public readonly struct ContactInformation
        {
            public string Address { get; }
            public string Telephone { get; }

            public ContactInformation(string addr, string telephone)
            {
                Address = addr;
                Telephone = telephone;
            }

            public readonly string GetAllContactData()
            {
                return $"{Address} : {Telephone} : Österreich";
            }
        }
    }

    public class Abteilung4Mitarbeiter
    {
        public List<PersonalInfo> Abt4Mitarbeiter { get; set; }
        public Abteilung4Mitarbeiter()
        {
            Abt4Mitarbeiter = new List<PersonalInfo>()
            {
                new PersonalInfo()
                {
                    NachName="abhijit", VorName="chitnis", level = PersonalInfo.Level.intermediate,
                    contact = new PersonalInfo.ContactInformation("Erentrudisstr.23","650 6514370")
                },
                new PersonalInfo()
                {
                    NachName="markus", VorName="hochradl", level = PersonalInfo.Level.advanced,
                    contact = new PersonalInfo.ContactInformation("nuntiusgasse 12","650 6599970")
                }
            };
        }
    }

    //#region Singleton Class
    //internal sealed class SingletonTestStorageClass
    //{
    //    private static SingletonTestStorageClass myInstance = new SingletonTestStorageClass(); //singleton instance
    //    private static readonly object customerLock = new object(); //thread safety
    //    public List<PersonalInfo> myWorkers = new List<PersonalInfo>();

    //    public static SingletonTestStorageClass Instance
    //    {
    //        get
    //        {
    //            lock (customerLock)
    //            {
    //                return myInstance;
    //            }
    //        }
    //    }

    //    public void FillWorkersData()
    //    {
    //        myWorkers.Add(new PersonalInfo { NachName = "Chitnis", VorName = "abhijit" });
    //        myWorkers.Add(new PersonalInfo { NachName = "Hatzinger", VorName = "uli" });
    //    }
    //}
    //#endregion

    //internal sealed class SingletonDataStorage
    //{
    //    private static SingletonDataStorage myInstance = new SingletonDataStorage(); //singleton instance
    //    private static readonly object dataStoreLock = new object(); //thread safety

    //    public static SingletonDataStorage Instance
    //    {
    //        get
    //        {
    //            lock (dataStoreLock)
    //            {
    //                return myInstance;
    //            }
    //        }
    //    }

    //    public void UpdateWorkers()
    //    {
    //        SingletonTestStorageClass.Instance.myWorkers.Add(new PersonalInfo { VorName = "robert", NachName = "kienberger" });
    //    }
    //}

    //public class PersonalFirma
    //{
    //    public PersonalFirma()
    //    {
    //        LoadWorkers();
    //    }
    //    private void LoadWorkers()
    //    {
    //        SingletonTestStorageClass.Instance.FillWorkersData();
    //    }
    //}

    //public class Betrieb
    //{
    //    private PersonalFirma PersonalRecruitmentAgency { get; set; }
    //    public Betrieb()
    //    {
    //        PersonalRecruitmentAgency = new PersonalFirma();
    //    }

    //    public void ChangeWorkerData(PersonalInfo pers)
    //    {
    //        pers.VorName = "Rudiger";
    //        pers.NachName = "XXL Lutz";
    //    }

    //    public void ChangeWorkersData()
    //    {
    //        SingletonTestStorageClass.Instance.myWorkers.Add(new PersonalInfo { NachName = "bangerl", VorName = "manfred" });
    //    }
    //}
}
