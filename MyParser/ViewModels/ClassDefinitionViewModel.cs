﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Windows.ViewModels
{
    class ClassDefinitionViewModel : ViewModelBase
    {
        private string name;

        public Guid Id { get; }

        public ClassDefinitionViewModel() : this(Guid.Empty) { }
        public ClassDefinitionViewModel(Guid id) { Id = id; }

        public string Name
        {
            get { return name; }
            set { SetProperty(value, ref name); }
        }

        public ObservableCollection<PropertyDefinitionViewModel> Properties { get; } = new ObservableCollection<PropertyDefinitionViewModel>();
    }
}
