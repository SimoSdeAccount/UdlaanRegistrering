//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Computer_UdlaanRegistrering
{
    using System;
    using System.Collections.Generic;
    
    public partial class Computer
    {
        public string Computernummer { get; set; }
        public string Model { get; set; }
        public int Antal { get; set; }
    
        public virtual Model Model1 { get; set; }
    }
}
