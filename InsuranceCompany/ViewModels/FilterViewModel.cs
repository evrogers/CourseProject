namespace InsuranceCompany.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(string ClientName)
        {
            SelectedClientName = ClientName;
        }
        public string SelectedClientName { get; set; }

        

        public FilterViewModel(string Name, string Description)
        {
            SelectedName = Name;
            SelectedDescription = Description;
        }
        public string SelectedName { get; set; }
        public string SelectedDescription { get; set; }

    }
}
