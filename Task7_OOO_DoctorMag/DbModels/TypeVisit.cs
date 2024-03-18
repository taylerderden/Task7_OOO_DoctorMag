using System;
using System.Collections.Generic;

namespace Task7_OOO_DoctorMag.DbModels;

public partial class TypeVisit
{
    public int IdTypeVisit { get; set; }

    public string TypeVisitName { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
