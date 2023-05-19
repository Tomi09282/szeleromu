using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace szeleromu
{
    internal class Szeleromu
    {
        ObservableCollection<Szeleromu> szeleromuvek = new ObservableCollection<Szeleromu>();
        public string Helyszin { get; set; }
        public int Egységek { get; set; }
        public int Teljesitmeny { get; set; }
        public DateTime MukesKezdete { get; set; }

        public Szeleromu(string helyszin, int egységek, int teljesitmeny, int mukesKezdete)
        {
            Helyszin = helyszin;
            Egységek = egységek;
            Teljesitmeny = teljesitmeny;
            MukesKezdete = new DateTime(mukesKezdete, 1, 1);
        }


        public char GetCategory()
        {
            if (Teljesitmeny >= 1000)
            {
                return 'A';
            }
            else if (Teljesitmeny >= 500 && Teljesitmeny < 1000)
            {
                return 'B';
            }
            else
            {
                return 'C';
            }
        }
    }

}
