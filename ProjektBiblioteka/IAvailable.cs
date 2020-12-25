using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBiblioteka
{

    public enum Available { available, not_available };
    
        


    public interface IAvailable
    {

      
        Available GetState(); // zwraca aktualny stan urządzenia

        int Counter { get; }  // liczy ile razy dany egzemplarz byl wypozyczony
        void Borrowed(); // ksiazka zmienia stan na not available
        void Returned(); //ksiazka zmienia stan na available
    }
}

