using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;
//using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Sammak.SandBox.Models
{
    public class DeviceLog : INotifyPropertyChanged
    {
        #region Constructors

        public DeviceLog()
        {
        }

        #endregion

        #region Variables

        protected Guid IdValue;
        protected Guid DeviceIdValue;
        protected String DescriptionValue;

        #endregion

        #region Properties

        [DataMember]
        public virtual Guid Id
        {
            get
            {
                return IdValue;
            }
            protected internal set
            {
                if (IdValue != value)
                {
                    OnPropertyChanging("Id");
                    IdValue = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        [DataMember]
        public virtual Guid DeviceId
        {
            get
            {
                return DeviceIdValue;
            }
            set
            {
                if (DeviceIdValue != value)
                {
                    OnPropertyChanging("DeviceId");
                    DeviceIdValue = value;
                    OnPropertyChanged("DeviceId");
                }
            }
        }

        [DataMember]
        public virtual String Description
        {
            get
            {
                return DescriptionValue;
            }
            set
            {
                if (DescriptionValue != value)
                {
                    OnPropertyChanging("Description");
                    DescriptionValue = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        [DataMember]
        public virtual String CreatedUser { get; set; }

        [DataMember]
        public virtual DateTime CreatedDate { get; set; }

        [DataMember]
        public virtual String ModifiedUser { get; set; }

        [DataMember]
        public virtual DateTime ModifiedDate { get; set; }

        #endregion

        #region Change Tracking

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;
        protected ArrayList PropertiesChanged = new ArrayList();
        protected Boolean IsChangedValue = false;
        protected internal Boolean TrackChanges = true;
        protected internal Boolean IsNewValue = true;

        [OnDeserializing]
        protected void Initialize(StreamingContext Context)
        {
            PropertiesChanged = new ArrayList();
            IsNewValue = true;
            TrackChanges = true;
            IsChangedValue = false;
        }

        protected void Device_PropertyChanged(Object Sender, PropertyChangedEventArgs EventArgs)
        {
            OnPropertyChanged("Device");
        }

        protected void Device_PropertyChanging(Object Sender, PropertyChangingEventArgs EventArgs)
        {
            OnPropertyChanging("Device");
        }

        public virtual void AcceptChanges()
        {
            //Clear property change tracking
            PropertiesChanged.Clear();

            //Clear change flags
            IsChangedValue = false;
            IsNew = false;

            //Turn change tracking on
            TrackChanges = true;
        }

        public virtual Boolean IsNew
        {
            get { return IsNewValue; }
            protected set { IsNewValue = value; }
        }

        public virtual Boolean IsChanged
        {
            get { return IsChangedValue && TrackChanges; }
        }

        public virtual Boolean IsChangedProperty(String PropertyName)
        {
            return PropertiesChanged.Contains(PropertyName);
        }

        protected void OnPropertyChanged(String PropertyName)
        {
            if (TrackChanges)
            {
                IsChangedValue = true;
                if (!PropertiesChanged.Contains(PropertyName))
                {
                    PropertiesChanged.Add(PropertyName);
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
                    }
                }
            }
        }

        protected void OnPropertyChanging(String PropertyName)
        {
            if (TrackChanges)
            {
                if (PropertyChanging != null)
                {
                    PropertyChanging(this, new PropertyChangingEventArgs(PropertyName));
                }
            }
        }

        #endregion

        public virtual DeviceLog Clone()
        {
            return (DeviceLog)this.MemberwiseClone();
        }

        #region IEquatable Methods

        public override int GetHashCode()
        {
            Int32 HashCode = 17;
            unchecked
            {
                HashCode = (HashCode * 31) + Id.GetHashCode();
            }
            return HashCode;
        }

        public Boolean Equals(DeviceLog Other)
        {
            return !ReferenceEquals(Other, null) && Id == Other.Id;
        }

        public override Boolean Equals(Object Other)
        {
            return Equals(Other as DeviceLog);
        }

        public static Boolean operator ==(DeviceLog DeviceLog1, DeviceLog DeviceLog2)
        {
            return ReferenceEquals(DeviceLog1, DeviceLog2) || (!ReferenceEquals(DeviceLog1, null) && DeviceLog1.Equals(DeviceLog2));
        }

        public static Boolean operator !=(DeviceLog DeviceLog1, DeviceLog DeviceLog2)
        {
            return !(DeviceLog1 == DeviceLog2);
        }

        public override String ToString()
        {
            String ToString = "DeviceLog ";
            ToString += String.Concat("Id:", Id);
            ToString += String.Concat(", DeviceId:", DeviceId);
            ToString += String.Concat(", Description:", Description);
            return ToString;
        }

        #endregion
    }
}
