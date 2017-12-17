using InsuranceCompany.Models;
using InsuranceCompany.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceCompany.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Client> Client { get; set; }
        public IEnumerable<ClientGroups> ClientGroups { get; set; }
        public IEnumerable<Policys> Policys { get; set; }
        public IEnumerable<PolicyTypes> PolicyTypes { get; set; }
        public IEnumerable<Risks> Risks { get; set; }
        public IEnumerable<Staffs> Staffs { get; set; }

        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        
            public FilterViewModel FilterViewModel { get; set; }

    }
}
