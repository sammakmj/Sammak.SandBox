using System;
using System.Collections.Generic;
using System.Text;

namespace Sammak.SandBox.Models
{
    public class ComparableExternalMessagePayload
    {
        #region Properties

        public int MessageType { get; set; }

        public string OrderId { get; set; }

        public int SourceSystemId { get; set; }

        public ComparableDictionary<string, string> Metadata { get; set; }

        public string Username { get; set; }

        #endregion

        #region  Equals and Hashcode Overrides

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var other = (ComparableExternalMessagePayload)obj;

            return
                MessageType == other.MessageType &&
                OrderId == other.OrderId &&
                Username == other.Username &&
                SourceSystemId == other.SourceSystemId &&
                Metadata == other.Metadata;
        }

        public override int GetHashCode()
        {
            return OrderId.GetHashCode() ^
                MessageType.GetHashCode() ^
                SourceSystemId.GetHashCode() ^
                Metadata.GetHashCode();
        }

        public static bool operator ==(ComparableExternalMessagePayload item1, ComparableExternalMessagePayload item2)
        {
            if (ReferenceEquals(item1, item2))
                return true;
            if (item1 is null || item2 is null)
                return false;

            return item1.Equals(item2);
        }

        public static bool operator !=(ComparableExternalMessagePayload item1, ComparableExternalMessagePayload item2)
        {
            return !(item1 == item2);
        }

        #endregion

    }
}
