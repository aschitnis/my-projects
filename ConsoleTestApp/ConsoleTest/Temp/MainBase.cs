using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Temp
{
    public abstract class RootAbstractDataContainer
    {
        public virtual IEnumerable<string> Names { get; set; } = new string[] {"default Name 01", "default Name 02"};
        public abstract IEnumerable<string> Postleitzahl { get; set; }

        public RootAbstractDataContainer()
        {
           
        }
    }

    public class DataContainer : RootAbstractDataContainer, IEvents
    {
        public override IEnumerable<string> Names { get; set; } = new string[] { "Name 01", "Name 02", "Name 03" };
        public override IEnumerable<string> Postleitzahl { get; set; } = new string[] { "5020","5551","5023","5153","5600" };

        public DataContainer()
        {
            ((IEvents)this).SaveDataEvent -= new EventHandler<string>(new Action<object, string>((x, y) => { CheckData(y); }));
            ((IEvents)this).SaveDataEvent += new EventHandler<string>(new Action<object, string>((x, y) => { CheckData(y); })); 
        }
        private void CheckData(string postleitzahl)
        {
            if (postleitzahl == "5020")
            {
                Console.WriteLine($"{postleitzahl} -- Salzburg Stadt");
            }
        }

        private event EventHandler<string> _savedataevent;
        event EventHandler<string> IEvents.SaveDataEvent
        {
            add 
            {
                if(_savedataevent == null)
                    _savedataevent += value;
            }
            remove { _savedataevent -= value; }
        }

        public void OnPlzFound(object sender, string s)
        {
            _savedataevent.Invoke(sender, s);
        }

    }

    public class MainViewModel
    {
        public DataContainer SecondBaseObject { get; set; } = new DataContainer();
        public void Display()
        {
            Each(SecondBaseObject.Names, (n) => { SecondBaseObject.OnPlzFound(null, n); });
            Each(SecondBaseObject.Postleitzahl, (n) => { SecondBaseObject.OnPlzFound(null, n); });
        }

        public void Each(IEnumerable<string> items, Action<string> action)
        {
            foreach (string item in items)
                action(item);
        }
    }
}
