//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjektBiblioteka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class loginPass
    {
        [Required(AllowEmptyStrings = false, ErrorMessage ="Username is required!")]
        public string username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required!")]
        public string password { get; set; }
    }
}