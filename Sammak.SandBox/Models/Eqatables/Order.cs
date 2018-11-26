//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Sammak.SandBox.Models.Sso
//{
//    class Order
//    {
//        #region IEquatable Methods

//        public override int GetHashCode()
//        {
//            int HashCode = 17;
//            unchecked
//            {
//                HashCode = (HashCode * 31) + Id.GetHashCode();
//            }
//            return HashCode;
//        }

//        public bool Equals(OrderBase Other)
//        {
//            return !ReferenceEquals(Other, null) && Id == Other.Id;
//        }

//        public override bool Equals(object Other)
//        {
//            return Equals(Other as OrderBase);
//        }

//        public static bool operator ==(OrderBase Order1, OrderBase Order2)
//        {
//            return ReferenceEquals(Order1, Order2) || (!ReferenceEquals(Order1, null) && Order1.Equals(Order2));
//        }

//        public static bool operator !=(OrderBase Order1, OrderBase Order2)
//        {
//            return !(Order1 == Order2);
//        }

//        public override string ToString()
//        {
//            string ToString = "Order ";
//            ToString += string.Concat("Id:", Id);
//            ToString += string.Concat(", Number:", Number);
//            ToString += string.Concat(", AddressId:", AddressId);
//            ToString += string.Concat(", SpecialInstructionId:", SpecialInstructionId);
//            ToString += string.Concat(", ShipmentId:", ShipmentId);
//            ToString += string.Concat(", OrderTypeId:", OrderTypeId);
//            ToString += string.Concat(", FulfillmentNoteId:", FulfillmentNoteId);
//            ToString += string.Concat(", DeliverByDate:", DeliverByDate);
//            ToString += string.Concat(", OrderStatusId:", OrderStatusId);
//            ToString += string.Concat(", EnrollmentId:", EnrollmentId);
//            ToString += string.Concat(", FacilityId:", FacilityId);
//            ToString += string.Concat(", FulfilledDate:", FulfilledDate);
//            ToString += string.Concat(", FulfilledBy:", FulfilledBy);
//            ToString += string.Concat(", CenterId:", CenterId);
//            ToString += string.Concat(", WOSupplyOrderId:", WOSupplyOrderId);
//            return ToString;
//        }

//        #endregion
//    }
//}
