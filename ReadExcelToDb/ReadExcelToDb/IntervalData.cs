//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReadExcelToDb
{
    using System;
    using System.Collections.Generic;
    
    public partial class IntervalData
    {
        public int Id { get; set; }
        public long DeliveryPoint { get; set; }
        public System.DateTime Date { get; set; }
        public System.TimeSpan TimeSlot { get; set; }
        public Nullable<decimal> SlotVal { get; set; }
    }
}