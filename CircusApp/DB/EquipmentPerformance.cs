//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CircusApp.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class EquipmentPerformance
    {
        public int id { get; set; }
        public int equipmentId { get; set; }
        public int performanceId { get; set; }
    
        public virtual Equipment Equipment { get; set; }
        public virtual Performance Performance { get; set; }
    }
}