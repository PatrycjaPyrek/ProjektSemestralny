using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBiblioteka
{

    public enum Checked { check, not_check };
    
        


    public interface IChecked
    {

      
        Checked GetState(); // zwraca aktualny stan 

        //int Counter { get; }  // liczy ile razy dany egzemplarz byl wypozyczony
        void Check();
        void NoCheck();
        
    }
}

