using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sammak.SandBox.Models.Eqatables
{
    public abstract class OrderBase : INotifyPropertyChanged, INotifyPropertyChanging, IEquatable<OrderBase>
    {
        #region Constructors

        public OrderBase()
        {
        }

        #endregion

        #region Variables

        protected Guid IdValue;
        protected int NumberValue;
        protected Guid AddressIdValue;
        protected Guid? SpecialInstructionIdValue;
        protected Guid? ShipmentIdValue;
        protected Guid OrderTypeIdValue;
        protected Guid? FulfillmentNoteIdValue;
        protected DateTime? DeliverByDateValue;
        protected Guid OrderStatusIdValue;
        protected Guid? EnrollmentIdValue;
        protected Guid? FacilityIdValue;
        protected DateTime? FulfilledDateValue;
        protected string FulfilledByValue;
        protected Guid CenterIdValue;
        protected int? WOSupplyOrderIdValue;

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
        public virtual int Number
        {
            get
            {
                return NumberValue;
            }
            set
            {
                if (NumberValue != value)
                {
                    OnPropertyChanging("Number");
                    NumberValue = value;
                    OnPropertyChanged("Number");
                }
            }
        }

        [DataMember]
        public virtual Guid AddressId
        {
            get
            {
                return AddressIdValue;
            }
            set
            {
                if (AddressIdValue != value)
                {
                    OnPropertyChanging("AddressId");
                    AddressIdValue = value;
                    OnPropertyChanged("AddressId");
                }
            }
        }

        [DataMember]
        public virtual Guid? SpecialInstructionId
        {
            get
            {
                return SpecialInstructionIdValue;
            }
            set
            {
                if (SpecialInstructionIdValue != value)
                {
                    OnPropertyChanging("SpecialInstructionId");
                    SpecialInstructionIdValue = value;
                    OnPropertyChanged("SpecialInstructionId");
                }
            }
        }

        [DataMember]
        public virtual Guid? ShipmentId
        {
            get
            {
                return ShipmentIdValue;
            }
            set
            {
                if (ShipmentIdValue != value)
                {
                    OnPropertyChanging("ShipmentId");
                    ShipmentIdValue = value;
                    OnPropertyChanged("ShipmentId");
                }
            }
        }

        [DataMember]
        public virtual Guid OrderTypeId
        {
            get
            {
                return OrderTypeIdValue;
            }
            set
            {
                if (OrderTypeIdValue != value)
                {
                    OnPropertyChanging("OrderTypeId");
                    OrderTypeIdValue = value;
                    OnPropertyChanged("OrderTypeId");
                }
            }
        }

        [DataMember]
        public virtual Guid? FulfillmentNoteId
        {
            get
            {
                return FulfillmentNoteIdValue;
            }
            set
            {
                if (FulfillmentNoteIdValue != value)
                {
                    OnPropertyChanging("FulfillmentNoteId");
                    FulfillmentNoteIdValue = value;
                    OnPropertyChanged("FulfillmentNoteId");
                }
            }
        }

        [DataMember]
        public virtual DateTime? DeliverByDate
        {
            get
            {
                return DeliverByDateValue;
            }
            set
            {
                if (DeliverByDateValue != value)
                {
                    OnPropertyChanging("DeliverByDate");
                    DeliverByDateValue = value;
                    OnPropertyChanged("DeliverByDate");
                }
            }
        }

        [DataMember]
        public virtual Guid OrderStatusId
        {
            get
            {
                return OrderStatusIdValue;
            }
            set
            {
                if (OrderStatusIdValue != value)
                {
                    OnPropertyChanging("OrderStatusId");
                    OrderStatusIdValue = value;
                    OnPropertyChanged("OrderStatusId");
                }
            }
        }

        [DataMember]
        public virtual Guid? EnrollmentId
        {
            get
            {
                return EnrollmentIdValue;
            }
            set
            {
                if (EnrollmentIdValue != value)
                {
                    OnPropertyChanging("EnrollmentId");
                    EnrollmentIdValue = value;
                    OnPropertyChanged("EnrollmentId");
                }
            }
        }

        [DataMember]
        public virtual Guid? FacilityId
        {
            get
            {
                return FacilityIdValue;
            }
            set
            {
                if (FacilityIdValue != value)
                {
                    OnPropertyChanging("FacilityId");
                    FacilityIdValue = value;
                    OnPropertyChanged("FacilityId");
                }
            }
        }

        [DataMember]
        public virtual DateTime? FulfilledDate
        {
            get
            {
                return FulfilledDateValue;
            }
            set
            {
                if (FulfilledDateValue != value)
                {
                    OnPropertyChanging("FulfilledDate");
                    FulfilledDateValue = value;
                    OnPropertyChanged("FulfilledDate");
                }
            }
        }

        [DataMember]
        public virtual string FulfilledBy
        {
            get
            {
                return FulfilledByValue;
            }
            set
            {
                if (FulfilledByValue != value)
                {
                    OnPropertyChanging("FulfilledBy");
                    FulfilledByValue = value;
                    OnPropertyChanged("FulfilledBy");
                }
            }
        }

        [DataMember]
        public virtual Guid CenterId
        {
            get
            {
                return CenterIdValue;
            }
            set
            {
                if (CenterIdValue != value)
                {
                    OnPropertyChanging("CenterId");
                    CenterIdValue = value;
                    OnPropertyChanged("CenterId");
                }
            }
        }

        private void OnPropertyChanging(string v)
        {
            throw new NotImplementedException();
        }

        [DataMember]
        public virtual int? WOSupplyOrderId
        {
            get
            {
                return WOSupplyOrderIdValue;
            }
            set
            {
                if (WOSupplyOrderIdValue != value)
                {
                    OnPropertyChanging("WOSupplyOrderId");
                    WOSupplyOrderIdValue = value;
                    OnPropertyChanged("WOSupplyOrderId");
                }
            }
        }

        [DataMember]
        public virtual string CreatedUser { get; set; }

        [DataMember]
        public virtual DateTime CreatedDate { get; set; }

        [DataMember]
        public virtual string ModifiedUser { get; set; }

        [DataMember]
        public virtual DateTime ModifiedDate { get; set; }

        #endregion

        #region Change Tracking

        protected ArrayList PropertiesChanged = new ArrayList();
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;
        protected bool IsChangedValue = false;
        protected internal bool TrackChanges = true;
        protected internal bool IsNewValue = true;

        [OnDeserializing]
        protected void Initialize(StreamingContext Context)
        {
            IsNewValue = true;
            TrackChanges = true;
        }

        protected void SpecialInstruction_PropertyChanged(object Sender, PropertyChangedEventArgs EventArgs)
        {
            OnPropertyChanged("SpecialInstruction");
        }

        protected void SpecialInstruction_PropertyChanging(object Sender, PropertyChangingEventArgs EventArgs)
        {
            OnPropertyChanging("SpecialInstruction");
        }

        protected void Shipment_PropertyChanged(object Sender, PropertyChangedEventArgs EventArgs)
        {
            OnPropertyChanged("Shipment");
        }

        protected void Shipment_PropertyChanging(object Sender, PropertyChangingEventArgs EventArgs)
        {
            OnPropertyChanging("Shipment");
        }

        protected void OrderType_PropertyChanged(object Sender, PropertyChangedEventArgs EventArgs)
        {
            OnPropertyChanged("OrderType");
        }

        protected void OrderType_PropertyChanging(object Sender, PropertyChangingEventArgs EventArgs)
        {
            OnPropertyChanging("OrderType");
        }

        protected void FulFillmentNote_PropertyChanged(object Sender, PropertyChangedEventArgs EventArgs)
        {
            OnPropertyChanged("FulFillmentNote");
        }

        protected void FulFillmentNote_PropertyChanging(object Sender, PropertyChangingEventArgs EventArgs)
        {
            OnPropertyChanging("FulFillmentNote");
        }

        protected void OrderStatus_PropertyChanged(object Sender, PropertyChangedEventArgs EventArgs)
        {
            OnPropertyChanged("OrderStatus");
        }

        protected void OrderStatus_PropertyChanging(object Sender, PropertyChangingEventArgs EventArgs)
        {
            OnPropertyChanging("OrderStatus");
        }

        protected void Enrollment_PropertyChanged(object Sender, PropertyChangedEventArgs EventArgs)
        {
            OnPropertyChanged("Enrollment");
        }

        protected void Enrollment_PropertyChanging(object Sender, PropertyChangingEventArgs EventArgs)
        {
            OnPropertyChanging("Enrollment");
        }

        protected void Facility_PropertyChanged(object Sender, PropertyChangedEventArgs EventArgs)
        {
            OnPropertyChanged("Facility");
        }

        protected void Facility_PropertyChanging(object Sender, PropertyChangingEventArgs EventArgs)
        {
            OnPropertyChanging("Facility");
        }

        protected void OnPropertyChanged(string PropertyName)
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

        public virtual bool IsNew
        {
            get { return IsNewValue; }
            protected set { IsNewValue = value; }
        }

        public virtual bool IsChanged
        {
            get { return IsChangedValue && TrackChanges; }
        }

        #endregion

        #region IEquatable Methods

        public override int GetHashCode()
        {
            int HashCode = 17;
            unchecked
            {
                HashCode = (HashCode * 31) + Id.GetHashCode();
            }
            return HashCode;
        }

        public bool Equals(OrderBase Other) => !(Other is null) && Id == Other.Id;

        public override bool Equals(object Other) => Equals(Other as OrderBase);

        public static bool operator ==(OrderBase Order1, OrderBase Order2) =>
            ReferenceEquals(Order1, Order2) || (!(Order1 is null) && Order1.Equals(Order2));

        public static bool operator !=(OrderBase Order1, OrderBase Order2) => 
            !(Order1 == Order2);

        public override string ToString()
        {
            var result = $"Order Id: {Id}";
            result += $", Number: {Number}";
            result += $", Number: {Number}";
            result += $", AddressId: {AddressId}";
            result += $", SpecialInstructionId: {SpecialInstructionId}";
            result += $", ShipmentId: {ShipmentId}";
            result += $", OrderTypeId: {OrderTypeId}";
            result += $", FulfillmentNoteId: {FulfillmentNoteId}";
            result += $", DeliverByDate: {DeliverByDate}";
            result += $", OrderStatusId: {OrderStatusId}";
            result += $", EnrollmentId: {EnrollmentId}";
            result += $", FacilityId: {FacilityId}";
            result += $", FulfilledDate: {FulfilledDate}";
            result += $", FulfilledBy: {FulfilledBy}";
            result += $", CenterId: {CenterId}";
            result += $", WOSupplyOrderId: {WOSupplyOrderId}";
            return result;
        }

        #endregion
    }
}
