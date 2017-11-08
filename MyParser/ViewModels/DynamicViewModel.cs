using Oss.BuisinessLayer.ViewDtos;
using System;
using System.ComponentModel;
using System.Linq;

namespace Oss.Windows.ViewModels
{
    class DynamicViewModel : ICustomTypeDescriptor, INotifyPropertyChanged
    {
        private class DynamicPropertyDescriptor : PropertyDescriptor
        {
            private readonly DynamicClassPropertyDefinition propertyDefinition;

            public override Type ComponentType
            {
                get
                {
                    return typeof(DynamicViewModel);
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            public override Type PropertyType
            {
                get
                {
                    return propertyDefinition.Type;
                }
            }

            public DynamicPropertyDescriptor(DynamicClassPropertyDefinition propertyDefinition) 
                : base(propertyDefinition.Name, null)
            {
                this.propertyDefinition = propertyDefinition;
            }

            public override bool CanResetValue(object component)
            {
                return false;
            }

            public override object GetValue(object component)
            {
                var viewModel = (DynamicViewModel)component;
                return viewModel.dynamicObject[propertyDefinition.Name];
            }

            public override void ResetValue(object component)
            {
                //not supported
            }

            public override void SetValue(object component, object value)
            {
                var viewModel = (DynamicViewModel)component;
                viewModel.dynamicObject[propertyDefinition.Name] = value;
                viewModel.PropertyChanged?.Invoke(viewModel, new PropertyChangedEventArgs(propertyDefinition.Name));
            }

            public override bool ShouldSerializeValue(object component)
            {
                return true;
            }
        }

        private readonly DynamicObject dynamicObject;

        public event PropertyChangedEventHandler PropertyChanged;

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(dynamicObject, true);
        }

        public string GetClassName()
        {
            return dynamicObject.Class.Name;
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(dynamicObject, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(dynamicObject, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(dynamicObject, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(dynamicObject, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(dynamicObject, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(dynamicObject, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(dynamicObject, attributes, true);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            var properties = dynamicObject.Class.Properties.Select(p => new DynamicPropertyDescriptor(p));
            return new PropertyDescriptorCollection(properties.ToArray());
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        public DynamicViewModel(DynamicObject obj)
        {
            this.dynamicObject = obj;
        }
    }
}
