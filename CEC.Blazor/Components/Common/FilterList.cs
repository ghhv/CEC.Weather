using System.Collections.Generic;

namespace CEC.Blazor.Components
{
    public class FilterList : IFilterList
    {
        public Dictionary<string, object> Filters { get; set; } = new Dictionary<string, object>();

        public IFilterList.FilterViewState ShowState { get; set; } = IFilterList.FilterViewState.NotSet;

        public bool OnlyLoadIfFilters { get; set; } = false;

        //public bool TryGetFilter(string name, out object value)
        //{
        //    value = null;
        //    if (Filters.ContainsKey(name)) value = this.Filters[name];
        //    return value != null;
        //}

        //public bool SetFilter(string name, object value)
        //{
        //    if (Filters.ContainsKey(name)) this.Filters[name] = value;
        //    else Filters.Add(name, value);
        //    return Filters.ContainsKey(name);
        //}
    }
}
