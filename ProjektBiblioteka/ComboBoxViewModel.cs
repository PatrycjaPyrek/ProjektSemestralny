using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBiblioteka
{
    class ComboBoxViewModel
    {
        public List<string> OptionCollection { get; set; }

        public ComboBoxViewModel()
        {
            OptionCollection = new List<string>()
            {
                "Create new application user",
                "Create new client",
                "Edit client's information",
                "Delete client",
                "Add new book",
                "Edit orders",
                "Delete user"

            };
        }

    }
}
