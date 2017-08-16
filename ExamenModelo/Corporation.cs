using Dapper.Contrib.Extensions;
using System;

namespace Examen.Modelos
{
    public class Corporation
    {
        [Key]
        public int Corp_No { get; set; }
        public string Corp_Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State_Prov { get; set; }
        public string Country { get; set; }
        public string Mail_Code { get; set; }
        public string Phone_No { get; set; }
        public DateTime Expr_Dt { get; set; }
        public string Corp_Code { get; set; }        
    }
}
