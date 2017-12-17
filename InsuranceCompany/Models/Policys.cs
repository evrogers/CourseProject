using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models
{ 
    public partial class Policys
    {
        public int Id { get; set; }
        public int PolicyNumber { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateBegin { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateEnd { get; set; }
        public int Cost { get; set; }
        public int Summ { get; set; }
        public int TypeId { get; set; }
        public string PaymentMark { get; set; }
        public string EndMark { get; set; }
        public int ClientId { get; set; }
        public int StaffId { get; set; }

        public Client Client { get; set; }
        public Staffs Staff { get; set; }
        public PolicyTypes Type { get; set; }
    }
}
