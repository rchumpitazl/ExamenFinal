using Dapper.Contrib.Extensions;
using System;

namespace Examen.Modelos
{
    public class Member
    {
        [Key]
        public int Member_No { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State_Prov { get; set; }
        public string Country { get; set; }
        public string Mail_Code { get; set; }
        public string Phone_No { get; set; }
        public DateTime Issue_Dt { get; set; }
        public DateTime Expr_Dt { get; set; }
        public int Corp_No { get; set; }
        public decimal Prev_Balance { get; set; }
        public decimal Curr_Balance { get; set; }
        public string Member_Code { get; set; }
    }
}
