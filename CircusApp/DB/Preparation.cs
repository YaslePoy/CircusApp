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
    
    public partial class Preparation
    {
        public int id { get; set; }
        public int userId { get; set; }
        public System.DateTime timestamp { get; set; }
        public int typeId { get; set; }
    
        public virtual PreparationType PreparationType { get; set; }
        public virtual User User { get; set; }
    }
}