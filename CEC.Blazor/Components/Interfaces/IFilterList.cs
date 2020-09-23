﻿using System;
using System.Collections.Generic;

namespace CEC.Blazor.Components
{
    public interface IFilterList
    {
        public enum FilterViewState
        {
            NotSet = 0,
            Show = 1,
            Hide = 2
        }

        /// <summary>
        /// Guid to Identify object instance
        /// </summary>
        public Guid GUID { get => Guid.NewGuid(); }

        /// <summary>
        /// Boolean to determine if the filter should be shown in the UI
        /// </summary>
        public bool Show { get => this.ShowState == 0; }

        /// <summary>
        /// Current state of the Filter
        /// </summary>
        public FilterViewState ShowState { get; set; }

        /// <summary>
        /// Dictionary of filters - key is the field/property name and value is the value to filter on
        /// </summary>
        public Dictionary<string, object> Filters { get; set; }

        /// <summary>
        /// Boolean to tell the list loader if we load an empty recordset if there are no filters set
        /// Set to false when the base recordset is very large
        /// </summary>
        public bool OnlyLoadIfFilters { get; set; }

        /// <summary>
        /// Boolean to tell the list loader if it need to load
        /// </summary>
        public bool Load { get => this.Filters.Count > 0 || !this.OnlyLoadIfFilters; }

        /// <summary>
        /// Method to reset the filter
        /// </summary>
        public void Reset()
        {
            this.ShowState = IFilterList.FilterViewState.NotSet;
        }

        /// <summary>
        /// Method to Copy the Filter
        /// </summary>
        /// <returns></returns>
        public IFilterList Copy()
        {
            var list = new FilterList()
            {
                ShowState = this.ShowState
            };
            foreach (var set in this.Filters) list.Filters.Add(set.Key, set.Value);
            return list;
        }
    }
}
