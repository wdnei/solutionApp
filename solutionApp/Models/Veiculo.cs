//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace solutionApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Veiculo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Veiculo()
        {
            this.Multa = new HashSet<Multa>();
        }
    
        public int id { get; set; }
        public string cor { get; set; }
        public string modelo { get; set; }
        public string placa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Multa> Multa { get; set; }
    }
}
