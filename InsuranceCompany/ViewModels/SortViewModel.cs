using InsuranceCompany.Models;

namespace InsuranceCompany.ViewModels
{
    public class SortViewModel
    {
        public SortState ClientName { get; private set; }
        public SortState DataBegin { get; private set; }
        public SortState DataEnd { get; private set; }
        public SortState PolicyNumber { get; private set; }
        public SortState Cost { get; private set; }
        public SortState PolicyName { get; private set; }
        public SortState PolicyDescription { get; private set; }
        public SortState PolicyCondition { get; private set; }
        public SortState GroupName { get; private set; }
        public SortState StaffExperience { get; private set; }
        // Текущее значение сортировки.
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            ClientName = sortOrder == SortState.ClientNameAsc ? SortState.ClientNameDesc : SortState.ClientNameAsc;
            DataBegin = sortOrder == SortState.DataBeginAsc ? SortState.DataBeginDesc : SortState.DataBeginAsc;
            DataEnd = sortOrder == SortState.DataEndAsc ? SortState.DataEndDesc : SortState.DataEndAsc;
            PolicyNumber = sortOrder == SortState.PolicyNumberAsc ? SortState.PolicyNumberDesc : SortState.PolicyNumberAsc;
            Cost = sortOrder == SortState.CostAsc ? SortState.CostDesc : SortState.CostAsc;
            PolicyName = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            PolicyDescription = sortOrder == SortState.DescriptionAsc ? SortState.DescriptionDesc : SortState.DescriptionAsc;
            PolicyCondition = sortOrder == SortState.PolicyConditionAsc ? SortState.PolicyConditionDesc : SortState.PolicyConditionAsc;
            StaffExperience = sortOrder == SortState.StaffExperienceAsc ? SortState.StaffExperienceDesc : SortState.StaffExperienceAsc;
            GroupName = sortOrder == SortState.GroupNameAsc ? SortState.GroupNameDesc : SortState.GroupNameAsc;
            Current = sortOrder;
        }

    }
}
