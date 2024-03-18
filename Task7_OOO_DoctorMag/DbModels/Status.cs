using System;
using System.Collections.Generic;

namespace Task7_OOO_DoctorMag.DbModels;

public partial class Status
{
    public int IdStatus { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
